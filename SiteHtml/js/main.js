$(function() {
    $("#wizard").steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "fade",
        enableAllSteps: true,
        transitionEffectSpeed: 300,
        labels: {
            next: "Proximo",
            previous: "Voltar"
        },
        onStepChanging: function(event, currentIndex, newIndex) {
            if (newIndex === 1) {
                $('.steps').addClass('step-2');
            } else {
                $('.steps').removeClass('step-2');
            }
        }
    });
    $('.forward').click(function() {
        $("#wizard").steps('next');
    })
    $('.backward').click(function() {
        $("#wizard").steps('previous');
    })
    $('html').click(function() {
        $('.select .dropdown').hide();
    });
    $('.select').click(function(event) {
        event.stopPropagation();
    });
    $('.select .select-control').click(function() {
        $(this).parent().next().toggle().toggleClass('active');
    })
    $('.select .dropdown li').click(function() {
        $(this).parent().toggle();
        var text = $(this).attr('rel');
        $(this).parent().prev().find('div').text(text);
    })
    $('.payment-block .payment-item').click(function() {
        $('.payment-block .payment-item').removeClass('active');
        $(this).addClass('active');
    })
    var dp1 = $('#dp1').datepicker().data('datepicker');
})