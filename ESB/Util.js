var Util = Util || {};
Util.date = Util.date || {},
Util.filters = Util.filters || {},
Util.list = Util.list || {};

Util.date.datePrefix = function(n){
    var r = (n<10) ? ("0"+n) : n;
    return r;
}
Util.date.milliToDate = function(milli){
	//1377672302207 --> YY-MM-DD HH:MM:SS
	var _date = new Date(milli),
	    y = _date.getFullYear(),
	    m = Util.date.datePrefix(_date.getMonth() + 1),
	    d = Util.date.datePrefix(_date.getDate()),
	    h = Util.date.datePrefix(_date.getHours()),
	    mi = Util.date.datePrefix(_date.getMinutes()),
	    s = Util.date.datePrefix(_date.getSeconds());
	return y + '-' + m + '-' + d + ' ' + h + ':' + mi + ':' + s;
}
Util.date.dateToMilli = function(_date){
	//YY-MM-DD HH:MM:SS --> 1377672302207
    var reg = /^(\d{4})-(\d{2})-(\d{2})\s(\d{2}):(\d{2}):(\d{2})$/,
        nd = _date.replace(reg,function(a,y,m,d,h,mi,s){
            return (new Date(y,m-1,d,h,mi,s)*1);
        });
    return parseInt(nd);
}

Util.filters.toJson = function(str){
	//"Util.dateched.server.hitratio.avg product::hotel;result::miss;cluster::hotel.order.search"
	//" Util.dateched.server.hitratio.avg product::hotel;result::hit;cluster::hotelinnovation"
	//Object {metric: "Util.dateched.server.hitratio.avg", product: "hotel", result: "miss", cluster: "hotel.order.search"} 
	//Object {metric: "Util.dateched.server.hitratio.avg", product: "hotel", result: "hit", cluster: "hotelinnovation"} 
    var reg = /(.+)\s(.+)/,
    temp = str.replace(/^\s/,""),
    result = {};
    if(!temp.match(reg))
        return result;
    result.metric = temp.match(reg)[1];
    var tags = temp.match(reg)[2].split(";");
    for(var i=0,len=tags.length;i<len;i++){
        var kvarr = tags[i].split("::");
        result[kvarr[0]] = kvarr[1];
    }
    return result;
}
Util.filters.toSearch = function(str){
	//"Util.dateched.server.hitratio.avg product::hotel;result::miss;cluster::hotel.order.search"
	//" Util.dateched.server.hitratio.avg product::hotel;result::hit;cluster::hotelinnovation"
	//?metric=Util.dateched.server.hitratio.avg&product=hotel&result=miss&cluster=hotel.order.search dbtest.html:100
	//?metric=Util.dateched.server.hitratio.avg&product=hotel&result=hit&cluster=hotelinnovation 
	var reg = /(.+)\s(.+)/,
    temp = str.replace(/^\s/,""),
    result = "?";
    if(!temp.match(reg))
        return result;
    result +=("metric="+temp.match(reg)[1]);
    var tags = temp.match(reg)[2].split(";");
    for(var i=0,len=tags.length;i<len;i++){
        var kvarr = tags[i].split("::");
        result+=("&"+[kvarr[0]]+"="+kvarr[1]);
    }
    return result;
}
Util.list.sum = function(arr){
	var sum = 0;
	for(var i=0,len=arr.length;i<len;i++){
		sum+=arr[i];
	}
	return sum;
}
Util.list.avg = function(arr){
	if(arr.length)
	return Util.list.sum(arr)/arr.length;
}
