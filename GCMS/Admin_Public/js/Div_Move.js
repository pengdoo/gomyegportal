<!--
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);
// -->

function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_dragLayer(objName,x,hL,hT,hW,hH,toFront,dropBack,cU,cD,cL,cR,targL,targT,tol,dropJS,et,dragJS) { //v3.6
  //Copyright 1998 Macromedia, Inc. All rights reserved.
  var i,j,aLayer,retVal,curDrag=null,NS=document.layers, MZ=(!document.all && document.getElementById),curLeft, curTop;
  if (!document.all && !document.layers && !document.getElementById) return false;
  retVal = true; if(!NS && !MZ && event) event.returnValue = true;
  if (MM_dragLayer.arguments.length > 1) {
    curDrag = (MZ)?document.getElementById(objName):MM_findObj(objName); if (!curDrag) return false;
    if (!document.allLayers) { document.allLayers = new Array();
      with (document) if (NS) { for (i=0; i<layers.length; i++) allLayers[i]=layers[i];
        for (i=0; i<allLayers.length; i++) if (allLayers[i].document && allLayers[i].document.layers)
          with (allLayers[i].document) for (j=0; j<layers.length; j++) allLayers[allLayers.length]=layers[j];
      } else if(MZ){
            var mzall = getElementsByTagName("div");
            for (i=0;i<mzall.length;i++) if (mzall[i].style&&mzall[i].style.position) allLayers[allLayers.length]=mzall[i];
          } else {
                for (i=0;i<all.length;i++) if (all[i].style&&all[i].style.position) allLayers[allLayers.length]=all[i];
          }}
    curDrag.MM_dragOk=true; curDrag.MM_targL=targL; curDrag.MM_targT=targT;
    curDrag.MM_tol=Math.pow(tol,2); curDrag.MM_hLeft=hL; curDrag.MM_hTop=hT;
    curDrag.MM_hWidth=hW; curDrag.MM_hHeight=hH; curDrag.MM_toFront=toFront;
    curDrag.MM_dropBack=dropBack; curDrag.MM_dropJS=dropJS;
    curDrag.MM_everyTime=et; curDrag.MM_dragJS=dragJS;
    curDrag.MM_oldZ = (NS)?curDrag.zIndex:curDrag.style.zIndex;
    curLeft= (NS)?curDrag.left:(MZ)?curDrag.style.left.replace("px","")/1:curDrag.style.pixelLeft;
        curDrag.MM_startL = curLeft;
    curTop = (NS)?curDrag.top:(MZ)?curDrag.style.top.replace("px","")/1:curDrag.style.pixelTop;
        curDrag.MM_startT = curTop;
    curDrag.MM_bL=(cL<0)?null:curLeft-cL; curDrag.MM_bT=(cU<0)?null:curTop -cU;
    curDrag.MM_bR=(cR<0)?null:curLeft+cR; curDrag.MM_bB=(cD<0)?null:curTop +cD;
    curDrag.MM_LEFTRIGHT=0; curDrag.MM_UPDOWN=0; curDrag.MM_SNAPPED=false; //use in your JS!
    document.onmousedown = MM_dragLayer; document.onmouseup = MM_dragLayer;
    if (NS||MZ) document.captureEvents(Event.MOUSEDOWN|Event.MOUSEUP);
  } else {
    var theEvent = ((NS||MZ)?objName.type:event.type);
    if (theEvent == 'mousedown') {
      var mouseX = (NS||MZ)?objName.pageX : event.clientX + document.body.scrollLeft;
      var mouseY = (NS||MZ)?objName.pageY : event.clientY + document.body.scrollTop;
      var maxDragZ=null; document.MM_maxZ = 0;
      for (i=0; i<document.allLayers.length; i++) { aLayer = document.allLayers[i];
        var aLayerZ = (NS)?aLayer.zIndex:aLayer.style.zIndex/1;
        if (aLayerZ > document.MM_maxZ) document.MM_maxZ = aLayerZ;
        var isVisible = (((NS)?aLayer.visibility:aLayer.style.visibility).indexOf('hid') == -1);
        if (aLayer.MM_dragOk != null && isVisible) with (aLayer) {
          var parentL=0; var parentT=0;
          if (!NS) { parentLayer = (MZ)?aLayer.parentNode:aLayer.parentElement;
            while (parentLayer != null && parentLayer.style.position) {
              parentL += parentLayer.offsetLeft/1; parentT += parentLayer.offsetTop/1;
              parentLayer = (MZ)?parentLayer.parentNode:parentLayer.parentElement;
          } }
          var tmpX=mouseX-(((NS)?pageX:((MZ)?style.left.replace("px","")/1:style.pixelLeft)+parentL)+MM_hLeft);
          var tmpY=mouseY-(((NS)?pageY:((MZ)?style.top.replace("px","")/1:style.pixelTop) +parentT)+MM_hTop);
          var tmpW = MM_hWidth;  if (tmpW <= 0) tmpW += ((NS)?clip.width :offsetWidth);
          var tmpH = MM_hHeight; if (tmpH <= 0) tmpH += ((NS)?clip.height:offsetHeight);
          if ((0 <= tmpX && tmpX < tmpW && 0 <= tmpY && tmpY < tmpH) && (maxDragZ == null
              || maxDragZ <= aLayerZ)) { curDrag = aLayer; maxDragZ = aLayerZ; } } }
      if (curDrag) {
        document.onmousemove = MM_dragLayer; if (NS) document.captureEvents(Event.MOUSEMOVE);
        curLeft = (NS)?curDrag.left:(MZ)?curDrag.style.left.replace("px","")/1:curDrag.style.pixelLeft;
        curTop = (NS)?curDrag.top:(MZ)?curDrag.style.top.replace("px","")/1:curDrag.style.pixelTop;
        MM_oldX = mouseX - curLeft; MM_oldY = mouseY - curTop;
        document.MM_curDrag = curDrag;  curDrag.MM_SNAPPED=false;
        if(curDrag.MM_toFront) {
          eval('curDrag.'+((NS)?'':'style.')+'zIndex=document.MM_maxZ'+((MZ)?'':'+1') );
          if (!curDrag.MM_dropBack) document.MM_maxZ++; }
        retVal = false; if(!NS&&!MZ) event.returnValue = false;
    } } else if (theEvent == 'mousemove') {
      if (document.MM_curDrag) with (document.MM_curDrag) {
        var mouseX = (NS||MZ)?objName.pageX : event.clientX + document.body.scrollLeft;
        var mouseY = (NS||MZ)?objName.pageY : event.clientY + document.body.scrollTop;
        newLeft = mouseX-MM_oldX; newTop  = mouseY-MM_oldY;
        if (MM_bL!=null) newLeft = Math.max(newLeft,MM_bL);
        if (MM_bR!=null) newLeft = Math.min(newLeft,MM_bR);
        if (MM_bT!=null) newTop  = Math.max(newTop ,MM_bT);
        if (MM_bB!=null) newTop  = Math.min(newTop ,MM_bB);
        MM_LEFTRIGHT = newLeft-MM_startL; MM_UPDOWN = newTop-MM_startT;
        if (NS) {left = newLeft; top = newTop;}
                else if (MZ){style.left = newLeft; style.top = newTop;}
        else {style.pixelLeft = newLeft; style.pixelTop = newTop;}
        if (MM_dragJS) eval(MM_dragJS);
        retVal = false; if(!NS&&!MZ) event.returnValue = false;
    } } else if (theEvent == 'mouseup') {
      document.onmousemove = null;
      if (NS||MZ) document.releaseEvents(Event.MOUSEMOVE);
      if (NS||MZ) document.captureEvents(Event.MOUSEDOWN); //for mac NS
      if (document.MM_curDrag) with (document.MM_curDrag) {
        if (typeof MM_targL =='number' && typeof MM_targT == 'number' &&
            (Math.pow(MM_targL-((NS)?left:(MZ)?style.left.replace("px","")/1:style.pixelLeft),2)+
             Math.pow(MM_targT-((NS)?top:(MZ)?style.top.replace("px","")/1:style.pixelTop),2))<=MM_tol) {
          if (NS) {left = MM_targL; top = MM_targT;}
                  else if(MZ){style.left = MM_targL; style.top = MM_targT;}
          else {style.pixelLeft = MM_targL; style.pixelTop = MM_targT;}
          MM_SNAPPED = true; MM_LEFTRIGHT = MM_startL-MM_targL; MM_UPDOWN = MM_startT-MM_targT; }
        if (MM_everyTime || MM_SNAPPED) eval(MM_dropJS);
        if(MM_dropBack) {if (NS) zIndex = MM_oldZ; else style.zIndex = MM_oldZ;}
        retVal = false; if(!NS&&!MZ) event.returnValue = false; }
      document.MM_curDrag = null;
    }
    if (NS||MZ) document.routeEvent(objName);
  } return retVal;
}
//-->
