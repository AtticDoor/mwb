#pragma strict
    var url = "file://C:/test/tex.png";


    function Start () {
    // Start a download of the given URL
    var www : WWW = new WWW (url);
     
    // Wait for download to complete
    yield www;
     
    // assign texture
    GetComponent.<Renderer>().material.mainTexture = www.texture;
    }