(
    function (ctx) {

        ctx.upload = function() {
            var formData = new FormData($('#imageUpload')[0]);
            $.ajax({
                url: helper.actionUrl("UploadImage", "Home"),
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function() {
                    $('#avatar').prop("src", $('#avatar').prop("src"));
                }
            });
        };

        ctx.onUpload = function() {
            $("#imageSelect").click();
        };
    }
)(window.uploader = window.uploader || {});