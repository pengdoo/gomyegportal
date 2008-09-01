//浪仔猫 2003.1
//数组名+次序号码
//数组内容第一个值为标题
//"样式名称","字体颜色","名称","连接","目标窗口","Java"
/*
OutBarFolder1=new Array(
"操作(<u>A</u>)",
"none","default","新建用户(<u>N</u>)","none","_top","none",
"none","default","删除用户(<u>D</u>)","none","_top","none",
"break","default","<hr>","none","none","none",
"none","default","隶属角色","none","_top","none",
"none","default","设置密码","none","_top","none",
"break","default","<hr>","none","none","none",
"none","default","刷新(<u>R</u>)","none","_top","doReFresh();"
);
*/
	var childActive = null 
    var menuActive = null
    var lastHighlight = null
    var active = false

    function getReal(el) {

      // Find a table cell element in the parent chain */

      temp = el

      while ((temp!=null) && (temp.tagName!="TABLE") && (temp.className!="root") && (temp.id!="menuBar")) {

        if (temp.tagName=="TD")

          el = temp

        temp = temp.parentElement

      }

      return el

    }



	function raiseMenu(el) {
		el.style.borderLeft = "1px #08246B solid"
		el.style.borderTop = "1px #08246B solid"
		el.style.borderRight = "1px #08246B solid"
		el.style.borderBottom = "1px #08246B solid"
		el.style.background = "#B5BED6"
	}



	function clearHighlight(el) {
		if (el==null) return
		el.style.borderRight = "1px BUTTONFACE solid"
		el.style.borderBottom = "1px BUTTONFACE solid"
		el.style.borderTop = "1px BUTTONFACE solid"
		el.style.borderLeft = "1px BUTTONFACE solid" 
		el.style.background = "BUTTONFACE"
	}



    function sinkMenu(el) {
      el.style.borderRight = "1px BUTTONHIGHLIGHT solid"
      el.style.borderBottom = "1px BUTTONHIGHLIGHT solid"
      el.style.borderTop = "1px BUTTONSHADOW solid"
      el.style.borderLeft = "1px BUTTONSHADOW solid"
      el.style.background = "BUTTONFACE"
    }



    function menuHandler(menuItem) {

      // Write generic menu handlers here!

      // Returning true collapses the menu. Returning false does not collapse the menu

      return true

    }



    function getOffsetPos(which,el,tagName) {

      var pos = 0 // el["offset" + which]

      while (el.tagName!=tagName) {

        pos+=el["offset" + which]

        el = el.offsetParent

      }

      return pos	

    }



    function getRootTable(el) {

      el = el.offsetParent

      if (el.tagName=="TR") 

        el = el.offsetParent

      return el

    }



    function getElement(el,tagName) {

      while ((el!=null) && (el.tagName!=tagName) )

        el = el.parentElement

      return el

    }



    function processClick() {

      var el = getReal(event.srcElement)

      if ((getRootTable(el).id=="menuBar") && (active)) {        

        cleanupMenu(menuActive)

        clearHighlight(menuActive)

        active=false

        lastHighlight=null

        doHighlight(el)

      }

      else {

        if ((el.className=="root") || (!menuHandler(el)))

          doMenuDown(el)

        else {

          if (el._childItem==null) 

            el._childItem = getChildren(el)

          if (el._childItem!=null)  return;

          if ((el.id!="break") && (el.className!="disabled") && (el.className!="disabledhighlight") && (el.className!="clear"))  {

            if (menuHandler(el)) {

              cleanupMenu(menuActive)

              clearHighlight(menuActive)

              active=false

              lastHighlight=null

            }

          }

        }

      }

    }



    function getChildren(el) {

      var tList = el.children.tags("TABLE")

      var i = 0

      while ((i<tList.length) && (tList[i].tagName!="TABLE"))

        i++

      if (i==tList.length)

        return null

      else

        return tList[i]

    }



    function doMenuDown(el) {

      if (el._childItem==null) 

        el._childItem = getChildren(el)

      if ((el._childItem!=null) && (el.className!="disabled") && (el.className!="disabledhighlight")) {

        // Performance Optimization - Cache child element

        ch = el._childItem

        if (ch.style.display=="block") {

          removeHighlight(ch.active)

          return

        }

        ch.style.display = "block"

        if (el.className=="root") {

          ch.style.pixelTop = el.offsetHeight + el.offsetTop + 2

          ch.style.pixelLeft = el.offsetLeft + 1

	  if (ch.style.pixelWidth==0)

            ch.style.pixelWidth = ch.rows[0].offsetWidth+50

          sinkMenu(el)

          active = true

          menuActive = el

        } else {

          childActive = el

          ch.style.pixelTop = getOffsetPos("Top",el,"TABLE") -3 // el.offsetTop + el.offsetParent.offsetTop - 3

          ch.style.pixelLeft = el.offsetLeft + el.offsetWidth

	  if (ch.style.pixelWidth==0)

            ch.style.pixelWidth = ch.offsetWidth+50

        }     

      }

    }

 

    function doHighlight(el) {

      el = getReal(el)

      if ("root"==el.className) {

        if ((menuActive!=null) && (menuActive!=el)) {

          clearHighlight(menuActive)

        }

        if (!active) {

          raiseMenu(el)

        }          

        else 

          sinkMenu(el)

        if ((active) && (menuActive!=el)) {

          cleanupMenu(menuActive)          

          doMenuDown(el)

        }

        menuActive = el  

        lastHighlight=null

      }

      else {

        if (childActive!=null) 

          if (!childActive.contains(el)) 

            closeMenu(childActive, el)    



        if (("TD"==el.tagName) && ("clear"!=el.className)) {

          var ch = getRootTable(el)         

          if (ch.active!=null) {

            if (ch.active!=el) {

              if (ch.active.className=="disabledhighlight")  

                ch.active.className="disabled"

              else

                ch.active.className=""

              }

            }

          ch.active = el

          lastHighlight = el

          if ((el.className=="disabled") || (el.className=="disabledhighlight") || (el.id=="break")) 

            el.className = "disabledhighlight"

          else {

            if (el.id!="break") {

              el.className = "highlight"

              if (el._childItem==null) 

                el._childItem = getChildren(el)

              if (el._childItem!=null) {

                doMenuDown(el)

              }

            }  

          }

        }

      }

    }



    function removeHighlight(el) {

      if (el!=null)

        if ((el.className=="disabled") || (el.className=="disabledhighlight"))  

          el.className="disabled"

        else

          el.className=""

    }



    function cleanupMenu(el) {

      if (el==null) return

      for (var i = 0; i < el.all.length; i++) {

        var item = el.all[i]

        if (item.tagName=="TABLE")

         item.style.display = ""

        removeHighlight(item.active)

        item.active=null

      }

    }





    function closeMenu(ch, el) {

      var start = ch

      while (ch.className!="root") {

          ch = ch.parentElement

          if (((!ch.contains(el)) && (ch.className!="root"))) {

            start=ch

          }

      }

      cleanupMenu(start)

    }

 

    function checkMenu() {      

      if (document.all.menuBar==null) return

      if ((!document.all.menuBar.contains(event.srcElement)) && (menuActive!=null)) {

        clearHighlight(menuActive)

        closeMenu(menuActive)

        active = false

        menuActive=null

        choiceActive = null

      }

    }



    function doCheckOut() {

      var el = event.toElement      

      if ((!active) && (menuActive!=null) && (!menuActive.contains(el))) {

        clearHighlight(menuActive)

        menuActive=null

      }

    }





    function processKey() {

      if (active) {

        switch (event.keyCode) {

         case 13: lastHighlight.click(); break;

         case 39:  // right

           if ((lastHighlight==null) || (lastHighlight._childItem==null)) {

             var idx = menuActive.cellIndex

//             if (idx==menuActive.offsetParent.cells.length-2)

             if (idx==getElement(menuActive,"TR").cells.length-2)

               idx = 0

             else

               idx++

             newItem = getElement(menuActive,"TR").cells[idx]

           } else

           {

             newItem = lastHighlight._childItem.rows[0].cells[0]

           }

           doHighlight(newItem)

           break; 

         case 37: //left

           if ((lastHighlight==null) || (getElement(getRootTable(lastHighlight),"TR").id=="menuBar")) {

             var idx = menuActive.cellIndex

             if (idx==0)

               idx = getElement(menuActive,"TR").cells.length-2

             else

               idx--

             newItem = getElement(menuActive,"TR").cells[idx]

           } else

           {

             newItem = getElement(lastHighlight,"TR")

             while (newItem.tagName!="TD")

               newItem = newItem.parentElement

           }

           doHighlight(newItem)

           break; 

         case 40: // down

            if (lastHighlight==null) {

              itemCell = menuActive._childItem

              curCell=0

              curRow = 0

            }

            else {

              itemCell = getRootTable(lastHighlight)

              if (lastHighlight.cellIndex==getElement(lastHighlight,"TR").cells.length-1) {

                curCell = 0

                curRow = getElement(lastHighlight,"TR").rowIndex+1

                if (getElement(lastHighlight,"TR").rowIndex==itemCell.rows.length-1)

                  curRow = 0

              } else {

                curCell = lastHighlight.cellIndex+1

                curRow = getElement(lastHighlight,"TR").rowIndex

              }

            }

            doHighlight(itemCell.rows[curRow].cells[curCell])

            break;

         case 38: // up

            if (lastHighlight==null) {

              itemCell = menuActive._childItem

              curRow = itemCell.rows.length-1

              curCell= itemCell.rows[curRow].cells.length-1



            }

            else {

              itemCell = getRootTable(lastHighlight)

              if (lastHighlight.cellIndex==0) {

                curRow = getElement(lastHighlight,"TR").rowIndex-1

                if (curRow==-1)

                  curRow = itemCell.rows.length-1

                curCell= itemCell.rows[curRow].cells.length-1



              } else {

                curCell = lastHighlight.cellIndex - 1

                curRow = getElement(lastHighlight,"TR").rowIndex

              }

            }

            doHighlight(itemCell.rows[curRow].cells[curCell])

            break;

           if (lastHighlight==null) {

              curCell = menuActive._childItem

              curRow = curCell.rows.length-1

            }

            else {

              curCell = getRootTable(lastHighlight)

              if (getElement(lastHighlight,"TR").rowIndex==0)

                curRow = curCell.rows.length-1

              else

                curRow = getElement(lastHighlight,"TR").rowIndex-1

            }

            doHighlight(curCell.rows[curRow].cells[0])

            break;

        }

      }

    }





function make_menu() {

document.write("<table width='100%' cellpadding='0' cellspacing='0' border='0' class='coolBar'>");
document.write("<tr><TD><SPAN class=handbtn></SPAN></TD><td>");
document.write("<TABLE ID=menuBar ONSELECTSTART='return false' ONCLICK='processClick()' ONMOUSEOVER='doHighlight(event.toElement)' ONMOUSEOUT='doCheckOut()' ONKEYDOWN='processKey()' cellpadding='0' cellspacing='0' border='0'><tr>");

	j=1;
	while(eval("window.OutBarFolder"+j))
		j++;
	i=1;
	while(i<j)
	{
		Folder=eval("OutBarFolder"+i)
		document.write("<TD NOWRAP CLASS=root>"+Folder[0]+"<TABLE cellpadding='0' cellspacing='0' border='0'>");
		MakeItems(Folder);
		document.write("</TABLE>");
		i++;
	}
document.write("</TD></tr></TABLE>");
document.write("</td><td width=100% ONMOUSEOVER='processClick()'></td></tr></table>");
}



function MakeItems(Folder)
{
	var items=0;
	while(Folder[items+1])
		items+=7;
	items/=7;
	for(var i=1;i<items*7;i+=7)
	{
/*
		if(Folder[i+1]=="BREAK") {
			document.write("<TD NOWRAP ID=break><HR></TD>");
		}
		else {
			document.write("<tr><TD NOWRAP>"+Folder[i+1]+"</TD></tr>");
		}
*/
	document.write("<tr><TD"+((Folder[i+6]=="none")?"":" disabled")+" NOWRAP"+((Folder[i+0]=="none")?"":" ID='"+Folder[i+0]+"'")+((Folder[i+3]=="none")?"":" onclick=go(1,'"+Folder[i+3]+"')")+((Folder[i+5]=="none")?"":" onclick=\""+Folder[i+5]+"\"")+">"+((Folder[i+1]=="default")?"":"<font color="+Folder[i+1]+">")+Folder[i+2]+((Folder[i+1]=="default")?"":"</font>")+"</TD></tr>");

	//alert("<tr><TD NOWRAP "+((Folder[i+0]=="none")?"":"ID='"+Folder[i+0]+"'")+((Folder[i+3]=="none")?"":" onclick=go(1,'"+Folder[i+3]+"')")+((Folder[i+5]=="none")?"":" onclick=\""+Folder[i+5]+"\"")+">"+((Folder[i+1]=="default")?"":"<font color="+Folder[i+1]+">")+Folder[i+2]+((Folder[i+1]=="default")?"":"</font>")+"</TD></tr>");
	}
}



function go(i,iurl) {

	switch (i) 

	{

		case 1 : location=iurl;break; //返回首页
		case 2 : top.main.location='login.htm';break;  //登录
		case 3 : top.main.location='shenqing.htm';break;  //注册
		case 4 : top.main.location='addnew.asp';break;   //增加新贴
		case 5 : top.main.location='index1.asp';break;   //第一页
		case 6 : //上一页
			var obj=top.main
			var str=obj.location.href;	
			if(str.indexOf("index1")>0)
				obj.location="index1.asp@page="+obj.document.all("ppage").value;
			else
				obj.location="index1.asp";
			break;   
		case 7 : //下一页
			var obj=top.main
			var str=obj.location.href;	
			if(str.indexOf("index1")>0)
				obj.location="index1.asp@page="+obj.document.all("npage").value;
			else
				obj.location="index1.asp";
			break;   
		case 8 :  //最后一页
			var obj=top.main
			var str=obj.location.href;	
			if(str.indexOf("index1")>0)
				obj.location="index1.asp@page="+obj.document.all("epage").value;
			else
				obj.location="index1.asp";
			break;   
		case 9: top.main.location='editinfo.asp';break;
		case 10: top.main.location='quit.asp';break;
		case 11: top.main.location='userinfo.asp';break;			
		case 12: 
			var newwin=top.open("../../../../waha.3322.net/default.htm");
			newwin.focus();
			break;
	}
}



//make_menu();