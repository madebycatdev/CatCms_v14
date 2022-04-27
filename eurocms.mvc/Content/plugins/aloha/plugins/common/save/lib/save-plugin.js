/* save-plugin.js is part of Aloha Editor project http://aloha-editor.org
 *
 * Aloha Editor is a WYSIWYG HTML5 inline editing library and editor.
 * Copyright (c) 2010-2013 Gentics Software GmbH, Vienna, Austria.
 * Contributors http://aloha-editor.org/contribution.php
 *
 * Aloha Editor is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or any later version.
 *
 * Aloha Editor is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 *
 * As an additional permission to the GNU GPL version 2, you may distribute
 * non-source (e.g., minimized or compacted) forms of the Aloha-Editor
 * source code without the copy of the GNU GPL normally required,
 * provided you include this license notice and a URL through which
 * recipients can access the Corresponding Source.
 */
define([
	'aloha',
	'aloha/jquery',
	'aloha/plugin',
	'util/arrays',
	'util/dom2',
	'ui/ui',
	'ui/button',
	'ui/floating',
	'PubSub',
	'./save',
	'./overlay',
	'./palette',
	'i18n!save/nls/i18n',
], function (
	Aloha,
	$,
	Plugin,
	Arrays,
	Dom,
	Ui,
	Button,
	Floating,
	PubSub,
	save,
	Overlay,
	Palette,
	i18n
) {
	

	/**
	 * Whether or not the given string represents a color value.
	 *
	 * @param {String} value
	 * @return {Boolean}
	 */
	

	/**
	 * Generates swatches.
	 *
	 * @param {Object} colors
	 * @param {Function(String)} getSwatchClass
	 * @return {Array<String>}
	 */
	
	/**
	 * Mark the given swatch element as selected in the overlay.
	 *
	 * @param {Overlay} overlay
	 * @param {jQuery<DOMObject>} $swatch
	 */

	/**
	 * Update the given range's text color based on the selected swatch element.
	 *
	 * @param {DOMObject} selected
	 * @param {Range} range
	 */
	function onSelect(selected, range) {
		if (range.collapsed) {
			Dom.extendToWord(range);
		}
		var $swatch = $('>div', selected);
		if ($swatch.hasClass('removecolor')) {
			save.unsetColor(range);
		} else {
			save.setColor(
				range,
				Dom.getComputedStyle($swatch[0], 'background-color')
			);
		}
		var selection = Aloha.getSelection();
		selection.removeAllRanges();
		selection.addRange(range);
	}

	var overlays = {};

	var rangeAtOpen;

	/**
	 * Gets/generates an overlay object for the given editable.
	 *
	 * @param {Editable} editable
	 * @param {Plugin} plugin
	 * @param {Button} button
	 * @param {Function(String)} getSwatchClass
	 * @return {Overlay}
	 */
	function getOverlay(editable, plugin, button, getSwatchClass) {
		// Because each editable may have its own configuration and therefore
		// each may have its own overlay.
		var config = plugin.getEditableConfig(editable.obj);
		if (!config || config.length < 1) {
			return null;
		}
		var id = editable.getId();
		var overlay = overlays[id];
		if (!overlay) {
			overlay = new Overlay(
				function (swatch) { onSelect(swatch, rangeAtOpen); },
				button.element[0]
			);
			overlay.$element
			       .addClass('aloha-ui-save-picker-overlay')
			       .css('position', Floating.POSITION_STYLE);
			overlays[id] = overlay;
		}
		return overlay;
	}

	/**
	 * Sets up the ui.
	 *
	 * @param {Plugin} plugin
	 * @param {Button} button
	 * @param {Function(String)} getSwatchClass
	 */
	function ui(plugin, button) {
		PubSub.sub('aloha.editable.activated', function (message) {
			plugin.overlay = getOverlay(
				message.data.editable,
				plugin,
				button
			);
			if (plugin.overlay) {
				button.show();
			} else {
				button.hide();
			}
		});

	}

	return Plugin.create('save', {

		config: Palette,

		_constructor: function () {
			this._super('save');
		},

		init: function () {
			var plugin = this;

			if (Aloha.settings.plugins && Aloha.settings.plugins.save) {
				plugin.config = Aloha.settings.plugins.save;
			}
			
			var button = Ui.adopt('colorPicker', Button, {
				tooltip: i18n.t('button-text'),
				icon: 'aloha-icon-save',
				scope: 'Aloha.continuoustext',
				click: function () {
					var content = Aloha.activeEditable.getContents();
					var contentId = Aloha.activeEditable.obj[0].id;
					if ( contentId.match(/-aloha$/gi) ) {
						contentId = contentId.replace( /-aloha/gi, '' );
					}							
				}
			});

			ui(plugin, button);

			Aloha.ready(function () {
				(function prepare(pos) {
					if (pos) {
						var index = pos - 1;
						setTimeout(function () {
							prepare(index);
						}, 100);
					}
				}(Aloha.editables.length));
			});
		}
	});
});
