(function(){
    window.get = get;
    window.post = post;
    // 创造ajax对象
    function createXHR(){
        if(XMLHttpRequest){
            var xhr = new XMLHttpRequest();
        }else{
            var xhr = new ActiveXObject("msxml2.XMLHTTP");
        }
        return xhr;
    }
    // 参数对象转为参数字符串
    function changeJSON2String(obj){
        var temp = [];
        for(var k in obj){
            temp.push(k+"="+encodeURIComponent);
        }
        return temp.join("&");
    }
    // 状态码监听
    function request(xhr,callback){
        xhr.onreadystatechange = function(){
            if(xhr . readyState ==4){
                if(xhr.status.toString().charAt(0) == 2||xhr.status == "304"){
                    var result = xhr.responseText;
                    callback && callback(result);
                }
            }
        }
    }
    // get
    function get(url,obj,callback){
        var xhr = createXHR();
        request(xhr,callback);
        var objStr = changeJSON2String(obj);
        xhr.open("get",url+"?"+objStr,true);
        xhr.send(null);
    }
    // post
    function post(url,obj,callback){
        var xhr = createXHR();
        request(xhr,callback);
        var objStr = changeJSON2String(obj);
        xhr.open("post",url,true);
        xhr.setRequestHeader("Content-type","application/x-www-form-urlencoded");
        xhr.send(objStr);
    }
})();