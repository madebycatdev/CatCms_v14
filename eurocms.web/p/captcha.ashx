<%@ WebHandler Language="VB" Class="tipstricks.org.captcha" %>
<%@ Assembly Src="./wordlist.vb" %>

Imports System
Imports System.Web
Imports tipstricks.org.wordlist

Namespace tipstricks.org
    Public Class captcha : Implements IHttpHandler, IRequiresSessionState
        'ASP.Net Security Image Generator v4.5 - 19/September/2011
        'Generate images to make a CAPTCHA test
        '© 2006-2011 Emir Tüzül. All rights reserved.
        'http://www.tipstricks.org

        'This program is free software; you can redistribute it and/or
        'modify it under the terms of the Common Public License
        'as published by the Open Source Initiative OSI; either version 1.0
        'of the License, or any later version.

        'This program is distributed in the hope that it will be useful,
        'but WITHOUT ANY WARRANTY; without even the implied warranty of
        'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
        'Common Public License for more details.

        '*[null pixel]Numbers[repeat count], #[text]Numbers[repeat count], &[row reference]number[referenced row index]
        'First row [font height, chars...]
        'Following rows [char width, pixel maps...]
        Private FontMap() As Array = { _
            Split("13,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9", ","), _
            Split("14,*5#4*5,*4#6*4,&2,&2,*3#3*2#3*3,&5,*2#4*2#4*2,*2#3*4#3*2,*2#10*2,*1#12*1,*1#3*6#3*1,&11,#3*8#3", ","), _
            Split("11,#8*3,#10*1,#3*4#3*1,&3,&3,&1,&2,#3*4#4,#3*5#3,&9,&8,&2,#9*2", ","), _
            Split("11,*4#6*1,*2#9,*1#4*4#2,*1#3*6#1,#3*8,&5,&5,&5,&5,&4,&3,&2,&1", ","), _
            Split("12,#8*4,#10*2,#3*4#4*1,#3*5#3*1,#3*6#3,&5,&5,&5,&5,&4,&3,&2,&1", ","), _
            Split("9,#9,&1,#3*6,&3,&3,#8*1,&6,&3,&3,&3,&3,&1,&1", ","), _
            Split("9,#9,&1,#3*6,&3,&3,&1,&1,&3,&3,&3,&3,&3,&3", ","), _
            Split("13,*4#7,*2#11,*1#4*5#3,*1#3*8#1,#3,#3,#3*4#6,&7,#3*7#3,*1#3*6#3,*1#5*4#3,&2,&1", ","), _
            Split("11,#3*5#3,&1,&1,&1,&1,#11,&6,&1,&1,&1,&1,&1,&1", ","), _
            Split("7,#7,#7,*2#3,&3,&3,&3,&3,&3,&3,&3,&3,&1,&1", ","), _
            Split("8,*2#6,&1,*5#3,&3,&3,&3,&3,&3,&3,&3,*4#4,#7,#6", ","), _
            Split("12,#3*5#4,#3*4#4,#3*3#4,#3*2#4,#3*2#3,#3*1#3,#7,#8,&5,#3*3#3,#3*4#3,#3*5#3,&1", ","), _
            Split("9,#3,#3,#3,#3,#3,#3,#3,#3,#3,#3,#3,#9,#9", ","), _
            Split("13,#3*7#3,#4*5#4,&2,#5*3#5,&4,#6*1#6,#3*1#2*1#2*1#3,#3*1#5*1#3,#3*2#3*2#3,&9,#3*7#3,&11,&11", ","), _
            Split("11,#4*4#3,#5*3#3,&2,#6*2#3,&4,#3*1#3*1#3,&6,#3*2#6,&8,#3*3#5,&10,#3*4#4,#3*5#3", ","), _
            Split("13,*4#5,*2#9,*1#4*3#4,*1#3*5#3,#3*7#3,&5,&5,&5,&5,&4,&3,&2,&1", ","), _
            Split("10,#8,#9,#3*3#4,#3*4#3,&4,&4,&3,&2,#7,#3,#3,#3,#3", ","), _
            Split("13,*3#6,*2#8,*1#3*4#3,*1#2*6#2,#3*7#2,#2*8#2,&6,#2*5#1*2#2,#3*3#3*1#2,*1#2*4#4,*1#3*4#3,*2#10,*3#6*1#1", ","), _
            Split("12,#8,#9,#3*4#3,&3,&3,#3*3#4,&2,&1,#3*2#4,#3*3#3,&3,#3*4#4,#3*5#4", ","), _
            Split("11,*3#6,*1#9,#4*4#2,#3*6#1,#4,#8,&2,*3#8,*7#4,#1*7#3,#3*4#4,#10,*1#7", ","), _
            Split("11,#11,&1,*4#3,&3,&3,&3,&3,&3,&3,&3,&3,&3,&3", ","), _
            Split("11,#3*5#3,&1,&1,&1,&1,&1,&1,&1,&1,&1,#4*3#4,*1#9,*3#5", ","), _
            Split("14,#3*8#3,*1#3*6#3,&2,*1#3*5#4,*2#3*4#3,&5,*3#3*2#3,&7,&7,*4#6,&10,&10,*5#4", ","), _
            Split("17,#3*4#3*4#3,&1,#3*3#5*3#3,*1#3*2#2*1#2*2#3,&4,*1#3*1#3*1#3*1#3,&6,*1#3*1#2*3#2*1#3,&8,*2#5*3#5,&10,*2#4*5#4,&12", ","), _
            Split("14,#4*6#4,*1#4*4#4,*2#4*2#4,*3#3*2#3,*3#8,*4#6,*5#4,&6,&5,&4,&3,&2,&1", ","), _
            Split("13,#4*5#4,*1#3*5#3,*2#3*3#3,*2#4*1#4,*3#3*1#3,*3#7,*4#5,*5#3,&8,&8,&8,&8,&8", ","), _
            Split("10,#10,&1,*6#4,*5#4,*5#3,*4#3,*3#4,*3#3,*2#3,*1#4,#4,&1,&1", ","), _
            Split("10,*3#4,*1#8,*1#3*3#2,#2*5#3,#2*4#4,#2*3#2*1#2,#2*2#2*2#2,#2*1#2*3#2,#4*4#2,#3*5#2,*1#2*3#3,*1#8,*3#4", ","), _
            Split("9,*3#3*3,&1,#6*3,&3,*3#3*3,&5,&5,&5,&5,&5,&5,#9,&12", ","), _
            Split("10,*1#6*3,#8*2,#2*3#4*1,#1*5#3*1,*6#3*1,&5,*5#3*2,*4#4*2,*3#4*3,*2#4*4,*1#4*5,#10,&12", ","), _
            Split("11,*1#8*2,#10*1,#3*5#3,#1*7#3,*7#3*1,*3#6*2,*3#7*1,*7#4,*8#3,&4,#3*4#4,&2,*1#7*3", ","), _
            Split("12,*6#4*2,*5#5*2,&2,*4#2*1#3*2,*3#3*1#3*2,*2#3*2#3*2,*1#3*3#3*2,#3*4#3*2,#12,&9,*7#3*2,&11,&11", ","), _
            Split("11,*1#10,&1,*1#3*7,&3,*1#8*2,*1#9*1,*7#4,*8#3,&8,#1*7#3,#3*4#3*1,#10*1,*1#7*3", ","), _
            Split("11,*4#6*1,*2#8*1,*1#4*6,*1#3*7,#3*1#5*2,#10*1,#3*4#4,#3*5#3,&8,&8,*1#3*3#3*1,*1#9*1,*3#5*3", ","), _
            Split("11,#11,&1,*7#4,*7#3*1,*6#4*1,*6#3*2,*5#3*3,*4#4*3,*4#3*4,*3#4*4,*3#3*5,*2#3*6,*1#4*6", ","), _
            Split("11,*2#7*2,*1#9*1,#3*4#4,#3*5#3,#4*3#3*1,*1#8*2,&1,*1#3*1#5*1,&4,&4,#4*3#4,&2,*2#6*3", ","), _
            Split("11,*3#5*3,*1#9*1,*1#3*3#3*1,#3*5#3,&4,&4,#4*4#3,*1#10,*2#5*1#3,*7#3*1,*6#4*1,*1#8*2,*1#6*4", ",") _
        } 'Previous row must end with _

        '#Begin ColorMap
        Const BmpColorMap = "dffeff000c851700"

        Private ColorMap() As Array = { _
        Split("00,01,01", ",") _
        } 'End ColorMap

    
        '#Auto calculated variables
        Dim ImageWidth As Integer
        Dim ImageHeight As Integer
        Dim arrTextWidth() As Integer
        Dim TextHeight As Integer
        Dim LeftMargin As Integer
        Dim arrTopMargin() As Integer
        Dim CursorPos As Integer
        Dim BmpEndLine As String
        Dim BColor, TColor, NColor As String
        Dim i, j, k, x, y As Integer

        '#Editable consts and variables
        Dim Bitmap(25, 130) As String '[Height,Width]
        Dim CodeLength = 6 'Secure code length (Max:8)
        Const CodeType = 1 '0[Random numbers], 1[Random chars and numbers], 2[Wordlist]
        Const CharTracking = 2 'Set the tracking between two characters
        Const RndTopMargin = True 'Randomize top margin every character
        Const NoiseEffect = 2 '0[none], 1[sketch], 2[random foreground lines], 3[Random lines of background color over text (Recommed maximum NoiseLine=4)]
        Const NoiseLine = 6 'Low values make easy OCR, high values decrease readability
        Const MinLineLength = 7 'Minimum noise line length
        Const SessionName = "SCRTY_CODE" 'Where store your secure code
    
        '#Global variables
        Dim Ctx As HttpContext
        Dim secureCode As String
        Dim mRnd As Random = New Random()
    
        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            Ctx = context
            If CodeType < 2 Then
                secureCode = CreateGUID(CodeLength)
            Else
                secureCode = RandomWord()
                CodeLength = secureCode.Length
            End If
            context.Session(SessionName) = secureCode
            PrepareBitmap(secureCode)
            If NoiseEffect > 0 Then
                AddNoise()
            End If
            SendBitmap()
        End Sub
 
        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property
    
        Private Function CreateGUID(ByVal valLength As Integer) As String
            Dim strValid As String
            If CodeType = 1 Then
                strValid = "A0B1C2D3E4F5G6H7I8J9K8L7M6N5O4P3Q2R1S0T1U2V3W4X5Y6Z7"
            Else
                strValid = "0516273849"
            End If
            Dim tmpGUID As String = vbNullString
            Dim tmpChr As String = vbNullString
            For cGUID As Integer = 1 To valLength
                Do
                    tmpChr = Mid(strValid, RndInterval(1, Len(strValid)), 1)
                Loop While CStr(tmpChr) = CStr(Right(tmpGUID, 1))
                tmpGUID = tmpGUID & tmpChr
            Next
            CreateGUID = tmpGUID
        End Function

        Function RandomWord() As String
            Return words(RndInterval(0, UBound(words)))
        End Function

        Function RndInterval(ByVal valMin As Integer, ByVal valMax As Integer) As Integer
            RndInterval = mRnd.Next(valMin, (valMax + 1))
        End Function
    
        Function GetCharMap(ByVal valChr As String) As Array
            Dim i, j As Integer
            j = 0
            For i = 1 To UBound(FontMap(0))
                If CStr(FontMap(0)(i)) = CStr(valChr) Then
                    j = i
                    Exit For
                End If
            Next

            If j > 0 Then
                GetCharMap = FontMap(j)
            Else
                GetCharMap = New Array() {}
            End If
        End Function
    
        Sub WriteCanvas(ByVal valChr As String, ByVal valTopMargin As Integer)
            Dim i, j, k, curPos As Integer
            Dim tmpChr, strPixMap, pixRepeat As String
            Dim arrChrMap() As String
            Dim drawPixel As Boolean

            'find char map
            arrChrMap = GetCharMap(valChr)
            If UBound(arrChrMap) < 1 Then
                Exit Sub
            End If

            'write char
            For i = 1 To UBound(arrChrMap)
                'get pixel map active line
                strPixMap = arrChrMap(i)
                If Left(strPixMap, 1) = "&" Then
                    j = Mid(strPixMap, 2)
                    If (IsNumeric(j) = True) Then
                        strPixMap = arrChrMap(CInt(j))
                    Else
                        strPixMap = vbNullString
                    End If
                End If
                strPixMap = Trim(strPixMap)

                'drawing pixel
                curPos = CursorPos
                drawPixel = False
                pixRepeat = vbNullString
                For j = 1 To Len(strPixMap)
                    tmpChr = Mid(strPixMap, j, 1)
                    If (IsNumeric(tmpChr) = True) And (j < Len(strPixMap)) Then
                        pixRepeat = pixRepeat & tmpChr
                    Else
                        'end pixel map?
                        If IsNumeric(tmpChr) = True Then
                            pixRepeat = pixRepeat & tmpChr
                        End If

                        'draw pixel
                        If (drawPixel = True) And (IsNumeric(pixRepeat) = True) Then
                            For k = 1 To CInt(pixRepeat)
                                curPos = curPos + 1
                                Bitmap((valTopMargin + i), curPos) = TColor
                            Next
                        ElseIf IsNumeric(pixRepeat) = True Then
                            curPos = curPos + CInt(pixRepeat)
                        End If

                        'what is new command?
                        If tmpChr = "#" Then
                            drawPixel = True
                        Else
                            drawPixel = False
                        End If
                        pixRepeat = vbNullString
                    End If
                Next
            Next
        End Sub

        Sub PrepareBitmap(ByVal valSecureCode As String)
            Dim i, j As Integer
            'image dimensions
            ImageWidth = UBound(Bitmap, 2)
            ImageHeight = UBound(Bitmap, 1)

            'char and text width
            ReDim arrTextWidth(CodeLength)
            arrTextWidth(0) = 0
            For i = 1 To CodeLength
                arrTextWidth(i) = CInt(GetCharMap(Mid(secureCode, i, 1))(0))
                arrTextWidth(0) = arrTextWidth(0) + arrTextWidth(i)
            Next
            arrTextWidth(0) = arrTextWidth(0) + ((CodeLength - 1) * CharTracking)

            'text height
            TextHeight = CInt(FontMap(0)(0))
        
            'left margin
            LeftMargin = Math.Round((ImageWidth - arrTextWidth(0)) / 2)

            'top margin
            ReDim arrTopMargin(CodeLength)
            arrTopMargin(0) = Math.Round((ImageHeight - TextHeight) / 2)
            If RndTopMargin = True Then
                For i = 1 To CodeLength
                    arrTopMargin(i) = RndInterval(Math.Round(arrTopMargin(0) / 2), (arrTopMargin(0) + Math.Round(arrTopMargin(0) / 2)))
                Next
            Else
                For i = 1 To CodeLength
                    arrTopMargin(i) = arrTopMargin(0)
                Next
            End If

            'color selection
            i = RndInterval(0, UBound(ColorMap))
            BColor = ColorMap(i)(0)
            NColor = ColorMap(i)(1)
            TColor = ColorMap(i)(2)

            'write text
            For i = 1 To CodeLength
                'calculate cursor pos
                CursorPos = 0
                For j = (i - 1) To 1 Step -1
                    CursorPos = CursorPos + arrTextWidth(j) + CharTracking
                Next
                CursorPos = LeftMargin + CursorPos

                'write active char
                WriteCanvas(Mid(secureCode, i, 1), arrTopMargin(i))
            Next
        End Sub

        Sub DrawLine(ByVal x0 As Integer, ByVal y0 As Integer, ByVal x1 As Integer, ByVal y1 As Integer, ByVal valClr As String)
            'Reference from Donald Hearn and M. Pauline Baker, Computer Graphics C Version
            Dim m, b, dx, dy As Double
            Dim clrNoise As String
        
            If (NoiseEffect = 3) And (Bitmap(y0, x0) = TColor) Then
                clrNoise = vbNullString
            Else
                clrNoise = valClr
            End If
            Bitmap(y0, x0) = clrNoise

            dx = x1 - x0
            dy = y1 - y0
            If Math.Abs(dx) > Math.Abs(dy) Then
                m = (dy / dx)
                b = y0 - (m * x0)

                If dx < 0 Then
                    dx = -1
                Else
                    dx = 1
                End If

                Do While x0 <> x1
                    x0 = x0 + dx

                    If (NoiseEffect = 3) And (Bitmap(Math.Round((m * x0) + b), x0) = TColor) Then
                        clrNoise = vbNullString
                    Else
                        clrNoise = valClr
                    End If
                    Bitmap(Math.Round((m * x0) + b), x0) = clrNoise
                Loop
            ElseIf dy <> 0 Then
                m = (dx / dy)
                b = x0 - (m * y0)

                If dy < 0 Then
                    dy = -1
                Else
                    dy = 1
                End If

                Do While y0 <> y1
                    y0 = y0 + dy

                    If (NoiseEffect = 3) And (Bitmap(y0, Math.Round((m * y0) + b)) = TColor) Then
                        clrNoise = vbNullString
                    Else
                        clrNoise = valClr
                    End If
                    Bitmap(y0, Math.Round((m * y0) + b)) = clrNoise
                Loop
            End If
        End Sub
    
        Sub AddNoise()
            Dim median, i, j, x0, y0, x1, y1, dx, dy, dxy As Integer
            Dim clrNoise As String

            If NoiseEffect = 1 Then
                clrNoise = vbNullString
            Else
                clrNoise = NColor
            End If

            For i = 1 To NoiseLine
                x0 = RndInterval(1, ImageWidth)
                y0 = RndInterval(1, ImageHeight)
                x1 = RndInterval(1, ImageWidth)
                y1 = RndInterval(1, ImageHeight)

                'Check minimum line length
                dx = Math.Abs(x1 - x0)
                dy = Math.Abs(y1 - y0)
                median = Math.Round(Math.Sqrt((dx * dx) + (dy * dy)) / 2)
                If median < MinLineLength Then
                    dxy = MinLineLength - median

                    If x1 < x0 Then
                        dx = -1
                    Else
                        dx = 1
                    End If

                    If y1 < y0 Then
                        dy = -1
                    Else
                        dy = 1
                    End If

                    For j = 1 To dxy
                        If ((x1 + dx) < 1) Or ((x1 + dx) > ImageWidth) Or ((y1 + dy) < 1) Or ((y1 + dy) > ImageHeight) Then
                            Exit For
                        End If
                        x1 = x1 + dx
                        y1 = y1 + dy
                    Next
                End If

                'Draw noise line
                DrawLine(x0, y0, x1, y1, clrNoise)
            Next
        End Sub

        Function FormatHex(ByVal valHex As String, ByVal fixByte As Integer, ByVal fixDrctn As Integer, ByVal valReverse As Boolean) As String
            fixByte = fixByte * 2
            Dim tmpLen As Integer = Len(valHex)
            If fixByte > tmpLen Then
            
                Dim tmpFixHex As String = New String("0", (fixByte - tmpLen))
                If fixDrctn = 1 Then
                    valHex = valHex & tmpFixHex
                Else
                    valHex = tmpFixHex & valHex
                End If
            End If

            If valReverse = True Then
                Dim tmpHex As String = vbNullString
                For cFrmtHex As Integer = 1 To Len(valHex) Step 2
                    tmpHex = Mid(valHex, cFrmtHex, 2) & tmpHex
                Next
                FormatHex = tmpHex
            Else
                FormatHex = CStr(valHex)
            End If
        End Function

        Sub SendHex(ByVal valHex As String)
            For cHex As Integer = 1 To Len(valHex) Step 2
                Ctx.Response.BinaryWrite(New Byte() {CByte("&H" & Mid(valHex, cHex, 2))})
            Next
        End Sub
    
        Sub SendBitmap()
            If (ImageWidth Mod 4) <> 0 Then
                BmpEndLine = New String("0", (4 - (ImageWidth Mod 4)) * 2)
            Else
                BmpEndLine = vbNullString
            End If
            Dim BmpInfoHeader() As String = New String() {"28000000", "00000000", "00000000", "0100", "0800", "00000000", "00000000", "120B0000", "120B0000", "00000000", "00000000"}
            BmpInfoHeader(1) = FormatHex(Hex(ImageWidth), 4, 0, True)
            BmpInfoHeader(2) = FormatHex(Hex(ImageHeight), 4, 0, True)
            BmpInfoHeader(6) = FormatHex(Hex((ImageHeight * ImageWidth) + (ImageHeight * (Len(BmpEndLine) / 2))), 4, 0, True)
            BmpInfoHeader(9) = FormatHex(Hex(Len(BmpColorMap) / 8), 4, 0, True)
            BmpInfoHeader(10) = BmpInfoHeader(9)
            Dim BmpHeader() As String = New String() {"424D", "00000000", "0000", "0000", "00000000"}
            BmpHeader(1) = FormatHex(Hex((Len(Join(BmpHeader, "")) / 2) + (Len(Join(BmpInfoHeader, "")) / 2) + (Len(BmpColorMap) / 2) + (ImageHeight * ImageWidth) + (ImageHeight * (Len(BmpEndLine) / 2))), 4, 0, True)
            BmpHeader(4) = FormatHex(Hex((Len(Join(BmpHeader, "")) / 2) + (Len(Join(BmpInfoHeader, "")) / 2) + (Len(BmpColorMap) / 2)), 4, 0, True)

            Ctx.Response.Clear()
            Ctx.Response.Buffer = True
            Ctx.Response.ContentType = "image/bmp"
            Ctx.Response.AddHeader("Content-Disposition", "inline; filename=captcha.bmp")
            Ctx.Response.CacheControl = "no-cache"
            Ctx.Response.AddHeader("Pragma", "no-cache")
            Ctx.Response.Expires = -1

            SendHex(Join(BmpHeader, ""))
            SendHex(Join(BmpInfoHeader, ""))
            SendHex(BmpColorMap)
            For y As Integer = ImageHeight To 1 Step -1
                For x As Integer = 1 To ImageWidth
                    Dim tmpHex As String = Bitmap(y, x)
                    If tmpHex = vbNullString Then
                        SendHex(BColor)
                    Else
                        SendHex(tmpHex)
                    End If
                Next
                SendHex(BmpEndLine)
            Next
            Ctx.Response.Flush()
        End Sub
    End Class
End Namespace