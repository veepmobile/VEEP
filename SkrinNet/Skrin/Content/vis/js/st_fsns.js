var labelType, useGradients, nativeTextSupport, animate, dbl;
dbl = 0;
var STV;
var ds = 0;
var prev_ds = -1;
var refresh = 0;
var first = true;
var sc = 1
var st;
var first_obj;
(function () {
    var ua = navigator.userAgent,
        iStuff = ua.match(/iPhone/i) || ua.match(/iPad/i),
        typeOfCanvas = typeof HTMLCanvasElement,
        nativeCanvasSupport = (typeOfCanvas == 'object' || typeOfCanvas == 'function'),
        textSupport = nativeCanvasSupport
          && (typeof document.createElement('canvas').getContext('2d').fillText == 'function');
    //I'm setting this based on the fact that ExCanvas provides text support for IE
    //and that as of today iPhone/iPad current text support is lame
    labelType = (!nativeCanvasSupport || (textSupport && !iStuff)) ? 'Native' : 'HTML';
    nativeTextSupport = labelType == 'Native';
    useGradients = nativeCanvasSupport;
    animate = !(iStuff || !nativeCanvasSupport);
})();

var Log = {
    elem: false,
    write: function (text) {
        if (!this.elem)
            this.elem = document.getElementById('log');
        this.elem.innerHTML = text;
       // this.elem.style.left = (500 - this.elem.offsetWidth / 2) + 'px';
    }
};


function init() {
    //init data
    var json = start_data;
    //end
    //A client-side tree generator
    var getTree = (function () {
        var i = 0;

        return function (nodeId, level) {
            var iss = nodeId.split("_")[0];
            var type = nodeId.split("_")[1];
            var parent = $("#TT" + nodeId.split("_")[2]).attr("parent");
            var ptype = $("#TT" + nodeId.split("_")[2]).attr("ptype");
            //alert( "id:" + iss + "\nis_first:0\ntype:" + type + "\np:" +  parent + "\npt:" +  ptype )
            if (iss.length > 0 && level > 0) {
                var st = $.ajax({ url: "/vis/GetNodes", async: false, data: ({ "id": iss, "is_first": 0, "type": type, "p": parent, "pt": ptype }), dataType: "text" }).responseText
                var subtree;
                refresh = 0;
                try {
                    subtree = eval("(" + st + ")");
                }
                catch (e) {
                    subtree = eval("([])");
                }

                return ({ "id": nodeId, "children": subtree });
            }
        };
    })();
    //Implement a node rendering function called 'nodeline' that plots a straight line
    //when contracting or expanding a subtree.
    $jit.ST.Plot.NodeTypes.implement({
        'nodeline': {
            'render': function (node, canvas, animating) {
                if (animating === 'expand' || animating === 'contract') {
                    var pos = node.pos.getc(true), nconfig = this.node, data = node.data;
                    var width = nconfig.width, height = nconfig.height;
                    var algnPos = this.getAlignedPos(pos, width, height);
                    var ctx = canvas.getCtx(), ort = this.config.orientation;
                    ctx.beginPath();
                    if (ort == 'left' || ort == 'right') {
                        ctx.moveTo(algnPos.x, algnPos.y + height / 2);
                        ctx.lineTo(algnPos.x + width, algnPos.y + height / 2);
                    } else {
                        ctx.moveTo(algnPos.x + width / 2, algnPos.y);
                        ctx.lineTo(algnPos.x + width / 2, algnPos.y + height);
                    }
                    ctx.stroke();
                }
            }
        }

    });

    //init Spacetree
    //Create a new ST instance
    st = new $jit.ST({
        'injectInto': 'infovis',
        orientation: 'left',
        //set duration for the animation
        duration: 200,
        //set animation transition type
        transition: $jit.Trans.Quart.easeInOut,
        //set distance between node and its children
        levelDistance: 80,
        //set max levels to show. Useful when used with
        //the request method for requesting trees of specific depth
        levelsToShow: 1,
        Navigation: {
            enable: true,
            panning: true
        },
        //set node and edge styles
        //set overridable=true for styling individual
        //nodes or edges
        /*Label: {  
            overridable: false,  
            type: 'Native',  
            style: ' ',  
            size: 10,  
            family: 'sans-serif',  
            textAlign: 'center',  
            textBaseline: 'alphabetic',  
            color: '#222'  
        } ,*/
        Node: {
            height: 60,
            width: 190,
            //autoWidth:true,
            //use a custom
            //node rendering function
            type: 'rectangle',
            color: '#FFF',
            lineWidth: 1,
            align: "center",
            overridable: true
        },

        Edge: {
            type: 'quadratic:begin',
            dim: 35,
            //lineWidth: 1,
            color: '#555',
            overridable: true
        },
        constrained: false,
        /*Events: {  
              enable: true,  
              enableForEdges: true,  
              type: 'auto',  
              onClick: function(node,eventInfo, e){
                if(node.id.length==32){
                    void(0);
                }else{
                    st.onClick(node.id);
                }    
              },  
              onRightClick: $.empty,  
              onMouseMove: $.empty,  
              onMouseEnter: $.empty,  
              onMouseLeave: $.empty,  
              onDragStart: $.empty,  
              onDragMove: $.empty,  
              onDragCancel: $.empty,  
              onDragEnd: $.empty,  
              onTouchStart: $.empty,  
              onTouchMove: $.empty,  
              onTouchEnd: $.empty,  
              onTouchCancel: $.empty,  
              onMouseWheel: $.empty  
        }, */
        //Add a request method for requesting on-demand json trees. 
        //This method gets called when a node
        //is clicked and its subtree has a smaller depth
        //than the one specified by the levelsToShow parameter.
        //In that case a subtree is requested and is added to the dataset.
        //This method is asynchronous, so you can make an Ajax request for that
        //subtree and then handle it to the onComplete callback.
        //Here we just use a client-side tree generator (the getTree function).
        request: function (nodeId, level, onComplete) {
            var ans = getTree(nodeId, level);
            onComplete.onComplete(nodeId, ans);
        },

        onBeforeCompute: function (node) {
            cn = node.name;
            Log.write("Загрузка данных о " + cn);
            showClock();

        },

        onAfterCompute: function () {
            Log.write("");
            hideClock();

        },

        //This method is called on DOM label creation.
        //Use this method to add event handlers and styles to
        //your node.
        onCreateLabel: function (label, node) {
            label.id = node.id;
            label.className = "st_label"
            $(label).attr("ticker", node.data.ticker);
            $(label).attr("type_id", node.data.type);
            
            $(label).css("-moz-transform", "scale(" + sc + "," + sc + ")")
            $(label).css("-webkit-transform", "scale(" + sc + "," + sc + ")")
            $(label).css("-ms-transform", "scale(" + sc + "," + sc + ")")
            $(label).css("-o-transform", "scale(" + sc + "," + sc + ")")
            if (ds == -1) {
                first_obj = label
            }
            var tr = ""
            var nodes_data = node.name.split("_")
            var link = "<span style=\"color:#555;\">" + node.name + "</span>";
            
            if (node.data.ticker.length > 0 && String(node.data.type) != 2 && String(node.data.type) != 3) {
                    link = "<a  style=\"text-decoration:underline;\" target=\"_blank\" href=\"/issuers/" + node.data.ticker + "\">" + ((node.name.length>50)? node.name.substring(0,50)+'...':node.name) + "</a>";
               
            }

            var indic_style;
            var indic_src = "";
            var clr = ""
            var cls = "";
            switch (node.data.type) {
                case "0": {
                    cls = " m";
                    break;
                }
                case "1": {
                    cls = " p";
                    break;
                }
                case "2": {
                    cls = " m";
                    break;
                }
                case "3": {
                    cls = " r";
                    break;
                }
                case "4": {
                    cls = " mr";
                    break;
                }
                case "5": {
                    cls = " p";
                    break;
                }
                case "6": {
                    cls = " mr";
                    break;
                }
                case "-3": {
                    cls = " gr";
                    break;
                }

            }
            indic_style = "l_ic-skrin_i";

            var indic_cls = "l_ic l_ic-skrin_p"
            clr = "#777"
            var lstr = "<table class=\"labtab" + cls + "\" id=\"TT" + node.id.split("_")[2] + "\" ticker=\"" + node.data.ticker + "\" type_id=\"" + node.data.type + "\" parent=\"" + node.data.parent + "\" ptype=\"" + node.data.ptype + "\"><tr><td style=\"text-align:left;vertical-align:top;padding-left:4px;width:100%;z-index:1;\" class=\"nname\" ><span style=\"color:#990000\">" + ((node.data.share == "0" || node.data.share == "-") ? "" : node.data.share + node.data.ed) + "</span> " + link + ((node.data.ogrn.length > 0) ? "<br/>" + node.data.ogrn : "") + "</td><td rowspan=\"2\" style=\"width:13px;\"><table cellspacin=\"0\" cellpadding=\"0\" style=\"margin:0 0 0 0; height:100%; width:100%;\">";

            var btns = ""
            btns += "<tr><td style='height:30%;font-size:16px;padding-left:2px;'>" + ((node.data.type==5 ||node.data.type==6)? "≈" : " ") + "</td></tr><tr><td style=\"width:13px;padding-top:0;vertical-align:top;\">" + ((String(node.data.ex) == "1" && node.data.type != -1 && node.data.ticker.length > 0) ? "<div class=\"" + indic_cls + "\"  ><img onclick=\"ds=0;\" src=\"/Content/vis/null.gif\" style=\"width:13px;height:13px;\"/></div>" : "") + "</td></tr>";
            lstr += btns + "</table>"
         
            lstr += "</table>"
            label.innerHTML = lstr
            label.onclick = function (e) {
                e = window.event ? window.event : e;

                if ((e.target ? e.target : e.srcElement).src.indexOf("null.gif") > 0 && $(e.target ? e.target : e.srcElement).attr("is_r") != "1") {
                    

                       
                            $(label).find("div[class*='l_ic-egrul_m']").attr("class", "l_ic l_ic-egrul_p");
                            $(label).find("div[class*='l_ic-gmc_m']").attr("class", "l_ic l_ic-gmc_p");
                            if ($(label).find("div[class*='l_ic-skrin_p']").length > 0) {
                                $(label).find("div[class*='l_ic-skrin_p']").attr("class", "l_ic l_ic-skrin_m");
                            } else {
                                $(label).find("div[class*='l_ic-skrin_m']").attr("class", "l_ic l_ic-skrin_p");
                            }
                    

                    }
                    if (window.event) {
                        e = window.event;
                    }
                    if (e.stopPropagation) {
                        e.stopPropagation();
                    } else {
                        e.cancelBubble = true;
                    }

                    if (node.id.length == 32) {
                        void (0);
                    } else {
                        if (first) {
                            //node.collapsed=true;
                            first = false;
                        }
                        st.select(node.id);
                        if (ds != node.data.curr_ds || refresh == 1) {
                            node.collapsed = false;

                            st.removeSubtree(node.id, false, "replot", {
                                hideLabels: true
                            });
                        } else {
                            if (refresh == 0) {
                                if (node.collapsed || String(node.collapsed) == "undefined") {
                                    st.op.expand(node, { type: "replot" });
                                    node.collapsed = false;
                                } else {
                                    st.op.contract(node, { hideLabels: true, type: "replot" })
                                    node.collapsed = true;

                                }
                            }
                        }
                    }
                    node.data.curr_ds = ds;
                }


         
            //set label styles
            var style = label.style;

           

            var c = node.adjacencies.length
        },

        //This method is called right before plotting
        //a node. It's useful for changing an individual node
        //style properties before plotting it.
        //The data properties prefixed with a dollar
        //sign will override the global node style properties.
        onBeforePlotNode: function (node) {
            //add some color to the nodes in the path between the
            //root node and the selected node.
            if (node.selected) {
                node.data.$color = "#FFF";
            }
            else {
                delete node.data.$color;

            }
        },

        //This method is called right before plotting
        //an edge. It's useful for changing an individual edge
        //style properties before plotting it.
        //Edge data proprties prefixed with a dollar sign will
        //override the Edge global style properties.
        onBeforePlotLine: function (adj) {
            if (adj.nodeFrom.selected && adj.nodeTo.selected) {
                adj.data.$color = "#FF2221";
                adj.data.$lineWidth = 1;
            }
            else {
                delete adj.data.$color;
                delete adj.data.$lineWidth;
            }
        }
    });
    //load json data

    st.loadJSON(eval('(' + json + ')'));
    //compute node positions and layout
    st.compute();
    //emulate a click on the root node.

    st.onClick(st.root, {
        onComplete: function () {
            if (first_node) {
                ds = 0;
                st.onClick(st.root, {
                    onComplete: function () {
                        //$("#" + first_obj.id).find("div[class*='l_ic-skrin_p']").attr("class", "l_ic l_ic-skrin_m");
                    }
                });
                first_node = false;
            }
            // adjust the height of label container (otherwise its 0, so no printing)
            if (typeof (s) != "undefined") {
                var labelContainer = s.labels.getLabelContainer();
                labelContainer.style.height = st.canvas.getSize().height + 'px';
            }
        }

    });
    //end
    //Add event handlers to switch spacetree orientation.

    function get(id) {
        return document.getElementById(id);
    };

    var top = get('r-top'),
    left = get('r-left'),
    bottom = get('r-bottom'),
    right = get('r-right');

    function changeHandler() {
        if (this.checked) {
            top.disabled = bottom.disabled = right.disabled = left.disabled = true;
            st.switchPosition(this.value, "animate", {
                onComplete: function () {
                    top.disabled = bottom.disabled = right.disabled = left.disabled = false;
                }
            });
        }
    };

    //top.onchange = left.onchange = bottom.onchange = right.onchange = changeHandler;
    //end

}
function res(msht) {
    if (sc * msht > 0.1 && sc * msht < 1.25) {
        sc = sc * msht;
        var canv = st.canvas;
        canv.scale(msht, msht);
        $(".st_label").each(function () {
            $(this).css("-moz-transform", "scale(" + sc + "," + sc + ")")
            $(this).css("-webkit-transform", "scale(" + sc + "," + sc + ")")
            $(this).css("-ms-transform", "scale(" + sc + "," + sc + ")")
            $(this).css("-o-transform", "scale(" + sc + "," + sc + ")")

        })

    }
}
function dodbl(e, ticker) {
    if (window.event) {
        e = window.event;
    }
    if (e.stopPropagation) {
        e.stopPropagation();
    } else {
        e.cancelBubble = true;
    }

    if (ticker.length > 32) {
        ticker = ticker.split("_")[0]
        openIssuer(ticker);
    }
}
function sonclick(e) {
    e = window.event ? window.event : e;
    var obj = e.target ? e.target : e.srcElement
}
// stView here is the spacetree. Set the label container height on the initial call to onClick.
// (or alternately simply set the canvas size ahead of time)
/*
stView.onClick(nodeId, {
   onComplete: function() {
      // adjust the height of label container (otherwise its 0, so no printing)
      var labelContainer = stView.labels.getLabelContainer();
      labelContainer.style.height = stView.canvas.getSize().height +'px';
   }

}); 
*/