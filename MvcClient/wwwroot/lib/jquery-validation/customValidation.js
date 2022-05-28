$.validator.unobtrusive.adapters.add('isLater', ['startTime'], function(options) {
    options.rules['isLater'] = { startTime: options.params.startTime };
    options.messages['isLater'] = options.message;
});

$.validator.addMethod("isLater", function (value, element, param) {
    var start = $('#' + param.startTime);
    if (start.val() != '') {
        var date = new Date();
        var startTimeArray = start.val().split(':');
        var startTime = new Date(date.getYear(), date.getMonth(), date.getDate(), startTimeArray[0], startTimeArray[1]);
        if (startTime != '') {
            var endTimeArray = value.split(':');
            var endTime = new Date(date.getYear(), date.getMonth(), date.getDate(), endTimeArray[0], endTimeArray[1]);
            return endTime > startTime;
        }
    }

    return true;
});

$.validator.unobtrusive.adapters.add('isBefore', ['endTime'], function(options) {
    options.rules['isBefore'] = { endTime: options.params.endTime };
    options.messages['isBefore'] = options.message;
});

$.validator.addMethod("isBefore", function(value, element, param) {
    var end = $('#' + param.endTime);
    if (end.val() != '') {
        var date = new Date();
        var endTimeArray = end.val().split(':');
        var endTime = new Date(date.getYear(), date.getMonth(), date.getDate(), endTimeArray[0], endTimeArray[1]);
        if (endTime != '') {
            var startTimeArray = value.split(':');
            var startTime = new Date(date.getYear(), date.getMonth(), date.getDate(), startTimeArray[0], startTimeArray[1]);
            return endTime > startTime;
        }
    }

    return true;
});