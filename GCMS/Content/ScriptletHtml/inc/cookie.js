function WriteValue(name,value,expiry){
	var expDate = new Date();
	var oneMinute = 60*1000;
	if (expiry){
		}
	else{
		expiry = 60*24*365;
	}
	expDate.setTime(expDate.getTime()+expiry*oneMinute);
	document.cookie = name + "=" + escape(value) + ";expires=" + expDate.toGMTString() + ";path=/";
	}
function ReadValue(CookieName){
	var CookieString = document.cookie;
	var CookieSet = CookieString.split (';');
	var SetSize = CookieSet.length;
	var CookiePieces
	var ReturnValue = "";
	var x = 0; 
	
	for (x = 0; ((x < SetSize) && (ReturnValue == "")); x++) {
		CookiePieces = CookieSet[x].split ('=');
		if (CookiePieces[0].substring (0,1) == ' ') {
			CookiePieces[0] = CookiePieces[0].substring (1, CookiePieces[0].length);
			}
		if (CookiePieces[0] == CookieName) {
			ReturnValue = CookiePieces[1];
			}
		}
	return(ReturnValue);
	}