var ViewModel = function (){
    var self = this;
    self.albums = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.artists= ko.observableArray();
    self.newAlbum = {
        Artist: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }

    var artistUri = '/api/artists/';
    var albumsUri = '/api/albums/';

    //get array of artist
    function getArtists() {
        ajaxHelper(artistUri, 'GET').done(function (data) {
            self.artists(data);
        });
    }

    self.addAlbum = function (formElement) {
        var album = {
            ArtistId: self.newAlbum().Artist().Id,
            Genre: self.newAlbum.Genre(),
            Price: self.newAlbum.Price(),
            Title: self.newAlbum.Title(),
            Year: self.newAlbum.Year()
        };

        //post user added album and artist to albums array
        ajaxHelper(albumsUri, 'POST', album).done(function (item) {
            self.albums.push(item);
        });
    }

    getArtists();

    //ajax helper class
    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    //get array of albums
    function getAllAlbums() {
        ajaxHelper(albumsUri, 'GET').done(function (data) {
            self.albums(data);
           
        });
    }
    // Fetch the initial data.
    getAllAlbums();

    //get album details
    self.getAlbumDetails = function (item) {
        ajaxHelper(albumsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

};
 
ko.applyBindings(new ViewModel());


