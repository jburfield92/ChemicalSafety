

function callAlert() {

    var map = document.getElementById("googleMap");
    map.removeAttribute("hidden");
    initialize();

}

function initialize() {
    // var map2 = document.getElementById("googleMap");
    // map2.removeAttribute("hidden");
        var mapProp = {
            zoom: 5,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
        var geocoder = new google.maps.Geocoder();
        var eventLoc = document.getElementById("locStuff");
        var address = eventLoc.value;

    /* var address = "fake address florida 99999"; */

        var eventName = document.getElementById("nameStuff").value;
        var eventDate = document.getElementById("dateStuff").value;
        var twitText = "I will be attending ";
        twitText = twitText.concat(eventName);
        twitText = twitText.concat(" on ");
        twitText = twitText.concat(eventDate);

        document.getElementById("twitterButton").setAttribute("data-text", twitText);

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                result = results[0].geometry.location;
                console.log(result);

                map.setCenter(result);
                map.setZoom(13);
                var marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location
                });
            }
            else {

            }
        });

    }