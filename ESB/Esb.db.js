function buildProductsTags (data){
	for(var i=0,len=data.length;i<len;i++){
		$("#btnGroup").append("<button class='btn' apps='"+data[i].AppIDs+"' services='"+data[i].WebServices+"'>"+data[i].Name+"</button>")
	}
}

(function(win,doc){

var Esb = {env:"product",instance:{}},
productNameCache = $("#btnGroup button")[0];

Esb.getProducts = function(){
	$.ajax("http://cms.fx.sh.ctripcorp.com/gov/Dashboard/Dashboard/products",{
		"dataType":"jsonp",
		"jsonpCallback":"buildProductsTags"
	})	
}
Esb.getInterval = function () {
    return $('#intervalVal').attr("v");
}
Esb.getStartTime = function () {
    return $("#startTime").val();
}
Esb.getEndTime = function () {
    return $("#endTime").val();
}
Esb.getProduct = function () {
    return $("#productName").val();
}
Esb.calendar = function(){
	var startTime = new Date() * 1 - 65 * 60 * 1000,
	    endTime = new Date() * 1 - 5 * 60 * 1000;
	$("#startTime").iCalendar({
	    type: 'datetime',
	    placeholder: Util.date.milliToDate(startTime)
	})
	$("#endTime").iCalendar({
	    type: 'datetime',
	    placeholder: Util.date.milliToDate(endTime)
	})
}
Esb.bindProductBtn = function(){
	$("#btnGroup").click(function(event){
		var btn = event.target;
		if(btn.tagName.toLowerCase()=="button"){
            /*if(Esb.isRealTime())
                $("#realtime").trigger("click");*/

			var appids = $(btn).attr("apps"),
			services = $(btn).attr("services"),
			proname = $(btn).text();
			$("#productName").val(proname);
			$("#appIds").val(appids);
			$("#webServ").val(services);

			Esb.request();

			$(productNameCache).removeClass("btn-inverse");
			$(btn).addClass("btn-inverse");
			productNameCache = btn;


		}
	})
}
Esb.bindRelTime = function(){
	$(".time-prev").bind("click", function () {
	    var btn = $(this),
	        rev = btn.attr("rev"),
	        startTime = Util.date.dateToMilli($("#" + rev + "_starttime").text()),
	        endTime = Util.date.dateToMilli($("#" + rev + "_endtime").text()),
	        upd_st = Util.date.milliToDate(startTime - 3600 * 1000),
	        upd_et = Util.date.milliToDate(endTime - 3600 * 1000);
	    $("#" + rev + "_starttime").text(upd_st);
	    $("#" + rev + "_endtime").text(upd_et);
	    Esb.identReq(rev, upd_st, upd_et);
	})

	$(".time-next").bind("click", function () {
	    var btn = $(this),
	        rev = btn.attr("rev"),
	        startTime = Util.date.dateToMilli($("#" + rev + "_starttime").text()),
	        endTime = Util.date.dateToMilli($("#" + rev + "_endtime").text()),
	        upd_st = startTime + 3600 * 1000,
	        upd_et = endTime + 3600 * 1000;
	    if (upd_st > (new Date() * 1 - 3600 * 1000)) upd_st = (new Date() * 1 - 3600 * 1000);
	    if (upd_et > (new Date() * 1)) upd_et = (new Date() * 1);
	    upd_st = Util.date.milliToDate(upd_st), upd_et = Util.date.milliToDate(upd_et);
	    $("#" + rev + "_starttime").text(upd_st);
	    $("#" + rev + "_endtime").text(upd_et);
	    Esb.identReq(rev, upd_st, upd_et);
	})
}
Esb.isRealTime = function(){
    //return document.getElementById("realtime").checked;
    return false;
}
Esb.identReq = function (ident, st, et) {
    $("#" + ident).loading();
    switch (ident) {
    case "delaydistr":
        Esb.reqDelaydistr(st, et);
        break;
    case "exception":
        Esb.reqException(st, et);
        break;
    case "callway":
        Esb.reqCallway(st, et);
        break;
    case "totalreq":
        Esb.reqTotalreq(st, et);
        break;
    }
}
Esb.resetTime = function () {
    var startTime = Esb.getStartTime(),
        endTime = Esb.getEndTime(),
        rev = ["delaydistr", "exception", "callway", "totalreq"];
    for (var i = 0, len = rev.length; i < len; i++) {
        $("#" + rev[i] + "_starttime").text(startTime);
        $("#" + rev[i] + "_endtime").text(endTime);
    }
}
Esb.initBindEvent = function(){

	Esb.calendar();

	Esb.bindProductBtn();

	Esb.bindRelTime();

    $('#intervalOptions').bind('click', function(event){
            if(event.target.nodeName.toLowerCase() === 'a'){
                var target = $(event.target);
                $('#intervalVal').text(target.text()).attr("v",target.attr("v"));
            }
    }); 

    $("#submit").bind("click",function(){
            Esb.request();
    });
    /*var _timer;
    $("#realtime").bind("click",function(){
        var bool = Esb.isRealTime(),
        upv = 60;
        if(bool){
            $("#mask").show();
            $("#timer").show();
            $('#intervalVal').attr("v","1m").text("1分钟");
            _timer = setInterval(function(){
                $("#leftsec").text(--upv);
                if(upv<=0) upv = 60;
            },1000)
            $(".tagtimeover").show();
        }else{
            $("#mask").hide();
            $("#timer").hide();
            $("#leftsec").text(upv);
            _timer && clearInterval(_timer);
            $(".tagtimeover").hide();
        }
        Esb.request();
    });*/

}
Esb.loading = function(id){
	if(!id)
		$("#delaydistr,#exception,#callway,#totalreq").loading();
	else{
		$(id).loading();
	}
}
Esb.getAppId = function(){
	var v = $("#appIds").val();
	if(v)
		return v.split(",");
}
Esb.getWebService = function(){
	var v = $("#webServ").val();
	if(v)
		return v.split(",");
}
Esb.request = function(){

	Esb.reqDelaydistr();
	Esb.reqException();
	Esb.reqCallway();
	Esb.reqTotalreq();
    Esb.resetTime();
	Esb.loading();

}
Esb.reqDelaydistr = function(st,et){

	var filters = {};
	if(Esb.getWebService()){
		filters = {
			"distribution": ["1~5s","5~10s","10~30s","30~100s",">100s"],
			"webservice":Esb.getWebService()
        }
	}else{
		filters = {
			"distribution": ["1~5s","5~10s","10~30s","30~100s",">100s"]
        }
	}

	var startTime = st || Esb.getStartTime(),
        endTime = et || Esb.getEndTime();  

    Esb.instance.delay && Esb.instance.delay.destroy(); 
    Esb.instance.delay = CData().dashboard({
            env:Esb.env,
            bases: {
                "startTime": startTime,
                "endTime": endTime,
                "interval": Esb.getInterval(),
                "metric": ["esb.request.distribution"],
                "filters": filters,
                "realtime" : Esb.isRealTime()
            },
            "namespace": null,
            "statistics": "sum"
        }).line({
            id: "delaydistr",
            params: {
                xAxis: {
                    type: "datetime"
                },
                yAxis: { 
                    allowDecimals:false 
                },
                tooltip: {
                    crosshairs: true,
                    shared: false,
                    formatter:function(){
                        return '<b style="font-weight:700;color:'+this.series.color+';">'+ this.series.name +'</b><br/><span style="font-weight:700;">Time:</span>'+ Util.date.milliToDate(this.x) +' <span style="font-weight:700;">Value:</span>'+ this.y;
                    }
                },
                cclick:function(data){
                    var params = Util.filters.toSearch(data.name),
                    product = $("#productName").val(),
                    interval = Esb.getInterval();
                    window.open('delay.distribution.php'+params+"&product="+product+"&startTime="+startTime+"&endTime="+endTime+"&interval="+interval+"&webservice="+filters.webservice,"")
                }
            }
        })     
}
Esb.reqException = function(st,et){

	var filters = {};
	if(Esb.getWebService()){
		filters = {
			"exceptiontype": [],
			"webservice":Esb.getWebService()
        }
	}else{
		filters = {
			"exceptiontype": []
        }
	}

	var startTime = st || Esb.getStartTime(),
        endTime = et || Esb.getEndTime();  

    Esb.instance.exception && Esb.instance.exception.destroy(); 
    Esb.instance.exception = CData().dashboard({
            env:Esb.env,
            bases: {
                "startTime": startTime,
                "endTime": endTime,
                "interval": Esb.getInterval(),
                "metric": ["esb.exception.count"],
                "filters": filters,
                "realtime" : Esb.isRealTime()
            },
            "namespace": null,
            "statistics": "sum"
        }).line({
            id: "exception",
            params: {
                xAxis: {
                    type: "datetime"
                },
                yAxis: { 
                    allowDecimals:false 
                },
                tooltip: {
                    crosshairs: true,
                    shared: false,
                    formatter:function(){
                        return '<b style="font-weight:700;color:'+this.series.color+';">'+ this.series.name +'</b><br/><span style="font-weight:700;">Time:</span>'+ Util.date.milliToDate(this.x) +' <span style="font-weight:700;">Value:</span>'+ this.y;
                    }
                },
                cclick:function(data){
                    var params = Util.filters.toSearch(data.name),
                    product = $("#productName").val(),
                    interval = Esb.getInterval();
                    window.open('exception.php'+params+"&product="+product+"&startTime="+startTime+"&endTime="+endTime+"&interval="+interval+"&webservice="+filters.webservice,"")
                }
            }
        })     
}
Esb.reqCallway = function(st,et){

    var filters = {};
    if(Esb.getAppId()){
        filters = {
            "from": [],
            "appid":Esb.getAppId()
        }
    }else{
        filters = {
            "from": []
        }
    }
	var startTime = st || Esb.getStartTime(),
        endTime = et || Esb.getEndTime(); 

    Esb.instance.callway && Esb.instance.callway.destroy(); 
    Esb.instance.callway = CData().dashboard({
            env:Esb.env,
            bases: {
                "startTime": startTime,
                "endTime": endTime,
                "interval": Esb.getInterval(),
                "metric": ["esb.request.count"],
                "filters": filters,
                "realtime" : Esb.isRealTime()
            },
            "namespace": null,
            "statistics": "sum",
            start: function () {

            },
            filter:function(data,callback){
            	var list = data.results[0]['data-lists'],
				_da_li = [['直连', []], ['非直连', []]],
				matchLog = /(from::log)|(form::client)/,
				matchEsb = /(from::esb)/,
				maxArrLen = 0,
				logResult = [],
				esbResult = [];
				for(var i = 0,len = list.length; i < list.length; i++){
					if(list[i][1].length>maxArrLen)
						maxArrLen = list[i][1].length;
					if(list[i][0].match(matchLog))
						logResult.push(list[i]);
					if(list[i][0].match(matchEsb))
						esbResult.push(list[i]);
				}
				for(var i=0;i<maxArrLen;i++){
					var row_log = 0,row_esb = 0;
					for(var j=0,len=logResult.length;j<len;j++){
						row_log +=  (logResult[j][1][i] || 0);
						_da_li[0][1][i] = row_log;
					}
					for(var j=0,len=esbResult.length;j<len;j++){
						row_esb +=  (esbResult[j][1][i] || 0);
						_da_li[1][1][i] = row_esb;
					}
				}
				data.results[0]['data-lists'] = _da_li;
				callback(data);
            },
            end: function () {

            }
        }).line({
            id: "callway",
            params: {
                xAxis: {
                    type: "datetime"
                },
                cclick:function(data){
                    var _name = data.name,
                    product = $("#productName").val(),
                    interval = Esb.getInterval();
                   
                    window.open("callway.php?from="+_name+"&product="+product+"&startTime="+startTime+"&endTime="+endTime+"&interval="+interval+"&appid="+filters.appid,"")
                }
            }
        })     
}
Esb.reqTotalreq = function(st,et){
    var filters = {};
    if(Esb.getWebService()){
        filters = {
            "webservice":Esb.getWebService()
        }
    }else{
        filters = {
        }
    }
    var startTime = st || Esb.getStartTime(),
        endTime = et || Esb.getEndTime(); 

    Esb.instance.totalreq && Esb.instance.totalreq.destroy(); 
    Esb.instance.totalreq = CData().dashboard({
            env:Esb.env,
            bases: {
                "startTime": startTime,
                "endTime": endTime,
                "interval": Esb.getInterval(),
                "metric": ["esb.request.count"],
                "filters" : filters,
                "realtime" : Esb.isRealTime()
            },
            "namespace": null,
            "statistics": "sum"
        }).line({
            id: "totalreq",
            params: {
                xAxis: {
                    type: "datetime"
                },
                tooltip: {
                    crosshairs: true,
                    shared: false,
                    formatter:function(){
                        return '<b style="font-weight:700;color:'+this.series.color+';">'+ this.series.name +'</b><br/><span style="font-weight:700;">Time:</span>'+ Util.date.milliToDate(this.x) +' <span style="font-weight:700;">Value:</span>'+ this.y;
                    }
                },
                cclick:function(data){
                    var params = Util.filters.toSearch(data.name),
                    product = $("#productName").val(),
                    interval = Esb.getInterval();
                    window.open("totalreq.php"+params+"&product="+product+"&startTime="+startTime+"&endTime="+endTime+"&interval="+interval+"&webservice="+filters.webservice,"")
                }
            }
        })  
}

Esb.init = function(){

	Esb.getProducts();

	Esb.initBindEvent();

	Esb.request();
}


win.Esb = Esb;
})(window,document)


