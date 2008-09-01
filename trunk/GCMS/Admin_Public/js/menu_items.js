var code="code";
var url="url";
var sub="sub";
var defcolor={"border":"#666666","shadow":"#5A5A5A","bgON":"white","bgOVER":"#b6bdd2","imagebg":"#dbd8d1","oimagebg":"#b6bdd2"};
var defcss = {"ON":"clsCMTopOn", "OVER":"clsCMTopOver"};
var TOP_LEFT = {"border":1, "borders":[1,1,0,1], "shadow":2, "color":defcolor, "css":defcss};
var TOP_MIDDLE = {"border":1, "borders":[0,1,0,1], "shadow":2, "color":defcolor, "css":defcss};
var TOP_RIGHT = {"border":1, "borders":[0,1,1,1],"shadow":2, "color":defcolor, "css":defcss};
var STYLE1 = {"border":1, "borders":[1,0,1,0], "shadow":2, "color":defcolor, "css":defcss};
var STYLE_TOP = {"border":1, "borders":[1,1,1,0], "shadow":2, "color":defcolor, "css":defcss};
var STYLE_BOTT = {"border":1, "borders":[1,0,1,1], "shadow":2, "color":defcolor, "css":defcss};

var MENU_ITEMS = [
	{"pos":"relative", "size":[22,80], "itemoff":[0,80], "imgsize":[16,27], "leveloff":[0,0], "delay":600, "image":"Admin_Public/Images/Menu/blank.gif", "imgsize":[20,24], "arrow":"Admin_Public/Images/Menu/arrow.gif", "arrsize":[13,13],  "style":TOP_MIDDLE},
    {code:"Scripts", "format":{"arrow":null, "image":null},
    sub:[
        {"size":[22,150], "itemoff":[21,0], "leveloff":[21,0], "arrow":"Admin_Public/Images/Menu/arrow.gif", "oarrow":"Admin_Public/Images/Menu/oarrow.gif", "style":STYLE1, "image":"Admin_Public/Images/Menu/b.gif"},
        {code:"COOLjsMenu",  "format":{"image":"Admin_Public/Images/Menu/menu.gif", "oimage":"Admin_Public/Images/Menu/omenu.gif", "style":STYLE_TOP},
        sub:[
            {"size":[22,150], "itemoff":[21,0], "leveloff":[3,147], "image":"Admin_Public/Images/Menu/b.gif", "oimage":"Admin_Public/Images/Menu/b.gif", "style":STYLE1},
            {code:"Overview", url:"/scripts/coolmenu/", "format":{"style":STYLE_TOP}},
            {code:"Demos", url:"/scripts/coolmenu/demos/"},
            {code:"Documentation", url:"/scripts/coolmenu/docs/", "format":{"style":STYLE_BOTT}}
            ]
        },
        {code:"COOLjsTree", "format":{"image":"Admin_Public/Images/Menu/tree.gif", "oimage":"Admin_Public/Images/Menu/otree.gif"},
        sub:[
            {"size":[22,150], "itemoff":[21,0], "leveloff":[3,147],"image":"Admin_Public/Images/Menu/b.gif", "oimage":"Admin_Public/Images/Menu/b.gif", "style":STYLE1},
            {code:"Overview", url:"/scripts/cooltree/", "format":{"style":STYLE_TOP}},
            {code:"Demos",
            sub:[
                {"size":[22,150], "itemoff":[21,0], "style":STYLE1},
                {code:"Simple Tree", url:"/scripts/cooltree/demos/simple/index.shtml", "format":{"style":STYLE_TOP}},
                {code:"Site Map", url:"/scripts/cooltree/demos/menu1/index.shtml"},
                {code:"Different Styles", url:"/scripts/cooltree/demos/styles/index.shtml"},
                {code:"Collapsable Forms", url:"/scripts/cooltree/demos/forms/index.shtml"},
                {code:"Cool Tree", url:"/scripts/cooltree/demos/cool/index.shtml", "format":{"style":STYLE_BOTT}}
                ]
            },
            {code:"Documentation", url:"/scripts/cooltree/docs/", "format":{"style":STYLE_BOTT}}
            ]
        },
        {code:"COOLjsMenu Pro", "format":{"image":"Admin_Public/Images/Menu/menup.gif", "oimage":"Admin_Public/Images/Menu/omenup.gif"},
        sub:[
            {"size":[22,150], "itemoff":[21,0], "leveloff":[3,147], "image":"Admin_Public/Images/Menu/b.gif", "oimage":"Admin_Public/Images/Menu/b.gif", "style":STYLE1},
            {code:"Overview", url:"/scripts/coolmenupro/", "format":{"style":STYLE_TOP}},
            {code:"Demos", url:"/scripts/coolmenupro/demos/"},
            {code:"Documentation", url:"/scripts/coolmenupro/docs/", "format":{"style":STYLE_BOTT}}
            ]
        },
        {code:"COOLjsTree Pro", "format":{"image":"Admin_Public/Images/Menu/treep.gif", "oimage":"Admin_Public/Images/Menu/otreep.gif"},
        sub:[
            {"size":[22,150], "itemoff":[21,0], "leveloff":[3,147], "image":"Admin_Public/Images/Menu/b.gif", "oimage":"Admin_Public/Images/Menu/b.gif", "style":STYLE1},
            {code:"Overview", url:"/scripts/cooltreepro/", "format":{"style":STYLE_TOP}},
            {code:"Demos",
            sub:[
                {"size":[22,150], "itemoff":[21,0], "style":STYLE1},
                {code:"Relative", url:"/scripts/cooltreepro/demos/relative.shtml", "target":"_blank", "format":{"style":STYLE_TOP}},
                {code:"Placed in TD", url:"/scripts/cooltreepro/demos/intdtag.shtml", "target":"_blank"},
                {code:"Different images", url:"/scripts/cooltreepro/demos/diffimages.shtml", "target":"_blank", "format":{"style":STYLE_BOTT}}
                ]
            },
            {code:"Documentation", url:"/scripts/cooltreepro/docs/", "format":{"style":STYLE_BOTT}}
            ]
        },
        {code:"Download/Order", url:"/pricing.shtml",  "format":{"image":"Admin_Public/Images/Menu/download.gif", "oimage":"Admin_Public/Images/Menu/odownload.gif", "style":STYLE_BOTT}}
        ]
    },
    {code:"Visual builders", "format":{"size":[22,120], "arrow":null, "image":null},
    sub:[
        {"size":[22,150], "itemoff":[21,0], "leveloff":[21,0], "arrow":"Admin_Public/Images/Menu/arrow.gif", "style":STYLE1, "image":"Admin_Public/Images/Menu/b.gif"},
        {code:"COOLjsTree builder", url:"/utils/cooltreebuilder.shtml", "format":{"style":STYLE_TOP}},
        {code:"COOLjsMenu builder", url:"/utils/coolmenubuilder.shtml", "format":{"style":STYLE_BOTT}}
        ]
    },
    {code:"нд╪Ч", "format":{"itemoff":[0,120], "arrow":null, "image":null, "style":TOP_RIGHT},
    sub:[
        {"size":[22,150], "itemoff":[21,0], "leveloff":[21,0], "image":"Admin_Public/Images/Menu/email.gif", "oimage":"Admin_Public/Images/Menu/oemail.gif", "style":STYLE1, "image":"Admin_Public/Images/Menu/b.gif"},
        {code:"General", url:"mailto:jsinfo@cooldev.com", "format":{"image":"Admin_Public/Images/Menu/email.gif", "style":STYLE_TOP}},
        {code:"Support", url:"mailto:jssupport@cooldev.com", "format":{"image":"Admin_Public/Images/Menu/email.gif"}}, 
        {code:"Sales", url:"mailto:jssales@cooldev.com", "format":{"image":"Admin_Public/Images/Menu/email.gif", "style":STYLE_BOTT}}
        ]
    }
];

