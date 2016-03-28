var slider : float;
var Hour : int;
var sun: Light;
var FogNight : Color;
var FogDay : Color;
var FogDensity = 0.005;
var SunNight : Color;
var SunDay : Color;
var SunClouded: Color;
var SkyBoxMaterial : Material;
var NightTint : Color;
var EarlyMorningTint : Color;
var MorningTint : Color;
var MiddayTint : Color;
var Sunbrightness :float;
var sHour : String;

function OnGUI () {
if(slider>1){
slider = 0;
}
Hour = slider*24;
sHour= Hour.ToString();
GUI.Label (Rect (230, 15, 100, 30), "Time"+"   "+sHour);
slider= GUI.HorizontalSlider( Rect(20,20,200,30), slider, 0,1.0);
sun.transform.localEulerAngles.x= (slider*360)-90;
slider = slider +Time.deltaTime/100;
if(slider<=0.5){
	sun.intensity=Sunbrightness +slider;
	RenderSettings.skybox.SetFloat("_Blend", slider*2);
	RenderSettings.fogColor = Color.Lerp (FogNight, FogDay, slider*2);
	sun.color = Color.Lerp (SunNight, SunDay, slider*2);

	if (slider<0.16666){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (NightTint, EarlyMorningTint, slider*6));

	}
else
	if (0.16666<slider&&slider<0.33333){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (EarlyMorningTint, MorningTint, (slider*6)-1));

	}
else
	if (0.33333<slider&&slider<0.5){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (MorningTint, MiddayTint, (slider*6)-2));


	}
}

if(slider>0.5){

sun.intensity=Sunbrightness +1-slider;
	RenderSettings.skybox.SetFloat("_Blend", 1-(slider-0.5)*2);
	RenderSettings.fogColor = Color.Lerp ( FogDay, FogNight,(slider*2)-1);
	sun.color = Color.Lerp ( SunDay, SunNight, (slider*2)-1);

if (slider<0.66666){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (MiddayTint, MorningTint, (slider*6)-3));

	}
else
	if (0.66666<slider&&slider<0.83333){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (MorningTint,EarlyMorningTint, (slider*6)-4));

	}
else
	if (0.83333<slider&&slider<1){
	SkyBoxMaterial.SetColor ("_Tint", Color.Lerp (EarlyMorningTint, NightTint, (slider*6)-5));


	}

}


}