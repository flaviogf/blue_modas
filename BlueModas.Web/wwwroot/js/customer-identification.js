$(document).ready(function() {
    $('[name=Phone]').on('input', function() {
        $(this).val($mask.phone($(this).val()))
    })
})
