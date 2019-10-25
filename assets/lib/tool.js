function dynamicLoadCss(url) {
    var head = document.getElementsByTagName('head')[0];
    var link = document.createElement('link');
    link.type = 'text/css';
    link.rel = 'stylesheet';
    link.href = url;
    head.appendChild(link);
}

(function () {
    var styles = ['default.css', 'simple.css'];
    var currentStyle = window.localStorage.getItem('currentStyle') || styles[0];
    var cssSelect = window.document.createElement('select');
    cssSelect.setAttribute('style','position:absolute;bottom:5px;right:5px;')
    var styleurl = './assets/theme/' + currentStyle;
    dynamicLoadCss(styleurl);
    cssSelect.value = currentStyle;
    for (var i = 0; i < styles.length; i++) {
        var el = window.document.createElement('option');
        var val = styles[i];
        el.setAttribute('value',val);
        el.innerText = val;
        if (val == currentStyle)
            el.setAttribute('selected', true);
        cssSelect.appendChild(el);
    }

    cssSelect.onchange = function(){
        var selStyle = cssSelect.value;
        window.localStorage.setItem('currentStyle', selStyle);
        window.location.reload();
    }

    window.document.body.appendChild(cssSelect);
})();