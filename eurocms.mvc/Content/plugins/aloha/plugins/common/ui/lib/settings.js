Aloha.settings.toolbar = {
    tabs: [
        {
            label: 'Format',
            components: [
                [ 'bold', 'italic', 'underline', '\n',
                  'subscript', 'superscript', 'strikethrough' ],
                [ 'formatBlock' ]
            ]
        },
        {
            label: 'Insert',
            exclusive: true,
            components: [
                "createTable", "characterPicker", "insertLink",
            ]
        },
        {
            label: 'Link',
            components: [ 'editlink' ]
        }
    ],
    exclude: [ 'strong', 'emphasis', 'strikethrough' ]
};