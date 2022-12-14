"ace" in window || (window.ace = {}), ace.config = {
    cookie_expiry: 604800,
    cookie_path: "",
    storage_method: 2
}, "vars" in window.ace || (window.ace.vars = {}), ace.vars.very_old_ie = !("querySelector" in document.documentElement), ace.settings = {
    is: function (a, b) {
        return 1 == ace.data.get("settings", a + "-" + b)
    },
    exists: function (a, b) {
        return null !== ace.data.get("settings", a + "-" + b)
    },
    set: function (a, b) {
        ace.data.set("settings", a + "-" + b, 1)
    },
    unset: function (a, b) {
        ace.data.set("settings", a + "-" + b, -1)
    },
    remove: function (a, b) {
        ace.data.remove("settings", a + "-" + b)
    },
    navbar_fixed: function (a, b, c, d) {
        if (ace.vars.very_old_ie) return !1;
        var a = a || "#navbar";
        if ("string" == typeof a && (a = document.querySelector(a)), !a) return !1;
        if (b = b || !1, c = c && !0, !b && d !== !1) {
            var e = null;
            (ace.settings.is("sidebar", "fixed") || (e = document.getElementById("sidebar")) && ace.hasClass(e, "sidebar-fixed")) && ace.settings.sidebar_fixed(e, !1, c)
        }
        b ? (ace.hasClass(a, "navbar-fixed-top") || ace.addClass(a, "navbar-fixed-top"), c !== !1 && ace.settings.set("navbar", "fixed")) : (ace.removeClass(a, "navbar-fixed-top"), c !== !1 && ace.settings.unset("navbar", "fixed"));
        try {
            document.getElementById("ace-settings-navbar").checked = b
        } catch (f) { }
        window.jQuery && jQuery(document).trigger("settings.ace", ["navbar_fixed", b, a])
    },
    sidebar_fixed: function (a, b, c, d) {
        if (ace.vars.very_old_ie) return !1;
        var a = a || "#sidebar";
        if ("string" == typeof a && (a = document.querySelector(a)), !a) return !1;
        if (b = b || !1, c = c && !0, !b && d !== !1) {
            var e = null;
            (ace.settings.is("breadcrumbs", "fixed") || (e = document.getElementById("breadcrumbs")) && ace.hasClass(e, "breadcrumbs-fixed")) && ace.settings.breadcrumbs_fixed(e, !1, c)
        }
        if (b && d !== !1 && !ace.settings.is("navbar", "fixed") && ace.settings.navbar_fixed(null, !0, c), b) {
            if (!ace.hasClass(a, "sidebar-fixed")) {
                ace.addClass(a, "sidebar-fixed");
                var f = document.getElementById("menu-toggler");
                f && ace.addClass(f, "fixed")
            }
            c !== !1 && ace.settings.set("sidebar", "fixed")
        } else {
            ace.removeClass(a, "sidebar-fixed");
            var f = document.getElementById("menu-toggler");
            f && ace.removeClass(f, "fixed"), c !== !1 && ace.settings.unset("sidebar", "fixed")
        }
        try {
            document.getElementById("ace-settings-sidebar").checked = b
        } catch (g) { }
        window.jQuery && jQuery(document).trigger("settings.ace", ["sidebar_fixed", b, a])
    },
    breadcrumbs_fixed: function (a, b, c, d) {
        if (ace.vars.very_old_ie) return !1;
        var a = a || "#breadcrumbs";
        if ("string" == typeof a && (a = document.querySelector(a)), !a) return !1;
        b = b || !1, c = c && !0, b && d !== !1 && !ace.settings.is("sidebar", "fixed") && ace.settings.sidebar_fixed(null, !0, c), b ? (ace.hasClass(a, "breadcrumbs-fixed") || ace.addClass(a, "breadcrumbs-fixed"), c !== !1 && ace.settings.set("breadcrumbs", "fixed")) : (ace.removeClass(a, "breadcrumbs-fixed"), c !== !1 && ace.settings.unset("breadcrumbs", "fixed"));
        try {
            document.getElementById("ace-settings-breadcrumbs").checked = b
        } catch (e) { }
        window.jQuery && jQuery(document).trigger("settings.ace", ["breadcrumbs_fixed", b, a])
    },
    main_container_fixed: function (a, b, c) {
        if (ace.vars.very_old_ie) return !1;
        b = b || !1, c = c && !0;
        var a = a || "#main-container";
        if ("string" == typeof a && (a = document.querySelector(a)), !a) return !1;
        var d = document.getElementById("navbar-container");
        b ? (ace.hasClass(a, "container") || ace.addClass(a, "container"), d && !ace.hasClass(d, "container") && ace.addClass(d, "container"), c !== !1 && ace.settings.set("main-container", "fixed")) : (ace.removeClass(a, "container"), d && ace.removeClass(d, "container"), c !== !1 && ace.settings.unset("main-container", "fixed"));
        try {
            document.getElementById("ace-settings-add-container").checked = b
        } catch (e) { }
        if (navigator.userAgent.match(/webkit/i)) {
            var f = document.getElementById("sidebar");
            ace.toggleClass(f, "menu-min"), setTimeout(function () {
                ace.toggleClass(f, "menu-min")
            }, 0)
        }
        window.jQuery && jQuery(document).trigger("settings.ace", ["main_container_fixed", b, a])
    },
    sidebar_collapsed: function (a, b, c) {
        if (ace.vars.very_old_ie) return !1;
        var a = a || "#sidebar";
        if ("string" == typeof a && (a = document.querySelector(a)), !a) return !1;
        if (b = b || !1, b ? (ace.addClass(a, "menu-min"), c !== !1 && ace.settings.set("sidebar", "collapsed")) : (ace.removeClass(a, "menu-min"), c !== !1 && ace.settings.unset("sidebar", "collapsed")), window.jQuery && jQuery(document).trigger("settings.ace", ["sidebar_collapsed", b, a]), !window.jQuery) {
            var d = document.querySelector('.sidebar-collapse[data-target="#' + (a.getAttribute("id") || "") + '"]');
            if (d || (d = a.querySelector(".sidebar-collapse")), !d) return;
            var e, f, g = d.querySelector("[data-icon1][data-icon2]");
            if (!g) return;
            e = g.getAttribute("data-icon1"), f = g.getAttribute("data-icon2"), b ? (ace.removeClass(g, e), ace.addClass(g, f)) : (ace.removeClass(g, f), ace.addClass(g, e))
        }
    }
}, ace.settings.check = function (a, b) {
    if (ace.settings.exists(a, b)) {
        var c = ace.settings.is(a, b),
            d = {
                "navbar-fixed": "navbar-fixed-top",
                "sidebar-fixed": "sidebar-fixed",
                "breadcrumbs-fixed": "breadcrumbs-fixed",
                "sidebar-collapsed": "menu-min",
                "main-container-fixed": "container"
            },
            e = document.getElementById(a);
        c != ace.hasClass(e, d[a + "-" + b]) && ace.settings[a.replace("-", "_") + "_" + b](null, c)
    }
}, ace.data_storage = function (a, b) {
    var c = "ace_",
        d = null,
        e = 0;
    (1 == a || a === b) && "localStorage" in window && null !== window.localStorage ? (d = ace.storage, e = 1) : null == d && (2 == a || a === b) && "cookie" in document && null !== document.cookie && (d = ace.cookie, e = 2), this.set = function (a, b, f, g, h) {
        if (d)
            if (f === h) f = b, b = a, null == f ? d.remove(c + b) : 1 == e ? d.set(c + b, f) : 2 == e && d.set(c + b, f, ace.config.cookie_expiry, g || ace.config.cookie_path);
            else if (1 == e) null == f ? d.remove(c + a + "_" + b) : d.set(c + a + "_" + b, f);
            else if (2 == e) {
                var i = d.get(c + a),
                    j = i ? JSON.parse(i) : {};
                if (null == f) {
                    if (delete j[b], 0 == ace.sizeof(j)) return void d.remove(c + a)
                } else j[b] = f;
                d.set(c + a, JSON.stringify(j), ace.config.cookie_expiry, g || ace.config.cookie_path)
            }
    }, this.get = function (a, b, f) {
        if (!d) return null;
        if (b === f) return b = a, d.get(c + b);
        if (1 == e) return d.get(c + a + "_" + b);
        if (2 == e) {
            var g = d.get(c + a),
                h = g ? JSON.parse(g) : {};
            return b in h ? h[b] : null
        }
    }, this.remove = function (a, b, c) {
        d && (b === c ? (b = a, this.set(b, null)) : this.set(a, b, null))
    }
}, ace.cookie = {
    get: function (a) {
        var b, c, d = document.cookie,
            e = a + "=";
        if (d) {
            if (c = d.indexOf("; " + e), -1 == c) {
                if (c = d.indexOf(e), 0 != c) return null
            } else c += 2;
            return b = d.indexOf(";", c), -1 == b && (b = d.length), decodeURIComponent(d.substring(c + e.length, b))
        }
    },
    set: function (a, b, c, d, e, f) {
        var g = new Date;
        "object" == typeof c && c.toGMTString ? c = c.toGMTString() : parseInt(c, 10) ? (g.setTime(g.getTime() + 1e3 * parseInt(c, 10)), c = g.toGMTString()) : c = "", document.cookie = a + "=" + encodeURIComponent(b) + (c ? "; expires=" + c : "") + (d ? "; path=" + d : "") + (e ? "; domain=" + e : "") + (f ? "; secure" : "")
    },
    remove: function (a, b) {
        this.set(a, "", -1e3, b)
    }
}, ace.storage = {
    get: function (a) {
        return window.localStorage.getItem(a)
    },
    set: function (a, b) {
        window.localStorage.setItem(a, b)
    },
    remove: function (a) {
        window.localStorage.removeItem(a)
    }
}, ace.sizeof = function (a) {
    var b = 0;
    for (var c in a) a.hasOwnProperty(c) && b++;
    return b
}, ace.hasClass = function (a, b) {
    return (" " + a.className + " ").indexOf(" " + b + " ") > -1
}, ace.addClass = function (a, b) {
    if (!ace.hasClass(a, b)) {
        var c = a.className;
        a.className = c + (c.length ? " " : "") + b
    }
}, ace.removeClass = function (a, b) {
    ace.replaceClass(a, b)
}, ace.replaceClass = function (a, b, c) {
    var d = new RegExp("(^|\\s)" + b + "(\\s|$)", "i");
    a.className = a.className.replace(d, function (a, b, d) {
        return c ? b + c + d : " "
    }).replace(/^\s+|\s+$/g, "")
}, ace.toggleClass = function (a, b) {
    ace.hasClass(a, b) ? ace.removeClass(a, b) : ace.addClass(a, b)
}, ace.isHTTMlElement = function (a) {
    return window.HTMLElement ? a instanceof HTMLElement : "nodeType" in a ? 1 == a.nodeType : !1
}, ace.data = new ace.data_storage(ace.config.storage_method);