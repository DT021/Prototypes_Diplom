ymaps.ready(init);


const balloonContentGeneral = [ 
    '<div class="map__balloon">',
            '<img class="map__burger-img" src="img/drone.png" alt="Дрон"/>',
            '<p>Описание дрона</p>',
    '</div>'
]


var placemarks = [
    {
        latitude: 59.97,
        longitude: 30.31,
        hintContent: '<div class="map__hint">Дрон №1</div>',
        balloonContent: balloonContentGeneral
    },
    {
        latitude: 59.94,
        longitude: 30.25,
        hintContent: '<div class="map__hint">Дрон №2</div>',
        balloonContent: balloonContentGeneral
    },
    {
        latitude: 59.93,
        longitude: 30.34,
        hintContent: '<div class="map__hint">Дрон №3</div>',
        balloonContent: balloonContentGeneral
    }
],
    geoObjects = [];

function init() {
    var map = new ymaps.Map('map', {
        center: [59.94, 30.32],
        zoom: 12,
        controls: ['zoomControl'],
        behaviors: ['drag']
    });

    for (var i = 0; i < placemarks.length; i++) {
        geoObjects[i] = new ymaps.Placemark([placemarks[i].latitude, placemarks[i].longitude],
            {
                hintContent: placemarks[i].hintContent,
                balloonContent: placemarks[i].balloonContent.join('')
            },
            {
                iconLayout: 'default#image',
                iconImageHref: 'img/drone.png',
            
            });
    }

    var clusterer = new ymaps.Clusterer({
        clusterIcons: [
            {
                href: 'img/drone.png',
                size: [100, 100],
                offset: [-50, -50]
            }
        ],
        clusterIconContentLayout: null
    });

    clusterer.add(geoObjects);
    map.geoObjects.add(clusterer);

}