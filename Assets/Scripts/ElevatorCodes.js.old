#pragma strict
static var Codes:boolean[];
static var TVsDisabled:boolean[];
static var initialized:boolean;


static function ClearCode()
{
	if (!initialized)
		InitCodes();

	
}


static function InitCodes()
{
	Codes= new Array(100);//[false,false];
	initialized=true;

	TVsDisabled= new Array(100);//[false,false];
}



static function ClearTV(num:int)
{
	if (!initialized)
		InitCodes();
	TVsDisabled[num]=true;
}



static function TVCleared(num:int):boolean
{
	if (!initialized)
		InitCodes();
	return(TVsDisabled[num]);
}
