var beat = 0.0;
var beatSub = 0;
var beatTot = 0.0;
static var rhythmAble = false;
var guiPop : GUITexture;
var Mo : AudioSource;
 
function Start()
{
    GetComponent.<AudioSource>().Play();
}
 
function Update()
{
    beat = Mo.time;
    beatSub = Mo.time;
    beatTot = beat - beatSub;
    guiPop.pixelInset.x = beatTot*100 + 220;
    if ((beatTot > 0) && (beatTot < .4)) {
        rhythmAble = true;
    } else {
        rhythmAble = false;
    }
    var beatStr = beatTot.ToString();
    GetComponent.<GUIText>().text = beatStr;
}   