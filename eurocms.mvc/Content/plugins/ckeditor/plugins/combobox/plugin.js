/**
 * Basic sample plugin inserting current date and time into CKEditor editing area.
 *
 * Created out of the CKEditor Plugin SDK:
 * http://docs.ckeditor.com/#!/guide/plugin_sdk_intro
 */

CKEDITOR.plugins.add( 'combobox', {
    init: function( editor ) {
		
		var config = editor.config,
		lang = editor.lang.format;
		
		var tags = []; //new Array();
		tags[1] = ['##afiles_{alias}_1##', 'Article Files 1st File', ''];
		tags[2] = ['##afiles_{alias}_1_exists##', 'Article Files 1 Exist', ''];
		tags[3] = ['##afiles_{alias}_2##', 'Article Files 2nd File', ''];
		tags[4] = ['##afiles_{alias}_2_exists##', 'Article Files 1 Exist', ''];
		tags[5] = ['##afiles_{alias}_3##', 'Article Files 3rd File', ''];
		tags[6] = ['##afiles_{alias}_3_exists##', 'Article Files 1 Exist', ''];
		tags[7] = ['##afiles_{alias}_4##', 'Article Files 4th File', ''];
		tags[8] = ['##afiles_{alias}_4_exists##', 'Article Files 1 Exist', ''];
		tags[9] = ['##afiles_{alias}_5##', 'Article Files 5th File', ''];
		tags[10] = ['##afiles_{alias}_5_exists##', 'Article Files 1 Exist', ''];
		tags[11] = ['##afiles_{alias}_comment##', 'Article Files Comment', ''];
		tags[12] = ['##afiles_{alias}_title##', 'Article Files Title', ''];
		tags[13] = ['##article_1##', 'Article 1', ''];
		tags[14] = ['##article_2##', 'Article 2', ''];
		tags[15] = ['##article_3##', 'Article 3', ''];
		tags[16] = ['##article_4##', 'Article 4', ''];
		tags[17] = ['##article_5##', 'Article 5', ''];
		tags[18] = ['##article_detail_url##', 'Article Detail URL', ''];
		tags[19] = ['##article_id##', 'Article ID', ''];
		tags[20] = ['##article_keywords##', 'Article Keywords', ''];
		tags[21] = ['##article_meta_description##', 'Article Meta Description', ''];
		tags[22] = ['##article_meta_title##', 'Article Meta Title', ''];
		tags[23] = ['##article_type##', 'Article Type - Exist', ''];
		tags[24] = ['##article_type_detail##', 'Article Type - Exist', ''];
		tags[25] = ['##custom_1##', 'Custom 1', ''];
		tags[26] = ['##custom_2##', 'Custom 2', ''];
		tags[27] = ['##custom_20##', 'Custom 20', ''];
		tags[28] = ['##custom_3##', 'Custom 3', ''];
		tags[29] = ['##custom_4##', 'Custom 4', ''];
		tags[30] = ['##custom_5##', 'Custom 5', ''];
		tags[31] = ['##date_1##', 'Custom Date 1', ''];
		tags[32] = ['##date_1_exist##', 'Custom Date 1 - Exist', ''];
		tags[33] = ['##date_2##', 'Custom Date 2', ''];
		tags[34] = ['##date_2_exist##', 'Custom Date 2 - Exist', ''];
		tags[35] = ['##date_3##', 'Custom Date 3', ''];
		tags[36] = ['##date_3_exist##', 'Custom Date 3 - Exist', ''];
		tags[37] = ['##date_4##', 'Custom Date 4', ''];
		tags[38] = ['##date_4_exist##', 'Custom Date 4 - Exist', ''];
		tags[39] = ['##date_5##', 'Custom Date 5', ''];
		tags[40] = ['##date_5_exist##', 'Custom Date 5 - Exist', ''];
		tags[41] = ['##enddate##', 'Article Publish End Date', ''];
		tags[42] = ['##flag_1##', 'Flag 1', ''];
		tags[43] = ['##flag_2##', 'Flag 2', ''];
		tags[44] = ['##flag_3##', 'Flag 3', ''];
		tags[45] = ['##flag_4##', 'Flag 4', ''];
		tags[46] = ['##flag_5##', 'Flag 5', ''];
		tags[47] = ['##headline##', 'Article Headline', ''];
		tags[48] = ['##headline_exist##', 'Headline - Exist', ''];
		tags[49] = ['##menu_text##', 'Menu Text', ''];
		tags[50] = ['##menu_text_exist##', 'Menu Text - Exist', ''];
		tags[51] = ['##menu_text_headline##', 'Menu Text - Exist', ''];
		tags[52] = ['##page_title##', 'Page Title', ''];
		tags[53] = ['##random,5##', 'Random, 5', ''];
		tags[54] = ['##site_id##', 'Site ID', ''];
		tags[55] = ['##site_keywords##', 'Site Keywords', ''];
		tags[56] = ['##site_meta_description##', 'Site Meta Description', ''];
		tags[57] = ['##site_name##', 'Site Name Display', ''];
		tags[58] = ['##site_name_display##', 'Site Name Display', ''];
		tags[59] = ['##startdate##', 'Article Publish Start Date', ''];
		tags[60] = ['##summary,100##', 'Article Summary', ''];
		tags[61] = ['##summary_exist##', 'Summary - Exist', ''];
		tags[62] = ['##updated##', 'Article Last Update Date', ''];
		tags[63] = ['##zone_group_keywords##', 'Zone Group Keywords', ''];
		tags[64] = ['##zone_group_meta_description##', 'Zone Group Meta Description', ''];
		tags[65] = ['##zone_group_name_display##', 'Zone Group Name - Display', ''];
		tags[66] = ['##zone_id##', 'Zone ID', ''];
		tags[67] = ['##zone_keywords##', 'Zone Keywords', ''];
		tags[68] = ['##zone_meta_description##', 'Zone Meta Description', ''];
		tags[69] = ['##zone_name##', 'Zone Name - Exist', ''];
		tags[70] = ['##zone_name_display##', 'Zone Name - Display', ''];


		
		editor.ui.addRichCombo('cmsCustomVariables',
		{
			label: "Content Variables",
			title: "Content Variables",
			className : 'cke_format',
			multiSelect: false,
			panel :
			{
				css : [ config.contentsCss, CKEDITOR.getUrl('skins/moono-lisa/' + 'editor.css' ) ],
				voiceLabel : lang.panelVoiceLabel
			},
			init: function () { 
				for (var this_tag in tags){
					this.add(tags[this_tag][0], tags[this_tag][1], tags[this_tag][2]);
				}   
			},
			onClick : function( value )
			{        
				editor.focus();
				editor.fire( 'saveSnapshot' );
			    //var element = CKEDITOR.dom.element.createFromHtml('<img src="hello.png" border="0" title="Hello" />');
				//var element = CKEDITOR.dom.element.createFromHtml(value);
				editor.insertHtml(value);
				editor.fire( 'saveSnapshot' );
			}
		})


        //CMS SYSTEM VARIABLES
		var tags2 = [];
		tags2[1] = ['##session_{n}##', 'Session Value', ''];
		tags2[2] = ['##cookie_{n}##', 'Cookie Value', ''];
		tags2[3] = ['##today##', 'Today Date', ''];
		tags2[4] = ['##today_full##', 'Today Full Date', ''];
		tags2[5] = ['##site_url##', 'Site Domain Name', ''];




		editor.ui.addRichCombo('cmsSystemVariables',
                {
                    label: "System Variables",
                    title: "System Variables",
                    className: 'cke_format',
                    multiSelect: false,
                    panel:
                    {
                        css: [config.contentsCss, CKEDITOR.getUrl('skins/moono-lisa/' + 'editor.css')],
                        voiceLabel: lang.panelVoiceLabel
                    },
                    init: function () {
                        for (var this_tag in tags2) {
                            this.add(tags2[this_tag][0], tags2[this_tag][1], tags2[this_tag][2]);
                        }
                    },
                    onClick: function (value) {
                        editor.focus();
                        editor.fire('saveSnapshot');
                        editor.insertHtml(value);
                        editor.fire('saveSnapshot');
                    }
                })

		
    }
});



