

function callAlert() {

    initialize();

}

function initialize() {

        var geocoder = new google.maps.Geocoder();
        var eventLoc = document.getElementById("verifyAddress");
        var address = eventLoc.value;

        /* var address = "fake address florida 99999"; */
        var btn = document.getElementById("verifyResult");

        geocoder.geocode({ 'address': address }, function (results, status) {

            if (status == google.maps.GeocoderStatus.OK) {
                btn.value = "good";
            }
            else {
                btn.value = "bad";
            }
        });

    }