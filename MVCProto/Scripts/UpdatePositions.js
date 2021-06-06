


// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var phub = $.connection.dataSource; // the generated client-side hub proxy

    function init() {
        phub.server.getInitial().done(function (ls) {
            //console.log(ls);
            for (var s in ls)                             
                $('#' + ls[s].split(/(ID[0-9]+)/)[1]).html(ls[s]);
            

            ymaps.ready(initMap);
        })

        var group = $("#positionDiv").attr("active");
        var ids = group.split(';');
        for (var id in ids)
            $('#' + ids[id]).addClass('font-weight-bold');
        phub.server.joinGroup(group);
        
    }

    // Add a client-side hub method that the server will call
    phub.client.updatePositions = function (posList) {
        $.connection.hub.log(posList.length);
        for (var s in posList)
            $('#' + posList[s].split(/(ID[0-9]+)/)[1]).html(posList[s]);
    }

    // Start the connection
    $.connection.hub.logging = true;
    $.connection.hub.start().done(init);



   

});









function initMap() {


    /**
     * An example implementation of a custom control based on inheritance from collection.Item.
     * The control displays the name of the object that is in the center of the map.
     */
    var map = new ymaps.Map("map", {
        center: [56.736506, 37.221947],
        zoom: 16,
        controls: []
    }, {
        // Зададим ограниченную область прямоугольником
        restrictMapArea: [
            [56.699118, 37.055495],
            [56.788962, 37.395487]
        ]
    },
    ),
        // Creating a custom class.
        CustomControlClass = function (options) {
            CustomControlClass.superclass.constructor.call(this, options);
            this._$content = null;
            this._geocoderDeferred = null;
        };

    // And inheriting it from the collection.Item.
    ymaps.util.augment(CustomControlClass, ymaps.collection.Item, {
        onAddToMap: function (map) {
            CustomControlClass.superclass.onAddToMap.call(this, map);
            this._lastCenter = null;
            this.getParent().getChildElement(this).then(this._onGetChildElement, this);
        },

        onRemoveFromMap: function (oldMap) {
            this._lastCenter = null;
            if (this._$content) {
                this._$content.remove();
                this._mapEventGroup.removeAll();
            }
            CustomControlClass.superclass.onRemoveFromMap.call(this, oldMap);
        },

       

        _createRequest: function () {
            var lastCenter = this._lastCenter = this.getMap().getCenter().join(',');
            // Requesting information about the place by the coordinates of the map center.
            ymaps.geocode(this._lastCenter, {
                // Specifying that the response should be in JSON format.
                json: true,
                // Setting a limit on the number of records in the response.
                results: 1
            }).then(function (result) {
                // We will only process the response from the last request.
                if (lastCenter == this._lastCenter) {
                    this._onServerResponse(result);
                }
            }, this);
        },

        _onServerResponse: function (result) {
            /**
             * The data has been received from the server and now it must be displayed.
             * Description of the response in JSON format.
             */
            let members = result.GeoObjectCollection.featureMember,
                geoObjectData = (members && members.length) ? members[0].GeoObject : null;
            if (geoObjectData) {
                this._$content.text(geoObjectData.metaDataProperty.GeocoderMetaData.text);
            }
        }
    });

    let customControl = new CustomControlClass();

    map.controls.add(customControl, {
        float: 'none',
        position: {
            top: 10,
            left: 10
        }
    });

    // Создаём макет содержимого.
    MyIconContentLayout = ymaps.templateLayoutFactory.createClass(
        '<div style="color: #FFFFFF; font-weight: bold;">$[properties.iconContent]</div>'
    );


    //let rows = $(".font-weight-bold");
    let rows = $("#positionDiv div");
    let coordinates = []
    for (let row of rows) {
        coordinates.push([
            parseFloat( row.innerHTML.split("; ")[1].replace(",", ".")),
            parseFloat(row.innerHTML.split("; ")[2].replace(",", "."))
        ])
        
    }
    console.log(coordinates);

    for (let i = 0; i < coordinates.length; i++) {
        myPlacemark = new ymaps.Placemark(coordinates[i], {
            hintContent: 'Drone',
            balloonContent: 'Drone content description',
            iconContent: (i + 1).toString()
        }, {
            iconLayout: 'default#imageWithContent',
            iconImageHref: '../img/drone.png',
            iconImageSize: [48, 42],
            iconImageOffset: [-24, -21],
            iconContentOffset: [15, 15],
            iconContentLayout: MyIconContentLayout
        })

        map.geoObjects
            .add(myPlacemark);
        console.log(coordinates[i])
    }

    // Добавляем отображение координат
    map.events.add('click', function (e) {
        if (!map.balloon.isOpen()) {
            var coords = e.get('coords');
            map.balloon.open(coords, {
                contentHeader: '',
                contentBody: '' +
                    '<p>' + [
                        coords[0].toPrecision(6),
                        coords[1].toPrecision(6)
                    ].join(', ') + '</p>',
                contentFooter: ''
            });
        }
        else {
            map.balloon.close();
        }
    });

} 