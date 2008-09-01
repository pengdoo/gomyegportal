
// The Color for MultiColor Row
var _multiColorRow1 = "MultiColor1";
var _multiColorRow2 = "MultiColor2";


// The Color for the table default background Color
var _backgroundColor="#fffef9";

// The Color for the table selected row
var _selectedColor = "#fff3c6";

function SetMultiColor(objTable)
{
	//MulitColor for table
	
	var bIsC = false;
	
	if(objTable.tagName.toUpperCase()!='TABLE') return;
	
	for(var i=0;i<objTable.rows.length;i++)	
	{
		if(objTable.rows(i).cells(0).tagName.toUpperCase()=='TD')
		{
			if(bIsC)
			{
				objTable.rows(i).className = 'multiColor2';
			}
			else
			{
				objTable.rows(i).className = 'multiColor1';
			}
			bIsC = !bIsC;
		}
	}

}


function CellKeyDown()
{
	if(event.button!=0) return;
	
	var objCell = GetCell(event.srcElement);
	
	if(objCell==null) return;
	
	switch (event.keyCode){
		case 13:
			//KeyEnter
			if(objCell.tagName.toUpperCase()!='TD') return;
			if(objCell.parentElement.parentElement.parentElement.ondblclick!=null)
				objCell.parentElement.parentElement.parentElement.ondblclick();
			break;
		case 38:
			// KeyUP
			//event.keyCode = 0;
			switch(objCell.tagName.toUpperCase()){
				case 'TD':
					var objRow = objCell.parentElement;
					var objTable = objRow.parentElement.parentElement;
					var irowIndex = objRow.rowIndex;
					var icellIndex = objCell.cellIndex;
					
					if((irowIndex==0)||(objTable.rows.length<2)) break;
					
					if(objTable.rows(irowIndex - 1).cells(icellIndex)!=null)
					{
						if(objTable.rows(irowIndex - 1).cells(icellIndex).tagName.toUpperCase()!='TD') return;
					}
					else
					{
						if(objTable.rows(irowIndex - 1).cells(0).tagName.toUpperCase()!='TD') return;
					}
					
					SelectRow(objTable,irowIndex,false);
					SelectRow(objTable,irowIndex - 1,true);
					if(objTable.rows(irowIndex - 1).cells(icellIndex)!=null) 
					{
						objTable.rows(irowIndex - 1).cells(icellIndex).focus();
					}
					else
					{
						objTable.rows(irowIndex - 1).cells(0).focus();
					}
					onrowfocusChange.fire();
					break;

				default:break;
					
			}
			break;
		case 40:
			//KeyDown
			event.keyCode = 0;
			switch(objCell.tagName.toUpperCase()){
				case 'TD':
					var objRow = objCell.parentElement;
					var objTable = objRow.parentElement.parentElement;
					var irowIndex = objRow.rowIndex;
					var icellIndex = objCell.cellIndex;
					
					if((irowIndex>=(objTable.rows.length - 1))||(objTable.rows.length<2)) break;
					
					if(objTable.rows(irowIndex + 1).cells(icellIndex)!=null)
					{
						if(objTable.rows(irowIndex + 1).cells(icellIndex).tagName.toUpperCase()!='TD') return;
					}
					else
					{
						if(objTable.rows(irowIndex + 1).cells(0).tagName.toUpperCase()!='TD') return;
					}

					
					SelectRow(objTable,irowIndex,false);
					SelectRow(objTable,irowIndex + 1,true);
					if(objTable.rows(irowIndex + 1).cells(icellIndex)!=null) 
					{
						objTable.rows(irowIndex + 1).cells(icellIndex).focus();
					}
					else
					{
						objTable.rows(irowIndex + 1).cells(0).focus();
					}
					onrowfocusChange.fire();
					break;
				default:break;
			}
			break;
	}
	
}

function CellClick()
{
	if(event.keyCode!=0) return;
	
	// For the Click event in TD/TH
	var objCell = GetCell(event.srcElement);
	if(objCell==null) return;
	
	
	var row = objCell.parentElement;
	var body = row.parentElement;
	var table = body.parentElement;
	
	if(GetSelectedRow(table)==row.rowIndex) return;
	
	if(GetSelectedRow(table) >= 0) SelectRow(table,GetSelectedRow(table),false);
	
	SelectRow(table,row.rowIndex,true);
	
	onrowfocusChange.fire();
	
}


function MultiColorSelectRow(objTable,iSelectedRow,bIsSelected)
{
	if(bIsSelected)	//If Select a row to highlight
	{
		objTable.rows(iSelectedRow).className = 'selectedRow';
		return;
	}
	
	if(!bIsSelected) SetMultiColor(objTable);
	
}


function SelectRow(objTable,iSelectedRow,bIsSelected)
{
	// Select the row in table
	
	if(objTable.tagName.toUpperCase()!='TABLE') return;
	
	if(iSelectedRow<=-1)		return;	//MultiRow is Selected
	
	if(IsMultiColor(objTable))
	{
		MultiColorSelectRow(objTable,iSelectedRow,bIsSelected);
	}
	else
	{
		if(bIsSelected)
		{
			objTable.rows(iSelectedRow).className = "selectedRow";
			return;
		}
		else
		{
			objTable.rows(iSelectedRow).removeAttribute("className");
			return;
		}
	}
}

function IsMultiColor(objTable)
{
	//Valid Whether the objTable is a MultiColor	
	if(objTable.tagName.toUpperCase()!='TABLE') return false;
	
	if(objTable.rows.length<1) return false;
	
	var iColorCount;
	var bBegin = false;
	for(var i=0;i<objTable.rows.length;i++)
	{
		if((objTable.rows(i).className.toUpperCase()=='MULTICOLOR1')||(objTable.rows(i).className.toUpperCase()=='MULTICOLOR2'))		return true;
	}
	
	return false;
}

function GetSelectedRow(objTable)
{
	//Get the Selected Row Index
	// Return : -1 No Row is selected
	//             Otherwise the index for selected row
	
	if(objTable.tagName.toUpperCase()!='TABLE') return -1;
	
	for(var i=0;i<objTable.rows.length;i++)
	{
		if(objTable.rows(i).className.toUpperCase()=='SELECTEDROW') return i;
	}
	
	if(i==objTable.rows.length) return -1;
}

function getCurrentRow()
{
	//Get the Current Row
	if(element.tagName.toUpperCase()!='TABLE') return -1;
	
	return GetSelectedRow(element);
}

function setCurrentRow(iRow)
{
	//Set the Current Row
	if(iRow<1) return false;
	
	SelectRow(element,iRow,true);
}

function IsCellChild(obj)
{
	// Check if the obj is a child of the td
	
	if(obj.tagName.toUpperCase()=='TD') return true;
	
	while(obj.tagName.toUpperCase()!='TABLE')
	{
		obj = obj.parentElement;
		if(obj==null) return null;
		
		if(obj.tagName.toUpperCase()=='TD') return true;
	}
	
	return false;
	
}

function GetCellParentTable(obj)
{
	//Get the obj's parent table object
	if(!IsCellChild(obj)) return null;
	
	while(obj.tagName.toUpperCase()!='TABLE') {obj=obj.parentElement;}
	
	if(obj.tagName.toUpperCase()=='TABLE')
	{
		return obj
	}
	else
	{
		return null;
	}
	
}

function GetCell(obj)
{
	//Get the obj's parent td object
	if(!IsCellChild(obj)) return null;
	
	if(obj.tagName.toUpperCase()=='TD') return obj;
	while(obj.tagName.toUpperCase()!='TR')
	{
		obj = obj.parentElement;
		if(obj==null) return null;
		
		if(obj.tagName.toUpperCase()=='TD') return obj;
	}
	
	return null;
	
}

function SelectStart()
{
	event.returnValue = false;
}
