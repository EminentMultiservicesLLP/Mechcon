!(function () {
    var n = { version: "3.5.17" },
        t = [].slice,
        e = function (n) {
            return t.call(n);
        },
        r = this.document;
    function i(n) {
        return n && (n.ownerDocument || n.document || n).documentElement;
    }
    function u(n) {
        return n && ((n.ownerDocument && n.ownerDocument.defaultView) || (n.document && n) || n.defaultView);
    }
    if (r)
        try {
            e(r.documentElement.childNodes)[0].nodeType;
        } catch (n) {
            e = function (n) {
                for (var t = n.length, e = new Array(t); t--; ) e[t] = n[t];
                return e;
            };
        }
    if (
        (Date.now ||
            (Date.now = function () {
                return +new Date();
            }),
        r)
    )
        try {
            r.createElement("DIV").style.setProperty("opacity", 0, "");
        } catch (n) {
            var o = this.Element.prototype,
                a = o.setAttribute,
                l = o.setAttributeNS,
                c = this.CSSStyleDeclaration.prototype,
                f = c.setProperty;
            (o.setAttribute = function (n, t) {
                a.call(this, n, t + "");
            }),
                (o.setAttributeNS = function (n, t, e) {
                    l.call(this, n, t, e + "");
                }),
                (c.setProperty = function (n, t, e) {
                    f.call(this, n, t + "", e);
                });
        }
    function s(n, t) {
        return n < t ? -1 : n > t ? 1 : n >= t ? 0 : NaN;
    }
    function h(n) {
        return null === n ? NaN : +n;
    }
    function p(n) {
        return !isNaN(n);
    }
    function g(n) {
        return {
            left: function (t, e, r, i) {
                for (arguments.length < 3 && (r = 0), arguments.length < 4 && (i = t.length); r < i; ) {
                    var u = (r + i) >>> 1;
                    n(t[u], e) < 0 ? (r = u + 1) : (i = u);
                }
                return r;
            },
            right: function (t, e, r, i) {
                for (arguments.length < 3 && (r = 0), arguments.length < 4 && (i = t.length); r < i; ) {
                    var u = (r + i) >>> 1;
                    n(t[u], e) > 0 ? (i = u) : (r = u + 1);
                }
                return r;
            },
        };
    }
    (n.ascending = s),
        (n.descending = function (n, t) {
            return t < n ? -1 : t > n ? 1 : t >= n ? 0 : NaN;
        }),
        (n.min = function (n, t) {
            var e,
                r,
                i = -1,
                u = n.length;
            if (1 === arguments.length) {
                for (; ++i < u; )
                    if (null != (r = n[i]) && r >= r) {
                        e = r;
                        break;
                    }
                for (; ++i < u; ) null != (r = n[i]) && e > r && (e = r);
            } else {
                for (; ++i < u; )
                    if (null != (r = t.call(n, n[i], i)) && r >= r) {
                        e = r;
                        break;
                    }
                for (; ++i < u; ) null != (r = t.call(n, n[i], i)) && e > r && (e = r);
            }
            return e;
        }),
        (n.max = function (n, t) {
            var e,
                r,
                i = -1,
                u = n.length;
            if (1 === arguments.length) {
                for (; ++i < u; )
                    if (null != (r = n[i]) && r >= r) {
                        e = r;
                        break;
                    }
                for (; ++i < u; ) null != (r = n[i]) && r > e && (e = r);
            } else {
                for (; ++i < u; )
                    if (null != (r = t.call(n, n[i], i)) && r >= r) {
                        e = r;
                        break;
                    }
                for (; ++i < u; ) null != (r = t.call(n, n[i], i)) && r > e && (e = r);
            }
            return e;
        }),
        (n.extent = function (n, t) {
            var e,
                r,
                i,
                u = -1,
                o = n.length;
            if (1 === arguments.length) {
                for (; ++u < o; )
                    if (null != (r = n[u]) && r >= r) {
                        e = i = r;
                        break;
                    }
                for (; ++u < o; ) null != (r = n[u]) && (e > r && (e = r), i < r && (i = r));
            } else {
                for (; ++u < o; )
                    if (null != (r = t.call(n, n[u], u)) && r >= r) {
                        e = i = r;
                        break;
                    }
                for (; ++u < o; ) null != (r = t.call(n, n[u], u)) && (e > r && (e = r), i < r && (i = r));
            }
            return [e, i];
        }),
        (n.sum = function (n, t) {
            var e,
                r = 0,
                i = n.length,
                u = -1;
            if (1 === arguments.length) for (; ++u < i; ) p((e = +n[u])) && (r += e);
            else for (; ++u < i; ) p((e = +t.call(n, n[u], u))) && (r += e);
            return r;
        }),
        (n.mean = function (n, t) {
            var e,
                r = 0,
                i = n.length,
                u = -1,
                o = i;
            if (1 === arguments.length) for (; ++u < i; ) p((e = h(n[u]))) ? (r += e) : --o;
            else for (; ++u < i; ) p((e = h(t.call(n, n[u], u)))) ? (r += e) : --o;
            if (o) return r / o;
        }),
        (n.quantile = function (n, t) {
            var e = (n.length - 1) * t + 1,
                r = Math.floor(e),
                i = +n[r - 1],
                u = e - r;
            return u ? i + u * (n[r] - i) : i;
        }),
        (n.median = function (t, e) {
            var r,
                i = [],
                u = t.length,
                o = -1;
            if (1 === arguments.length) for (; ++o < u; ) p((r = h(t[o]))) && i.push(r);
            else for (; ++o < u; ) p((r = h(e.call(t, t[o], o)))) && i.push(r);
            if (i.length) return n.quantile(i.sort(s), 0.5);
        }),
        (n.variance = function (n, t) {
            var e,
                r,
                i = n.length,
                u = 0,
                o = 0,
                a = -1,
                l = 0;
            if (1 === arguments.length) for (; ++a < i; ) p((e = h(n[a]))) && (o += (r = e - u) * (e - (u += r / ++l)));
            else for (; ++a < i; ) p((e = h(t.call(n, n[a], a)))) && (o += (r = e - u) * (e - (u += r / ++l)));
            if (l > 1) return o / (l - 1);
        }),
        (n.deviation = function () {
            var t = n.variance.apply(this, arguments);
            return t ? Math.sqrt(t) : t;
        });
    var v = g(s);
    function d(n) {
        return n.length;
    }
    (n.bisectLeft = v.left),
        (n.bisect = n.bisectRight = v.right),
        (n.bisector = function (n) {
            return g(
                1 === n.length
                    ? function (t, e) {
                          return s(n(t), e);
                      }
                    : n
            );
        }),
        (n.shuffle = function (n, t, e) {
            (u = arguments.length) < 3 && ((e = n.length), u < 2 && (t = 0));
            for (var r, i, u = e - t; u; ) (i = (Math.random() * u--) | 0), (r = n[u + t]), (n[u + t] = n[i + t]), (n[i + t] = r);
            return n;
        }),
        (n.permute = function (n, t) {
            for (var e = t.length, r = new Array(e); e--; ) r[e] = n[t[e]];
            return r;
        }),
        (n.pairs = function (n) {
            for (var t = 0, e = n.length - 1, r = n[0], i = new Array(e < 0 ? 0 : e); t < e; ) i[t] = [r, (r = n[++t])];
            return i;
        }),
        (n.transpose = function (t) {
            if (!(u = t.length)) return [];
            for (var e = -1, r = n.min(t, d), i = new Array(r); ++e < r; ) for (var u, o = -1, a = (i[e] = new Array(u)); ++o < u; ) a[o] = t[o][e];
            return i;
        }),
        (n.zip = function () {
            return n.transpose(arguments);
        }),
        (n.keys = function (n) {
            var t = [];
            for (var e in n) t.push(e);
            return t;
        }),
        (n.values = function (n) {
            var t = [];
            for (var e in n) t.push(n[e]);
            return t;
        }),
        (n.entries = function (n) {
            var t = [];
            for (var e in n) t.push({ key: e, value: n[e] });
            return t;
        }),
        (n.merge = function (n) {
            for (var t, e, r, i = n.length, u = -1, o = 0; ++u < i; ) o += n[u].length;
            for (e = new Array(o); --i >= 0; ) for (t = (r = n[i]).length; --t >= 0; ) e[--o] = r[t];
            return e;
        });
    var y = Math.abs;
    function m(n, t) {
        for (var e in t) Object.defineProperty(n.prototype, e, { value: t[e], enumerable: !1 });
    }
    function M() {
        this._ = Object.create(null);
    }
    (n.range = function (n, t, e) {
        if ((arguments.length < 3 && ((e = 1), arguments.length < 2 && ((t = n), (n = 0))), (t - n) / e == 1 / 0)) throw new Error("infinite range");
        var r,
            i = [],
            u = (function (n) {
                var t = 1;
                for (; (n * t) % 1; ) t *= 10;
                return t;
            })(y(e)),
            o = -1;
        if (((n *= u), (t *= u), (e *= u) < 0)) for (; (r = n + e * ++o) > t; ) i.push(r / u);
        else for (; (r = n + e * ++o) < t; ) i.push(r / u);
        return i;
    }),
        (n.map = function (n, t) {
            var e = new M();
            if (n instanceof M)
                n.forEach(function (n, t) {
                    e.set(n, t);
                });
            else if (Array.isArray(n)) {
                var r,
                    i = -1,
                    u = n.length;
                if (1 === arguments.length) for (; ++i < u; ) e.set(i, n[i]);
                else for (; ++i < u; ) e.set(t.call(n, (r = n[i]), i), r);
            } else for (var o in n) e.set(o, n[o]);
            return e;
        });
    var x = "__proto__",
        b = "\0";
    function _(n) {
        return (n += "") === x || n[0] === b ? b + n : n;
    }
    function w(n) {
        return (n += "")[0] === b ? n.slice(1) : n;
    }
    function S(n) {
        return _(n) in this._;
    }
    function k(n) {
        return (n = _(n)) in this._ && delete this._[n];
    }
    function N() {
        var n = [];
        for (var t in this._) n.push(w(t));
        return n;
    }
    function E() {
        var n = 0;
        for (var t in this._) ++n;
        return n;
    }
    function A() {
        for (var n in this._) return !1;
        return !0;
    }
    function C() {
        this._ = Object.create(null);
    }
    function z(n) {
        return n;
    }
    function L(n, t, e) {
        return function () {
            var r = e.apply(t, arguments);
            return r === t ? n : r;
        };
    }
    function q(n, t) {
        if (t in n) return t;
        t = t.charAt(0).toUpperCase() + t.slice(1);
        for (var e = 0, r = T.length; e < r; ++e) {
            var i = T[e] + t;
            if (i in n) return i;
        }
    }
    m(M, {
        has: S,
        get: function (n) {
            return this._[_(n)];
        },
        set: function (n, t) {
            return (this._[_(n)] = t);
        },
        remove: k,
        keys: N,
        values: function () {
            var n = [];
            for (var t in this._) n.push(this._[t]);
            return n;
        },
        entries: function () {
            var n = [];
            for (var t in this._) n.push({ key: w(t), value: this._[t] });
            return n;
        },
        size: E,
        empty: A,
        forEach: function (n) {
            for (var t in this._) n.call(this, w(t), this._[t]);
        },
    }),
        (n.nest = function () {
            var t,
                e,
                r = {},
                i = [],
                u = [];
            function o(n, u, a) {
                if (a >= i.length) return e ? e.call(r, u) : t ? u.sort(t) : u;
                for (var l, c, f, s, h = -1, p = u.length, g = i[a++], v = new M(); ++h < p; ) (s = v.get((l = g((c = u[h]))))) ? s.push(c) : v.set(l, [c]);
                return (
                    n
                        ? ((c = n()),
                          (f = function (t, e) {
                              c.set(t, o(n, e, a));
                          }))
                        : ((c = {}),
                          (f = function (t, e) {
                              c[t] = o(n, e, a);
                          })),
                    v.forEach(f),
                    c
                );
            }
            return (
                (r.map = function (n, t) {
                    return o(t, n, 0);
                }),
                (r.entries = function (t) {
                    return (function n(t, e) {
                        if (e >= i.length) return t;
                        var r = [],
                            o = u[e++];
                        return (
                            t.forEach(function (t, i) {
                                r.push({ key: t, values: n(i, e) });
                            }),
                            o
                                ? r.sort(function (n, t) {
                                      return o(n.key, t.key);
                                  })
                                : r
                        );
                    })(o(n.map, t, 0), 0);
                }),
                (r.key = function (n) {
                    return i.push(n), r;
                }),
                (r.sortKeys = function (n) {
                    return (u[i.length - 1] = n), r;
                }),
                (r.sortValues = function (n) {
                    return (t = n), r;
                }),
                (r.rollup = function (n) {
                    return (e = n), r;
                }),
                r
            );
        }),
        (n.set = function (n) {
            var t = new C();
            if (n) for (var e = 0, r = n.length; e < r; ++e) t.add(n[e]);
            return t;
        }),
        m(C, {
            has: S,
            add: function (n) {
                return (this._[_((n += ""))] = !0), n;
            },
            remove: k,
            values: N,
            size: E,
            empty: A,
            forEach: function (n) {
                for (var t in this._) n.call(this, w(t));
            },
        }),
        (n.behavior = {}),
        (n.rebind = function (n, t) {
            for (var e, r = 1, i = arguments.length; ++r < i; ) n[(e = arguments[r])] = L(n, t, t[e]);
            return n;
        });
    var T = ["webkit", "ms", "moz", "Moz", "o", "O"];
    function R() {}
    function D() {}
    function P(n) {
        var t = [],
            e = new M();
        function r() {
            for (var e, r = t, i = -1, u = r.length; ++i < u; ) (e = r[i].on) && e.apply(this, arguments);
            return n;
        }
        return (
            (r.on = function (r, i) {
                var u,
                    o = e.get(r);
                return arguments.length < 2 ? o && o.on : (o && ((o.on = null), (t = t.slice(0, (u = t.indexOf(o))).concat(t.slice(u + 1))), e.remove(r)), i && t.push(e.set(r, { on: i })), n);
            }),
            r
        );
    }
    function U() {
        n.event.preventDefault();
    }
    function j() {
        for (var t, e = n.event; (t = e.sourceEvent); ) e = t;
        return e;
    }
    function F(t) {
        for (var e = new D(), r = 0, i = arguments.length; ++r < i; ) e[arguments[r]] = P(e);
        return (
            (e.of = function (r, i) {
                return function (u) {
                    try {
                        var o = (u.sourceEvent = n.event);
                        (u.target = t), (n.event = u), e[u.type].apply(r, i);
                    } finally {
                        n.event = o;
                    }
                };
            }),
            e
        );
    }
    (n.dispatch = function () {
        for (var n = new D(), t = -1, e = arguments.length; ++t < e; ) n[arguments[t]] = P(n);
        return n;
    }),
        (D.prototype.on = function (n, t) {
            var e = n.indexOf("."),
                r = "";
            if ((e >= 0 && ((r = n.slice(e + 1)), (n = n.slice(0, e))), n)) return arguments.length < 2 ? this[n].on(r) : this[n].on(r, t);
            if (2 === arguments.length) {
                if (null == t) for (n in this) this.hasOwnProperty(n) && this[n].on(r, null);
                return this;
            }
        }),
        (n.event = null),
        (n.requote = function (n) {
            return n.replace(H, "\\$&");
        });
    var H = /[\\\^\$\*\+\?\|\[\]\(\)\.\{\}]/g,
        O = {}.__proto__
            ? function (n, t) {
                  n.__proto__ = t;
              }
            : function (n, t) {
                  for (var e in t) n[e] = t[e];
              };
    function I(n) {
        return O(n, X), n;
    }
    var Y = function (n, t) {
            return t.querySelector(n);
        },
        Z = function (n, t) {
            return t.querySelectorAll(n);
        },
        V = function (n, t) {
            var e = n.matches || n[q(n, "matchesSelector")];
            return (V = function (n, t) {
                return e.call(n, t);
            })(n, t);
        };
    "function" == typeof Sizzle &&
        ((Y = function (n, t) {
            return Sizzle(n, t)[0] || null;
        }),
        (Z = Sizzle),
        (V = Sizzle.matchesSelector)),
        (n.selection = function () {
            return n.select(r.documentElement);
        });
    var X = (n.selection.prototype = []);
    function $(n) {
        return "function" == typeof n
            ? n
            : function () {
                  return Y(n, this);
              };
    }
    function B(n) {
        return "function" == typeof n
            ? n
            : function () {
                  return Z(n, this);
              };
    }
    (X.select = function (n) {
        var t,
            e,
            r,
            i,
            u = [];
        n = $(n);
        for (var o = -1, a = this.length; ++o < a; ) {
            u.push((t = [])), (t.parentNode = (r = this[o]).parentNode);
            for (var l = -1, c = r.length; ++l < c; ) (i = r[l]) ? (t.push((e = n.call(i, i.__data__, l, o))), e && "__data__" in i && (e.__data__ = i.__data__)) : t.push(null);
        }
        return I(u);
    }),
        (X.selectAll = function (n) {
            var t,
                r,
                i = [];
            n = B(n);
            for (var u = -1, o = this.length; ++u < o; ) for (var a = this[u], l = -1, c = a.length; ++l < c; ) (r = a[l]) && (i.push((t = e(n.call(r, r.__data__, l, u)))), (t.parentNode = r));
            return I(i);
        });
    var W = "http://www.w3.org/1999/xhtml",
        J = { svg: "http://www.w3.org/2000/svg", xhtml: W, xlink: "http://www.w3.org/1999/xlink", xml: "http://www.w3.org/XML/1998/namespace", xmlns: "http://www.w3.org/2000/xmlns/" };
    function G(t, e) {
        return (
            (t = n.ns.qualify(t)),
            null == e
                ? t.local
                    ? function () {
                          this.removeAttributeNS(t.space, t.local);
                      }
                    : function () {
                          this.removeAttribute(t);
                      }
                : "function" == typeof e
                ? t.local
                    ? function () {
                          var n = e.apply(this, arguments);
                          null == n ? this.removeAttributeNS(t.space, t.local) : this.setAttributeNS(t.space, t.local, n);
                      }
                    : function () {
                          var n = e.apply(this, arguments);
                          null == n ? this.removeAttribute(t) : this.setAttribute(t, n);
                      }
                : t.local
                ? function () {
                      this.setAttributeNS(t.space, t.local, e);
                  }
                : function () {
                      this.setAttribute(t, e);
                  }
        );
    }
    function K(n) {
        return n.trim().replace(/\s+/g, " ");
    }
    function Q(t) {
        return new RegExp("(?:^|\\s+)" + n.requote(t) + "(?:\\s+|$)", "g");
    }
    function nn(n) {
        return (n + "").trim().split(/^|\s+/);
    }
    function tn(n, t) {
        var e = (n = nn(n).map(en)).length;
        return "function" == typeof t
            ? function () {
                  for (var r = -1, i = t.apply(this, arguments); ++r < e; ) n[r](this, i);
              }
            : function () {
                  for (var r = -1; ++r < e; ) n[r](this, t);
              };
    }
    function en(n) {
        var t = Q(n);
        return function (e, r) {
            if ((i = e.classList)) return r ? i.add(n) : i.remove(n);
            var i = e.getAttribute("class") || "";
            r ? ((t.lastIndex = 0), t.test(i) || e.setAttribute("class", K(i + " " + n))) : e.setAttribute("class", K(i.replace(t, " ")));
        };
    }
    function rn(n, t, e) {
        return null == t
            ? function () {
                  this.style.removeProperty(n);
              }
            : "function" == typeof t
            ? function () {
                  var r = t.apply(this, arguments);
                  null == r ? this.style.removeProperty(n) : this.style.setProperty(n, r, e);
              }
            : function () {
                  this.style.setProperty(n, t, e);
              };
    }
    function un(n, t) {
        return null == t
            ? function () {
                  delete this[n];
              }
            : "function" == typeof t
            ? function () {
                  var e = t.apply(this, arguments);
                  null == e ? delete this[n] : (this[n] = e);
              }
            : function () {
                  this[n] = t;
              };
    }
    function on(t) {
        return "function" == typeof t
            ? t
            : (t = n.ns.qualify(t)).local
            ? function () {
                  return this.ownerDocument.createElementNS(t.space, t.local);
              }
            : function () {
                  var n = this.ownerDocument,
                      e = this.namespaceURI;
                  return e === W && n.documentElement.namespaceURI === W ? n.createElement(t) : n.createElementNS(e, t);
              };
    }
    function an() {
        var n = this.parentNode;
        n && n.removeChild(this);
    }
    function ln(n) {
        return { __data__: n };
    }
    function cn(n) {
        return function () {
            return V(this, n);
        };
    }
    function fn(n, t) {
        for (var e = 0, r = n.length; e < r; e++) for (var i, u = n[e], o = 0, a = u.length; o < a; o++) (i = u[o]) && t(i, o, e);
        return n;
    }
    function sn(n) {
        return O(n, hn), n;
    }
    (n.ns = {
        prefix: J,
        qualify: function (n) {
            var t = n.indexOf(":"),
                e = n;
            return t >= 0 && "xmlns" !== (e = n.slice(0, t)) && (n = n.slice(t + 1)), J.hasOwnProperty(e) ? { space: J[e], local: n } : n;
        },
    }),
        (X.attr = function (t, e) {
            if (arguments.length < 2) {
                if ("string" == typeof t) {
                    var r = this.node();
                    return (t = n.ns.qualify(t)).local ? r.getAttributeNS(t.space, t.local) : r.getAttribute(t);
                }
                for (e in t) this.each(G(e, t[e]));
                return this;
            }
            return this.each(G(t, e));
        }),
        (X.classed = function (n, t) {
            if (arguments.length < 2) {
                if ("string" == typeof n) {
                    var e = this.node(),
                        r = (n = nn(n)).length,
                        i = -1;
                    if ((t = e.classList)) {
                        for (; ++i < r; ) if (!t.contains(n[i])) return !1;
                    } else for (t = e.getAttribute("class"); ++i < r; ) if (!Q(n[i]).test(t)) return !1;
                    return !0;
                }
                for (t in n) this.each(tn(t, n[t]));
                return this;
            }
            return this.each(tn(n, t));
        }),
        (X.style = function (n, t, e) {
            var r = arguments.length;
            if (r < 3) {
                if ("string" != typeof n) {
                    for (e in (r < 2 && (t = ""), n)) this.each(rn(e, n[e], t));
                    return this;
                }
                if (r < 2) {
                    var i = this.node();
                    return u(i).getComputedStyle(i, null).getPropertyValue(n);
                }
                e = "";
            }
            return this.each(rn(n, t, e));
        }),
        (X.property = function (n, t) {
            if (arguments.length < 2) {
                if ("string" == typeof n) return this.node()[n];
                for (t in n) this.each(un(t, n[t]));
                return this;
            }
            return this.each(un(n, t));
        }),
        (X.text = function (n) {
            return arguments.length
                ? this.each(
                      "function" == typeof n
                          ? function () {
                                var t = n.apply(this, arguments);
                                this.textContent = null == t ? "" : t;
                            }
                          : null == n
                          ? function () {
                                this.textContent = "";
                            }
                          : function () {
                                this.textContent = n;
                            }
                  )
                : this.node().textContent;
        }),
        (X.html = function (n) {
            return arguments.length
                ? this.each(
                      "function" == typeof n
                          ? function () {
                                var t = n.apply(this, arguments);
                                this.innerHTML = null == t ? "" : t;
                            }
                          : null == n
                          ? function () {
                                this.innerHTML = "";
                            }
                          : function () {
                                this.innerHTML = n;
                            }
                  )
                : this.node().innerHTML;
        }),
        (X.append = function (n) {
            return (
                (n = on(n)),
                this.select(function () {
                    return this.appendChild(n.apply(this, arguments));
                })
            );
        }),
        (X.insert = function (n, t) {
            return (
                (n = on(n)),
                (t = $(t)),
                this.select(function () {
                    return this.insertBefore(n.apply(this, arguments), t.apply(this, arguments) || null);
                })
            );
        }),
        (X.remove = function () {
            return this.each(an);
        }),
        (X.data = function (n, t) {
            var e,
                r,
                i = -1,
                u = this.length;
            if (!arguments.length) {
                for (n = new Array((u = (e = this[0]).length)); ++i < u; ) (r = e[i]) && (n[i] = r.__data__);
                return n;
            }
            function o(n, e) {
                var r,
                    i,
                    u,
                    o = n.length,
                    f = e.length,
                    s = Math.min(o, f),
                    h = new Array(f),
                    p = new Array(f),
                    g = new Array(o);
                if (t) {
                    var v,
                        d = new M(),
                        y = new Array(o);
                    for (r = -1; ++r < o; ) (i = n[r]) && (d.has((v = t.call(i, i.__data__, r))) ? (g[r] = i) : d.set(v, i), (y[r] = v));
                    for (r = -1; ++r < f; ) (i = d.get((v = t.call(e, (u = e[r]), r)))) ? !0 !== i && ((h[r] = i), (i.__data__ = u)) : (p[r] = ln(u)), d.set(v, !0);
                    for (r = -1; ++r < o; ) r in y && !0 !== d.get(y[r]) && (g[r] = n[r]);
                } else {
                    for (r = -1; ++r < s; ) (i = n[r]), (u = e[r]), i ? ((i.__data__ = u), (h[r] = i)) : (p[r] = ln(u));
                    for (; r < f; ++r) p[r] = ln(e[r]);
                    for (; r < o; ++r) g[r] = n[r];
                }
                (p.update = h), (p.parentNode = h.parentNode = g.parentNode = n.parentNode), a.push(p), l.push(h), c.push(g);
            }
            var a = sn([]),
                l = I([]),
                c = I([]);
            if ("function" == typeof n) for (; ++i < u; ) o((e = this[i]), n.call(e, e.parentNode.__data__, i));
            else for (; ++i < u; ) o((e = this[i]), n);
            return (
                (l.enter = function () {
                    return a;
                }),
                (l.exit = function () {
                    return c;
                }),
                l
            );
        }),
        (X.datum = function (n) {
            return arguments.length ? this.property("__data__", n) : this.property("__data__");
        }),
        (X.filter = function (n) {
            var t,
                e,
                r,
                i = [];
            "function" != typeof n && (n = cn(n));
            for (var u = 0, o = this.length; u < o; u++) {
                i.push((t = [])), (t.parentNode = (e = this[u]).parentNode);
                for (var a = 0, l = e.length; a < l; a++) (r = e[a]) && n.call(r, r.__data__, a, u) && t.push(r);
            }
            return I(i);
        }),
        (X.order = function () {
            for (var n = -1, t = this.length; ++n < t; ) for (var e, r = this[n], i = r.length - 1, u = r[i]; --i >= 0; ) (e = r[i]) && (u && u !== e.nextSibling && u.parentNode.insertBefore(e, u), (u = e));
            return this;
        }),
        (X.sort = function (n) {
            n = function (n) {
                arguments.length || (n = s);
                return function (t, e) {
                    return t && e ? n(t.__data__, e.__data__) : !t - !e;
                };
            }.apply(this, arguments);
            for (var t = -1, e = this.length; ++t < e; ) this[t].sort(n);
            return this.order();
        }),
        (X.each = function (n) {
            return fn(this, function (t, e, r) {
                n.call(t, t.__data__, e, r);
            });
        }),
        (X.call = function (n) {
            var t = e(arguments);
            return n.apply((t[0] = this), t), this;
        }),
        (X.empty = function () {
            return !this.node();
        }),
        (X.node = function () {
            for (var n = 0, t = this.length; n < t; n++)
                for (var e = this[n], r = 0, i = e.length; r < i; r++) {
                    var u = e[r];
                    if (u) return u;
                }
            return null;
        }),
        (X.size = function () {
            var n = 0;
            return (
                fn(this, function () {
                    ++n;
                }),
                n
            );
        });
    var hn = [];
    function pn(t, r, i) {
        var u = "__on" + t,
            o = t.indexOf("."),
            a = vn;
        o > 0 && (t = t.slice(0, o));
        var l = gn.get(t);
        function c() {
            var n = this[u];
            n && (this.removeEventListener(t, n, n.$), delete this[u]);
        }
        return (
            l && ((t = l), (a = dn)),
            o
                ? r
                    ? function () {
                          var n = a(r, e(arguments));
                          c.call(this), this.addEventListener(t, (this[u] = n), (n.$ = i)), (n._ = r);
                      }
                    : c
                : r
                ? R
                : function () {
                      var e,
                          r = new RegExp("^__on([^.]+)" + n.requote(t) + "$");
                      for (var i in this)
                          if ((e = i.match(r))) {
                              var u = this[i];
                              this.removeEventListener(e[1], u, u.$), delete this[i];
                          }
                  }
        );
    }
    (n.selection.enter = sn),
        (n.selection.enter.prototype = hn),
        (hn.append = X.append),
        (hn.empty = X.empty),
        (hn.node = X.node),
        (hn.call = X.call),
        (hn.size = X.size),
        (hn.select = function (n) {
            for (var t, e, r, i, u, o = [], a = -1, l = this.length; ++a < l; ) {
                (r = (i = this[a]).update), o.push((t = [])), (t.parentNode = i.parentNode);
                for (var c = -1, f = i.length; ++c < f; ) (u = i[c]) ? (t.push((r[c] = e = n.call(i.parentNode, u.__data__, c, a))), (e.__data__ = u.__data__)) : t.push(null);
            }
            return I(o);
        }),
        (hn.insert = function (n, t) {
            var e, r, i;
            return (
                arguments.length < 2 &&
                    ((e = this),
                    (t = function (n, t, u) {
                        var o,
                            a = e[u].update,
                            l = a.length;
                        for (u != i && ((i = u), (r = 0)), t >= r && (r = t + 1); !(o = a[r]) && ++r < l; );
                        return o;
                    })),
                X.insert.call(this, n, t)
            );
        }),
        (n.select = function (n) {
            var t;
            return "string" == typeof n ? ((t = [Y(n, r)]).parentNode = r.documentElement) : ((t = [n]).parentNode = i(n)), I([t]);
        }),
        (n.selectAll = function (n) {
            var t;
            return "string" == typeof n ? ((t = e(Z(n, r))).parentNode = r.documentElement) : ((t = e(n)).parentNode = null), I([t]);
        }),
        (X.on = function (n, t, e) {
            var r = arguments.length;
            if (r < 3) {
                if ("string" != typeof n) {
                    for (e in (r < 2 && (t = !1), n)) this.each(pn(e, n[e], t));
                    return this;
                }
                if (r < 2) return (r = this.node()["__on" + n]) && r._;
                e = !1;
            }
            return this.each(pn(n, t, e));
        });
    var gn = n.map({ mouseenter: "mouseover", mouseleave: "mouseout" });
    function vn(t, e) {
        return function (r) {
            var i = n.event;
            (n.event = r), (e[0] = this.__data__);
            try {
                t.apply(this, e);
            } finally {
                n.event = i;
            }
        };
    }
    function dn(n, t) {
        var e = vn(n, t);
        return function (n) {
            var t = n.relatedTarget;
            (t && (t === this || 8 & t.compareDocumentPosition(this))) || e.call(this, n);
        };
    }
    r &&
        gn.forEach(function (n) {
            "on" + n in r && gn.remove(n);
        });
    var yn,
        mn = 0;
    function Mn(t) {
        var e = ".dragsuppress-" + ++mn,
            r = "click" + e,
            o = n
                .select(u(t))
                .on("touchmove" + e, U)
                .on("dragstart" + e, U)
                .on("selectstart" + e, U);
        if ((null == yn && (yn = !("onselectstart" in t) && q(t.style, "userSelect")), yn)) {
            var a = i(t).style,
                l = a[yn];
            a[yn] = "none";
        }
        return function (n) {
            if ((o.on(e, null), yn && (a[yn] = l), n)) {
                var t = function () {
                    o.on(r, null);
                };
                o.on(
                    r,
                    function () {
                        U(), t();
                    },
                    !0
                ),
                    setTimeout(t, 0);
            }
        };
    }
    n.mouse = function (n) {
        return bn(n, j());
    };
    var xn = this.navigator && /WebKit/.test(this.navigator.userAgent) ? -1 : 0;
    function bn(t, e) {
        e.changedTouches && (e = e.changedTouches[0]);
        var r = t.ownerSVGElement || t;
        if (r.createSVGPoint) {
            var i = r.createSVGPoint();
            if (xn < 0) {
                var o = u(t);
                if (o.scrollX || o.scrollY) {
                    var a = (r = n.select("body").append("svg").style({ position: "absolute", top: 0, left: 0, margin: 0, padding: 0, border: "none" }, "important"))[0][0].getScreenCTM();
                    (xn = !(a.f || a.e)), r.remove();
                }
            }
            return xn ? ((i.x = e.pageX), (i.y = e.pageY)) : ((i.x = e.clientX), (i.y = e.clientY)), [(i = i.matrixTransform(t.getScreenCTM().inverse())).x, i.y];
        }
        var l = t.getBoundingClientRect();
        return [e.clientX - l.left - t.clientLeft, e.clientY - l.top - t.clientTop];
    }
    function _n() {
        return n.event.changedTouches[0].identifier;
    }
    (n.touch = function (n, t, e) {
        if ((arguments.length < 3 && ((e = t), (t = j().changedTouches)), t)) for (var r, i = 0, u = t.length; i < u; ++i) if ((r = t[i]).identifier === e) return bn(n, r);
    }),
        (n.behavior.drag = function () {
            var t = F(o, "drag", "dragstart", "dragend"),
                e = null,
                r = a(R, n.mouse, u, "mousemove", "mouseup"),
                i = a(_n, n.touch, z, "touchmove", "touchend");
            function o() {
                this.on("mousedown.drag", r).on("touchstart.drag", i);
            }
            function a(r, i, u, o, a) {
                return function () {
                    var l,
                        c = n.event.target.correspondingElement || n.event.target,
                        f = this.parentNode,
                        s = t.of(this, arguments),
                        h = 0,
                        p = r(),
                        g = ".drag" + (null == p ? "" : "-" + p),
                        v = n
                            .select(u(c))
                            .on(o + g, function () {
                                var n,
                                    t,
                                    e = i(f, p);
                                if (!e) return;
                                (n = e[0] - y[0]), (t = e[1] - y[1]), (h |= n | t), (y = e), s({ type: "drag", x: e[0] + l[0], y: e[1] + l[1], dx: n, dy: t });
                            })
                            .on(a + g, function () {
                                if (!i(f, p)) return;
                                v.on(o + g, null).on(a + g, null), d(h), s({ type: "dragend" });
                            }),
                        d = Mn(c),
                        y = i(f, p);
                    (l = e ? [(l = e.apply(this, arguments)).x - y[0], l.y - y[1]] : [0, 0]), s({ type: "dragstart" });
                };
            }
            return (
                (o.origin = function (n) {
                    return arguments.length ? ((e = n), o) : e;
                }),
                n.rebind(o, t, "on")
            );
        }),
        (n.touches = function (n, t) {
            return (
                arguments.length < 2 && (t = j().touches),
                t
                    ? e(t).map(function (t) {
                          var e = bn(n, t);
                          return (e.identifier = t.identifier), e;
                      })
                    : []
            );
        });
    var wn = 1e-6,
        Sn = wn * wn,
        kn = Math.PI,
        Nn = 2 * kn,
        En = Nn - wn,
        An = kn / 2,
        Cn = kn / 180,
        zn = 180 / kn;
    function Ln(n) {
        return n > 0 ? 1 : n < 0 ? -1 : 0;
    }
    function qn(n, t, e) {
        return (t[0] - n[0]) * (e[1] - n[1]) - (t[1] - n[1]) * (e[0] - n[0]);
    }
    function Tn(n) {
        return n > 1 ? 0 : n < -1 ? kn : Math.acos(n);
    }
    function Rn(n) {
        return n > 1 ? An : n < -1 ? -An : Math.asin(n);
    }
    function Dn(n) {
        return ((n = Math.exp(n)) + 1 / n) / 2;
    }
    function Pn(n) {
        return (n = Math.sin(n / 2)) * n;
    }
    var Un = Math.SQRT2;
    (n.interpolateZoom = function (n, t) {
        var e,
            r,
            i = n[0],
            u = n[1],
            o = n[2],
            a = t[0],
            l = t[1],
            c = t[2],
            f = a - i,
            s = l - u,
            h = f * f + s * s;
        if (h < Sn)
            (r = Math.log(c / o) / Un),
                (e = function (n) {
                    return [i + n * f, u + n * s, o * Math.exp(Un * n * r)];
                });
        else {
            var p = Math.sqrt(h),
                g = (c * c - o * o + 4 * h) / (2 * o * 2 * p),
                v = (c * c - o * o - 4 * h) / (2 * c * 2 * p),
                d = Math.log(Math.sqrt(g * g + 1) - g),
                y = Math.log(Math.sqrt(v * v + 1) - v);
            (r = (y - d) / Un),
                (e = function (n) {
                    var t,
                        e = n * r,
                        a = Dn(d),
                        l =
                            (o / (2 * p)) *
                            (a * ((t = Un * e + d), ((t = Math.exp(2 * t)) - 1) / (t + 1)) -
                                (function (n) {
                                    return ((n = Math.exp(n)) - 1 / n) / 2;
                                })(d));
                    return [i + l * f, u + l * s, (o * a) / Dn(Un * e + d)];
                });
        }
        return (e.duration = 1e3 * r), e;
    }),
        (n.behavior.zoom = function () {
            var t,
                e,
                i,
                o,
                a,
                l,
                c,
                f,
                s,
                h = { x: 0, y: 0, k: 1 },
                p = [960, 500],
                g = Hn,
                v = 250,
                d = 0,
                y = "mousedown.zoom",
                m = "mousemove.zoom",
                M = "mouseup.zoom",
                x = "touchstart.zoom",
                b = F(_, "zoomstart", "zoom", "zoomend");
            function _(n) {
                n.on(y, L)
                    .on(Fn + ".zoom", T)
                    .on("dblclick.zoom", R)
                    .on(x, q);
            }
            function w(n) {
                return [(n[0] - h.x) / h.k, (n[1] - h.y) / h.k];
            }
            function S(n) {
                h.k = Math.max(g[0], Math.min(g[1], n));
            }
            function k(n, t) {
                (t = (function (n) {
                    return [n[0] * h.k + h.x, n[1] * h.k + h.y];
                })(t)),
                    (h.x += n[0] - t[0]),
                    (h.y += n[1] - t[1]);
            }
            function N(t, r, i, u) {
                (t.__chart__ = { x: h.x, y: h.y, k: h.k }), S(Math.pow(2, u)), k((e = r), i), (t = n.select(t)), v > 0 && (t = t.transition().duration(v)), t.call(_.event);
            }
            function E() {
                c &&
                    c.domain(
                        l
                            .range()
                            .map(function (n) {
                                return (n - h.x) / h.k;
                            })
                            .map(l.invert)
                    ),
                    s &&
                        s.domain(
                            f
                                .range()
                                .map(function (n) {
                                    return (n - h.y) / h.k;
                                })
                                .map(f.invert)
                        );
            }
            function A(n) {
                d++ || n({ type: "zoomstart" });
            }
            function C(n) {
                E(), n({ type: "zoom", scale: h.k, translate: [h.x, h.y] });
            }
            function z(n) {
                --d || (n({ type: "zoomend" }), (e = null));
            }
            function L() {
                var t = this,
                    e = b.of(t, arguments),
                    r = 0,
                    i = n
                        .select(u(t))
                        .on(m, function () {
                            (r = 1), k(n.mouse(t), o), C(e);
                        })
                        .on(M, function () {
                            i.on(m, null).on(M, null), a(r), z(e);
                        }),
                    o = w(n.mouse(t)),
                    a = Mn(t);
                ha.call(t), A(e);
            }
            function q() {
                var t,
                    e = this,
                    r = b.of(e, arguments),
                    i = {},
                    u = 0,
                    o = ".zoom-" + n.event.changedTouches[0].identifier,
                    l = "touchmove" + o,
                    c = "touchend" + o,
                    f = [],
                    s = n.select(e),
                    p = Mn(e);
                function g() {
                    var r = n.touches(e);
                    return (
                        (t = h.k),
                        r.forEach(function (n) {
                            n.identifier in i && (i[n.identifier] = w(n));
                        }),
                        r
                    );
                }
                function v() {
                    var t = n.event.target;
                    n.select(t).on(l, d).on(c, m), f.push(t);
                    for (var r = n.event.changedTouches, o = 0, s = r.length; o < s; ++o) i[r[o].identifier] = null;
                    var p = g(),
                        v = Date.now();
                    if (1 === p.length) {
                        if (v - a < 500) {
                            var y = p[0];
                            N(e, y, i[y.identifier], Math.floor(Math.log(h.k) / Math.LN2) + 1), U();
                        }
                        a = v;
                    } else if (p.length > 1) {
                        y = p[0];
                        var M = p[1],
                            x = y[0] - M[0],
                            b = y[1] - M[1];
                        u = x * x + b * b;
                    }
                }
                function d() {
                    var o,
                        l,
                        c,
                        f,
                        s = n.touches(e);
                    ha.call(e);
                    for (var h = 0, p = s.length; h < p; ++h, f = null)
                        if (((c = s[h]), (f = i[c.identifier]))) {
                            if (l) break;
                            (o = c), (l = f);
                        }
                    if (f) {
                        var g = (g = c[0] - o[0]) * g + (g = c[1] - o[1]) * g,
                            v = u && Math.sqrt(g / u);
                        (o = [(o[0] + c[0]) / 2, (o[1] + c[1]) / 2]), (l = [(l[0] + f[0]) / 2, (l[1] + f[1]) / 2]), S(v * t);
                    }
                    (a = null), k(o, l), C(r);
                }
                function m() {
                    if (n.event.touches.length) {
                        for (var t = n.event.changedTouches, e = 0, u = t.length; e < u; ++e) delete i[t[e].identifier];
                        for (var a in i) return void g();
                    }
                    n.selectAll(f).on(o, null), s.on(y, L).on(x, q), p(), z(r);
                }
                v(), A(r), s.on(y, null).on(x, v);
            }
            function T() {
                var r = b.of(this, arguments);
                o ? clearTimeout(o) : (ha.call(this), (t = w((e = i || n.mouse(this)))), A(r)),
                    (o = setTimeout(function () {
                        (o = null), z(r);
                    }, 50)),
                    U(),
                    S(Math.pow(2, 0.002 * jn()) * h.k),
                    k(e, t),
                    C(r);
            }
            function R() {
                var t = n.mouse(this),
                    e = Math.log(h.k) / Math.LN2;
                N(this, t, w(t), n.event.shiftKey ? Math.ceil(e) - 1 : Math.floor(e) + 1);
            }
            return (
                Fn ||
                    (Fn =
                        "onwheel" in r
                            ? ((jn = function () {
                                  return -n.event.deltaY * (n.event.deltaMode ? 120 : 1);
                              }),
                              "wheel")
                            : "onmousewheel" in r
                            ? ((jn = function () {
                                  return n.event.wheelDelta;
                              }),
                              "mousewheel")
                            : ((jn = function () {
                                  return -n.event.detail;
                              }),
                              "MozMousePixelScroll")),
                (_.event = function (t) {
                    t.each(function () {
                        var t = b.of(this, arguments),
                            r = h;
                        va
                            ? n
                                  .select(this)
                                  .transition()
                                  .each("start.zoom", function () {
                                      (h = this.__chart__ || { x: 0, y: 0, k: 1 }), A(t);
                                  })
                                  .tween("zoom:zoom", function () {
                                      var i = p[0],
                                          u = p[1],
                                          o = e ? e[0] : i / 2,
                                          a = e ? e[1] : u / 2,
                                          l = n.interpolateZoom([(o - h.x) / h.k, (a - h.y) / h.k, i / h.k], [(o - r.x) / r.k, (a - r.y) / r.k, i / r.k]);
                                      return function (n) {
                                          var e = l(n),
                                              r = i / e[2];
                                          (this.__chart__ = h = { x: o - e[0] * r, y: a - e[1] * r, k: r }), C(t);
                                      };
                                  })
                                  .each("interrupt.zoom", function () {
                                      z(t);
                                  })
                                  .each("end.zoom", function () {
                                      z(t);
                                  })
                            : ((this.__chart__ = h), A(t), C(t), z(t));
                    });
                }),
                (_.translate = function (n) {
                    return arguments.length ? ((h = { x: +n[0], y: +n[1], k: h.k }), E(), _) : [h.x, h.y];
                }),
                (_.scale = function (n) {
                    return arguments.length ? ((h = { x: h.x, y: h.y, k: null }), S(+n), E(), _) : h.k;
                }),
                (_.scaleExtent = function (n) {
                    return arguments.length ? ((g = null == n ? Hn : [+n[0], +n[1]]), _) : g;
                }),
                (_.center = function (n) {
                    return arguments.length ? ((i = n && [+n[0], +n[1]]), _) : i;
                }),
                (_.size = function (n) {
                    return arguments.length ? ((p = n && [+n[0], +n[1]]), _) : p;
                }),
                (_.duration = function (n) {
                    return arguments.length ? ((v = +n), _) : v;
                }),
                (_.x = function (n) {
                    return arguments.length ? ((c = n), (l = n.copy()), (h = { x: 0, y: 0, k: 1 }), _) : c;
                }),
                (_.y = function (n) {
                    return arguments.length ? ((s = n), (f = n.copy()), (h = { x: 0, y: 0, k: 1 }), _) : s;
                }),
                n.rebind(_, b, "on")
            );
        });
    var jn,
        Fn,
        Hn = [0, 1 / 0];
    function On() {}
    function In(n, t, e) {
        return this instanceof In ? ((this.h = +n), (this.s = +t), void (this.l = +e)) : arguments.length < 2 ? (n instanceof In ? new In(n.h, n.s, n.l) : ft("" + n, st, In)) : new In(n, t, e);
    }
    (n.color = On),
        (On.prototype.toString = function () {
            return this.rgb() + "";
        }),
        (n.hsl = In);
    var Yn = (In.prototype = new On());
    function Zn(n, t, e) {
        var r, i;
        function u(n) {
            return Math.round(
                255 *
                    (function (n) {
                        return n > 360 ? (n -= 360) : n < 0 && (n += 360), n < 60 ? r + ((i - r) * n) / 60 : n < 180 ? i : n < 240 ? r + ((i - r) * (240 - n)) / 60 : r;
                    })(n)
            );
        }
        return (
            (n = isNaN(n) ? 0 : (n %= 360) < 0 ? n + 360 : n), (t = isNaN(t) ? 0 : t < 0 ? 0 : t > 1 ? 1 : t), (r = 2 * (e = e < 0 ? 0 : e > 1 ? 1 : e) - (i = e <= 0.5 ? e * (1 + t) : e + t - e * t)), new ut(u(n + 120), u(n), u(n - 120))
        );
    }
    function Vn(t, e, r) {
        return this instanceof Vn
            ? ((this.h = +t), (this.c = +e), void (this.l = +r))
            : arguments.length < 2
            ? t instanceof Vn
                ? new Vn(t.h, t.c, t.l)
                : tt(t instanceof Bn ? t.l : (t = ht((t = n.rgb(t)).r, t.g, t.b)).l, t.a, t.b)
            : new Vn(t, e, r);
    }
    (Yn.brighter = function (n) {
        return (n = Math.pow(0.7, arguments.length ? n : 1)), new In(this.h, this.s, this.l / n);
    }),
        (Yn.darker = function (n) {
            return (n = Math.pow(0.7, arguments.length ? n : 1)), new In(this.h, this.s, n * this.l);
        }),
        (Yn.rgb = function () {
            return Zn(this.h, this.s, this.l);
        }),
        (n.hcl = Vn);
    var Xn = (Vn.prototype = new On());
    function $n(n, t, e) {
        return isNaN(n) && (n = 0), isNaN(t) && (t = 0), new Bn(e, Math.cos((n *= Cn)) * t, Math.sin(n) * t);
    }
    function Bn(n, t, e) {
        return this instanceof Bn ? ((this.l = +n), (this.a = +t), void (this.b = +e)) : arguments.length < 2 ? (n instanceof Bn ? new Bn(n.l, n.a, n.b) : n instanceof Vn ? $n(n.h, n.c, n.l) : ht((n = ut(n)).r, n.g, n.b)) : new Bn(n, t, e);
    }
    (Xn.brighter = function (n) {
        return new Vn(this.h, this.c, Math.min(100, this.l + Wn * (arguments.length ? n : 1)));
    }),
        (Xn.darker = function (n) {
            return new Vn(this.h, this.c, Math.max(0, this.l - Wn * (arguments.length ? n : 1)));
        }),
        (Xn.rgb = function () {
            return $n(this.h, this.c, this.l).rgb();
        }),
        (n.lab = Bn);
    var Wn = 18,
        Jn = 0.95047,
        Gn = 1,
        Kn = 1.08883,
        Qn = (Bn.prototype = new On());
    function nt(n, t, e) {
        var r = (n + 16) / 116,
            i = r + t / 500,
            u = r - e / 200;
        return new ut(it(3.2404542 * (i = et(i) * Jn) - 1.5371385 * (r = et(r) * Gn) - 0.4985314 * (u = et(u) * Kn)), it(-0.969266 * i + 1.8760108 * r + 0.041556 * u), it(0.0556434 * i - 0.2040259 * r + 1.0572252 * u));
    }
    function tt(n, t, e) {
        return n > 0 ? new Vn(Math.atan2(e, t) * zn, Math.sqrt(t * t + e * e), n) : new Vn(NaN, NaN, n);
    }
    function et(n) {
        return n > 0.206893034 ? n * n * n : (n - 4 / 29) / 7.787037;
    }
    function rt(n) {
        return n > 0.008856 ? Math.pow(n, 1 / 3) : 7.787037 * n + 4 / 29;
    }
    function it(n) {
        return Math.round(255 * (n <= 0.00304 ? 12.92 * n : 1.055 * Math.pow(n, 1 / 2.4) - 0.055));
    }
    function ut(n, t, e) {
        return this instanceof ut ? ((this.r = ~~n), (this.g = ~~t), void (this.b = ~~e)) : arguments.length < 2 ? (n instanceof ut ? new ut(n.r, n.g, n.b) : ft("" + n, ut, Zn)) : new ut(n, t, e);
    }
    function ot(n) {
        return new ut(n >> 16, (n >> 8) & 255, 255 & n);
    }
    function at(n) {
        return ot(n) + "";
    }
    (Qn.brighter = function (n) {
        return new Bn(Math.min(100, this.l + Wn * (arguments.length ? n : 1)), this.a, this.b);
    }),
        (Qn.darker = function (n) {
            return new Bn(Math.max(0, this.l - Wn * (arguments.length ? n : 1)), this.a, this.b);
        }),
        (Qn.rgb = function () {
            return nt(this.l, this.a, this.b);
        }),
        (n.rgb = ut);
    var lt = (ut.prototype = new On());
    function ct(n) {
        return n < 16 ? "0" + Math.max(0, n).toString(16) : Math.min(255, n).toString(16);
    }
    function ft(n, t, e) {
        var r,
            i,
            u,
            o = 0,
            a = 0,
            l = 0;
        if ((r = /([a-z]+)\((.*)\)/.exec((n = n.toLowerCase()))))
            switch (((i = r[2].split(",")), r[1])) {
                case "hsl":
                    return e(parseFloat(i[0]), parseFloat(i[1]) / 100, parseFloat(i[2]) / 100);
                case "rgb":
                    return t(gt(i[0]), gt(i[1]), gt(i[2]));
            }
        return (u = vt.get(n))
            ? t(u.r, u.g, u.b)
            : (null == n ||
                  "#" !== n.charAt(0) ||
                  isNaN((u = parseInt(n.slice(1), 16))) ||
                  (4 === n.length ? ((o = (3840 & u) >> 4), (o |= o >> 4), (a = 240 & u), (a |= a >> 4), (l = 15 & u), (l |= l << 4)) : 7 === n.length && ((o = (16711680 & u) >> 16), (a = (65280 & u) >> 8), (l = 255 & u))),
              t(o, a, l));
    }
    function st(n, t, e) {
        var r,
            i,
            u = Math.min((n /= 255), (t /= 255), (e /= 255)),
            o = Math.max(n, t, e),
            a = o - u,
            l = (o + u) / 2;
        return a ? ((i = l < 0.5 ? a / (o + u) : a / (2 - o - u)), (r = n == o ? (t - e) / a + (t < e ? 6 : 0) : t == o ? (e - n) / a + 2 : (n - t) / a + 4), (r *= 60)) : ((r = NaN), (i = l > 0 && l < 1 ? 0 : r)), new In(r, i, l);
    }
    function ht(n, t, e) {
        var r = rt((0.4124564 * (n = pt(n)) + 0.3575761 * (t = pt(t)) + 0.1804375 * (e = pt(e))) / Jn),
            i = rt((0.2126729 * n + 0.7151522 * t + 0.072175 * e) / Gn);
        return Bn(116 * i - 16, 500 * (r - i), 200 * (i - rt((0.0193339 * n + 0.119192 * t + 0.9503041 * e) / Kn)));
    }
    function pt(n) {
        return (n /= 255) <= 0.04045 ? n / 12.92 : Math.pow((n + 0.055) / 1.055, 2.4);
    }
    function gt(n) {
        var t = parseFloat(n);
        return "%" === n.charAt(n.length - 1) ? Math.round(2.55 * t) : t;
    }
    (lt.brighter = function (n) {
        n = Math.pow(0.7, arguments.length ? n : 1);
        var t = this.r,
            e = this.g,
            r = this.b,
            i = 30;
        return t || e || r ? (t && t < i && (t = i), e && e < i && (e = i), r && r < i && (r = i), new ut(Math.min(255, t / n), Math.min(255, e / n), Math.min(255, r / n))) : new ut(i, i, i);
    }),
        (lt.darker = function (n) {
            return new ut((n = Math.pow(0.7, arguments.length ? n : 1)) * this.r, n * this.g, n * this.b);
        }),
        (lt.hsl = function () {
            return st(this.r, this.g, this.b);
        }),
        (lt.toString = function () {
            return "#" + ct(this.r) + ct(this.g) + ct(this.b);
        });
    var vt = n.map({
        aliceblue: 15792383,
        antiquewhite: 16444375,
        aqua: 65535,
        aquamarine: 8388564,
        azure: 15794175,
        beige: 16119260,
        bisque: 16770244,
        black: 0,
        blanchedalmond: 16772045,
        blue: 255,
        blueviolet: 9055202,
        brown: 10824234,
        burlywood: 14596231,
        cadetblue: 6266528,
        chartreuse: 8388352,
        chocolate: 13789470,
        coral: 16744272,
        cornflowerblue: 6591981,
        cornsilk: 16775388,
        crimson: 14423100,
        cyan: 65535,
        darkblue: 139,
        darkcyan: 35723,
        darkgoldenrod: 12092939,
        darkgray: 11119017,
        darkgreen: 25600,
        darkgrey: 11119017,
        darkkhaki: 12433259,
        darkmagenta: 9109643,
        darkolivegreen: 5597999,
        darkorange: 16747520,
        darkorchid: 10040012,
        darkred: 9109504,
        darksalmon: 15308410,
        darkseagreen: 9419919,
        darkslateblue: 4734347,
        darkslategray: 3100495,
        darkslategrey: 3100495,
        darkturquoise: 52945,
        darkviolet: 9699539,
        deeppink: 16716947,
        deepskyblue: 49151,
        dimgray: 6908265,
        dimgrey: 6908265,
        dodgerblue: 2003199,
        firebrick: 11674146,
        floralwhite: 16775920,
        forestgreen: 2263842,
        fuchsia: 16711935,
        gainsboro: 14474460,
        ghostwhite: 16316671,
        gold: 16766720,
        goldenrod: 14329120,
        gray: 8421504,
        green: 32768,
        greenyellow: 11403055,
        grey: 8421504,
        honeydew: 15794160,
        hotpink: 16738740,
        indianred: 13458524,
        indigo: 4915330,
        ivory: 16777200,
        khaki: 15787660,
        lavender: 15132410,
        lavenderblush: 16773365,
        lawngreen: 8190976,
        lemonchiffon: 16775885,
        lightblue: 11393254,
        lightcoral: 15761536,
        lightcyan: 14745599,
        lightgoldenrodyellow: 16448210,
        lightgray: 13882323,
        lightgreen: 9498256,
        lightgrey: 13882323,
        lightpink: 16758465,
        lightsalmon: 16752762,
        lightseagreen: 2142890,
        lightskyblue: 8900346,
        lightslategray: 7833753,
        lightslategrey: 7833753,
        lightsteelblue: 11584734,
        lightyellow: 16777184,
        lime: 65280,
        limegreen: 3329330,
        linen: 16445670,
        magenta: 16711935,
        maroon: 8388608,
        mediumaquamarine: 6737322,
        mediumblue: 205,
        mediumorchid: 12211667,
        mediumpurple: 9662683,
        mediumseagreen: 3978097,
        mediumslateblue: 8087790,
        mediumspringgreen: 64154,
        mediumturquoise: 4772300,
        mediumvioletred: 13047173,
        midnightblue: 1644912,
        mintcream: 16121850,
        mistyrose: 16770273,
        moccasin: 16770229,
        navajowhite: 16768685,
        navy: 128,
        oldlace: 16643558,
        olive: 8421376,
        olivedrab: 7048739,
        orange: 16753920,
        orangered: 16729344,
        orchid: 14315734,
        palegoldenrod: 15657130,
        palegreen: 10025880,
        paleturquoise: 11529966,
        palevioletred: 14381203,
        papayawhip: 16773077,
        peachpuff: 16767673,
        peru: 13468991,
        pink: 16761035,
        plum: 14524637,
        powderblue: 11591910,
        purple: 8388736,
        rebeccapurple: 6697881,
        red: 16711680,
        rosybrown: 12357519,
        royalblue: 4286945,
        saddlebrown: 9127187,
        salmon: 16416882,
        sandybrown: 16032864,
        seagreen: 3050327,
        seashell: 16774638,
        sienna: 10506797,
        silver: 12632256,
        skyblue: 8900331,
        slateblue: 6970061,
        slategray: 7372944,
        slategrey: 7372944,
        snow: 16775930,
        springgreen: 65407,
        steelblue: 4620980,
        tan: 13808780,
        teal: 32896,
        thistle: 14204888,
        tomato: 16737095,
        turquoise: 4251856,
        violet: 15631086,
        wheat: 16113331,
        white: 16777215,
        whitesmoke: 16119285,
        yellow: 16776960,
        yellowgreen: 10145074,
    });
    function dt(n) {
        return "function" == typeof n
            ? n
            : function () {
                  return n;
              };
    }
    function yt(n) {
        return function (t, e, r) {
            return 2 === arguments.length && "function" == typeof e && ((r = e), (e = null)), mt(t, e, n, r);
        };
    }
    function mt(t, r, i, u) {
        var o = {},
            a = n.dispatch("beforesend", "progress", "load", "error"),
            l = {},
            c = new XMLHttpRequest(),
            f = null;
        function s() {
            var n,
                t = c.status;
            if (
                (!t &&
                    (function (n) {
                        var t = n.responseType;
                        return t && "text" !== t ? n.response : n.responseText;
                    })(c)) ||
                (t >= 200 && t < 300) ||
                304 === t
            ) {
                try {
                    n = i.call(o, c);
                } catch (n) {
                    return void a.error.call(o, n);
                }
                a.load.call(o, n);
            } else a.error.call(o, c);
        }
        return (
            !this.XDomainRequest || "withCredentials" in c || !/^(http(s)?:)?\/\//.test(t) || (c = new XDomainRequest()),
            "onload" in c
                ? (c.onload = c.onerror = s)
                : (c.onreadystatechange = function () {
                      c.readyState > 3 && s();
                  }),
            (c.onprogress = function (t) {
                var e = n.event;
                n.event = t;
                try {
                    a.progress.call(o, c);
                } finally {
                    n.event = e;
                }
            }),
            (o.header = function (n, t) {
                return (n = (n + "").toLowerCase()), arguments.length < 2 ? l[n] : (null == t ? delete l[n] : (l[n] = t + ""), o);
            }),
            (o.mimeType = function (n) {
                return arguments.length ? ((r = null == n ? null : n + ""), o) : r;
            }),
            (o.responseType = function (n) {
                return arguments.length ? ((f = n), o) : f;
            }),
            (o.response = function (n) {
                return (i = n), o;
            }),
            ["get", "post"].forEach(function (n) {
                o[n] = function () {
                    return o.send.apply(o, [n].concat(e(arguments)));
                };
            }),
            (o.send = function (n, e, i) {
                if ((2 === arguments.length && "function" == typeof e && ((i = e), (e = null)), c.open(n, t, !0), null == r || "accept" in l || (l.accept = r + ",*/*"), c.setRequestHeader)) for (var u in l) c.setRequestHeader(u, l[u]);
                return (
                    null != r && c.overrideMimeType && c.overrideMimeType(r),
                    null != f && (c.responseType = f),
                    null != i &&
                        o.on("error", i).on("load", function (n) {
                            i(null, n);
                        }),
                    a.beforesend.call(o, c),
                    c.send(null == e ? null : e),
                    o
                );
            }),
            (o.abort = function () {
                return c.abort(), o;
            }),
            n.rebind(o, a, "on"),
            null == u
                ? o
                : o.get(
                      (function (n) {
                          return 1 === n.length
                              ? function (t, e) {
                                    n(null == t ? e : null);
                                }
                              : n;
                      })(u)
                  )
        );
    }
    vt.forEach(function (n, t) {
        vt.set(n, ot(t));
    }),
        (n.functor = dt),
        (n.xhr = yt(z)),
        (n.dsv = function (n, t) {
            var e = new RegExp('["' + n + "\n]"),
                r = n.charCodeAt(0);
            function i(n, e, r) {
                arguments.length < 3 && ((r = e), (e = null));
                var i = mt(n, t, null == e ? u : o(e), r);
                return (
                    (i.row = function (n) {
                        return arguments.length ? i.response(null == (e = n) ? u : o(n)) : e;
                    }),
                    i
                );
            }
            function u(n) {
                return i.parse(n.responseText);
            }
            function o(n) {
                return function (t) {
                    return i.parse(t.responseText, n);
                };
            }
            function a(t) {
                return t.map(l).join(n);
            }
            function l(n) {
                return e.test(n) ? '"' + n.replace(/\"/g, '""') + '"' : n;
            }
            return (
                (i.parse = function (n, t) {
                    var e;
                    return i.parseRows(n, function (n, r) {
                        if (e) return e(n, r - 1);
                        var i = new Function(
                            "d",
                            "return {" +
                                n
                                    .map(function (n, t) {
                                        return JSON.stringify(n) + ": d[" + t + "]";
                                    })
                                    .join(",") +
                                "}"
                        );
                        e = t
                            ? function (n, e) {
                                  return t(i(n), e);
                              }
                            : i;
                    });
                }),
                (i.parseRows = function (n, t) {
                    var e,
                        i,
                        u = {},
                        o = {},
                        a = [],
                        l = n.length,
                        c = 0,
                        f = 0;
                    function s() {
                        if (c >= l) return o;
                        if (i) return (i = !1), u;
                        var t = c;
                        if (34 === n.charCodeAt(t)) {
                            for (var e = t; e++ < l; )
                                if (34 === n.charCodeAt(e)) {
                                    if (34 !== n.charCodeAt(e + 1)) break;
                                    ++e;
                                }
                            return (c = e + 2), 13 === (a = n.charCodeAt(e + 1)) ? ((i = !0), 10 === n.charCodeAt(e + 2) && ++c) : 10 === a && (i = !0), n.slice(t + 1, e).replace(/""/g, '"');
                        }
                        for (; c < l; ) {
                            var a,
                                f = 1;
                            if (10 === (a = n.charCodeAt(c++))) i = !0;
                            else if (13 === a) (i = !0), 10 === n.charCodeAt(c) && (++c, ++f);
                            else if (a !== r) continue;
                            return n.slice(t, c - f);
                        }
                        return n.slice(t);
                    }
                    for (; (e = s()) !== o; ) {
                        for (var h = []; e !== u && e !== o; ) h.push(e), (e = s());
                        (t && null == (h = t(h, f++))) || a.push(h);
                    }
                    return a;
                }),
                (i.format = function (t) {
                    if (Array.isArray(t[0])) return i.formatRows(t);
                    var e = new C(),
                        r = [];
                    return (
                        t.forEach(function (n) {
                            for (var t in n) e.has(t) || r.push(e.add(t));
                        }),
                        [r.map(l).join(n)]
                            .concat(
                                t.map(function (t) {
                                    return r
                                        .map(function (n) {
                                            return l(t[n]);
                                        })
                                        .join(n);
                                })
                            )
                            .join("\n")
                    );
                }),
                (i.formatRows = function (n) {
                    return n.map(a).join("\n");
                }),
                i
            );
        }),
        (n.csv = n.dsv(",", "text/csv")),
        (n.tsv = n.dsv("\t", "text/tab-separated-values"));
    var Mt,
        xt,
        bt,
        _t,
        wt =
            this[q(this, "requestAnimationFrame")] ||
            function (n) {
                setTimeout(n, 17);
            };
    function St(n, t, e) {
        var r = arguments.length;
        r < 2 && (t = 0), r < 3 && (e = Date.now());
        var i = { c: n, t: e + t, n: null };
        return xt ? (xt.n = i) : (Mt = i), (xt = i), bt || ((_t = clearTimeout(_t)), (bt = 1), wt(kt)), i;
    }
    function kt() {
        var n = Nt(),
            t = Et() - n;
        t > 24 ? (isFinite(t) && (clearTimeout(_t), (_t = setTimeout(kt, t))), (bt = 0)) : ((bt = 1), wt(kt));
    }
    function Nt() {
        for (var n = Date.now(), t = Mt; t; ) n >= t.t && t.c(n - t.t) && (t.c = null), (t = t.n);
        return n;
    }
    function Et() {
        for (var n, t = Mt, e = 1 / 0; t; ) t.c ? (t.t < e && (e = t.t), (t = (n = t).n)) : (t = n ? (n.n = t.n) : (Mt = t.n));
        return (xt = n), e;
    }
    function At(n, t) {
        return t - (n ? Math.ceil(Math.log(n) / Math.LN10) : 1);
    }
    (n.timer = function () {
        St.apply(this, arguments);
    }),
        (n.timer.flush = function () {
            Nt(), Et();
        }),
        (n.round = function (n, t) {
            return t ? Math.round(n * (t = Math.pow(10, t))) / t : Math.round(n);
        });
    var Ct = ["y", "z", "a", "f", "p", "n", "µ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y"].map(function (n, t) {
        var e = Math.pow(10, 3 * y(8 - t));
        return {
            scale:
                t > 8
                    ? function (n) {
                          return n / e;
                      }
                    : function (n) {
                          return n * e;
                      },
            symbol: n,
        };
    });
    function zt(t) {
        var e = t.decimal,
            r = t.thousands,
            i = t.grouping,
            u = t.currency,
            o =
                i && r
                    ? function (n, t) {
                          for (var e = n.length, u = [], o = 0, a = i[0], l = 0; e > 0 && a > 0 && (l + a + 1 > t && (a = Math.max(1, t - l)), u.push(n.substring((e -= a), e + a)), !((l += a + 1) > t)); ) a = i[(o = (o + 1) % i.length)];
                          return u.reverse().join(r);
                      }
                    : z;
        return function (t) {
            var r = Lt.exec(t),
                i = r[1] || " ",
                a = r[2] || ">",
                l = r[3] || "-",
                c = r[4] || "",
                f = r[5],
                s = +r[6],
                h = r[7],
                p = r[8],
                g = r[9],
                v = 1,
                d = "",
                y = "",
                m = !1,
                M = !0;
            switch ((p && (p = +p.substring(1)), (f || ("0" === i && "=" === a)) && ((f = i = "0"), (a = "=")), g)) {
                case "n":
                    (h = !0), (g = "g");
                    break;
                case "%":
                    (v = 100), (y = "%"), (g = "f");
                    break;
                case "p":
                    (v = 100), (y = "%"), (g = "r");
                    break;
                case "b":
                case "o":
                case "x":
                case "X":
                    "#" === c && (d = "0" + g.toLowerCase());
                case "c":
                    M = !1;
                case "d":
                    (m = !0), (p = 0);
                    break;
                case "s":
                    (v = -1), (g = "r");
            }
            "$" === c && ((d = u[0]), (y = u[1])), "r" != g || p || (g = "g"), null != p && ("g" == g ? (p = Math.max(1, Math.min(21, p))) : ("e" != g && "f" != g) || (p = Math.max(0, Math.min(20, p)))), (g = qt.get(g) || Tt);
            var x = f && h;
            return function (t) {
                var r = y;
                if (m && t % 1) return "";
                var u = t < 0 || (0 === t && 1 / t < 0) ? ((t = -t), "-") : "-" === l ? "" : l;
                if (v < 0) {
                    var c = n.formatPrefix(t, p);
                    (t = c.scale(t)), (r = c.symbol + y);
                } else t *= v;
                var b,
                    _,
                    w = (t = g(t, p)).lastIndexOf(".");
                if (w < 0) {
                    var S = M ? t.lastIndexOf("e") : -1;
                    S < 0 ? ((b = t), (_ = "")) : ((b = t.substring(0, S)), (_ = t.substring(S)));
                } else (b = t.substring(0, w)), (_ = e + t.substring(w + 1));
                !f && h && (b = o(b, 1 / 0));
                var k = d.length + b.length + _.length + (x ? 0 : u.length),
                    N = k < s ? new Array((k = s - k + 1)).join(i) : "";
                return x && (b = o(N + b, N.length ? s - _.length : 1 / 0)), (u += d), (t = b + _), ("<" === a ? u + t + N : ">" === a ? N + u + t : "^" === a ? N.substring(0, (k >>= 1)) + u + t + N.substring(k) : u + (x ? t : N + t)) + r;
            };
        };
    }
    n.formatPrefix = function (t, e) {
        var r = 0;
        return (t = +t) && (t < 0 && (t *= -1), e && (t = n.round(t, At(t, e))), (r = 1 + Math.floor(1e-12 + Math.log(t) / Math.LN10)), (r = Math.max(-24, Math.min(24, 3 * Math.floor((r - 1) / 3))))), Ct[8 + r / 3];
    };
    var Lt = /(?:([^{])?([<>=^]))?([+\- ])?([$#])?(0)?(\d+)?(,)?(\.-?\d+)?([a-z%])?/i,
        qt = n.map({
            b: function (n) {
                return n.toString(2);
            },
            c: function (n) {
                return String.fromCharCode(n);
            },
            o: function (n) {
                return n.toString(8);
            },
            x: function (n) {
                return n.toString(16);
            },
            X: function (n) {
                return n.toString(16).toUpperCase();
            },
            g: function (n, t) {
                return n.toPrecision(t);
            },
            e: function (n, t) {
                return n.toExponential(t);
            },
            f: function (n, t) {
                return n.toFixed(t);
            },
            r: function (t, e) {
                return (t = n.round(t, At(t, e))).toFixed(Math.max(0, Math.min(20, At(t * (1 + 1e-15), e))));
            },
        });
    function Tt(n) {
        return n + "";
    }
    var Rt = (n.time = {}),
        Dt = Date;
    function Pt() {
        this._ = new Date(arguments.length > 1 ? Date.UTC.apply(this, arguments) : arguments[0]);
    }
    Pt.prototype = {
        getDate: function () {
            return this._.getUTCDate();
        },
        getDay: function () {
            return this._.getUTCDay();
        },
        getFullYear: function () {
            return this._.getUTCFullYear();
        },
        getHours: function () {
            return this._.getUTCHours();
        },
        getMilliseconds: function () {
            return this._.getUTCMilliseconds();
        },
        getMinutes: function () {
            return this._.getUTCMinutes();
        },
        getMonth: function () {
            return this._.getUTCMonth();
        },
        getSeconds: function () {
            return this._.getUTCSeconds();
        },
        getTime: function () {
            return this._.getTime();
        },
        getTimezoneOffset: function () {
            return 0;
        },
        valueOf: function () {
            return this._.valueOf();
        },
        setDate: function () {
            Ut.setUTCDate.apply(this._, arguments);
        },
        setDay: function () {
            Ut.setUTCDay.apply(this._, arguments);
        },
        setFullYear: function () {
            Ut.setUTCFullYear.apply(this._, arguments);
        },
        setHours: function () {
            Ut.setUTCHours.apply(this._, arguments);
        },
        setMilliseconds: function () {
            Ut.setUTCMilliseconds.apply(this._, arguments);
        },
        setMinutes: function () {
            Ut.setUTCMinutes.apply(this._, arguments);
        },
        setMonth: function () {
            Ut.setUTCMonth.apply(this._, arguments);
        },
        setSeconds: function () {
            Ut.setUTCSeconds.apply(this._, arguments);
        },
        setTime: function () {
            Ut.setTime.apply(this._, arguments);
        },
    };
    var Ut = Date.prototype;
    function jt(n, t, e) {
        function r(t) {
            var e = n(t),
                r = u(e, 1);
            return t - e < r - t ? e : r;
        }
        function i(e) {
            return t((e = n(new Dt(e - 1))), 1), e;
        }
        function u(n, e) {
            return t((n = new Dt(+n)), e), n;
        }
        function o(n, r, u) {
            var o = i(n),
                a = [];
            if (u > 1) for (; o < r; ) e(o) % u || a.push(new Date(+o)), t(o, 1);
            else for (; o < r; ) a.push(new Date(+o)), t(o, 1);
            return a;
        }
        (n.floor = n), (n.round = r), (n.ceil = i), (n.offset = u), (n.range = o);
        var a = (n.utc = Ft(n));
        return (
            (a.floor = a),
            (a.round = Ft(r)),
            (a.ceil = Ft(i)),
            (a.offset = Ft(u)),
            (a.range = function (n, t, e) {
                try {
                    Dt = Pt;
                    var r = new Pt();
                    return (r._ = n), o(r, t, e);
                } finally {
                    Dt = Date;
                }
            }),
            n
        );
    }
    function Ft(n) {
        return function (t, e) {
            try {
                Dt = Pt;
                var r = new Pt();
                return (r._ = t), n(r, e)._;
            } finally {
                Dt = Date;
            }
        };
    }
    function Ht(t) {
        var e = t.dateTime,
            r = t.date,
            i = t.time,
            u = t.periods,
            o = t.days,
            a = t.shortDays,
            l = t.months,
            c = t.shortMonths;
        function f(n) {
            var t = n.length;
            function e(e) {
                for (var r, i, u, o = [], a = -1, l = 0; ++a < t; )
                    37 === n.charCodeAt(a) && (o.push(n.slice(l, a)), null != (i = Ot[(r = n.charAt(++a))]) && (r = n.charAt(++a)), (u = b[r]) && (r = u(e, null == i ? ("e" === r ? " " : "0") : i)), o.push(r), (l = a + 1));
                return o.push(n.slice(l, a)), o.join("");
            }
            return (
                (e.parse = function (t) {
                    var e = { y: 1900, m: 0, d: 1, H: 0, M: 0, S: 0, L: 0, Z: null };
                    if (s(e, n, t, 0) != t.length) return null;
                    "p" in e && (e.H = (e.H % 12) + 12 * e.p);
                    var r = null != e.Z && Dt !== Pt,
                        i = new (r ? Pt : Dt)();
                    return (
                        "j" in e
                            ? i.setFullYear(e.y, 0, e.j)
                            : "W" in e || "U" in e
                            ? ("w" in e || (e.w = "W" in e ? 1 : 0), i.setFullYear(e.y, 0, 1), i.setFullYear(e.y, 0, "W" in e ? ((e.w + 6) % 7) + 7 * e.W - ((i.getDay() + 5) % 7) : e.w + 7 * e.U - ((i.getDay() + 6) % 7)))
                            : i.setFullYear(e.y, e.m, e.d),
                        i.setHours(e.H + ((e.Z / 100) | 0), e.M + (e.Z % 100), e.S, e.L),
                        r ? i._ : i
                    );
                }),
                (e.toString = function () {
                    return n;
                }),
                e
            );
        }
        function s(n, t, e, r) {
            for (var i, u, o, a = 0, l = t.length, c = e.length; a < l; ) {
                if (r >= c) return -1;
                if (37 === (i = t.charCodeAt(a++))) {
                    if (((o = t.charAt(a++)), !(u = _[o in Ot ? t.charAt(a++) : o]) || (r = u(n, e, r)) < 0)) return -1;
                } else if (i != e.charCodeAt(r++)) return -1;
            }
            return r;
        }
        (f.utc = function (n) {
            var t = f(n);
            function e(n) {
                try {
                    var e = new (Dt = Pt)();
                    return (e._ = n), t(e);
                } finally {
                    Dt = Date;
                }
            }
            return (
                (e.parse = function (n) {
                    try {
                        Dt = Pt;
                        var e = t.parse(n);
                        return e && e._;
                    } finally {
                        Dt = Date;
                    }
                }),
                (e.toString = t.toString),
                e
            );
        }),
            (f.multi = f.utc.multi = le);
        var h = n.map(),
            p = Vt(o),
            g = Xt(o),
            v = Vt(a),
            d = Xt(a),
            y = Vt(l),
            m = Xt(l),
            M = Vt(c),
            x = Xt(c);
        u.forEach(function (n, t) {
            h.set(n.toLowerCase(), t);
        });
        var b = {
                a: function (n) {
                    return a[n.getDay()];
                },
                A: function (n) {
                    return o[n.getDay()];
                },
                b: function (n) {
                    return c[n.getMonth()];
                },
                B: function (n) {
                    return l[n.getMonth()];
                },
                c: f(e),
                d: function (n, t) {
                    return Zt(n.getDate(), t, 2);
                },
                e: function (n, t) {
                    return Zt(n.getDate(), t, 2);
                },
                H: function (n, t) {
                    return Zt(n.getHours(), t, 2);
                },
                I: function (n, t) {
                    return Zt(n.getHours() % 12 || 12, t, 2);
                },
                j: function (n, t) {
                    return Zt(1 + Rt.dayOfYear(n), t, 3);
                },
                L: function (n, t) {
                    return Zt(n.getMilliseconds(), t, 3);
                },
                m: function (n, t) {
                    return Zt(n.getMonth() + 1, t, 2);
                },
                M: function (n, t) {
                    return Zt(n.getMinutes(), t, 2);
                },
                p: function (n) {
                    return u[+(n.getHours() >= 12)];
                },
                S: function (n, t) {
                    return Zt(n.getSeconds(), t, 2);
                },
                U: function (n, t) {
                    return Zt(Rt.sundayOfYear(n), t, 2);
                },
                w: function (n) {
                    return n.getDay();
                },
                W: function (n, t) {
                    return Zt(Rt.mondayOfYear(n), t, 2);
                },
                x: f(r),
                X: f(i),
                y: function (n, t) {
                    return Zt(n.getFullYear() % 100, t, 2);
                },
                Y: function (n, t) {
                    return Zt(n.getFullYear() % 1e4, t, 4);
                },
                Z: oe,
                "%": function () {
                    return "%";
                },
            },
            _ = {
                a: function (n, t, e) {
                    v.lastIndex = 0;
                    var r = v.exec(t.slice(e));
                    return r ? ((n.w = d.get(r[0].toLowerCase())), e + r[0].length) : -1;
                },
                A: function (n, t, e) {
                    p.lastIndex = 0;
                    var r = p.exec(t.slice(e));
                    return r ? ((n.w = g.get(r[0].toLowerCase())), e + r[0].length) : -1;
                },
                b: function (n, t, e) {
                    M.lastIndex = 0;
                    var r = M.exec(t.slice(e));
                    return r ? ((n.m = x.get(r[0].toLowerCase())), e + r[0].length) : -1;
                },
                B: function (n, t, e) {
                    y.lastIndex = 0;
                    var r = y.exec(t.slice(e));
                    return r ? ((n.m = m.get(r[0].toLowerCase())), e + r[0].length) : -1;
                },
                c: function (n, t, e) {
                    return s(n, b.c.toString(), t, e);
                },
                d: ne,
                e: ne,
                H: ee,
                I: ee,
                j: te,
                L: ue,
                m: Qt,
                M: re,
                p: function (n, t, e) {
                    var r = h.get(t.slice(e, (e += 2)).toLowerCase());
                    return null == r ? -1 : ((n.p = r), e);
                },
                S: ie,
                U: Bt,
                w: $t,
                W: Wt,
                x: function (n, t, e) {
                    return s(n, b.x.toString(), t, e);
                },
                X: function (n, t, e) {
                    return s(n, b.X.toString(), t, e);
                },
                y: Gt,
                Y: Jt,
                Z: Kt,
                "%": ae,
            };
        return f;
    }
    (Rt.year = jt(
        function (n) {
            return (n = Rt.day(n)).setMonth(0, 1), n;
        },
        function (n, t) {
            n.setFullYear(n.getFullYear() + t);
        },
        function (n) {
            return n.getFullYear();
        }
    )),
        (Rt.years = Rt.year.range),
        (Rt.years.utc = Rt.year.utc.range),
        (Rt.day = jt(
            function (n) {
                var t = new Dt(2e3, 0);
                return t.setFullYear(n.getFullYear(), n.getMonth(), n.getDate()), t;
            },
            function (n, t) {
                n.setDate(n.getDate() + t);
            },
            function (n) {
                return n.getDate() - 1;
            }
        )),
        (Rt.days = Rt.day.range),
        (Rt.days.utc = Rt.day.utc.range),
        (Rt.dayOfYear = function (n) {
            var t = Rt.year(n);
            return Math.floor((n - t - 6e4 * (n.getTimezoneOffset() - t.getTimezoneOffset())) / 864e5);
        }),
        ["sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday"].forEach(function (n, t) {
            t = 7 - t;
            var e = (Rt[n] = jt(
                function (n) {
                    return (n = Rt.day(n)).setDate(n.getDate() - ((n.getDay() + t) % 7)), n;
                },
                function (n, t) {
                    n.setDate(n.getDate() + 7 * Math.floor(t));
                },
                function (n) {
                    var e = Rt.year(n).getDay();
                    return Math.floor((Rt.dayOfYear(n) + ((e + t) % 7)) / 7) - (e !== t);
                }
            ));
            (Rt[n + "s"] = e.range),
                (Rt[n + "s"].utc = e.utc.range),
                (Rt[n + "OfYear"] = function (n) {
                    var e = Rt.year(n).getDay();
                    return Math.floor((Rt.dayOfYear(n) + ((e + t) % 7)) / 7);
                });
        }),
        (Rt.week = Rt.sunday),
        (Rt.weeks = Rt.sunday.range),
        (Rt.weeks.utc = Rt.sunday.utc.range),
        (Rt.weekOfYear = Rt.sundayOfYear);
    var Ot = { "-": "", _: " ", 0: "0" },
        It = /^\s*\d+/,
        Yt = /^%/;
    function Zt(n, t, e) {
        var r = n < 0 ? "-" : "",
            i = (r ? -n : n) + "",
            u = i.length;
        return r + (u < e ? new Array(e - u + 1).join(t) + i : i);
    }
    function Vt(t) {
        return new RegExp("^(?:" + t.map(n.requote).join("|") + ")", "i");
    }
    function Xt(n) {
        for (var t = new M(), e = -1, r = n.length; ++e < r; ) t.set(n[e].toLowerCase(), e);
        return t;
    }
    function $t(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 1));
        return r ? ((n.w = +r[0]), e + r[0].length) : -1;
    }
    function Bt(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e));
        return r ? ((n.U = +r[0]), e + r[0].length) : -1;
    }
    function Wt(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e));
        return r ? ((n.W = +r[0]), e + r[0].length) : -1;
    }
    function Jt(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 4));
        return r ? ((n.y = +r[0]), e + r[0].length) : -1;
    }
    function Gt(n, t, e) {
        It.lastIndex = 0;
        var r,
            i = It.exec(t.slice(e, e + 2));
        return i ? ((n.y = (r = +i[0]) + (r > 68 ? 1900 : 2e3)), e + i[0].length) : -1;
    }
    function Kt(n, t, e) {
        return /^[+-]\d{4}$/.test((t = t.slice(e, e + 5))) ? ((n.Z = -t), e + 5) : -1;
    }
    function Qt(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 2));
        return r ? ((n.m = r[0] - 1), e + r[0].length) : -1;
    }
    function ne(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 2));
        return r ? ((n.d = +r[0]), e + r[0].length) : -1;
    }
    function te(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 3));
        return r ? ((n.j = +r[0]), e + r[0].length) : -1;
    }
    function ee(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 2));
        return r ? ((n.H = +r[0]), e + r[0].length) : -1;
    }
    function re(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 2));
        return r ? ((n.M = +r[0]), e + r[0].length) : -1;
    }
    function ie(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 2));
        return r ? ((n.S = +r[0]), e + r[0].length) : -1;
    }
    function ue(n, t, e) {
        It.lastIndex = 0;
        var r = It.exec(t.slice(e, e + 3));
        return r ? ((n.L = +r[0]), e + r[0].length) : -1;
    }
    function oe(n) {
        var t = n.getTimezoneOffset(),
            e = t > 0 ? "-" : "+",
            r = (y(t) / 60) | 0,
            i = y(t) % 60;
        return e + Zt(r, "0", 2) + Zt(i, "0", 2);
    }
    function ae(n, t, e) {
        Yt.lastIndex = 0;
        var r = Yt.exec(t.slice(e, e + 1));
        return r ? e + r[0].length : -1;
    }
    function le(n) {
        for (var t = n.length, e = -1; ++e < t; ) n[e][0] = this(n[e][0]);
        return function (t) {
            for (var e = 0, r = n[e]; !r[1](t); ) r = n[++e];
            return r[0](t);
        };
    }
    n.locale = function (n) {
        return { numberFormat: zt(n), timeFormat: Ht(n) };
    };
    var ce = n.locale({
        decimal: ".",
        thousands: ",",
        grouping: [3],
        currency: ["$", ""],
        dateTime: "%a %b %e %X %Y",
        date: "%m/%d/%Y",
        time: "%H:%M:%S",
        periods: ["AM", "PM"],
        days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
        shortDays: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        shortMonths: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    });
    function fe() {}
    (n.format = ce.numberFormat),
        (n.geo = {}),
        (fe.prototype = {
            s: 0,
            t: 0,
            add: function (n) {
                he(n, this.t, se), he(se.s, this.s, this), this.s ? (this.t += se.t) : (this.s = se.t);
            },
            reset: function () {
                this.s = this.t = 0;
            },
            valueOf: function () {
                return this.s;
            },
        });
    var se = new fe();
    function he(n, t, e) {
        var r = (e.s = n + t),
            i = r - n,
            u = r - i;
        e.t = n - u + (t - i);
    }
    function pe(n, t) {
        n && ve.hasOwnProperty(n.type) && ve[n.type](n, t);
    }
    n.geo.stream = function (n, t) {
        n && ge.hasOwnProperty(n.type) ? ge[n.type](n, t) : pe(n, t);
    };
    var ge = {
            Feature: function (n, t) {
                pe(n.geometry, t);
            },
            FeatureCollection: function (n, t) {
                for (var e = n.features, r = -1, i = e.length; ++r < i; ) pe(e[r].geometry, t);
            },
        },
        ve = {
            Sphere: function (n, t) {
                t.sphere();
            },
            Point: function (n, t) {
                (n = n.coordinates), t.point(n[0], n[1], n[2]);
            },
            MultiPoint: function (n, t) {
                for (var e = n.coordinates, r = -1, i = e.length; ++r < i; ) (n = e[r]), t.point(n[0], n[1], n[2]);
            },
            LineString: function (n, t) {
                de(n.coordinates, t, 0);
            },
            MultiLineString: function (n, t) {
                for (var e = n.coordinates, r = -1, i = e.length; ++r < i; ) de(e[r], t, 0);
            },
            Polygon: function (n, t) {
                ye(n.coordinates, t);
            },
            MultiPolygon: function (n, t) {
                for (var e = n.coordinates, r = -1, i = e.length; ++r < i; ) ye(e[r], t);
            },
            GeometryCollection: function (n, t) {
                for (var e = n.geometries, r = -1, i = e.length; ++r < i; ) pe(e[r], t);
            },
        };
    function de(n, t, e) {
        var r,
            i = -1,
            u = n.length - e;
        for (t.lineStart(); ++i < u; ) (r = n[i]), t.point(r[0], r[1], r[2]);
        t.lineEnd();
    }
    function ye(n, t) {
        var e = -1,
            r = n.length;
        for (t.polygonStart(); ++e < r; ) de(n[e], t, 1);
        t.polygonEnd();
    }
    n.geo.area = function (t) {
        return (me = 0), n.geo.stream(t, Le), me;
    };
    var me,
        Me,
        xe,
        be,
        _e,
        we,
        Se,
        ke,
        Ne,
        Ee,
        Ae,
        Ce,
        ze = new fe(),
        Le = {
            sphere: function () {
                me += 4 * kn;
            },
            point: R,
            lineStart: R,
            lineEnd: R,
            polygonStart: function () {
                ze.reset(), (Le.lineStart = qe);
            },
            polygonEnd: function () {
                var n = 2 * ze;
                (me += n < 0 ? 4 * kn + n : n), (Le.lineStart = Le.lineEnd = Le.point = R);
            },
        };
    function qe() {
        var n, t, e, r, i;
        function u(n, t) {
            t = (t * Cn) / 2 + kn / 4;
            var u = (n *= Cn) - e,
                o = u >= 0 ? 1 : -1,
                a = o * u,
                l = Math.cos(t),
                c = Math.sin(t),
                f = i * c,
                s = r * l + f * Math.cos(a),
                h = f * o * Math.sin(a);
            ze.add(Math.atan2(h, s)), (e = n), (r = l), (i = c);
        }
        (Le.point = function (o, a) {
            (Le.point = u), (e = (n = o) * Cn), (r = Math.cos((a = ((t = a) * Cn) / 2 + kn / 4))), (i = Math.sin(a));
        }),
            (Le.lineEnd = function () {
                u(n, t);
            });
    }
    function Te(n) {
        var t = n[0],
            e = n[1],
            r = Math.cos(e);
        return [r * Math.cos(t), r * Math.sin(t), Math.sin(e)];
    }
    function Re(n, t) {
        return n[0] * t[0] + n[1] * t[1] + n[2] * t[2];
    }
    function De(n, t) {
        return [n[1] * t[2] - n[2] * t[1], n[2] * t[0] - n[0] * t[2], n[0] * t[1] - n[1] * t[0]];
    }
    function Pe(n, t) {
        (n[0] += t[0]), (n[1] += t[1]), (n[2] += t[2]);
    }
    function Ue(n, t) {
        return [n[0] * t, n[1] * t, n[2] * t];
    }
    function je(n) {
        var t = Math.sqrt(n[0] * n[0] + n[1] * n[1] + n[2] * n[2]);
        (n[0] /= t), (n[1] /= t), (n[2] /= t);
    }
    function Fe(n) {
        return [Math.atan2(n[1], n[0]), Rn(n[2])];
    }
    function He(n, t) {
        return y(n[0] - t[0]) < wn && y(n[1] - t[1]) < wn;
    }
    (n.geo.bounds = (function () {
        var t,
            e,
            r,
            i,
            u,
            o,
            a,
            l,
            c,
            f,
            s,
            h = {
                point: p,
                lineStart: v,
                lineEnd: d,
                polygonStart: function () {
                    (h.point = m), (h.lineStart = M), (h.lineEnd = x), (c = 0), Le.polygonStart();
                },
                polygonEnd: function () {
                    Le.polygonEnd(), (h.point = p), (h.lineStart = v), (h.lineEnd = d), ze < 0 ? ((t = -(r = 180)), (e = -(i = 90))) : c > wn ? (i = 90) : c < -wn && (e = -90), (s[0] = t), (s[1] = r);
                },
            };
        function p(n, u) {
            f.push((s = [(t = n), (r = n)])), u < e && (e = u), u > i && (i = u);
        }
        function g(n, o) {
            var a = Te([n * Cn, o * Cn]);
            if (l) {
                var c = De(l, a),
                    f = De([c[1], -c[0], 0], c);
                je(f), (f = Fe(f));
                var s = n - u,
                    h = s > 0 ? 1 : -1,
                    g = f[0] * zn * h,
                    v = y(s) > 180;
                if (v ^ (h * u < g && g < h * n)) (d = f[1] * zn) > i && (i = d);
                else if (v ^ (h * u < (g = ((g + 360) % 360) - 180) && g < h * n)) {
                    var d;
                    (d = -f[1] * zn) < e && (e = d);
                } else o < e && (e = o), o > i && (i = o);
                v ? (n < u ? b(t, n) > b(t, r) && (r = n) : b(n, r) > b(t, r) && (t = n)) : r >= t ? (n < t && (t = n), n > r && (r = n)) : n > u ? b(t, n) > b(t, r) && (r = n) : b(n, r) > b(t, r) && (t = n);
            } else p(n, o);
            (l = a), (u = n);
        }
        function v() {
            h.point = g;
        }
        function d() {
            (s[0] = t), (s[1] = r), (h.point = p), (l = null);
        }
        function m(n, t) {
            if (l) {
                var e = n - u;
                c += y(e) > 180 ? e + (e > 0 ? 360 : -360) : e;
            } else (o = n), (a = t);
            Le.point(n, t), g(n, t);
        }
        function M() {
            Le.lineStart();
        }
        function x() {
            m(o, a), Le.lineEnd(), y(c) > wn && (t = -(r = 180)), (s[0] = t), (s[1] = r), (l = null);
        }
        function b(n, t) {
            return (t -= n) < 0 ? t + 360 : t;
        }
        function _(n, t) {
            return n[0] - t[0];
        }
        function w(n, t) {
            return t[0] <= t[1] ? t[0] <= n && n <= t[1] : n < t[0] || t[1] < n;
        }
        return function (u) {
            if (((i = r = -(t = e = 1 / 0)), (f = []), n.geo.stream(u, h), (c = f.length))) {
                f.sort(_);
                for (var o = 1, a = [(v = f[0])]; o < c; ++o) w((p = f[o])[0], v) || w(p[1], v) ? (b(v[0], p[1]) > b(v[0], v[1]) && (v[1] = p[1]), b(p[0], v[1]) > b(v[0], v[1]) && (v[0] = p[0])) : a.push((v = p));
                for (var l, c, p, g = -1 / 0, v = ((o = 0), a[(c = a.length - 1)]); o <= c; v = p, ++o) (p = a[o]), (l = b(v[1], p[0])) > g && ((g = l), (t = p[0]), (r = v[1]));
            }
            return (
                (f = s = null),
                t === 1 / 0 || e === 1 / 0
                    ? [
                          [NaN, NaN],
                          [NaN, NaN],
                      ]
                    : [
                          [t, e],
                          [r, i],
                      ]
            );
        };
    })()),
        (n.geo.centroid = function (t) {
            (Me = xe = be = _e = we = Se = ke = Ne = Ee = Ae = Ce = 0), n.geo.stream(t, Oe);
            var e = Ee,
                r = Ae,
                i = Ce,
                u = e * e + r * r + i * i;
            return u < Sn && ((e = Se), (r = ke), (i = Ne), xe < wn && ((e = be), (r = _e), (i = we)), (u = e * e + r * r + i * i) < Sn) ? [NaN, NaN] : [Math.atan2(r, e) * zn, Rn(i / Math.sqrt(u)) * zn];
        });
    var Oe = {
        sphere: R,
        point: Ie,
        lineStart: Ze,
        lineEnd: Ve,
        polygonStart: function () {
            Oe.lineStart = Xe;
        },
        polygonEnd: function () {
            Oe.lineStart = Ze;
        },
    };
    function Ie(n, t) {
        n *= Cn;
        var e = Math.cos((t *= Cn));
        Ye(e * Math.cos(n), e * Math.sin(n), Math.sin(t));
    }
    function Ye(n, t, e) {
        (be += (n - be) / ++Me), (_e += (t - _e) / Me), (we += (e - we) / Me);
    }
    function Ze() {
        var n, t, e;
        function r(r, i) {
            r *= Cn;
            var u = Math.cos((i *= Cn)),
                o = u * Math.cos(r),
                a = u * Math.sin(r),
                l = Math.sin(i),
                c = Math.atan2(Math.sqrt((c = t * l - e * a) * c + (c = e * o - n * l) * c + (c = n * a - t * o) * c), n * o + t * a + e * l);
            (xe += c), (Se += c * (n + (n = o))), (ke += c * (t + (t = a))), (Ne += c * (e + (e = l))), Ye(n, t, e);
        }
        Oe.point = function (i, u) {
            i *= Cn;
            var o = Math.cos((u *= Cn));
            (n = o * Math.cos(i)), (t = o * Math.sin(i)), (e = Math.sin(u)), (Oe.point = r), Ye(n, t, e);
        };
    }
    function Ve() {
        Oe.point = Ie;
    }
    function Xe() {
        var n, t, e, r, i;
        function u(n, t) {
            n *= Cn;
            var u = Math.cos((t *= Cn)),
                o = u * Math.cos(n),
                a = u * Math.sin(n),
                l = Math.sin(t),
                c = r * l - i * a,
                f = i * o - e * l,
                s = e * a - r * o,
                h = Math.sqrt(c * c + f * f + s * s),
                p = e * o + r * a + i * l,
                g = h && -Tn(p) / h,
                v = Math.atan2(h, p);
            (Ee += g * c), (Ae += g * f), (Ce += g * s), (xe += v), (Se += v * (e + (e = o))), (ke += v * (r + (r = a))), (Ne += v * (i + (i = l))), Ye(e, r, i);
        }
        (Oe.point = function (o, a) {
            (n = o), (t = a), (Oe.point = u), (o *= Cn);
            var l = Math.cos((a *= Cn));
            (e = l * Math.cos(o)), (r = l * Math.sin(o)), (i = Math.sin(a)), Ye(e, r, i);
        }),
            (Oe.lineEnd = function () {
                u(n, t), (Oe.lineEnd = Ve), (Oe.point = Ie);
            });
    }
    function $e(n, t) {
        function e(e, r) {
            return (e = n(e, r)), t(e[0], e[1]);
        }
        return (
            n.invert &&
                t.invert &&
                (e.invert = function (e, r) {
                    return (e = t.invert(e, r)) && n.invert(e[0], e[1]);
                }),
            e
        );
    }
    function Be() {
        return !0;
    }
    function We(n, t, e, r, i) {
        var u = [],
            o = [];
        if (
            (n.forEach(function (n) {
                if (!((t = n.length - 1) <= 0)) {
                    var t,
                        e = n[0],
                        r = n[t];
                    if (He(e, r)) {
                        i.lineStart();
                        for (var a = 0; a < t; ++a) i.point((e = n[a])[0], e[1]);
                        i.lineEnd();
                    } else {
                        var l = new Ge(e, n, null, !0),
                            c = new Ge(e, null, l, !1);
                        (l.o = c), u.push(l), o.push(c), (l = new Ge(r, n, null, !1)), (c = new Ge(r, null, l, !0)), (l.o = c), u.push(l), o.push(c);
                    }
                }
            }),
            o.sort(t),
            Je(u),
            Je(o),
            u.length)
        ) {
            for (var a = 0, l = e, c = o.length; a < c; ++a) o[a].e = l = !l;
            for (var f, s, h = u[0]; ; ) {
                for (var p = h, g = !0; p.v; ) if ((p = p.n) === h) return;
                (f = p.z), i.lineStart();
                do {
                    if (((p.v = p.o.v = !0), p.e)) {
                        if (g) for (a = 0, c = f.length; a < c; ++a) i.point((s = f[a])[0], s[1]);
                        else r(p.x, p.n.x, 1, i);
                        p = p.n;
                    } else {
                        if (g) for (a = (f = p.p.z).length - 1; a >= 0; --a) i.point((s = f[a])[0], s[1]);
                        else r(p.x, p.p.x, -1, i);
                        p = p.p;
                    }
                    (f = (p = p.o).z), (g = !g);
                } while (!p.v);
                i.lineEnd();
            }
        }
    }
    function Je(n) {
        if ((t = n.length)) {
            for (var t, e, r = 0, i = n[0]; ++r < t; ) (i.n = e = n[r]), (e.p = i), (i = e);
            (i.n = e = n[0]), (e.p = i);
        }
    }
    function Ge(n, t, e, r) {
        (this.x = n), (this.z = t), (this.o = e), (this.e = r), (this.v = !1), (this.n = this.p = null);
    }
    function Ke(t, e, r, i) {
        return function (u, o) {
            var a,
                l = e(o),
                c = u.invert(i[0], i[1]),
                f = {
                    point: s,
                    lineStart: p,
                    lineEnd: g,
                    polygonStart: function () {
                        (f.point = x), (f.lineStart = b), (f.lineEnd = _), (a = []), (v = []);
                    },
                    polygonEnd: function () {
                        (f.point = s), (f.lineStart = p), (f.lineEnd = g), (a = n.merge(a));
                        var t = (function (n, t) {
                            var e = n[0],
                                r = n[1],
                                i = [Math.sin(e), -Math.cos(e), 0],
                                u = 0,
                                o = 0;
                            ze.reset();
                            for (var a = 0, l = t.length; a < l; ++a) {
                                var c = t[a],
                                    f = c.length;
                                if (f)
                                    for (var s = c[0], h = s[0], p = s[1] / 2 + kn / 4, g = Math.sin(p), v = Math.cos(p), d = 1; ;) {
                                        d === f && (d = 0);
                                        var y = (n = c[d])[0],
                                            m = n[1] / 2 + kn / 4,
                                            M = Math.sin(m),
                                            x = Math.cos(m),
                                            b = y - h,
                                            _ = b >= 0 ? 1 : -1,
                                            w = _ * b,
                                            S = w > kn,
                                            k = g * M;
                                        if ((ze.add(Math.atan2(k * _ * Math.sin(w), v * x + k * Math.cos(w))), (u += S ? b + _ * Nn : b), S ^ (h >= e) ^ (y >= e))) {
                                            var N = De(Te(s), Te(n));
                                            je(N);
                                            var E = De(i, N);
                                            je(E);
                                            var A = (S ^ (b >= 0) ? -1 : 1) * Rn(E[2]);
                                            (r > A || (r === A && (N[0] || N[1]))) && (o += S ^ (b >= 0) ? 1 : -1);
                                        }
                                        if (!d++) break;
                                        (h = y), (g = M), (v = x), (s = n);
                                    }
                            }
                            return (u < -wn || (u < wn && ze < -wn)) ^ (1 & o);
                        })(c, v);
                        a.length ? (M || (o.polygonStart(), (M = !0)), We(a, tr, t, r, o)) : t && (M || (o.polygonStart(), (M = !0)), o.lineStart(), r(null, null, 1, o), o.lineEnd()), M && (o.polygonEnd(), (M = !1)), (a = v = null);
                    },
                    sphere: function () {
                        o.polygonStart(), o.lineStart(), r(null, null, 1, o), o.lineEnd(), o.polygonEnd();
                    },
                };
            function s(n, e) {
                var r = u(n, e);
                t((n = r[0]), (e = r[1])) && o.point(n, e);
            }
            function h(n, t) {
                var e = u(n, t);
                l.point(e[0], e[1]);
            }
            function p() {
                (f.point = h), l.lineStart();
            }
            function g() {
                (f.point = s), l.lineEnd();
            }
            var v,
                d,
                y = nr(),
                m = e(y),
                M = !1;
            function x(n, t) {
                d.push([n, t]);
                var e = u(n, t);
                m.point(e[0], e[1]);
            }
            function b() {
                m.lineStart(), (d = []);
            }
            function _() {
                x(d[0][0], d[0][1]), m.lineEnd();
                var n,
                    t = m.clean(),
                    e = y.buffer(),
                    r = e.length;
                if ((d.pop(), v.push(d), (d = null), r))
                    if (1 & t) {
                        var i,
                            u = -1;
                        if ((r = (n = e[0]).length - 1) > 0) {
                            for (M || (o.polygonStart(), (M = !0)), o.lineStart(); ++u < r; ) o.point((i = n[u])[0], i[1]);
                            o.lineEnd();
                        }
                    } else r > 1 && 2 & t && e.push(e.pop().concat(e.shift())), a.push(e.filter(Qe));
            }
            return f;
        };
    }
    function Qe(n) {
        return n.length > 1;
    }
    function nr() {
        var n,
            t = [];
        return {
            lineStart: function () {
                t.push((n = []));
            },
            point: function (t, e) {
                n.push([t, e]);
            },
            lineEnd: R,
            buffer: function () {
                var e = t;
                return (t = []), (n = null), e;
            },
            rejoin: function () {
                t.length > 1 && t.push(t.pop().concat(t.shift()));
            },
        };
    }
    function tr(n, t) {
        return ((n = n.x)[0] < 0 ? n[1] - An - wn : An - n[1]) - ((t = t.x)[0] < 0 ? t[1] - An - wn : An - t[1]);
    }
    var er = Ke(
        Be,
        function (n) {
            var t,
                e = NaN,
                r = NaN,
                i = NaN;
            return {
                lineStart: function () {
                    n.lineStart(), (t = 1);
                },
                point: function (u, o) {
                    var a = u > 0 ? kn : -kn,
                        l = y(u - e);
                    y(l - kn) < wn
                        ? (n.point(e, (r = (r + o) / 2 > 0 ? An : -An)), n.point(i, r), n.lineEnd(), n.lineStart(), n.point(a, r), n.point(u, r), (t = 0))
                        : i !== a &&
                          l >= kn &&
                          (y(e - i) < wn && (e -= i * wn),
                          y(u - a) < wn && (u -= a * wn),
                          (r = (function (n, t, e, r) {
                              var i,
                                  u,
                                  o = Math.sin(n - e);
                              return y(o) > wn ? Math.atan((Math.sin(t) * (u = Math.cos(r)) * Math.sin(e) - Math.sin(r) * (i = Math.cos(t)) * Math.sin(n)) / (i * u * o)) : (t + r) / 2;
                          })(e, r, u, o)),
                          n.point(i, r),
                          n.lineEnd(),
                          n.lineStart(),
                          n.point(a, r),
                          (t = 0)),
                        n.point((e = u), (r = o)),
                        (i = a);
                },
                lineEnd: function () {
                    n.lineEnd(), (e = r = NaN);
                },
                clean: function () {
                    return 2 - t;
                },
            };
        },
        function (n, t, e, r) {
            var i;
            if (null == n) (i = e * An), r.point(-kn, i), r.point(0, i), r.point(kn, i), r.point(kn, 0), r.point(kn, -i), r.point(0, -i), r.point(-kn, -i), r.point(-kn, 0), r.point(-kn, i);
            else if (y(n[0] - t[0]) > wn) {
                var u = n[0] < t[0] ? kn : -kn;
                (i = (e * u) / 2), r.point(-u, i), r.point(0, i), r.point(u, i);
            } else r.point(t[0], t[1]);
        },
        [-kn, -kn / 2]
    );
    function rr(n, t, e, r) {
        return function (i) {
            var u,
                o = i.a,
                a = i.b,
                l = o.x,
                c = o.y,
                f = 0,
                s = 1,
                h = a.x - l,
                p = a.y - c;
            if (((u = n - l), h || !(u > 0))) {
                if (((u /= h), h < 0)) {
                    if (u < f) return;
                    u < s && (s = u);
                } else if (h > 0) {
                    if (u > s) return;
                    u > f && (f = u);
                }
                if (((u = e - l), h || !(u < 0))) {
                    if (((u /= h), h < 0)) {
                        if (u > s) return;
                        u > f && (f = u);
                    } else if (h > 0) {
                        if (u < f) return;
                        u < s && (s = u);
                    }
                    if (((u = t - c), p || !(u > 0))) {
                        if (((u /= p), p < 0)) {
                            if (u < f) return;
                            u < s && (s = u);
                        } else if (p > 0) {
                            if (u > s) return;
                            u > f && (f = u);
                        }
                        if (((u = r - c), p || !(u < 0))) {
                            if (((u /= p), p < 0)) {
                                if (u > s) return;
                                u > f && (f = u);
                            } else if (p > 0) {
                                if (u < f) return;
                                u < s && (s = u);
                            }
                            return f > 0 && (i.a = { x: l + f * h, y: c + f * p }), s < 1 && (i.b = { x: l + s * h, y: c + s * p }), i;
                        }
                    }
                }
            }
        };
    }
    var ir = 1e9;
    function ur(t, e, r, i) {
        return function (l) {
            var c,
                f,
                s,
                h,
                p,
                g,
                v,
                d,
                y,
                m,
                M,
                x = l,
                b = nr(),
                _ = rr(t, e, r, i),
                w = {
                    point: N,
                    lineStart: function () {
                        (w.point = E), f && f.push((s = []));
                        (m = !0), (y = !1), (v = d = NaN);
                    },
                    lineEnd: function () {
                        c && (E(h, p), g && y && b.rejoin(), c.push(b.buffer()));
                        (w.point = N), y && l.lineEnd();
                    },
                    polygonStart: function () {
                        (l = b), (c = []), (f = []), (M = !0);
                    },
                    polygonEnd: function () {
                        (l = x), (c = n.merge(c));
                        var e = (function (n) {
                                for (var t = 0, e = f.length, r = n[1], i = 0; i < e; ++i)
                                    for (var u, o = 1, a = f[i], l = a.length, c = a[0]; o < l; ++o) (u = a[o]), c[1] <= r ? u[1] > r && qn(c, u, n) > 0 && ++t : u[1] <= r && qn(c, u, n) < 0 && --t, (c = u);
                                return 0 !== t;
                            })([t, i]),
                            r = M && e,
                            u = c.length;
                        (r || u) && (l.polygonStart(), r && (l.lineStart(), S(null, null, 1, l), l.lineEnd()), u && We(c, o, e, S, l), l.polygonEnd()), (c = f = s = null);
                    },
                };
            function S(n, o, l, c) {
                var f = 0,
                    s = 0;
                if (null == n || (f = u(n, l)) !== (s = u(o, l)) || (a(n, o) < 0) ^ (l > 0))
                    do {
                        c.point(0 === f || 3 === f ? t : r, f > 1 ? i : e);
                    } while ((f = (f + l + 4) % 4) !== s);
                else c.point(o[0], o[1]);
            }
            function k(n, u) {
                return t <= n && n <= r && e <= u && u <= i;
            }
            function N(n, t) {
                k(n, t) && l.point(n, t);
            }
            function E(n, t) {
                var e = k((n = Math.max(-ir, Math.min(ir, n))), (t = Math.max(-ir, Math.min(ir, t))));
                if ((f && s.push([n, t]), m)) (h = n), (p = t), (g = e), (m = !1), e && (l.lineStart(), l.point(n, t));
                else if (e && y) l.point(n, t);
                else {
                    var r = { a: { x: v, y: d }, b: { x: n, y: t } };
                    _(r) ? (y || (l.lineStart(), l.point(r.a.x, r.a.y)), l.point(r.b.x, r.b.y), e || l.lineEnd(), (M = !1)) : e && (l.lineStart(), l.point(n, t), (M = !1));
                }
                (v = n), (d = t), (y = e);
            }
            return w;
        };
        function u(n, i) {
            return y(n[0] - t) < wn ? (i > 0 ? 0 : 3) : y(n[0] - r) < wn ? (i > 0 ? 2 : 1) : y(n[1] - e) < wn ? (i > 0 ? 1 : 0) : i > 0 ? 3 : 2;
        }
        function o(n, t) {
            return a(n.x, t.x);
        }
        function a(n, t) {
            var e = u(n, 1),
                r = u(t, 1);
            return e !== r ? e - r : 0 === e ? t[1] - n[1] : 1 === e ? n[0] - t[0] : 2 === e ? n[1] - t[1] : t[0] - n[0];
        }
    }
    function or(n) {
        var t = 0,
            e = kn / 3,
            r = zr(n),
            i = r(t, e);
        return (
            (i.parallels = function (n) {
                return arguments.length ? r((t = (n[0] * kn) / 180), (e = (n[1] * kn) / 180)) : [(t / kn) * 180, (e / kn) * 180];
            }),
            i
        );
    }
    function ar(n, t) {
        var e = Math.sin(n),
            r = (e + Math.sin(t)) / 2,
            i = 1 + e * (2 * r - e),
            u = Math.sqrt(i) / r;
        function o(n, t) {
            var e = Math.sqrt(i - 2 * r * Math.sin(t)) / r;
            return [e * Math.sin((n *= r)), u - e * Math.cos(n)];
        }
        return (
            (o.invert = function (n, t) {
                var e = u - t;
                return [Math.atan2(n, e) / r, Rn((i - (n * n + e * e) * r * r) / (2 * r))];
            }),
            o
        );
    }
    (n.geo.clipExtent = function () {
        var n,
            t,
            e,
            r,
            i,
            u,
            o = {
                stream: function (n) {
                    return i && (i.valid = !1), ((i = u(n)).valid = !0), i;
                },
                extent: function (a) {
                    return arguments.length
                        ? ((u = ur((n = +a[0][0]), (t = +a[0][1]), (e = +a[1][0]), (r = +a[1][1]))), i && ((i.valid = !1), (i = null)), o)
                        : [
                              [n, t],
                              [e, r],
                          ];
                },
            };
        return o.extent([
            [0, 0],
            [960, 500],
        ]);
    }),
        ((n.geo.conicEqualArea = function () {
            return or(ar);
        }).raw = ar),
        (n.geo.albers = function () {
            return n.geo.conicEqualArea().rotate([96, 0]).center([-0.6, 38.7]).parallels([29.5, 45.5]).scale(1070);
        }),
        (n.geo.albersUsa = function () {
            var t,
                e,
                r,
                i,
                u = n.geo.albers(),
                o = n.geo.conicEqualArea().rotate([154, 0]).center([-2, 58.5]).parallels([55, 65]),
                a = n.geo.conicEqualArea().rotate([157, 0]).center([-3, 19.9]).parallels([8, 18]),
                l = {
                    point: function (n, e) {
                        t = [n, e];
                    },
                };
            function c(n) {
                var u = n[0],
                    o = n[1];
                return (t = null), e(u, o), t || (r(u, o), t) || i(u, o), t;
            }
            return (
                (c.invert = function (n) {
                    var t = u.scale(),
                        e = u.translate(),
                        r = (n[0] - e[0]) / t,
                        i = (n[1] - e[1]) / t;
                    return (i >= 0.12 && i < 0.234 && r >= -0.425 && r < -0.214 ? o : i >= 0.166 && i < 0.234 && r >= -0.214 && r < -0.115 ? a : u).invert(n);
                }),
                (c.stream = function (n) {
                    var t = u.stream(n),
                        e = o.stream(n),
                        r = a.stream(n);
                    return {
                        point: function (n, i) {
                            t.point(n, i), e.point(n, i), r.point(n, i);
                        },
                        sphere: function () {
                            t.sphere(), e.sphere(), r.sphere();
                        },
                        lineStart: function () {
                            t.lineStart(), e.lineStart(), r.lineStart();
                        },
                        lineEnd: function () {
                            t.lineEnd(), e.lineEnd(), r.lineEnd();
                        },
                        polygonStart: function () {
                            t.polygonStart(), e.polygonStart(), r.polygonStart();
                        },
                        polygonEnd: function () {
                            t.polygonEnd(), e.polygonEnd(), r.polygonEnd();
                        },
                    };
                }),
                (c.precision = function (n) {
                    return arguments.length ? (u.precision(n), o.precision(n), a.precision(n), c) : u.precision();
                }),
                (c.scale = function (n) {
                    return arguments.length ? (u.scale(n), o.scale(0.35 * n), a.scale(n), c.translate(u.translate())) : u.scale();
                }),
                (c.translate = function (n) {
                    if (!arguments.length) return u.translate();
                    var t = u.scale(),
                        f = +n[0],
                        s = +n[1];
                    return (
                        (e = u
                            .translate(n)
                            .clipExtent([
                                [f - 0.455 * t, s - 0.238 * t],
                                [f + 0.455 * t, s + 0.238 * t],
                            ])
                            .stream(l).point),
                        (r = o
                            .translate([f - 0.307 * t, s + 0.201 * t])
                            .clipExtent([
                                [f - 0.425 * t + wn, s + 0.12 * t + wn],
                                [f - 0.214 * t - wn, s + 0.234 * t - wn],
                            ])
                            .stream(l).point),
                        (i = a
                            .translate([f - 0.205 * t, s + 0.212 * t])
                            .clipExtent([
                                [f - 0.214 * t + wn, s + 0.166 * t + wn],
                                [f - 0.115 * t - wn, s + 0.234 * t - wn],
                            ])
                            .stream(l).point),
                        c
                    );
                }),
                c.scale(1070)
            );
        });
    var lr,
        cr,
        fr,
        sr,
        hr,
        pr,
        gr = {
            point: R,
            lineStart: R,
            lineEnd: R,
            polygonStart: function () {
                (cr = 0), (gr.lineStart = vr);
            },
            polygonEnd: function () {
                (gr.lineStart = gr.lineEnd = gr.point = R), (lr += y(cr / 2));
            },
        };
    function vr() {
        var n, t, e, r;
        function i(n, t) {
            (cr += r * n - e * t), (e = n), (r = t);
        }
        (gr.point = function (u, o) {
            (gr.point = i), (n = e = u), (t = r = o);
        }),
            (gr.lineEnd = function () {
                i(n, t);
            });
    }
    var dr = {
        point: function (n, t) {
            n < fr && (fr = n);
            n > hr && (hr = n);
            t < sr && (sr = t);
            t > pr && (pr = t);
        },
        lineStart: R,
        lineEnd: R,
        polygonStart: R,
        polygonEnd: R,
    };
    function yr() {
        var n = mr(4.5),
            t = [],
            e = {
                point: r,
                lineStart: function () {
                    e.point = i;
                },
                lineEnd: o,
                polygonStart: function () {
                    e.lineEnd = a;
                },
                polygonEnd: function () {
                    (e.lineEnd = o), (e.point = r);
                },
                pointRadius: function (t) {
                    return (n = mr(t)), e;
                },
                result: function () {
                    if (t.length) {
                        var n = t.join("");
                        return (t = []), n;
                    }
                },
            };
        function r(e, r) {
            t.push("M", e, ",", r, n);
        }
        function i(n, r) {
            t.push("M", n, ",", r), (e.point = u);
        }
        function u(n, e) {
            t.push("L", n, ",", e);
        }
        function o() {
            e.point = r;
        }
        function a() {
            t.push("Z");
        }
        return e;
    }
    function mr(n) {
        return "m0," + n + "a" + n + "," + n + " 0 1,1 0," + -2 * n + "a" + n + "," + n + " 0 1,1 0," + 2 * n + "z";
    }
    var Mr,
        xr = {
            point: br,
            lineStart: _r,
            lineEnd: wr,
            polygonStart: function () {
                xr.lineStart = Sr;
            },
            polygonEnd: function () {
                (xr.point = br), (xr.lineStart = _r), (xr.lineEnd = wr);
            },
        };
    function br(n, t) {
        (be += n), (_e += t), ++we;
    }
    function _r() {
        var n, t;
        function e(e, r) {
            var i = e - n,
                u = r - t,
                o = Math.sqrt(i * i + u * u);
            (Se += (o * (n + e)) / 2), (ke += (o * (t + r)) / 2), (Ne += o), br((n = e), (t = r));
        }
        xr.point = function (r, i) {
            (xr.point = e), br((n = r), (t = i));
        };
    }
    function wr() {
        xr.point = br;
    }
    function Sr() {
        var n, t, e, r;
        function i(n, t) {
            var i = n - e,
                u = t - r,
                o = Math.sqrt(i * i + u * u);
            (Se += (o * (e + n)) / 2), (ke += (o * (r + t)) / 2), (Ne += o), (Ee += (o = r * n - e * t) * (e + n)), (Ae += o * (r + t)), (Ce += 3 * o), br((e = n), (r = t));
        }
        (xr.point = function (u, o) {
            (xr.point = i), br((n = e = u), (t = r = o));
        }),
            (xr.lineEnd = function () {
                i(n, t);
            });
    }
    function kr(n) {
        var t = 4.5,
            e = {
                point: r,
                lineStart: function () {
                    e.point = i;
                },
                lineEnd: o,
                polygonStart: function () {
                    e.lineEnd = a;
                },
                polygonEnd: function () {
                    (e.lineEnd = o), (e.point = r);
                },
                pointRadius: function (n) {
                    return (t = n), e;
                },
                result: R,
            };
        function r(e, r) {
            n.moveTo(e + t, r), n.arc(e, r, t, 0, Nn);
        }
        function i(t, r) {
            n.moveTo(t, r), (e.point = u);
        }
        function u(t, e) {
            n.lineTo(t, e);
        }
        function o() {
            e.point = r;
        }
        function a() {
            n.closePath();
        }
        return e;
    }
    function Nr(n) {
        var t = 0.5,
            e = Math.cos(30 * Cn),
            r = 16;
        function i(t) {
            return (r
                ? function (t) {
                      var e,
                          i,
                          o,
                          a,
                          l,
                          c,
                          f,
                          s,
                          h,
                          p,
                          g,
                          v,
                          d = {
                              point: y,
                              lineStart: m,
                              lineEnd: x,
                              polygonStart: function () {
                                  t.polygonStart(), (d.lineStart = b);
                              },
                              polygonEnd: function () {
                                  t.polygonEnd(), (d.lineStart = m);
                              },
                          };
                      function y(e, r) {
                          (e = n(e, r)), t.point(e[0], e[1]);
                      }
                      function m() {
                          (s = NaN), (d.point = M), t.lineStart();
                      }
                      function M(e, i) {
                          var o = Te([e, i]),
                              a = n(e, i);
                          u(s, h, f, p, g, v, (s = a[0]), (h = a[1]), (f = e), (p = o[0]), (g = o[1]), (v = o[2]), r, t), t.point(s, h);
                      }
                      function x() {
                          (d.point = y), t.lineEnd();
                      }
                      function b() {
                          m(), (d.point = _), (d.lineEnd = w);
                      }
                      function _(n, t) {
                          M((e = n), t), (i = s), (o = h), (a = p), (l = g), (c = v), (d.point = M);
                      }
                      function w() {
                          u(s, h, f, p, g, v, i, o, e, a, l, c, r, t), (d.lineEnd = x), x();
                      }
                      return d;
                  }
                : function (t) {
                      return Ar(t, function (e, r) {
                          (e = n(e, r)), t.point(e[0], e[1]);
                      });
                  })(t);
        }
        function u(r, i, o, a, l, c, f, s, h, p, g, v, d, m) {
            var M = f - r,
                x = s - i,
                b = M * M + x * x;
            if (b > 4 * t && d--) {
                var _ = a + p,
                    w = l + g,
                    S = c + v,
                    k = Math.sqrt(_ * _ + w * w + S * S),
                    N = Math.asin((S /= k)),
                    E = y(y(S) - 1) < wn || y(o - h) < wn ? (o + h) / 2 : Math.atan2(w, _),
                    A = n(E, N),
                    C = A[0],
                    z = A[1],
                    L = C - r,
                    q = z - i,
                    T = x * L - M * q;
                ((T * T) / b > t || y((M * L + x * q) / b - 0.5) > 0.3 || a * p + l * g + c * v < e) && (u(r, i, o, a, l, c, C, z, E, (_ /= k), (w /= k), S, d, m), m.point(C, z), u(C, z, E, _, w, S, f, s, h, p, g, v, d, m));
            }
        }
        return (
            (i.precision = function (n) {
                return arguments.length ? ((r = (t = n * n) > 0 && 16), i) : Math.sqrt(t);
            }),
            i
        );
    }
    function Er(n) {
        this.stream = n;
    }
    function Ar(n, t) {
        return {
            point: t,
            sphere: function () {
                n.sphere();
            },
            lineStart: function () {
                n.lineStart();
            },
            lineEnd: function () {
                n.lineEnd();
            },
            polygonStart: function () {
                n.polygonStart();
            },
            polygonEnd: function () {
                n.polygonEnd();
            },
        };
    }
    function Cr(n) {
        return zr(function () {
            return n;
        })();
    }
    function zr(t) {
        var e,
            r,
            i,
            u,
            o,
            a,
            l = Nr(function (n, t) {
                return [(n = e(n, t))[0] * c + u, o - n[1] * c];
            }),
            c = 150,
            f = 480,
            s = 250,
            h = 0,
            p = 0,
            g = 0,
            v = 0,
            d = 0,
            m = er,
            M = z,
            x = null,
            b = null;
        function _(n) {
            return [(n = i(n[0] * Cn, n[1] * Cn))[0] * c + u, o - n[1] * c];
        }
        function w(n) {
            return (n = i.invert((n[0] - u) / c, (o - n[1]) / c)) && [n[0] * zn, n[1] * zn];
        }
        function S() {
            i = $e((r = Rr(g, v, d)), e);
            var n = e(h, p);
            return (u = f - n[0] * c), (o = s + n[1] * c), k();
        }
        function k() {
            return a && ((a.valid = !1), (a = null)), _;
        }
        return (
            (_.stream = function (n) {
                return a && (a.valid = !1), ((a = Lr(m(r, l(M(n))))).valid = !0), a;
            }),
            (_.clipAngle = function (n) {
                return arguments.length
                    ? ((m =
                          null == n
                              ? ((x = n), er)
                              : (function (n) {
                                    var t = Math.cos(n),
                                        e = t > 0,
                                        r = y(t) > wn;
                                    return Ke(
                                        i,
                                        function (n) {
                                            var t, a, l, c, f;
                                            return {
                                                lineStart: function () {
                                                    (c = l = !1), (f = 1);
                                                },
                                                point: function (s, h) {
                                                    var p,
                                                        g = [s, h],
                                                        v = i(s, h),
                                                        d = e ? (v ? 0 : o(s, h)) : v ? o(s + (s < 0 ? kn : -kn), h) : 0;
                                                    if ((!t && (c = l = v) && n.lineStart(), v !== l && ((p = u(t, g)), (He(t, p) || He(g, p)) && ((g[0] += wn), (g[1] += wn), (v = i(g[0], g[1])))), v !== l))
                                                        (f = 0), v ? (n.lineStart(), (p = u(g, t)), n.point(p[0], p[1])) : ((p = u(t, g)), n.point(p[0], p[1]), n.lineEnd()), (t = p);
                                                    else if (r && t && e ^ v) {
                                                        var y;
                                                        d & a ||
                                                            !(y = u(g, t, !0)) ||
                                                            ((f = 0),
                                                            e ? (n.lineStart(), n.point(y[0][0], y[0][1]), n.point(y[1][0], y[1][1]), n.lineEnd()) : (n.point(y[1][0], y[1][1]), n.lineEnd(), n.lineStart(), n.point(y[0][0], y[0][1])));
                                                    }
                                                    !v || (t && He(t, g)) || n.point(g[0], g[1]), (t = g), (l = v), (a = d);
                                                },
                                                lineEnd: function () {
                                                    l && n.lineEnd(), (t = null);
                                                },
                                                clean: function () {
                                                    return f | ((c && l) << 1);
                                                },
                                            };
                                        },
                                        jr(n, 6 * Cn),
                                        e ? [0, -n] : [-kn, n - kn]
                                    );
                                    function i(n, e) {
                                        return Math.cos(n) * Math.cos(e) > t;
                                    }
                                    function u(n, e, r) {
                                        var i = [1, 0, 0],
                                            u = De(Te(n), Te(e)),
                                            o = Re(u, u),
                                            a = u[0],
                                            l = o - a * a;
                                        if (!l) return !r && n;
                                        var c = (t * o) / l,
                                            f = (-t * a) / l,
                                            s = De(i, u),
                                            h = Ue(i, c);
                                        Pe(h, Ue(u, f));
                                        var p = s,
                                            g = Re(h, p),
                                            v = Re(p, p),
                                            d = g * g - v * (Re(h, h) - 1);
                                        if (!(d < 0)) {
                                            var m = Math.sqrt(d),
                                                M = Ue(p, (-g - m) / v);
                                            if ((Pe(M, h), (M = Fe(M)), !r)) return M;
                                            var x,
                                                b = n[0],
                                                _ = e[0],
                                                w = n[1],
                                                S = e[1];
                                            _ < b && ((x = b), (b = _), (_ = x));
                                            var k = _ - b,
                                                N = y(k - kn) < wn;
                                            if ((!N && S < w && ((x = w), (w = S), (S = x)), N || k < wn ? (N ? (w + S > 0) ^ (M[1] < (y(M[0] - b) < wn ? w : S)) : w <= M[1] && M[1] <= S) : (k > kn) ^ (b <= M[0] && M[0] <= _))) {
                                                var E = Ue(p, (-g + m) / v);
                                                return Pe(E, h), [M, Fe(E)];
                                            }
                                        }
                                    }
                                    function o(t, r) {
                                        var i = e ? n : kn - n,
                                            u = 0;
                                        return t < -i ? (u |= 1) : t > i && (u |= 2), r < -i ? (u |= 4) : r > i && (u |= 8), u;
                                    }
                                })((x = +n) * Cn)),
                      k())
                    : x;
            }),
            (_.clipExtent = function (n) {
                return arguments.length ? ((b = n), (M = n ? ur(n[0][0], n[0][1], n[1][0], n[1][1]) : z), k()) : b;
            }),
            (_.scale = function (n) {
                return arguments.length ? ((c = +n), S()) : c;
            }),
            (_.translate = function (n) {
                return arguments.length ? ((f = +n[0]), (s = +n[1]), S()) : [f, s];
            }),
            (_.center = function (n) {
                return arguments.length ? ((h = (n[0] % 360) * Cn), (p = (n[1] % 360) * Cn), S()) : [h * zn, p * zn];
            }),
            (_.rotate = function (n) {
                return arguments.length ? ((g = (n[0] % 360) * Cn), (v = (n[1] % 360) * Cn), (d = n.length > 2 ? (n[2] % 360) * Cn : 0), S()) : [g * zn, v * zn, d * zn];
            }),
            n.rebind(_, l, "precision"),
            function () {
                return (e = t.apply(this, arguments)), (_.invert = e.invert && w), S();
            }
        );
    }
    function Lr(n) {
        return Ar(n, function (t, e) {
            n.point(t * Cn, e * Cn);
        });
    }
    function qr(n, t) {
        return [n, t];
    }
    function Tr(n, t) {
        return [n > kn ? n - Nn : n < -kn ? n + Nn : n, t];
    }
    function Rr(n, t, e) {
        return n ? (t || e ? $e(Pr(n), Ur(t, e)) : Pr(n)) : t || e ? Ur(t, e) : Tr;
    }
    function Dr(n) {
        return function (t, e) {
            return [(t += n) > kn ? t - Nn : t < -kn ? t + Nn : t, e];
        };
    }
    function Pr(n) {
        var t = Dr(n);
        return (t.invert = Dr(-n)), t;
    }
    function Ur(n, t) {
        var e = Math.cos(n),
            r = Math.sin(n),
            i = Math.cos(t),
            u = Math.sin(t);
        function o(n, t) {
            var o = Math.cos(t),
                a = Math.cos(n) * o,
                l = Math.sin(n) * o,
                c = Math.sin(t),
                f = c * e + a * r;
            return [Math.atan2(l * i - f * u, a * e - c * r), Rn(f * i + l * u)];
        }
        return (
            (o.invert = function (n, t) {
                var o = Math.cos(t),
                    a = Math.cos(n) * o,
                    l = Math.sin(n) * o,
                    c = Math.sin(t),
                    f = c * i - l * u;
                return [Math.atan2(l * i + c * u, a * e + f * r), Rn(f * e - a * r)];
            }),
            o
        );
    }
    function jr(n, t) {
        var e = Math.cos(n),
            r = Math.sin(n);
        return function (i, u, o, a) {
            var l = o * t;
            null != i ? ((i = Fr(e, i)), (u = Fr(e, u)), (o > 0 ? i < u : i > u) && (i += o * Nn)) : ((i = n + o * Nn), (u = n - 0.5 * l));
            for (var c, f = i; o > 0 ? f > u : f < u; f -= l) a.point((c = Fe([e, -r * Math.cos(f), -r * Math.sin(f)]))[0], c[1]);
        };
    }
    function Fr(n, t) {
        var e = Te(t);
        (e[0] -= n), je(e);
        var r = Tn(-e[1]);
        return ((-e[2] < 0 ? -r : r) + 2 * Math.PI - wn) % (2 * Math.PI);
    }
    function Hr(t, e, r) {
        var i = n.range(t, e - wn, r).concat(e);
        return function (n) {
            return i.map(function (t) {
                return [n, t];
            });
        };
    }
    function Or(t, e, r) {
        var i = n.range(t, e - wn, r).concat(e);
        return function (n) {
            return i.map(function (t) {
                return [t, n];
            });
        };
    }
    function Ir(n) {
        return n.source;
    }
    function Yr(n) {
        return n.target;
    }
    (n.geo.path = function () {
        var t,
            e,
            r,
            i,
            u,
            o = 4.5;
        function a(t) {
            return t && ("function" == typeof o && i.pointRadius(+o.apply(this, arguments)), (u && u.valid) || (u = r(i)), n.geo.stream(t, u)), i.result();
        }
        function l() {
            return (u = null), a;
        }
        return (
            (a.area = function (t) {
                return (lr = 0), n.geo.stream(t, r(gr)), lr;
            }),
            (a.centroid = function (t) {
                return (be = _e = we = Se = ke = Ne = Ee = Ae = Ce = 0), n.geo.stream(t, r(xr)), Ce ? [Ee / Ce, Ae / Ce] : Ne ? [Se / Ne, ke / Ne] : we ? [be / we, _e / we] : [NaN, NaN];
            }),
            (a.bounds = function (t) {
                return (
                    (hr = pr = -(fr = sr = 1 / 0)),
                    n.geo.stream(t, r(dr)),
                    [
                        [fr, sr],
                        [hr, pr],
                    ]
                );
            }),
            (a.projection = function (n) {
                return arguments.length
                    ? ((r = (t = n)
                          ? n.stream ||
                            ((e = n),
                            (i = Nr(function (n, t) {
                                return e([n * zn, t * zn]);
                            })),
                            function (n) {
                                return Lr(i(n));
                            })
                          : z),
                      l())
                    : t;
                var e, i;
            }),
            (a.context = function (n) {
                return arguments.length ? ((i = null == (e = n) ? new yr() : new kr(n)), "function" != typeof o && i.pointRadius(o), l()) : e;
            }),
            (a.pointRadius = function (n) {
                return arguments.length ? ((o = "function" == typeof n ? n : (i.pointRadius(+n), +n)), a) : o;
            }),
            a.projection(n.geo.albersUsa()).context(null)
        );
    }),
        (n.geo.transform = function (n) {
            return {
                stream: function (t) {
                    var e = new Er(t);
                    for (var r in n) e[r] = n[r];
                    return e;
                },
            };
        }),
        (Er.prototype = {
            point: function (n, t) {
                this.stream.point(n, t);
            },
            sphere: function () {
                this.stream.sphere();
            },
            lineStart: function () {
                this.stream.lineStart();
            },
            lineEnd: function () {
                this.stream.lineEnd();
            },
            polygonStart: function () {
                this.stream.polygonStart();
            },
            polygonEnd: function () {
                this.stream.polygonEnd();
            },
        }),
        (n.geo.projection = Cr),
        (n.geo.projectionMutator = zr),
        ((n.geo.equirectangular = function () {
            return Cr(qr);
        }).raw = qr.invert = qr),
        (n.geo.rotation = function (n) {
            function t(t) {
                return ((t = n(t[0] * Cn, t[1] * Cn))[0] *= zn), (t[1] *= zn), t;
            }
            return (
                (n = Rr((n[0] % 360) * Cn, n[1] * Cn, n.length > 2 ? n[2] * Cn : 0)),
                (t.invert = function (t) {
                    return ((t = n.invert(t[0] * Cn, t[1] * Cn))[0] *= zn), (t[1] *= zn), t;
                }),
                t
            );
        }),
        (Tr.invert = qr),
        (n.geo.circle = function () {
            var n,
                t,
                e = [0, 0],
                r = 6;
            function i() {
                var n = "function" == typeof e ? e.apply(this, arguments) : e,
                    r = Rr(-n[0] * Cn, -n[1] * Cn, 0).invert,
                    i = [];
                return (
                    t(null, null, 1, {
                        point: function (n, t) {
                            i.push((n = r(n, t))), (n[0] *= zn), (n[1] *= zn);
                        },
                    }),
                    { type: "Polygon", coordinates: [i] }
                );
            }
            return (
                (i.origin = function (n) {
                    return arguments.length ? ((e = n), i) : e;
                }),
                (i.angle = function (e) {
                    return arguments.length ? ((t = jr((n = +e) * Cn, r * Cn)), i) : n;
                }),
                (i.precision = function (e) {
                    return arguments.length ? ((t = jr(n * Cn, (r = +e) * Cn)), i) : r;
                }),
                i.angle(90)
            );
        }),
        (n.geo.distance = function (n, t) {
            var e,
                r = (t[0] - n[0]) * Cn,
                i = n[1] * Cn,
                u = t[1] * Cn,
                o = Math.sin(r),
                a = Math.cos(r),
                l = Math.sin(i),
                c = Math.cos(i),
                f = Math.sin(u),
                s = Math.cos(u);
            return Math.atan2(Math.sqrt((e = s * o) * e + (e = c * f - l * s * a) * e), l * f + c * s * a);
        }),
        (n.geo.graticule = function () {
            var t,
                e,
                r,
                i,
                u,
                o,
                a,
                l,
                c,
                f,
                s,
                h,
                p = 10,
                g = p,
                v = 90,
                d = 360,
                m = 2.5;
            function M() {
                return { type: "MultiLineString", coordinates: x() };
            }
            function x() {
                return n
                    .range(Math.ceil(i / v) * v, r, v)
                    .map(s)
                    .concat(n.range(Math.ceil(l / d) * d, a, d).map(h))
                    .concat(
                        n
                            .range(Math.ceil(e / p) * p, t, p)
                            .filter(function (n) {
                                return y(n % v) > wn;
                            })
                            .map(c)
                    )
                    .concat(
                        n
                            .range(Math.ceil(o / g) * g, u, g)
                            .filter(function (n) {
                                return y(n % d) > wn;
                            })
                            .map(f)
                    );
            }
            return (
                (M.lines = function () {
                    return x().map(function (n) {
                        return { type: "LineString", coordinates: n };
                    });
                }),
                (M.outline = function () {
                    return { type: "Polygon", coordinates: [s(i).concat(h(a).slice(1), s(r).reverse().slice(1), h(l).reverse().slice(1))] };
                }),
                (M.extent = function (n) {
                    return arguments.length ? M.majorExtent(n).minorExtent(n) : M.minorExtent();
                }),
                (M.majorExtent = function (n) {
                    return arguments.length
                        ? ((i = +n[0][0]), (r = +n[1][0]), (l = +n[0][1]), (a = +n[1][1]), i > r && ((n = i), (i = r), (r = n)), l > a && ((n = l), (l = a), (a = n)), M.precision(m))
                        : [
                              [i, l],
                              [r, a],
                          ];
                }),
                (M.minorExtent = function (n) {
                    return arguments.length
                        ? ((e = +n[0][0]), (t = +n[1][0]), (o = +n[0][1]), (u = +n[1][1]), e > t && ((n = e), (e = t), (t = n)), o > u && ((n = o), (o = u), (u = n)), M.precision(m))
                        : [
                              [e, o],
                              [t, u],
                          ];
                }),
                (M.step = function (n) {
                    return arguments.length ? M.majorStep(n).minorStep(n) : M.minorStep();
                }),
                (M.majorStep = function (n) {
                    return arguments.length ? ((v = +n[0]), (d = +n[1]), M) : [v, d];
                }),
                (M.minorStep = function (n) {
                    return arguments.length ? ((p = +n[0]), (g = +n[1]), M) : [p, g];
                }),
                (M.precision = function (n) {
                    return arguments.length ? ((m = +n), (c = Hr(o, u, 90)), (f = Or(e, t, m)), (s = Hr(l, a, 90)), (h = Or(i, r, m)), M) : m;
                }),
                M.majorExtent([
                    [-180, -90 + wn],
                    [180, 90 - wn],
                ]).minorExtent([
                    [-180, -80 - wn],
                    [180, 80 + wn],
                ])
            );
        }),
        (n.geo.greatArc = function () {
            var t,
                e,
                r = Ir,
                i = Yr;
            function u() {
                return { type: "LineString", coordinates: [t || r.apply(this, arguments), e || i.apply(this, arguments)] };
            }
            return (
                (u.distance = function () {
                    return n.geo.distance(t || r.apply(this, arguments), e || i.apply(this, arguments));
                }),
                (u.source = function (n) {
                    return arguments.length ? ((r = n), (t = "function" == typeof n ? null : n), u) : r;
                }),
                (u.target = function (n) {
                    return arguments.length ? ((i = n), (e = "function" == typeof n ? null : n), u) : i;
                }),
                (u.precision = function () {
                    return arguments.length ? u : 0;
                }),
                u
            );
        }),
        (n.geo.interpolate = function (n, t) {
            return (
                (e = n[0] * Cn),
                (r = n[1] * Cn),
                (i = t[0] * Cn),
                (u = t[1] * Cn),
                (o = Math.cos(r)),
                (a = Math.sin(r)),
                (l = Math.cos(u)),
                (c = Math.sin(u)),
                (f = o * Math.cos(e)),
                (s = o * Math.sin(e)),
                (h = l * Math.cos(i)),
                (p = l * Math.sin(i)),
                (g = 2 * Math.asin(Math.sqrt(Pn(u - r) + o * l * Pn(i - e)))),
                (v = 1 / Math.sin(g)),
                ((d = g
                    ? function (n) {
                          var t = Math.sin((n *= g)) * v,
                              e = Math.sin(g - n) * v,
                              r = e * f + t * h,
                              i = e * s + t * p,
                              u = e * a + t * c;
                          return [Math.atan2(i, r) * zn, Math.atan2(u, Math.sqrt(r * r + i * i)) * zn];
                      }
                    : function () {
                          return [e * zn, r * zn];
                      }).distance = g),
                d
            );
            var e, r, i, u, o, a, l, c, f, s, h, p, g, v, d;
        }),
        (n.geo.length = function (t) {
            return (Mr = 0), n.geo.stream(t, Zr), Mr;
        });
    var Zr = {
        sphere: R,
        point: R,
        lineStart: function () {
            var n, t, e;
            function r(r, i) {
                var u = Math.sin((i *= Cn)),
                    o = Math.cos(i),
                    a = y((r *= Cn) - n),
                    l = Math.cos(a);
                (Mr += Math.atan2(Math.sqrt((a = o * Math.sin(a)) * a + (a = e * u - t * o * l) * a), t * u + e * o * l)), (n = r), (t = u), (e = o);
            }
            (Zr.point = function (i, u) {
                (n = i * Cn), (t = Math.sin((u *= Cn))), (e = Math.cos(u)), (Zr.point = r);
            }),
                (Zr.lineEnd = function () {
                    Zr.point = Zr.lineEnd = R;
                });
        },
        lineEnd: R,
        polygonStart: R,
        polygonEnd: R,
    };
    function Vr(n, t) {
        function e(t, e) {
            var r = Math.cos(t),
                i = Math.cos(e),
                u = n(r * i);
            return [u * i * Math.sin(t), u * Math.sin(e)];
        }
        return (
            (e.invert = function (n, e) {
                var r = Math.sqrt(n * n + e * e),
                    i = t(r),
                    u = Math.sin(i),
                    o = Math.cos(i);
                return [Math.atan2(n * u, r * o), Math.asin(r && (e * u) / r)];
            }),
            e
        );
    }
    var Xr = Vr(
        function (n) {
            return Math.sqrt(2 / (1 + n));
        },
        function (n) {
            return 2 * Math.asin(n / 2);
        }
    );
    (n.geo.azimuthalEqualArea = function () {
        return Cr(Xr);
    }).raw = Xr;
    var $r = Vr(function (n) {
        var t = Math.acos(n);
        return t && t / Math.sin(t);
    }, z);
    function Br(n, t) {
        var e = Math.cos(n),
            r = function (n) {
                return Math.tan(kn / 4 + n / 2);
            },
            i = n === t ? Math.sin(n) : Math.log(e / Math.cos(t)) / Math.log(r(t) / r(n)),
            u = (e * Math.pow(r(n), i)) / i;
        if (!i) return Gr;
        function o(n, t) {
            u > 0 ? t < -An + wn && (t = -An + wn) : t > An - wn && (t = An - wn);
            var e = u / Math.pow(r(t), i);
            return [e * Math.sin(i * n), u - e * Math.cos(i * n)];
        }
        return (
            (o.invert = function (n, t) {
                var e = u - t,
                    r = Ln(i) * Math.sqrt(n * n + e * e);
                return [Math.atan2(n, e) / i, 2 * Math.atan(Math.pow(u / r, 1 / i)) - An];
            }),
            o
        );
    }
    function Wr(n, t) {
        var e = Math.cos(n),
            r = n === t ? Math.sin(n) : (e - Math.cos(t)) / (t - n),
            i = e / r + n;
        if (y(r) < wn) return qr;
        function u(n, t) {
            var e = i - t;
            return [e * Math.sin(r * n), i - e * Math.cos(r * n)];
        }
        return (
            (u.invert = function (n, t) {
                var e = i - t;
                return [Math.atan2(n, e) / r, i - Ln(r) * Math.sqrt(n * n + e * e)];
            }),
            u
        );
    }
    ((n.geo.azimuthalEquidistant = function () {
        return Cr($r);
    }).raw = $r),
        ((n.geo.conicConformal = function () {
            return or(Br);
        }).raw = Br),
        ((n.geo.conicEquidistant = function () {
            return or(Wr);
        }).raw = Wr);
    var Jr = Vr(function (n) {
        return 1 / n;
    }, Math.atan);
    function Gr(n, t) {
        return [n, Math.log(Math.tan(kn / 4 + t / 2))];
    }
    function Kr(n) {
        var t,
            e = Cr(n),
            r = e.scale,
            i = e.translate,
            u = e.clipExtent;
        return (
            (e.scale = function () {
                var n = r.apply(e, arguments);
                return n === e ? (t ? e.clipExtent(null) : e) : n;
            }),
            (e.translate = function () {
                var n = i.apply(e, arguments);
                return n === e ? (t ? e.clipExtent(null) : e) : n;
            }),
            (e.clipExtent = function (n) {
                var o = u.apply(e, arguments);
                if (o === e) {
                    if ((t = null == n)) {
                        var a = kn * r(),
                            l = i();
                        u([
                            [l[0] - a, l[1] - a],
                            [l[0] + a, l[1] + a],
                        ]);
                    }
                } else t && (o = null);
                return o;
            }),
            e.clipExtent(null)
        );
    }
    ((n.geo.gnomonic = function () {
        return Cr(Jr);
    }).raw = Jr),
        (Gr.invert = function (n, t) {
            return [n, 2 * Math.atan(Math.exp(t)) - An];
        }),
        ((n.geo.mercator = function () {
            return Kr(Gr);
        }).raw = Gr);
    var Qr = Vr(function () {
        return 1;
    }, Math.asin);
    (n.geo.orthographic = function () {
        return Cr(Qr);
    }).raw = Qr;
    var ni = Vr(
        function (n) {
            return 1 / (1 + n);
        },
        function (n) {
            return 2 * Math.atan(n);
        }
    );
    function ti(n, t) {
        return [Math.log(Math.tan(kn / 4 + t / 2)), -n];
    }
    function ei(n) {
        return n[0];
    }
    function ri(n) {
        return n[1];
    }
    function ii(n) {
        for (var t = n.length, e = [0, 1], r = 2, i = 2; i < t; i++) {
            for (; r > 1 && qn(n[e[r - 2]], n[e[r - 1]], n[i]) <= 0; ) --r;
            e[r++] = i;
        }
        return e.slice(0, r);
    }
    function ui(n, t) {
        return n[0] - t[0] || n[1] - t[1];
    }
    ((n.geo.stereographic = function () {
        return Cr(ni);
    }).raw = ni),
        (ti.invert = function (n, t) {
            return [-t, 2 * Math.atan(Math.exp(n)) - An];
        }),
        ((n.geo.transverseMercator = function () {
            var n = Kr(ti),
                t = n.center,
                e = n.rotate;
            return (
                (n.center = function (n) {
                    return n ? t([-n[1], n[0]]) : [(n = t())[1], -n[0]];
                }),
                (n.rotate = function (n) {
                    return n ? e([n[0], n[1], n.length > 2 ? n[2] + 90 : 90]) : [(n = e())[0], n[1], n[2] - 90];
                }),
                e([0, 0, 90])
            );
        }).raw = ti),
        (n.geom = {}),
        (n.geom.hull = function (n) {
            var t = ei,
                e = ri;
            if (arguments.length) return r(n);
            function r(n) {
                if (n.length < 3) return [];
                var r,
                    i = dt(t),
                    u = dt(e),
                    o = n.length,
                    a = [],
                    l = [];
                for (r = 0; r < o; r++) a.push([+i.call(this, n[r], r), +u.call(this, n[r], r), r]);
                for (a.sort(ui), r = 0; r < o; r++) l.push([a[r][0], -a[r][1]]);
                var c = ii(a),
                    f = ii(l),
                    s = f[0] === c[0],
                    h = f[f.length - 1] === c[c.length - 1],
                    p = [];
                for (r = c.length - 1; r >= 0; --r) p.push(n[a[c[r]][2]]);
                for (r = +s; r < f.length - h; ++r) p.push(n[a[f[r]][2]]);
                return p;
            }
            return (
                (r.x = function (n) {
                    return arguments.length ? ((t = n), r) : t;
                }),
                (r.y = function (n) {
                    return arguments.length ? ((e = n), r) : e;
                }),
                r
            );
        }),
        (n.geom.polygon = function (n) {
            return O(n, oi), n;
        });
    var oi = (n.geom.polygon.prototype = []);
    function ai(n, t, e) {
        return (e[0] - t[0]) * (n[1] - t[1]) < (e[1] - t[1]) * (n[0] - t[0]);
    }
    function li(n, t, e, r) {
        var i = n[0],
            u = e[0],
            o = t[0] - i,
            a = r[0] - u,
            l = n[1],
            c = e[1],
            f = t[1] - l,
            s = r[1] - c,
            h = (a * (l - c) - s * (i - u)) / (s * o - a * f);
        return [i + h * o, l + h * f];
    }
    function ci(n) {
        var t = n[0],
            e = n[n.length - 1];
        return !(t[0] - e[0] || t[1] - e[1]);
    }
    (oi.area = function () {
        for (var n, t = -1, e = this.length, r = this[e - 1], i = 0; ++t < e; ) (n = r), (r = this[t]), (i += n[1] * r[0] - n[0] * r[1]);
        return 0.5 * i;
    }),
        (oi.centroid = function (n) {
            var t,
                e,
                r = -1,
                i = this.length,
                u = 0,
                o = 0,
                a = this[i - 1];
            for (arguments.length || (n = -1 / (6 * this.area())); ++r < i; ) (t = a), (a = this[r]), (e = t[0] * a[1] - a[0] * t[1]), (u += (t[0] + a[0]) * e), (o += (t[1] + a[1]) * e);
            return [u * n, o * n];
        }),
        (oi.clip = function (n) {
            for (var t, e, r, i, u, o, a = ci(n), l = -1, c = this.length - ci(this), f = this[c - 1]; ++l < c; ) {
                for (t = n.slice(), n.length = 0, i = this[l], u = t[(r = t.length - a) - 1], e = -1; ++e < r; ) ai((o = t[e]), f, i) ? (ai(u, f, i) || n.push(li(u, o, f, i)), n.push(o)) : ai(u, f, i) && n.push(li(u, o, f, i)), (u = o);
                a && n.push(n[0]), (f = i);
            }
            return n;
        });
    var fi,
        si,
        hi,
        pi,
        gi,
        vi = [],
        di = [];
    function yi() {
        Di(this), (this.edge = this.site = this.circle = null);
    }
    function mi(n) {
        var t = vi.pop() || new yi();
        return (t.site = n), t;
    }
    function Mi(n) {
        Ai(n), hi.remove(n), vi.push(n), Di(n);
    }
    function xi(n) {
        var t = n.circle,
            e = t.x,
            r = t.cy,
            i = { x: e, y: r },
            u = n.P,
            o = n.N,
            a = [n];
        Mi(n);
        for (var l = u; l.circle && y(e - l.circle.x) < wn && y(r - l.circle.cy) < wn; ) (u = l.P), a.unshift(l), Mi(l), (l = u);
        a.unshift(l), Ai(l);
        for (var c = o; c.circle && y(e - c.circle.x) < wn && y(r - c.circle.cy) < wn; ) (o = c.N), a.push(c), Mi(c), (c = o);
        a.push(c), Ai(c);
        var f,
            s = a.length;
        for (f = 1; f < s; ++f) (c = a[f]), (l = a[f - 1]), qi(c.edge, l.site, c.site, i);
        (l = a[0]), ((c = a[s - 1]).edge = Li(l.site, c.site, null, i)), Ei(l), Ei(c);
    }
    function bi(n) {
        for (var t, e, r, i, u = n.x, o = n.y, a = hi._; a; )
            if ((r = _i(a, o) - u) > wn) a = a.L;
            else {
                if (!((i = u - wi(a, o)) > wn)) {
                    r > -wn ? ((t = a.P), (e = a)) : i > -wn ? ((t = a), (e = a.N)) : (t = e = a);
                    break;
                }
                if (!a.R) {
                    t = a;
                    break;
                }
                a = a.R;
            }
        var l = mi(n);
        if ((hi.insert(t, l), t || e)) {
            if (t === e) return Ai(t), (e = mi(t.site)), hi.insert(l, e), (l.edge = e.edge = Li(t.site, l.site)), Ei(t), void Ei(e);
            if (e) {
                Ai(t), Ai(e);
                var c = t.site,
                    f = c.x,
                    s = c.y,
                    h = n.x - f,
                    p = n.y - s,
                    g = e.site,
                    v = g.x - f,
                    d = g.y - s,
                    y = 2 * (h * d - p * v),
                    m = h * h + p * p,
                    M = v * v + d * d,
                    x = { x: (d * m - p * M) / y + f, y: (h * M - v * m) / y + s };
                qi(e.edge, c, g, x), (l.edge = Li(c, n, null, x)), (e.edge = Li(n, g, null, x)), Ei(t), Ei(e);
            } else l.edge = Li(t.site, l.site);
        }
    }
    function _i(n, t) {
        var e = n.site,
            r = e.x,
            i = e.y,
            u = i - t;
        if (!u) return r;
        var o = n.P;
        if (!o) return -1 / 0;
        var a = (e = o.site).x,
            l = e.y,
            c = l - t;
        if (!c) return a;
        var f = a - r,
            s = 1 / u - 1 / c,
            h = f / c;
        return s ? (-h + Math.sqrt(h * h - 2 * s * ((f * f) / (-2 * c) - l + c / 2 + i - u / 2))) / s + r : (r + a) / 2;
    }
    function wi(n, t) {
        var e = n.N;
        if (e) return _i(e, t);
        var r = n.site;
        return r.y === t ? r.x : 1 / 0;
    }
    function Si(n) {
        (this.site = n), (this.edges = []);
    }
    function ki(n, t) {
        return t.angle - n.angle;
    }
    function Ni() {
        Di(this), (this.x = this.y = this.arc = this.site = this.cy = null);
    }
    function Ei(n) {
        var t = n.P,
            e = n.N;
        if (t && e) {
            var r = t.site,
                i = n.site,
                u = e.site;
            if (r !== u) {
                var o = i.x,
                    a = i.y,
                    l = r.x - o,
                    c = r.y - a,
                    f = u.x - o,
                    s = 2 * (l * (d = u.y - a) - c * f);
                if (!(s >= -Sn)) {
                    var h = l * l + c * c,
                        p = f * f + d * d,
                        g = (d * h - c * p) / s,
                        v = (l * p - f * h) / s,
                        d = v + a,
                        y = di.pop() || new Ni();
                    (y.arc = n), (y.site = i), (y.x = g + o), (y.y = d + Math.sqrt(g * g + v * v)), (y.cy = d), (n.circle = y);
                    for (var m = null, M = gi._; M; )
                        if (y.y < M.y || (y.y === M.y && y.x <= M.x)) {
                            if (!M.L) {
                                m = M.P;
                                break;
                            }
                            M = M.L;
                        } else {
                            if (!M.R) {
                                m = M;
                                break;
                            }
                            M = M.R;
                        }
                    gi.insert(m, y), m || (pi = y);
                }
            }
        }
    }
    function Ai(n) {
        var t = n.circle;
        t && (t.P || (pi = t.N), gi.remove(t), di.push(t), Di(t), (n.circle = null));
    }
    function Ci(n, t) {
        var e = n.b;
        if (e) return !0;
        var r,
            i,
            u = n.a,
            o = t[0][0],
            a = t[1][0],
            l = t[0][1],
            c = t[1][1],
            f = n.l,
            s = n.r,
            h = f.x,
            p = f.y,
            g = s.x,
            v = s.y,
            d = (h + g) / 2,
            y = (p + v) / 2;
        if (v === p) {
            if (d < o || d >= a) return;
            if (h > g) {
                if (u) {
                    if (u.y >= c) return;
                } else u = { x: d, y: l };
                e = { x: d, y: c };
            } else {
                if (u) {
                    if (u.y < l) return;
                } else u = { x: d, y: c };
                e = { x: d, y: l };
            }
        } else if (((i = y - (r = (h - g) / (v - p)) * d), r < -1 || r > 1))
            if (h > g) {
                if (u) {
                    if (u.y >= c) return;
                } else u = { x: (l - i) / r, y: l };
                e = { x: (c - i) / r, y: c };
            } else {
                if (u) {
                    if (u.y < l) return;
                } else u = { x: (c - i) / r, y: c };
                e = { x: (l - i) / r, y: l };
            }
        else if (p < v) {
            if (u) {
                if (u.x >= a) return;
            } else u = { x: o, y: r * o + i };
            e = { x: a, y: r * a + i };
        } else {
            if (u) {
                if (u.x < o) return;
            } else u = { x: a, y: r * a + i };
            e = { x: o, y: r * o + i };
        }
        return (n.a = u), (n.b = e), !0;
    }
    function zi(n, t) {
        (this.l = n), (this.r = t), (this.a = this.b = null);
    }
    function Li(n, t, e, r) {
        var i = new zi(n, t);
        return fi.push(i), e && qi(i, n, t, e), r && qi(i, t, n, r), si[n.i].edges.push(new Ti(i, n, t)), si[t.i].edges.push(new Ti(i, t, n)), i;
    }
    function qi(n, t, e, r) {
        n.a || n.b ? (n.l === e ? (n.b = r) : (n.a = r)) : ((n.a = r), (n.l = t), (n.r = e));
    }
    function Ti(n, t, e) {
        var r = n.a,
            i = n.b;
        (this.edge = n), (this.site = t), (this.angle = e ? Math.atan2(e.y - t.y, e.x - t.x) : n.l === t ? Math.atan2(i.x - r.x, r.y - i.y) : Math.atan2(r.x - i.x, i.y - r.y));
    }
    function Ri() {
        this._ = null;
    }
    function Di(n) {
        n.U = n.C = n.L = n.R = n.P = n.N = null;
    }
    function Pi(n, t) {
        var e = t,
            r = t.R,
            i = e.U;
        i ? (i.L === e ? (i.L = r) : (i.R = r)) : (n._ = r), (r.U = i), (e.U = r), (e.R = r.L), e.R && (e.R.U = e), (r.L = e);
    }
    function Ui(n, t) {
        var e = t,
            r = t.L,
            i = e.U;
        i ? (i.L === e ? (i.L = r) : (i.R = r)) : (n._ = r), (r.U = i), (e.U = r), (e.L = r.R), e.L && (e.L.U = e), (r.R = e);
    }
    function ji(n) {
        for (; n.L; ) n = n.L;
        return n;
    }
    function Fi(n, t) {
        var e,
            r,
            i,
            u = n.sort(Hi).pop();
        for (fi = [], si = new Array(n.length), hi = new Ri(), gi = new Ri(); ; )
            if (((i = pi), u && (!i || u.y < i.y || (u.y === i.y && u.x < i.x)))) (u.x === e && u.y === r) || ((si[u.i] = new Si(u)), bi(u), (e = u.x), (r = u.y)), (u = n.pop());
            else {
                if (!i) break;
                xi(i.arc);
            }
        t &&
            ((function (n) {
                for (var t, e = fi, r = rr(n[0][0], n[0][1], n[1][0], n[1][1]), i = e.length; i--; ) (!Ci((t = e[i]), n) || !r(t) || (y(t.a.x - t.b.x) < wn && y(t.a.y - t.b.y) < wn)) && ((t.a = t.b = null), e.splice(i, 1));
            })(t),
            (function (n) {
                for (var t, e, r, i, u, o, a, l, c, f, s = n[0][0], h = n[1][0], p = n[0][1], g = n[1][1], v = si, d = v.length; d--; )
                    if ((u = v[d]) && u.prepare())
                        for (l = (a = u.edges).length, o = 0; o < l; )
                            (r = (f = a[o].end()).x),
                                (i = f.y),
                                (t = (c = a[++o % l].start()).x),
                                (e = c.y),
                                (y(r - t) > wn || y(i - e) > wn) &&
                                    (a.splice(
                                        o,
                                        0,
                                        new Ti(
                                            ((m = u.site),
                                            (M = f),
                                            (x =
                                                y(r - s) < wn && g - i > wn
                                                    ? { x: s, y: y(t - s) < wn ? e : g }
                                                    : y(i - g) < wn && h - r > wn
                                                    ? { x: y(e - g) < wn ? t : h, y: g }
                                                    : y(r - h) < wn && i - p > wn
                                                    ? { x: h, y: y(t - h) < wn ? e : p }
                                                    : y(i - p) < wn && r - s > wn
                                                    ? { x: y(e - p) < wn ? t : s, y: p }
                                                    : null),
                                            (b = void 0),
                                            (b = new zi(m, null)),
                                            (b.a = M),
                                            (b.b = x),
                                            fi.push(b),
                                            b),
                                            u.site,
                                            null
                                        )
                                    ),
                                    ++l);
                var m, M, x, b;
            })(t));
        var o = { cells: si, edges: fi };
        return (hi = gi = fi = si = null), o;
    }
    function Hi(n, t) {
        return t.y - n.y || t.x - n.x;
    }
    (Si.prototype.prepare = function () {
        for (var n, t = this.edges, e = t.length; e--; ) ((n = t[e].edge).b && n.a) || t.splice(e, 1);
        return t.sort(ki), t.length;
    }),
        (Ti.prototype = {
            start: function () {
                return this.edge.l === this.site ? this.edge.a : this.edge.b;
            },
            end: function () {
                return this.edge.l === this.site ? this.edge.b : this.edge.a;
            },
        }),
        (Ri.prototype = {
            insert: function (n, t) {
                var e, r, i;
                if (n) {
                    if (((t.P = n), (t.N = n.N), n.N && (n.N.P = t), (n.N = t), n.R)) {
                        for (n = n.R; n.L; ) n = n.L;
                        n.L = t;
                    } else n.R = t;
                    e = n;
                } else this._ ? ((n = ji(this._)), (t.P = null), (t.N = n), (n.P = n.L = t), (e = n)) : ((t.P = t.N = null), (this._ = t), (e = null));
                for (t.L = t.R = null, t.U = e, t.C = !0, n = t; e && e.C; )
                    e === (r = e.U).L
                        ? (i = r.R) && i.C
                            ? ((e.C = i.C = !1), (r.C = !0), (n = r))
                            : (n === e.R && (Pi(this, e), (e = (n = e).U)), (e.C = !1), (r.C = !0), Ui(this, r))
                        : (i = r.L) && i.C
                        ? ((e.C = i.C = !1), (r.C = !0), (n = r))
                        : (n === e.L && (Ui(this, e), (e = (n = e).U)), (e.C = !1), (r.C = !0), Pi(this, r)),
                        (e = n.U);
                this._.C = !1;
            },
            remove: function (n) {
                n.N && (n.N.P = n.P), n.P && (n.P.N = n.N), (n.N = n.P = null);
                var t,
                    e,
                    r,
                    i = n.U,
                    u = n.L,
                    o = n.R;
                if (
                    ((e = u ? (o ? ji(o) : u) : o),
                    i ? (i.L === n ? (i.L = e) : (i.R = e)) : (this._ = e),
                    u && o ? ((r = e.C), (e.C = n.C), (e.L = u), (u.U = e), e !== o ? ((i = e.U), (e.U = n.U), (n = e.R), (i.L = n), (e.R = o), (o.U = e)) : ((e.U = i), (i = e), (n = e.R))) : ((r = n.C), (n = e)),
                    n && (n.U = i),
                    !r)
                )
                    if (n && n.C) n.C = !1;
                    else {
                        do {
                            if (n === this._) break;
                            if (n === i.L) {
                                if (((t = i.R).C && ((t.C = !1), (i.C = !0), Pi(this, i), (t = i.R)), (t.L && t.L.C) || (t.R && t.R.C))) {
                                    (t.R && t.R.C) || ((t.L.C = !1), (t.C = !0), Ui(this, t), (t = i.R)), (t.C = i.C), (i.C = t.R.C = !1), Pi(this, i), (n = this._);
                                    break;
                                }
                            } else if (((t = i.L).C && ((t.C = !1), (i.C = !0), Ui(this, i), (t = i.L)), (t.L && t.L.C) || (t.R && t.R.C))) {
                                (t.L && t.L.C) || ((t.R.C = !1), (t.C = !0), Pi(this, t), (t = i.L)), (t.C = i.C), (i.C = t.L.C = !1), Ui(this, i), (n = this._);
                                break;
                            }
                            (t.C = !0), (n = i), (i = i.U);
                        } while (!n.C);
                        n && (n.C = !1);
                    }
            },
        }),
        (n.geom.voronoi = function (n) {
            var t = ei,
                e = ri,
                r = t,
                i = e,
                u = Oi;
            if (n) return o(n);
            function o(n) {
                var t = new Array(n.length),
                    e = u[0][0],
                    r = u[0][1],
                    i = u[1][0],
                    o = u[1][1];
                return (
                    Fi(a(n), u).cells.forEach(function (u, a) {
                        var l = u.edges,
                            c = u.site;
                        (t[a] = l.length
                            ? l.map(function (n) {
                                  var t = n.start();
                                  return [t.x, t.y];
                              })
                            : c.x >= e && c.x <= i && c.y >= r && c.y <= o
                            ? [
                                  [e, o],
                                  [i, o],
                                  [i, r],
                                  [e, r],
                              ]
                            : []).point = n[a];
                    }),
                    t
                );
            }
            function a(n) {
                return n.map(function (n, t) {
                    return { x: Math.round(r(n, t) / wn) * wn, y: Math.round(i(n, t) / wn) * wn, i: t };
                });
            }
            return (
                (o.links = function (n) {
                    return Fi(a(n))
                        .edges.filter(function (n) {
                            return n.l && n.r;
                        })
                        .map(function (t) {
                            return { source: n[t.l.i], target: n[t.r.i] };
                        });
                }),
                (o.triangles = function (n) {
                    var t = [];
                    return (
                        Fi(a(n)).cells.forEach(function (e, r) {
                            for (var i, u, o, a, l = e.site, c = e.edges.sort(ki), f = -1, s = c.length, h = c[s - 1].edge, p = h.l === l ? h.r : h.l; ++f < s; )
                                h, (i = p), (p = (h = c[f].edge).l === l ? h.r : h.l), r < i.i && r < p.i && ((o = i), (a = p), ((u = l).x - a.x) * (o.y - u.y) - (u.x - o.x) * (a.y - u.y) < 0) && t.push([n[r], n[i.i], n[p.i]]);
                        }),
                        t
                    );
                }),
                (o.x = function (n) {
                    return arguments.length ? ((r = dt((t = n))), o) : t;
                }),
                (o.y = function (n) {
                    return arguments.length ? ((i = dt((e = n))), o) : e;
                }),
                (o.clipExtent = function (n) {
                    return arguments.length ? ((u = null == n ? Oi : n), o) : u === Oi ? null : u;
                }),
                (o.size = function (n) {
                    return arguments.length ? o.clipExtent(n && [[0, 0], n]) : u === Oi ? null : u && u[1];
                }),
                o
            );
        });
    var Oi = [
        [-1e6, -1e6],
        [1e6, 1e6],
    ];
    function Ii(n) {
        return n.x;
    }
    function Yi(n) {
        return n.y;
    }
    function Zi(t, e) {
        (t = n.rgb(t)), (e = n.rgb(e));
        var r = t.r,
            i = t.g,
            u = t.b,
            o = e.r - r,
            a = e.g - i,
            l = e.b - u;
        return function (n) {
            return "#" + ct(Math.round(r + o * n)) + ct(Math.round(i + a * n)) + ct(Math.round(u + l * n));
        };
    }
    function Vi(n, t) {
        var e,
            r = {},
            i = {};
        for (e in n) e in t ? (r[e] = Ji(n[e], t[e])) : (i[e] = n[e]);
        for (e in t) e in n || (i[e] = t[e]);
        return function (n) {
            for (e in r) i[e] = r[e](n);
            return i;
        };
    }
    function Xi(n, t) {
        return (
            (n = +n),
            (t = +t),
            function (e) {
                return n * (1 - e) + t * e;
            }
        );
    }
    function $i(n, t) {
        var e,
            r,
            i,
            u = (Bi.lastIndex = Wi.lastIndex = 0),
            o = -1,
            a = [],
            l = [];
        for (n += "", t += ""; (e = Bi.exec(n)) && (r = Wi.exec(t)); )
            (i = r.index) > u && ((i = t.slice(u, i)), a[o] ? (a[o] += i) : (a[++o] = i)), (e = e[0]) === (r = r[0]) ? (a[o] ? (a[o] += r) : (a[++o] = r)) : ((a[++o] = null), l.push({ i: o, x: Xi(e, r) })), (u = Wi.lastIndex);
        return (
            u < t.length && ((i = t.slice(u)), a[o] ? (a[o] += i) : (a[++o] = i)),
            a.length < 2
                ? l[0]
                    ? ((t = l[0].x),
                      function (n) {
                          return t(n) + "";
                      })
                    : function () {
                          return t;
                      }
                : ((t = l.length),
                  function (n) {
                      for (var e, r = 0; r < t; ++r) a[(e = l[r]).i] = e.x(n);
                      return a.join("");
                  })
        );
    }
    (n.geom.delaunay = function (t) {
        return n.geom.voronoi().triangles(t);
    }),
        (n.geom.quadtree = function (n, t, e, r, i) {
            var u,
                o = ei,
                a = ri;
            if ((u = arguments.length)) return (o = Ii), (a = Yi), 3 === u && ((i = e), (r = t), (e = t = 0)), l(n);
            function l(n) {
                var l,
                    c,
                    f,
                    s,
                    h,
                    p,
                    g,
                    v,
                    d,
                    m = dt(o),
                    M = dt(a);
                if (null != t) (p = t), (g = e), (v = r), (d = i);
                else if (((v = d = -(p = g = 1 / 0)), (c = []), (f = []), (h = n.length), u)) for (s = 0; s < h; ++s) (l = n[s]).x < p && (p = l.x), l.y < g && (g = l.y), l.x > v && (v = l.x), l.y > d && (d = l.y), c.push(l.x), f.push(l.y);
                else
                    for (s = 0; s < h; ++s) {
                        var x = +m((l = n[s]), s),
                            b = +M(l, s);
                        x < p && (p = x), b < g && (g = b), x > v && (v = x), b > d && (d = b), c.push(x), f.push(b);
                    }
                var _ = v - p,
                    w = d - g;
                function S(n, t, e, r, i, u, o, a) {
                    if (!isNaN(e) && !isNaN(r))
                        if (n.leaf) {
                            var l = n.x,
                                c = n.y;
                            if (null != l)
                                if (y(l - e) + y(c - r) < 0.01) k(n, t, e, r, i, u, o, a);
                                else {
                                    var f = n.point;
                                    (n.x = n.y = n.point = null), k(n, f, l, c, i, u, o, a), k(n, t, e, r, i, u, o, a);
                                }
                            else (n.x = e), (n.y = r), (n.point = t);
                        } else k(n, t, e, r, i, u, o, a);
                }
                function k(n, t, e, r, i, u, o, a) {
                    var l = 0.5 * (i + o),
                        c = 0.5 * (u + a),
                        f = e >= l,
                        s = r >= c,
                        h = (s << 1) | f;
                    (n.leaf = !1), f ? (i = l) : (o = l), s ? (u = c) : (a = c), S((n = n.nodes[h] || (n.nodes[h] = { leaf: !0, nodes: [], point: null, x: null, y: null })), t, e, r, i, u, o, a);
                }
                _ > w ? (d = g + _) : (v = p + w);
                var N = {
                    leaf: !0,
                    nodes: [],
                    point: null,
                    x: null,
                    y: null,
                    add: function (n) {
                        S(N, n, +m(n, ++s), +M(n, s), p, g, v, d);
                    },
                };
                if (
                    ((N.visit = function (n) {
                        !(function n(t, e, r, i, u, o) {
                            if (!t(e, r, i, u, o)) {
                                var a = 0.5 * (r + u),
                                    l = 0.5 * (i + o),
                                    c = e.nodes;
                                c[0] && n(t, c[0], r, i, a, l), c[1] && n(t, c[1], a, i, u, l), c[2] && n(t, c[2], r, l, a, o), c[3] && n(t, c[3], a, l, u, o);
                            }
                        })(n, N, p, g, v, d);
                    }),
                    (N.find = function (n) {
                        return (function (n, t, e, r, i, u, o) {
                            var a,
                                l = 1 / 0;
                            return (
                                (function n(c, f, s, h, p) {
                                    if (!(f > u || s > o || h < r || p < i)) {
                                        if ((g = c.point)) {
                                            var g,
                                                v = t - c.x,
                                                d = e - c.y,
                                                y = v * v + d * d;
                                            if (y < l) {
                                                var m = Math.sqrt((l = y));
                                                (r = t - m), (i = e - m), (u = t + m), (o = e + m), (a = g);
                                            }
                                        }
                                        for (var M = c.nodes, x = 0.5 * (f + h), b = 0.5 * (s + p), _ = ((e >= b) << 1) | (t >= x), w = _ + 4; _ < w; ++_)
                                            if ((c = M[3 & _]))
                                                switch (3 & _) {
                                                    case 0:
                                                        n(c, f, s, x, b);
                                                        break;
                                                    case 1:
                                                        n(c, x, s, h, b);
                                                        break;
                                                    case 2:
                                                        n(c, f, b, x, p);
                                                        break;
                                                    case 3:
                                                        n(c, x, b, h, p);
                                                }
                                    }
                                })(n, r, i, u, o),
                                a
                            );
                        })(N, n[0], n[1], p, g, v, d);
                    }),
                    (s = -1),
                    null == t)
                ) {
                    for (; ++s < h; ) S(N, n[s], c[s], f[s], p, g, v, d);
                    --s;
                } else n.forEach(N.add);
                return (c = f = n = l = null), N;
            }
            return (
                (l.x = function (n) {
                    return arguments.length ? ((o = n), l) : o;
                }),
                (l.y = function (n) {
                    return arguments.length ? ((a = n), l) : a;
                }),
                (l.extent = function (n) {
                    return arguments.length
                        ? (null == n ? (t = e = r = i = null) : ((t = +n[0][0]), (e = +n[0][1]), (r = +n[1][0]), (i = +n[1][1])), l)
                        : null == t
                        ? null
                        : [
                              [t, e],
                              [r, i],
                          ];
                }),
                (l.size = function (n) {
                    return arguments.length ? (null == n ? (t = e = r = i = null) : ((t = e = 0), (r = +n[0]), (i = +n[1])), l) : null == t ? null : [r - t, i - e];
                }),
                l
            );
        }),
        (n.interpolateRgb = Zi),
        (n.interpolateObject = Vi),
        (n.interpolateNumber = Xi),
        (n.interpolateString = $i);
    var Bi = /[-+]?(?:\d+\.?\d*|\.?\d+)(?:[eE][-+]?\d+)?/g,
        Wi = new RegExp(Bi.source, "g");
    function Ji(t, e) {
        for (var r, i = n.interpolators.length; --i >= 0 && !(r = n.interpolators[i](t, e)); );
        return r;
    }
    function Gi(n, t) {
        var e,
            r = [],
            i = [],
            u = n.length,
            o = t.length,
            a = Math.min(n.length, t.length);
        for (e = 0; e < a; ++e) r.push(Ji(n[e], t[e]));
        for (; e < u; ++e) i[e] = n[e];
        for (; e < o; ++e) i[e] = t[e];
        return function (n) {
            for (e = 0; e < a; ++e) i[e] = r[e](n);
            return i;
        };
    }
    (n.interpolate = Ji),
        (n.interpolators = [
            function (n, t) {
                var e = typeof t;
                return ("string" === e ? (vt.has(t.toLowerCase()) || /^(#|rgb\(|hsl\()/i.test(t) ? Zi : $i) : t instanceof On ? Zi : Array.isArray(t) ? Gi : "object" === e && isNaN(t) ? Vi : Xi)(n, t);
            },
        ]),
        (n.interpolateArray = Gi);
    var Ki = function () {
            return z;
        },
        Qi = n.map({
            linear: Ki,
            poly: function (n) {
                return function (t) {
                    return Math.pow(t, n);
                };
            },
            quad: function () {
                return ru;
            },
            cubic: function () {
                return iu;
            },
            sin: function () {
                return ou;
            },
            exp: function () {
                return au;
            },
            circle: function () {
                return lu;
            },
            elastic: function (n, t) {
                var e;
                arguments.length < 2 && (t = 0.45);
                arguments.length ? (e = (t / Nn) * Math.asin(1 / n)) : ((n = 1), (e = t / 4));
                return function (r) {
                    return 1 + n * Math.pow(2, -10 * r) * Math.sin(((r - e) * Nn) / t);
                };
            },
            back: function (n) {
                n || (n = 1.70158);
                return function (t) {
                    return t * t * ((n + 1) * t - n);
                };
            },
            bounce: function () {
                return cu;
            },
        }),
        nu = n.map({
            in: z,
            out: tu,
            "in-out": eu,
            "out-in": function (n) {
                return eu(tu(n));
            },
        });
    function tu(n) {
        return function (t) {
            return 1 - n(1 - t);
        };
    }
    function eu(n) {
        return function (t) {
            return 0.5 * (t < 0.5 ? n(2 * t) : 2 - n(2 - 2 * t));
        };
    }
    function ru(n) {
        return n * n;
    }
    function iu(n) {
        return n * n * n;
    }
    function uu(n) {
        if (n <= 0) return 0;
        if (n >= 1) return 1;
        var t = n * n,
            e = t * n;
        return 4 * (n < 0.5 ? e : 3 * (n - t) + e - 0.75);
    }
    function ou(n) {
        return 1 - Math.cos(n * An);
    }
    function au(n) {
        return Math.pow(2, 10 * (n - 1));
    }
    function lu(n) {
        return 1 - Math.sqrt(1 - n * n);
    }
    function cu(n) {
        return n < 1 / 2.75 ? 7.5625 * n * n : n < 2 / 2.75 ? 7.5625 * (n -= 1.5 / 2.75) * n + 0.75 : n < 2.5 / 2.75 ? 7.5625 * (n -= 2.25 / 2.75) * n + 0.9375 : 7.5625 * (n -= 2.625 / 2.75) * n + 0.984375;
    }
    function fu(n, t) {
        return (
            (t -= n),
            function (e) {
                return Math.round(n + t * e);
            }
        );
    }
    function su(n) {
        var t,
            e,
            r,
            i = [n.a, n.b],
            u = [n.c, n.d],
            o = pu(i),
            a = hu(i, u),
            l = pu((((t = u)[0] += (r = -a) * (e = i)[0]), (t[1] += r * e[1]), t)) || 0;
        i[0] * u[1] < u[0] * i[1] && ((i[0] *= -1), (i[1] *= -1), (o *= -1), (a *= -1)),
            (this.rotate = (o ? Math.atan2(i[1], i[0]) : Math.atan2(-u[0], u[1])) * zn),
            (this.translate = [n.e, n.f]),
            (this.scale = [o, l]),
            (this.skew = l ? Math.atan2(a, l) * zn : 0);
    }
    function hu(n, t) {
        return n[0] * t[0] + n[1] * t[1];
    }
    function pu(n) {
        var t = Math.sqrt(hu(n, n));
        return t && ((n[0] /= t), (n[1] /= t)), t;
    }
    (n.ease = function (n) {
        var e,
            r = n.indexOf("-"),
            i = r >= 0 ? n.slice(0, r) : n,
            u = r >= 0 ? n.slice(r + 1) : "in";
        return (
            (i = Qi.get(i) || Ki),
            (u = nu.get(u) || z),
            (e = u(i.apply(null, t.call(arguments, 1)))),
            function (n) {
                return n <= 0 ? 0 : n >= 1 ? 1 : e(n);
            }
        );
    }),
        (n.interpolateHcl = function (t, e) {
            (t = n.hcl(t)), (e = n.hcl(e));
            var r = t.h,
                i = t.c,
                u = t.l,
                o = e.h - r,
                a = e.c - i,
                l = e.l - u;
            isNaN(a) && ((a = 0), (i = isNaN(i) ? e.c : i));
            isNaN(o) ? ((o = 0), (r = isNaN(r) ? e.h : r)) : o > 180 ? (o -= 360) : o < -180 && (o += 360);
            return function (n) {
                return $n(r + o * n, i + a * n, u + l * n) + "";
            };
        }),
        (n.interpolateHsl = function (t, e) {
            (t = n.hsl(t)), (e = n.hsl(e));
            var r = t.h,
                i = t.s,
                u = t.l,
                o = e.h - r,
                a = e.s - i,
                l = e.l - u;
            isNaN(a) && ((a = 0), (i = isNaN(i) ? e.s : i));
            isNaN(o) ? ((o = 0), (r = isNaN(r) ? e.h : r)) : o > 180 ? (o -= 360) : o < -180 && (o += 360);
            return function (n) {
                return Zn(r + o * n, i + a * n, u + l * n) + "";
            };
        }),
        (n.interpolateLab = function (t, e) {
            (t = n.lab(t)), (e = n.lab(e));
            var r = t.l,
                i = t.a,
                u = t.b,
                o = e.l - r,
                a = e.a - i,
                l = e.b - u;
            return function (n) {
                return nt(r + o * n, i + a * n, u + l * n) + "";
            };
        }),
        (n.interpolateRound = fu),
        (n.transform = function (t) {
            var e = r.createElementNS(n.ns.prefix.svg, "g");
            return (n.transform = function (n) {
                if (null != n) {
                    e.setAttribute("transform", n);
                    var t = e.transform.baseVal.consolidate();
                }
                return new su(t ? t.matrix : gu);
            })(t);
        }),
        (su.prototype.toString = function () {
            return "translate(" + this.translate + ")rotate(" + this.rotate + ")skewX(" + this.skew + ")scale(" + this.scale + ")";
        });
    var gu = { a: 1, b: 0, c: 0, d: 1, e: 0, f: 0 };
    function vu(n) {
        return n.length ? n.pop() + "," : "";
    }
    function du(t, e) {
        var r = [],
            i = [];
        return (
            (t = n.transform(t)),
            (e = n.transform(e)),
            (function (n, t, e, r) {
                if (n[0] !== t[0] || n[1] !== t[1]) {
                    var i = e.push("translate(", null, ",", null, ")");
                    r.push({ i: i - 4, x: Xi(n[0], t[0]) }, { i: i - 2, x: Xi(n[1], t[1]) });
                } else (t[0] || t[1]) && e.push("translate(" + t + ")");
            })(t.translate, e.translate, r, i),
            (function (n, t, e, r) {
                n !== t ? (n - t > 180 ? (t += 360) : t - n > 180 && (n += 360), r.push({ i: e.push(vu(e) + "rotate(", null, ")") - 2, x: Xi(n, t) })) : t && e.push(vu(e) + "rotate(" + t + ")");
            })(t.rotate, e.rotate, r, i),
            (function (n, t, e, r) {
                n !== t ? r.push({ i: e.push(vu(e) + "skewX(", null, ")") - 2, x: Xi(n, t) }) : t && e.push(vu(e) + "skewX(" + t + ")");
            })(t.skew, e.skew, r, i),
            (function (n, t, e, r) {
                if (n[0] !== t[0] || n[1] !== t[1]) {
                    var i = e.push(vu(e) + "scale(", null, ",", null, ")");
                    r.push({ i: i - 4, x: Xi(n[0], t[0]) }, { i: i - 2, x: Xi(n[1], t[1]) });
                } else (1 === t[0] && 1 === t[1]) || e.push(vu(e) + "scale(" + t + ")");
            })(t.scale, e.scale, r, i),
            (t = e = null),
            function (n) {
                for (var t, e = -1, u = i.length; ++e < u; ) r[(t = i[e]).i] = t.x(n);
                return r.join("");
            }
        );
    }
    function yu(n, t) {
        return (
            (t = (t -= n = +n) || 1 / t),
            function (e) {
                return (e - n) / t;
            }
        );
    }
    function mu(n, t) {
        return (
            (t = (t -= n = +n) || 1 / t),
            function (e) {
                return Math.max(0, Math.min(1, (e - n) / t));
            }
        );
    }
    function Mu(n) {
        for (
            var t = n.source,
                e = n.target,
                r = (function (n, t) {
                    if (n === t) return n;
                    var e = xu(n),
                        r = xu(t),
                        i = e.pop(),
                        u = r.pop(),
                        o = null;
                    for (; i === u; ) (o = i), (i = e.pop()), (u = r.pop());
                    return o;
                })(t, e),
                i = [t];
            t !== r;

        )
            (t = t.parent), i.push(t);
        for (var u = i.length; e !== r; ) i.splice(u, 0, e), (e = e.parent);
        return i;
    }
    function xu(n) {
        for (var t = [], e = n.parent; null != e; ) t.push(n), (n = e), (e = e.parent);
        return t.push(n), t;
    }
    function bu(n) {
        n.fixed |= 2;
    }
    function _u(n) {
        n.fixed &= -7;
    }
    function wu(n) {
        (n.fixed |= 4), (n.px = n.x), (n.py = n.y);
    }
    function Su(n) {
        n.fixed &= -5;
    }
    (n.interpolateTransform = du),
        (n.layout = {}),
        (n.layout.bundle = function () {
            return function (n) {
                for (var t = [], e = -1, r = n.length; ++e < r; ) t.push(Mu(n[e]));
                return t;
            };
        }),
        (n.layout.chord = function () {
            var t,
                e,
                r,
                i,
                u,
                o,
                a,
                l = {},
                c = 0;
            function f() {
                var l,
                    f,
                    h,
                    p,
                    g,
                    v = {},
                    d = [],
                    y = n.range(i),
                    m = [];
                for (t = [], e = [], l = 0, p = -1; ++p < i; ) {
                    for (f = 0, g = -1; ++g < i; ) f += r[p][g];
                    d.push(f), m.push(n.range(i)), (l += f);
                }
                for (
                    u &&
                        y.sort(function (n, t) {
                            return u(d[n], d[t]);
                        }),
                        o &&
                            m.forEach(function (n, t) {
                                n.sort(function (n, e) {
                                    return o(r[t][n], r[t][e]);
                                });
                            }),
                        l = (Nn - c * i) / l,
                        f = 0,
                        p = -1;
                    ++p < i;

                ) {
                    for (h = f, g = -1; ++g < i; ) {
                        var M = y[p],
                            x = m[M][g],
                            b = r[M][x],
                            _ = f,
                            w = (f += b * l);
                        v[M + "-" + x] = { index: M, subindex: x, startAngle: _, endAngle: w, value: b };
                    }
                    (e[M] = { index: M, startAngle: h, endAngle: f, value: d[M] }), (f += c);
                }
                for (p = -1; ++p < i; )
                    for (g = p - 1; ++g < i; ) {
                        var S = v[p + "-" + g],
                            k = v[g + "-" + p];
                        (S.value || k.value) && t.push(S.value < k.value ? { source: k, target: S } : { source: S, target: k });
                    }
                a && s();
            }
            function s() {
                t.sort(function (n, t) {
                    return a((n.source.value + n.target.value) / 2, (t.source.value + t.target.value) / 2);
                });
            }
            return (
                (l.matrix = function (n) {
                    return arguments.length ? ((i = (r = n) && r.length), (t = e = null), l) : r;
                }),
                (l.padding = function (n) {
                    return arguments.length ? ((c = n), (t = e = null), l) : c;
                }),
                (l.sortGroups = function (n) {
                    return arguments.length ? ((u = n), (t = e = null), l) : u;
                }),
                (l.sortSubgroups = function (n) {
                    return arguments.length ? ((o = n), (t = null), l) : o;
                }),
                (l.sortChords = function (n) {
                    return arguments.length ? ((a = n), t && s(), l) : a;
                }),
                (l.chords = function () {
                    return t || f(), t;
                }),
                (l.groups = function () {
                    return e || f(), e;
                }),
                l
            );
        }),
        (n.layout.force = function () {
            var t,
                e,
                r,
                i,
                u,
                o,
                a = {},
                l = n.dispatch("start", "tick", "end"),
                c = [1, 1],
                f = 0.9,
                s = ku,
                h = Nu,
                p = -30,
                g = Eu,
                v = 0.1,
                d = 0.64,
                y = [],
                m = [];
            function M(n) {
                return function (t, e, r, i) {
                    if (t.point !== n) {
                        var u = t.cx - n.x,
                            o = t.cy - n.y,
                            a = i - e,
                            l = u * u + o * o;
                        if ((a * a) / d < l) {
                            if (l < g) {
                                var c = t.charge / l;
                                (n.px -= u * c), (n.py -= o * c);
                            }
                            return !0;
                        }
                        if (t.point && l && l < g) {
                            c = t.pointCharge / l;
                            (n.px -= u * c), (n.py -= o * c);
                        }
                    }
                    return !t.charge;
                };
            }
            function x(t) {
                (t.px = n.event.x), (t.py = n.event.y), a.resume();
            }
            return (
                (a.tick = function () {
                    if ((r *= 0.99) < 0.005) return (t = null), l.end({ type: "end", alpha: (r = 0) }), !0;
                    var e,
                        a,
                        s,
                        h,
                        g,
                        d,
                        x,
                        b,
                        _,
                        w = y.length,
                        S = m.length;
                    for (a = 0; a < S; ++a)
                        (h = (s = m[a]).source),
                            (d = (b = (g = s.target).x - h.x) * b + (_ = g.y - h.y) * _) &&
                                ((b *= d = (r * u[a] * ((d = Math.sqrt(d)) - i[a])) / d), (_ *= d), (g.x -= b * (x = h.weight + g.weight ? h.weight / (h.weight + g.weight) : 0.5)), (g.y -= _ * x), (h.x += b * (x = 1 - x)), (h.y += _ * x));
                    if ((x = r * v) && ((b = c[0] / 2), (_ = c[1] / 2), (a = -1), x)) for (; ++a < w; ) ((s = y[a]).x += (b - s.x) * x), (s.y += (_ - s.y) * x);
                    if (p)
                        for (
                            !(function n(t, e, r) {
                                var i = 0,
                                    u = 0;
                                t.charge = 0;
                                if (!t.leaf) for (var o, a = t.nodes, l = a.length, c = -1; ++c < l; ) null != (o = a[c]) && (n(o, e, r), (t.charge += o.charge), (i += o.charge * o.cx), (u += o.charge * o.cy));
                                if (t.point) {
                                    t.leaf || ((t.point.x += Math.random() - 0.5), (t.point.y += Math.random() - 0.5));
                                    var f = e * r[t.point.index];
                                    (t.charge += t.pointCharge = f), (i += f * t.point.x), (u += f * t.point.y);
                                }
                                t.cx = i / t.charge;
                                t.cy = u / t.charge;
                            })((e = n.geom.quadtree(y)), r, o),
                                a = -1;
                            ++a < w;

                        )
                            (s = y[a]).fixed || e.visit(M(s));
                    for (a = -1; ++a < w; ) (s = y[a]).fixed ? ((s.x = s.px), (s.y = s.py)) : ((s.x -= (s.px - (s.px = s.x)) * f), (s.y -= (s.py - (s.py = s.y)) * f));
                    l.tick({ type: "tick", alpha: r });
                }),
                (a.nodes = function (n) {
                    return arguments.length ? ((y = n), a) : y;
                }),
                (a.links = function (n) {
                    return arguments.length ? ((m = n), a) : m;
                }),
                (a.size = function (n) {
                    return arguments.length ? ((c = n), a) : c;
                }),
                (a.linkDistance = function (n) {
                    return arguments.length ? ((s = "function" == typeof n ? n : +n), a) : s;
                }),
                (a.distance = a.linkDistance),
                (a.linkStrength = function (n) {
                    return arguments.length ? ((h = "function" == typeof n ? n : +n), a) : h;
                }),
                (a.friction = function (n) {
                    return arguments.length ? ((f = +n), a) : f;
                }),
                (a.charge = function (n) {
                    return arguments.length ? ((p = "function" == typeof n ? n : +n), a) : p;
                }),
                (a.chargeDistance = function (n) {
                    return arguments.length ? ((g = n * n), a) : Math.sqrt(g);
                }),
                (a.gravity = function (n) {
                    return arguments.length ? ((v = +n), a) : v;
                }),
                (a.theta = function (n) {
                    return arguments.length ? ((d = n * n), a) : Math.sqrt(d);
                }),
                (a.alpha = function (n) {
                    return arguments.length
                        ? ((n = +n), r ? (n > 0 ? (r = n) : ((t.c = null), (t.t = NaN), (t = null), l.end({ type: "end", alpha: (r = 0) }))) : n > 0 && (l.start({ type: "start", alpha: (r = n) }), (t = St(a.tick))), a)
                        : r;
                }),
                (a.start = function () {
                    var n,
                        t,
                        e,
                        r = y.length,
                        l = m.length,
                        f = c[0],
                        g = c[1];
                    for (n = 0; n < r; ++n) ((e = y[n]).index = n), (e.weight = 0);
                    for (n = 0; n < l; ++n) "number" == typeof (e = m[n]).source && (e.source = y[e.source]), "number" == typeof e.target && (e.target = y[e.target]), ++e.source.weight, ++e.target.weight;
                    for (n = 0; n < r; ++n) (e = y[n]), isNaN(e.x) && (e.x = v("x", f)), isNaN(e.y) && (e.y = v("y", g)), isNaN(e.px) && (e.px = e.x), isNaN(e.py) && (e.py = e.y);
                    if (((i = []), "function" == typeof s)) for (n = 0; n < l; ++n) i[n] = +s.call(this, m[n], n);
                    else for (n = 0; n < l; ++n) i[n] = s;
                    if (((u = []), "function" == typeof h)) for (n = 0; n < l; ++n) u[n] = +h.call(this, m[n], n);
                    else for (n = 0; n < l; ++n) u[n] = h;
                    if (((o = []), "function" == typeof p)) for (n = 0; n < r; ++n) o[n] = +p.call(this, y[n], n);
                    else for (n = 0; n < r; ++n) o[n] = p;
                    function v(e, i) {
                        if (!t) {
                            for (t = new Array(r), c = 0; c < r; ++c) t[c] = [];
                            for (c = 0; c < l; ++c) {
                                var u = m[c];
                                t[u.source.index].push(u.target), t[u.target.index].push(u.source);
                            }
                        }
                        for (var o, a = t[n], c = -1, f = a.length; ++c < f; ) if (!isNaN((o = a[c][e]))) return o;
                        return Math.random() * i;
                    }
                    return a.resume();
                }),
                (a.resume = function () {
                    return a.alpha(0.1);
                }),
                (a.stop = function () {
                    return a.alpha(0);
                }),
                (a.drag = function () {
                    if ((e || (e = n.behavior.drag().origin(z).on("dragstart.force", bu).on("drag.force", x).on("dragend.force", _u)), !arguments.length)) return e;
                    this.on("mouseover.force", wu).on("mouseout.force", Su).call(e);
                }),
                n.rebind(a, l, "on")
            );
        });
    var ku = 20,
        Nu = 1,
        Eu = 1 / 0;
    function Au(t, e) {
        return n.rebind(t, e, "sort", "children", "value"), (t.nodes = t), (t.links = Ru), t;
    }
    function Cu(n, t) {
        for (var e = [n]; null != (n = e.pop()); ) if ((t(n), (i = n.children) && (r = i.length))) for (var r, i; --r >= 0; ) e.push(i[r]);
    }
    function zu(n, t) {
        for (var e = [n], r = []; null != (n = e.pop()); ) if ((r.push(n), (u = n.children) && (i = u.length))) for (var i, u, o = -1; ++o < i; ) e.push(u[o]);
        for (; null != (n = r.pop()); ) t(n);
    }
    function Lu(n) {
        return n.children;
    }
    function qu(n) {
        return n.value;
    }
    function Tu(n, t) {
        return t.value - n.value;
    }
    function Ru(t) {
        return n.merge(
            t.map(function (n) {
                return (n.children || []).map(function (t) {
                    return { source: n, target: t };
                });
            })
        );
    }
    (n.layout.hierarchy = function () {
        var n = Tu,
            t = Lu,
            e = qu;
        function r(i) {
            var u,
                o = [i],
                a = [];
			for (i.depth = 0; null != (u = o.pop()); )
                if ((a.push(u), (c = t.call(r, u, u.depth)) && (l = c.length))) {
                    for (var l, c, f; --l >= 0; ) o.push((f = c[l])), (f.parent = u), (f.depth = u.depth + 1);
                    e && (u.value = 0), (u.children = c);
                } else e && (u.value = +e.call(r, u, u.depth) || 0), delete u.children;
            return (
                zu(i, function (t) {
                    var r, i;
                    n && (r = t.children) && r.sort(n), e && (i = t.parent) && (i.value += t.value);
                }),
                a
            );
        }
        return (
            (r.sort = function (t) {
                return arguments.length ? ((n = t), r) : n;
            }),
            (r.children = function (n) {
                return arguments.length ? ((t = n), r) : t;
            }),
            (r.value = function (n) {
                return arguments.length ? ((e = n), r) : e;
            }),
            (r.revalue = function (n) {
                return (
                    e &&
                        (Cu(n, function (n) {
                            n.children && (n.value = 0);
                        }),
                        zu(n, function (n) {
                            var t;
                            n.children || (n.value = +e.call(r, n, n.depth) || 0), (t = n.parent) && (t.value += n.value);
                        })),
                    n
                );
            }),
            r
        );
    }),
        (n.layout.partition = function () {
            var t = n.layout.hierarchy(),
                e = [1, 1];
            function r(n, r) {
                var i = t.call(this, n, r);
                return (
                    (function n(t, e, r, i) {
                        var u = t.children;
                        if (((t.x = e), (t.y = t.depth * i), (t.dx = r), (t.dy = i), u && (o = u.length))) {
                            var o,
                                a,
                                l,
                                c = -1;
                            for (r = t.value ? r / t.value : 0; ++c < o; ) n((a = u[c]), e, (l = a.value * r), i), (e += l);
                        }
                    })(
                        i[0],
                        0,
                        e[0],
                        e[1] /
                            (function n(t) {
                                var e = t.children,
                                    r = 0;
                                if (e && (i = e.length)) for (var i, u = -1; ++u < i; ) r = Math.max(r, n(e[u]));
                                return 1 + r;
                            })(i[0])
                    ),
                    i
                );
            }
            return (
                (r.size = function (n) {
                    return arguments.length ? ((e = n), r) : e;
                }),
                Au(r, t)
            );
        }),
        (n.layout.pie = function () {
            var t = Number,
                e = Du,
                r = 0,
                i = Nn,
                u = 0;
            function o(a) {
                var l,
                    c = a.length,
                    f = a.map(function (n, e) {
                        return +t.call(o, n, e);
                    }),
                    s = +("function" == typeof r ? r.apply(this, arguments) : r),
                    h = ("function" == typeof i ? i.apply(this, arguments) : i) - s,
                    p = Math.min(Math.abs(h) / c, +("function" == typeof u ? u.apply(this, arguments) : u)),
                    g = p * (h < 0 ? -1 : 1),
                    v = n.sum(f),
                    d = v ? (h - c * g) / v : 0,
                    y = n.range(c),
                    m = [];
                return (
                    null != e &&
                        y.sort(
                            e === Du
                                ? function (n, t) {
                                      return f[t] - f[n];
                                  }
                                : function (n, t) {
                                      return e(a[n], a[t]);
                                  }
                        ),
                    y.forEach(function (n) {
                        m[n] = { data: a[n], value: (l = f[n]), startAngle: s, endAngle: (s += l * d + g), padAngle: p };
                    }),
                    m
                );
            }
            return (
                (o.value = function (n) {
                    return arguments.length ? ((t = n), o) : t;
                }),
                (o.sort = function (n) {
                    return arguments.length ? ((e = n), o) : e;
                }),
                (o.startAngle = function (n) {
                    return arguments.length ? ((r = n), o) : r;
                }),
                (o.endAngle = function (n) {
                    return arguments.length ? ((i = n), o) : i;
                }),
                (o.padAngle = function (n) {
                    return arguments.length ? ((u = n), o) : u;
                }),
                o
            );
        });
    var Du = {};
    function Pu(n) {
        return n.x;
    }
    function Uu(n) {
        return n.y;
    }
    function ju(n, t, e) {
        (n.y0 = t), (n.y = e);
    }
    n.layout.stack = function () {
        var t = z,
            e = Ou,
            r = Iu,
            i = ju,
            u = Pu,
            o = Uu;
        function a(l, c) {
            if (!(p = l.length)) return l;
            var f = l.map(function (n, e) {
                    return t.call(a, n, e);
                }),
                s = f.map(function (n) {
                    return n.map(function (n, t) {
                        return [u.call(a, n, t), o.call(a, n, t)];
                    });
                }),
                h = e.call(a, s, c);
            (f = n.permute(f, h)), (s = n.permute(s, h));
            var p,
                g,
                v,
                d,
                y = r.call(a, s, c),
                m = f[0].length;
            for (v = 0; v < m; ++v) for (i.call(a, f[0][v], (d = y[v]), s[0][v][1]), g = 1; g < p; ++g) i.call(a, f[g][v], (d += s[g - 1][v][1]), s[g][v][1]);
            return l;
        }
        return (
            (a.values = function (n) {
                return arguments.length ? ((t = n), a) : t;
            }),
            (a.order = function (n) {
                return arguments.length ? ((e = "function" == typeof n ? n : Fu.get(n) || Ou), a) : e;
            }),
            (a.offset = function (n) {
                return arguments.length ? ((r = "function" == typeof n ? n : Hu.get(n) || Iu), a) : r;
            }),
            (a.x = function (n) {
                return arguments.length ? ((u = n), a) : u;
            }),
            (a.y = function (n) {
                return arguments.length ? ((o = n), a) : o;
            }),
            (a.out = function (n) {
                return arguments.length ? ((i = n), a) : i;
            }),
            a
        );
    };
    var Fu = n.map({
            "inside-out": function (t) {
                var e,
                    r,
                    i = t.length,
                    u = t.map(Yu),
                    o = t.map(Zu),
                    a = n.range(i).sort(function (n, t) {
                        return u[n] - u[t];
                    }),
                    l = 0,
                    c = 0,
                    f = [],
                    s = [];
                for (e = 0; e < i; ++e) (r = a[e]), l < c ? ((l += o[r]), f.push(r)) : ((c += o[r]), s.push(r));
                return s.reverse().concat(f);
            },
            reverse: function (t) {
                return n.range(t.length).reverse();
            },
            default: Ou,
        }),
        Hu = n.map({
            silhouette: function (n) {
                var t,
                    e,
                    r,
                    i = n.length,
                    u = n[0].length,
                    o = [],
                    a = 0,
                    l = [];
                for (e = 0; e < u; ++e) {
                    for (t = 0, r = 0; t < i; t++) r += n[t][e][1];
                    r > a && (a = r), o.push(r);
                }
                for (e = 0; e < u; ++e) l[e] = (a - o[e]) / 2;
                return l;
            },
            wiggle: function (n) {
                var t,
                    e,
                    r,
                    i,
                    u,
                    o,
                    a,
                    l,
                    c,
                    f = n.length,
                    s = n[0],
                    h = s.length,
                    p = [];
                for (p[0] = l = c = 0, e = 1; e < h; ++e) {
                    for (t = 0, i = 0; t < f; ++t) i += n[t][e][1];
                    for (t = 0, u = 0, a = s[e][0] - s[e - 1][0]; t < f; ++t) {
                        for (r = 0, o = (n[t][e][1] - n[t][e - 1][1]) / (2 * a); r < t; ++r) o += (n[r][e][1] - n[r][e - 1][1]) / a;
                        u += o * n[t][e][1];
                    }
                    (p[e] = l -= i ? (u / i) * a : 0), l < c && (c = l);
                }
                for (e = 0; e < h; ++e) p[e] -= c;
                return p;
            },
            expand: function (n) {
                var t,
                    e,
                    r,
                    i = n.length,
                    u = n[0].length,
                    o = 1 / i,
                    a = [];
                for (e = 0; e < u; ++e) {
                    for (t = 0, r = 0; t < i; t++) r += n[t][e][1];
                    if (r) for (t = 0; t < i; t++) n[t][e][1] /= r;
                    else for (t = 0; t < i; t++) n[t][e][1] = o;
                }
                for (e = 0; e < u; ++e) a[e] = 0;
                return a;
            },
            zero: Iu,
        });
    function Ou(t) {
        return n.range(t.length);
    }
    function Iu(n) {
        for (var t = -1, e = n[0].length, r = []; ++t < e; ) r[t] = 0;
        return r;
    }
    function Yu(n) {
        for (var t, e = 1, r = 0, i = n[0][1], u = n.length; e < u; ++e) (t = n[e][1]) > i && ((r = e), (i = t));
        return r;
    }
    function Zu(n) {
        return n.reduce(Vu, 0);
    }
    function Vu(n, t) {
        return n + t[1];
    }
    function Xu(n, t) {
        return $u(n, Math.ceil(Math.log(t.length) / Math.LN2 + 1));
    }
    function $u(n, t) {
        for (var e = -1, r = +n[0], i = (n[1] - r) / t, u = []; ++e <= t; ) u[e] = i * e + r;
        return u;
    }
    function Bu(t) {
        return [n.min(t), n.max(t)];
    }
    function Wu(n, t) {
        return n.value - t.value;
    }
    function Ju(n, t) {
        var e = n._pack_next;
        (n._pack_next = t), (t._pack_prev = n), (t._pack_next = e), (e._pack_prev = t);
    }
    function Gu(n, t) {
        (n._pack_next = t), (t._pack_prev = n);
    }
    function Ku(n, t) {
        var e = t.x - n.x,
            r = t.y - n.y,
            i = n.r + t.r;
        return 0.999 * i * i > e * e + r * r;
    }
    function Qu(n) {
        if ((t = n.children) && (l = t.length)) {
            var t,
                e,
                r,
                i,
                u,
                o,
                a,
                l,
                c = 1 / 0,
                f = -1 / 0,
                s = 1 / 0,
                h = -1 / 0;
            if ((t.forEach(no), ((e = t[0]).x = -e.r), (e.y = 0), M(e), l > 1 && (((r = t[1]).x = r.r), (r.y = 0), M(r), l > 2)))
                for (eo(e, r, (i = t[2])), M(i), Ju(e, i), e._pack_prev = i, Ju(i, r), r = e._pack_next, u = 3; u < l; u++) {
                    eo(e, r, (i = t[u]));
                    var p = 0,
                        g = 1,
                        v = 1;
                    for (o = r._pack_next; o !== r; o = o._pack_next, g++)
                        if (Ku(o, i)) {
                            p = 1;
                            break;
                        }
                    if (1 == p) for (a = e._pack_prev; a !== o._pack_prev && !Ku(a, i); a = a._pack_prev, v++);
                    p ? (g < v || (g == v && r.r < e.r) ? Gu(e, (r = o)) : Gu((e = a), r), u--) : (Ju(e, i), (r = i), M(i));
                }
            var d = (c + f) / 2,
                y = (s + h) / 2,
                m = 0;
            for (u = 0; u < l; u++) ((i = t[u]).x -= d), (i.y -= y), (m = Math.max(m, i.r + Math.sqrt(i.x * i.x + i.y * i.y)));
            (n.r = m), t.forEach(to);
        }
        function M(n) {
            (c = Math.min(n.x - n.r, c)), (f = Math.max(n.x + n.r, f)), (s = Math.min(n.y - n.r, s)), (h = Math.max(n.y + n.r, h));
        }
    }
    function no(n) {
        n._pack_next = n._pack_prev = n;
    }
    function to(n) {
        delete n._pack_next, delete n._pack_prev;
    }
    function eo(n, t, e) {
        var r = n.r + e.r,
            i = t.x - n.x,
            u = t.y - n.y;
        if (r && (i || u)) {
            var o = t.r + e.r,
                a = i * i + u * u,
                l = 0.5 + ((r *= r) - (o *= o)) / (2 * a),
                c = Math.sqrt(Math.max(0, 2 * o * (r + a) - (r -= a) * r - o * o)) / (2 * a);
            (e.x = n.x + l * i + c * u), (e.y = n.y + l * u - c * i);
        } else (e.x = n.x + r), (e.y = n.y);
    }
    function ro(n, t) {
        return n.parent == t.parent ? 1 : 2;
    }
    function io(n) {
        var t = n.children;
        return t.length ? t[0] : n.t;
    }
    function uo(n) {
        var t,
            e = n.children;
        return (t = e.length) ? e[t - 1] : n.t;
    }
    function oo(n, t, e) {
        var r = e / (t.i - n.i);
        (t.c -= r), (t.s += e), (n.c += r), (t.z += e), (t.m += e);
    }
    function ao(n, t, e) {
        return n.a.parent === t.parent ? n.a : e;
    }
    function lo(n) {
        return { x: n.x, y: n.y, dx: n.dx, dy: n.dy };
    }
    function co(n, t) {
        var e = n.x + t[3],
            r = n.y + t[0],
            i = n.dx - t[1] - t[3],
            u = n.dy - t[0] - t[2];
        return i < 0 && ((e += i / 2), (i = 0)), u < 0 && ((r += u / 2), (u = 0)), { x: e, y: r, dx: i, dy: u };
    }
    function fo(n) {
        var t = n[0],
            e = n[n.length - 1];
        return t < e ? [t, e] : [e, t];
    }
    function so(n) {
        return n.rangeExtent ? n.rangeExtent() : fo(n.range());
    }
    function ho(n, t, e, r) {
        var i = e(n[0], n[1]),
            u = r(t[0], t[1]);
        return function (n) {
            return u(i(n));
        };
    }
    function po(n, t) {
        var e,
            r = 0,
            i = n.length - 1,
            u = n[r],
            o = n[i];
        return o < u && ((e = r), (r = i), (i = e), (e = u), (u = o), (o = e)), (n[r] = t.floor(u)), (n[i] = t.ceil(o)), n;
    }
    function go(n) {
        return n
            ? {
                  floor: function (t) {
                      return Math.floor(t / n) * n;
                  },
                  ceil: function (t) {
                      return Math.ceil(t / n) * n;
                  },
              }
            : vo;
    }
    (n.layout.histogram = function () {
        var t = !0,
            e = Number,
            r = Bu,
            i = Xu;
        function u(u, o) {
            for (var a, l, c = [], f = u.map(e, this), s = r.call(this, f, o), h = i.call(this, s, f, o), p = ((o = -1), f.length), g = h.length - 1, v = t ? 1 : 1 / p; ++o < g; ) ((a = c[o] = []).dx = h[o + 1] - (a.x = h[o])), (a.y = 0);
            if (g > 0) for (o = -1; ++o < p; ) (l = f[o]) >= s[0] && l <= s[1] && (((a = c[n.bisect(h, l, 1, g) - 1]).y += v), a.push(u[o]));
            return c;
        }
        return (
            (u.value = function (n) {
                return arguments.length ? ((e = n), u) : e;
            }),
            (u.range = function (n) {
                return arguments.length ? ((r = dt(n)), u) : r;
            }),
            (u.bins = function (n) {
                return arguments.length
                    ? ((i =
                          "number" == typeof n
                              ? function (t) {
                                    return $u(t, n);
                                }
                              : dt(n)),
                      u)
                    : i;
            }),
            (u.frequency = function (n) {
                return arguments.length ? ((t = !!n), u) : t;
            }),
            u
        );
    }),
        (n.layout.pack = function () {
            var t,
                e = n.layout.hierarchy().sort(Wu),
                r = 0,
                i = [1, 1];
            function u(n, u) {
                var o = e.call(this, n, u),
                    a = o[0],
                    l = i[0],
                    c = i[1],
                    f =
                        null == t
                            ? Math.sqrt
                            : "function" == typeof t
                            ? t
                            : function () {
                                  return t;
                              };
                if (
                    ((a.x = a.y = 0),
                    zu(a, function (n) {
                        n.r = +f(n.value);
                    }),
                    zu(a, Qu),
                    r)
                ) {
                    var s = (r * (t ? 1 : Math.max((2 * a.r) / l, (2 * a.r) / c))) / 2;
                    zu(a, function (n) {
                        n.r += s;
                    }),
                        zu(a, Qu),
                        zu(a, function (n) {
                            n.r -= s;
                        });
                }
                return (
                    (function n(t, e, r, i) {
                        var u = t.children;
                        t.x = e += i * t.x;
                        t.y = r += i * t.y;
                        t.r *= i;
                        if (u) for (var o = -1, a = u.length; ++o < a; ) n(u[o], e, r, i);
                    })(a, l / 2, c / 2, t ? 1 : 1 / Math.max((2 * a.r) / l, (2 * a.r) / c)),
                    o
                );
            }
            return (
                (u.size = function (n) {
                    return arguments.length ? ((i = n), u) : i;
                }),
                (u.radius = function (n) {
                    return arguments.length ? ((t = null == n || "function" == typeof n ? n : +n), u) : t;
                }),
                (u.padding = function (n) {
                    return arguments.length ? ((r = +n), u) : r;
                }),
                Au(u, e)
            );
        }),
        (n.layout.tree = function () {
            var t = n.layout.hierarchy().sort(null).value(null),
                e = ro,
                r = [1, 1],
                i = null;
            function u(n, u) {
                var c = t.call(this, n, u),
                    f = c[0],
                    s = (function (n) {
                        var t,
                            e = { A: null, children: [n] },
                            r = [e];
                        for (; null != (t = r.pop()); )
                            for (var i, u = t.children, o = 0, a = u.length; o < a; ++o)
                                r.push(((u[o] = i = { _: u[o], parent: t, children: ((i = u[o].children) && i.slice()) || [], A: null, a: null, z: 0, m: 0, c: 0, s: 0, t: null, i: o }).a = i));
                        return e.children[0];
                    })(f);
                if ((zu(s, o), (s.parent.m = -s.z), Cu(s, a), i)) Cu(f, l);
                else {
                    var h = f,
                        p = f,
                        g = f;
                    Cu(f, function (n) {
                        n.x < h.x && (h = n), n.x > p.x && (p = n), n.depth > g.depth && (g = n);
                    });
                    var v = e(h, p) / 2 - h.x,
                        d = r[0] / (p.x + e(p, h) / 2 + v),
                        y = r[1] / (g.depth || 1);
                    Cu(f, function (n) {
                        (n.x = (n.x + v) * d), (n.y = n.depth * y);
                    });
                }
                return c;
            }
        function o(n) {
                var t = n.children,
                    r = n.parent.children,
                    i = n.i ? r[n.i - 1] : null;
                if (t.length) {
                    !(function (n) {
                        var t,
                            e = 0,
                            r = 0,
                            i = n.children,
                            u = i.length;
                        for (; --u >= 0; ) ((t = i[u]).z += e), (t.m += e), (e += t.s + (r += t.c));
                    })(n);
                    var u = (t[0].z + t[t.length - 1].z) / 2;
                    i ? ((n.z = i.z + e(n._, i._)), (n.m = n.z - u)) : (n.z = u);
                } else i && (n.z = i.z + e(n._, i._));
                n.parent.A = (function (n, t, r) {
                    if (t) {
                        for (var i, u = n, o = n, a = t, l = u.parent.children[0], c = u.m, f = o.m, s = a.m, h = l.m; (a = uo(a)), (u = io(u)), a && u; )
                            (l = io(l)), ((o = uo(o)).a = n), (i = a.z + s - u.z - c + e(a._, u._)) > 0 && (oo(ao(a, n, r), n, i), (c += i), (f += i)), (s += a.m), (c += u.m), (h += l.m), (f += o.m);
                        a && !uo(o) && ((o.t = a), (o.m += s - f)), u && !io(l) && ((l.t = u), (l.m += c - h), (r = n));
                    }
                    return r;
                })(n, i, n.parent.A || r[0]);
            }
        function a(n) {
            (n._.x = n.z + n.parent.m), (n.m += n.parent.m);
            }
        function l(n) {
            let xpos = n.x * r[0], ypos = n.depth * r[1];
            //if (n.depth >= 3) {
            //    if (n.parent.children.length === 1) { xpos = n.x * 60; ypos = n.depth * r[1] * (Math.floor(Math.random() * 3) + 1.5); }
            //    else { xpos = n.x * r[0]; ypos = n.depth * r[1]; }
            //}
            //else { xpos = n.x * r[0]; ypos = n.depth * r[1]; }

            if (n.depth >= 3 && (n.parent.children.length === 1)) { xpos = n.parent.x; }
            //if ((n.depth === 2 || n.depth >= 2 | && xpos !== n.parent.x) { xpos = xpos - 50;}
            (n.x = xpos, n.y =ypos );
        }
            return (
                (u.separation = function (n) {
                    return arguments.length ? ((e = n), u) : e;
                }),
                (u.size = function (n) {
                    return arguments.length ? ((i = null == (r = n) ? l : null), u) : i ? null : r;
                }),
                (u.nodeSize = function (n) {
                    return arguments.length ? ((i = null == (r = n) ? null : l), u) : i ? r : null;
                }),
                Au(u, t)
            );
        }),
        (n.layout.cluster = function () {
            var t = n.layout.hierarchy().sort(null).value(null),
                e = ro,
                r = [1, 1],
                i = !1;
            function u(u, o) {
                var a,
                    l = t.call(this, u, o),
                    c = l[0],
                    f = 0;
                zu(c, function (t) {
                    var r = t.children;
                    r && r.length
                        ? ((t.x = (function (n) {
                              return (
                                  n.reduce(function (n, t) {
                                      return n + t.x;
                                  }, 0) / n.length
                              );
                          })(r)),
                          (t.y = (function (t) {
                              return (
                                  1 +
                                  n.max(t, function (n) {
                                      return n.y;
                                  })
                              );
                          })(r)))
                        : ((t.x = a ? (f += e(t, a)) : 0), (t.y = 0), (a = t));
                });
                var s = (function n(t) {
                        var e = t.children;
                        return e && e.length ? n(e[0]) : t;
                    })(c),
                    h = (function n(t) {
                        var e,
                            r = t.children;
                        return r && (e = r.length) ? n(r[e - 1]) : t;
                    })(c),
                    p = s.x - e(s, h) / 2,
                    g = h.x + e(h, s) / 2;
                return (
                    zu(
                        c,
                        i
                            ? function (n) {
                                  (n.x = (n.x - c.x) * r[0]), (n.y = (c.y - n.y) * r[1]);
                              }
                            : function (n) {
                                  (n.x = ((n.x - p) / (g - p)) * r[0]), (n.y = (1 - (c.y ? n.y / c.y : 1)) * r[1]);
                              }
                    ),
                    l
                );
            }
            return (
                (u.separation = function (n) {
                    return arguments.length ? ((e = n), u) : e;
                }),
                (u.size = function (n) {
                    return arguments.length ? ((i = null == (r = n)), u) : i ? null : r;
                }),
                (u.nodeSize = function (n) {
                    return arguments.length ? ((i = null != (r = n)), u) : i ? r : null;
                }),
                Au(u, t)
            );
        }),
        (n.layout.treemap = function () {
            var t,
                e = n.layout.hierarchy(),
                r = Math.round,
                i = [1, 1],
                u = null,
                o = lo,
                a = !1,
                l = "squarify",
                c = 0.5 * (1 + Math.sqrt(5));
            function f(n, t) {
                for (var e, r, i = -1, u = n.length; ++i < u; ) (r = (e = n[i]).value * (t < 0 ? 0 : t)), (e.area = isNaN(r) || r <= 0 ? 0 : r);
            }
            function s(n) {
                var t = n.children;
                if (t && t.length) {
                    var e,
                        r,
                        i,
                        u = o(n),
                        a = [],
                        c = t.slice(),
                        h = 1 / 0,
                        v = "slice" === l ? u.dx : "dice" === l ? u.dy : "slice-dice" === l ? (1 & n.depth ? u.dy : u.dx) : Math.min(u.dx, u.dy);
                    for (f(c, (u.dx * u.dy) / n.value), a.area = 0; (i = c.length) > 0; )
                        a.push((e = c[i - 1])), (a.area += e.area), "squarify" !== l || (r = p(a, v)) <= h ? (c.pop(), (h = r)) : ((a.area -= a.pop().area), g(a, v, u, !1), (v = Math.min(u.dx, u.dy)), (a.length = a.area = 0), (h = 1 / 0));
                    a.length && (g(a, v, u, !0), (a.length = a.area = 0)), t.forEach(s);
                }
            }
            function h(n) {
                var t = n.children;
                if (t && t.length) {
                    var e,
                        r = o(n),
                        i = t.slice(),
                        u = [];
                    for (f(i, (r.dx * r.dy) / n.value), u.area = 0; (e = i.pop()); ) u.push(e), (u.area += e.area), null != e.z && (g(u, e.z ? r.dx : r.dy, r, !i.length), (u.length = u.area = 0));
                    t.forEach(h);
                }
            }
            function p(n, t) {
                for (var e, r = n.area, i = 0, u = 1 / 0, o = -1, a = n.length; ++o < a; ) (e = n[o].area) && (e < u && (u = e), e > i && (i = e));
                return (t *= t), (r *= r) ? Math.max((t * i * c) / r, r / (t * u * c)) : 1 / 0;
            }
            function g(n, t, e, i) {
                var u,
                    o = -1,
                    a = n.length,
                    l = e.x,
                    c = e.y,
                    f = t ? r(n.area / t) : 0;
                if (t == e.dx) {
                    for ((i || f > e.dy) && (f = e.dy); ++o < a; ) ((u = n[o]).x = l), (u.y = c), (u.dy = f), (l += u.dx = Math.min(e.x + e.dx - l, f ? r(u.area / f) : 0));
                    (u.z = !0), (u.dx += e.x + e.dx - l), (e.y += f), (e.dy -= f);
                } else {
                    for ((i || f > e.dx) && (f = e.dx); ++o < a; ) ((u = n[o]).x = l), (u.y = c), (u.dx = f), (c += u.dy = Math.min(e.y + e.dy - c, f ? r(u.area / f) : 0));
                    (u.z = !1), (u.dy += e.y + e.dy - c), (e.x += f), (e.dx -= f);
                }
            }
            function v(n) {
                var r = t || e(n),
                    u = r[0];
                return (u.x = u.y = 0), u.value ? ((u.dx = i[0]), (u.dy = i[1])) : (u.dx = u.dy = 0), t && e.revalue(u), f([u], (u.dx * u.dy) / u.value), (t ? h : s)(u), a && (t = r), r;
            }
            return (
                (v.size = function (n) {
                    return arguments.length ? ((i = n), v) : i;
                }),
                (v.padding = function (n) {
                    if (!arguments.length) return u;
                    function t(t) {
                        return co(t, n);
                    }
                    var e;
                    return (
                        (o =
                            null == (u = n)
                                ? lo
                                : "function" == (e = typeof n)
                                ? function (t) {
                                      var e = n.call(v, t, t.depth);
                                      return null == e ? lo(t) : co(t, "number" == typeof e ? [e, e, e, e] : e);
                                  }
                                : "number" === e
                                ? ((n = [n, n, n, n]), t)
                                : t),
                        v
                    );
                }),
                (v.round = function (n) {
                    return arguments.length ? ((r = n ? Math.round : Number), v) : r != Number;
                }),
                (v.sticky = function (n) {
                    return arguments.length ? ((a = n), (t = null), v) : a;
                }),
                (v.ratio = function (n) {
                    return arguments.length ? ((c = n), v) : c;
                }),
                (v.mode = function (n) {
                    return arguments.length ? ((l = n + ""), v) : l;
                }),
                Au(v, e)
            );
        }),
        (n.random = {
            normal: function (n, t) {
                var e = arguments.length;
                return (
                    e < 2 && (t = 1),
                    e < 1 && (n = 0),
                    function () {
                        var e, r, i;
                        do {
                            i = (e = 2 * Math.random() - 1) * e + (r = 2 * Math.random() - 1) * r;
                        } while (!i || i > 1);
                        return n + t * e * Math.sqrt((-2 * Math.log(i)) / i);
                    }
                );
            },
            logNormal: function () {
                var t = n.random.normal.apply(n, arguments);
                return function () {
                    return Math.exp(t());
                };
            },
            bates: function (t) {
                var e = n.random.irwinHall(t);
                return function () {
                    return e() / t;
                };
            },
            irwinHall: function (n) {
                return function () {
                    for (var t = 0, e = 0; e < n; e++) t += Math.random();
                    return t;
                };
            },
        }),
        (n.scale = {});
    var vo = { floor: z, ceil: z };
    function yo(t, e, r, i) {
        var u = [],
            o = [],
            a = 0,
            l = Math.min(t.length, e.length) - 1;
        for (t[l] < t[0] && ((t = t.slice().reverse()), (e = e.slice().reverse())); ++a <= l; ) u.push(r(t[a - 1], t[a])), o.push(i(e[a - 1], e[a]));
        return function (e) {
            var r = n.bisect(t, e, 1, l) - 1;
            return o[r](u[r](e));
        };
    }
    function mo(t, e) {
        return n.rebind(t, e, "range", "rangeRound", "interpolate", "clamp");
    }
    function Mo(n, t) {
        return po(n, go(xo(n, t)[2])), po(n, go(xo(n, t)[2])), n;
    }
    function xo(n, t) {
        null == t && (t = 10);
        var e = fo(n),
            r = e[1] - e[0],
            i = Math.pow(10, Math.floor(Math.log(r / t) / Math.LN10)),
            u = (t / r) * i;
        return u <= 0.15 ? (i *= 10) : u <= 0.35 ? (i *= 5) : u <= 0.75 && (i *= 2), (e[0] = Math.ceil(e[0] / i) * i), (e[1] = Math.floor(e[1] / i) * i + 0.5 * i), (e[2] = i), e;
    }
    function bo(t, e) {
        return n.range.apply(n, xo(t, e));
    }
    function _o(t, e, r) {
        var i = xo(t, e);
        if (r) {
            var u = Lt.exec(r);
            if ((u.shift(), "s" === u[8])) {
                var o = n.formatPrefix(Math.max(y(i[0]), y(i[1])));
                return (
                    u[7] || (u[7] = "." + So(o.scale(i[2]))),
                    (u[8] = "f"),
                    (r = n.format(u.join(""))),
                    function (n) {
                        return r(o.scale(n)) + o.symbol;
                    }
                );
            }
            u[7] ||
                (u[7] =
                    "." +
                    (function (n, t) {
                        var e = So(t[2]);
                        return n in wo ? Math.abs(e - So(Math.max(y(t[0]), y(t[1])))) + +("e" !== n) : e - 2 * ("%" === n);
                    })(u[8], i)),
                (r = u.join(""));
        } else r = ",." + So(i[2]) + "f";
        return n.format(r);
    }
    n.scale.linear = function () {
        return (function n(t, e, r, i) {
            var u, o;
            function a() {
                var n = Math.min(t.length, e.length) > 2 ? yo : ho,
                    a = i ? mu : yu;
                return (u = n(t, e, a, r)), (o = n(e, t, a, Ji)), l;
            }
            function l(n) {
                return u(n);
            }
            l.invert = function (n) {
                return o(n);
            };
            l.domain = function (n) {
                return arguments.length ? ((t = n.map(Number)), a()) : t;
            };
            l.range = function (n) {
                return arguments.length ? ((e = n), a()) : e;
            };
            l.rangeRound = function (n) {
                return l.range(n).interpolate(fu);
            };
            l.clamp = function (n) {
                return arguments.length ? ((i = n), a()) : i;
            };
            l.interpolate = function (n) {
                return arguments.length ? ((r = n), a()) : r;
            };
            l.ticks = function (n) {
                return bo(t, n);
            };
            l.tickFormat = function (n, e) {
                return _o(t, n, e);
            };
            l.nice = function (n) {
                return Mo(t, n), a();
            };
            l.copy = function () {
                return n(t, e, r, i);
            };
            return a();
        })([0, 1], [0, 1], Ji, !1);
    };
    var wo = { s: 1, g: 1, p: 1, r: 1, e: 1 };
    function So(n) {
        return -Math.floor(Math.log(n) / Math.LN10 + 0.01);
    }
    n.scale.log = function () {
        return (function t(e, r, i, u) {
            function o(n) {
                return (i ? Math.log(n < 0 ? 0 : n) : -Math.log(n > 0 ? 0 : -n)) / Math.log(r);
            }
            function a(n) {
                return i ? Math.pow(r, n) : -Math.pow(r, -n);
            }
            function l(n) {
                return e(o(n));
            }
            l.invert = function (n) {
                return a(e.invert(n));
            };
            l.domain = function (n) {
                return arguments.length ? ((i = n[0] >= 0), e.domain((u = n.map(Number)).map(o)), l) : u;
            };
            l.base = function (n) {
                return arguments.length ? ((r = +n), e.domain(u.map(o)), l) : r;
            };
            l.nice = function () {
                var n = po(u.map(o), i ? Math : No);
                return e.domain(n), (u = n.map(a)), l;
            };
            l.ticks = function () {
                var n = fo(u),
                    t = [],
                    e = n[0],
                    l = n[1],
                    c = Math.floor(o(e)),
                    f = Math.ceil(o(l)),
                    s = r % 1 ? 2 : r;
                if (isFinite(f - c)) {
                    if (i) {
                        for (; c < f; c++) for (var h = 1; h < s; h++) t.push(a(c) * h);
                        t.push(a(c));
                    } else for (t.push(a(c)); c++ < f; ) for (var h = s - 1; h > 0; h--) t.push(a(c) * h);
                    for (c = 0; t[c] < e; c++);
                    for (f = t.length; t[f - 1] > l; f--);
                    t = t.slice(c, f);
                }
                return t;
            };
            l.tickFormat = function (t, e) {
                if (!arguments.length) return ko;
                arguments.length < 2 ? (e = ko) : "function" != typeof e && (e = n.format(e));
                var i = Math.max(1, (r * t) / l.ticks().length);
                return function (n) {
                    var t = n / a(Math.round(o(n)));
                    return t * r < r - 0.5 && (t *= r), t <= i ? e(n) : "";
                };
            };
            l.copy = function () {
                return t(e.copy(), r, i, u);
            };
            return mo(l, e);
        })(n.scale.linear().domain([0, 1]), 10, !0, [1, 10]);
    };
    var ko = n.format(".0e"),
        No = {
            floor: function (n) {
                return -Math.ceil(-n);
            },
            ceil: function (n) {
                return -Math.floor(-n);
            },
        };
    function Eo(n) {
        return function (t) {
            return t < 0 ? -Math.pow(-t, n) : Math.pow(t, n);
        };
    }
    (n.scale.pow = function () {
        return (function n(t, e, r) {
            var i = Eo(e),
                u = Eo(1 / e);
            function o(n) {
                return t(i(n));
            }
            o.invert = function (n) {
                return u(t.invert(n));
            };
            o.domain = function (n) {
                return arguments.length ? (t.domain((r = n.map(Number)).map(i)), o) : r;
            };
            o.ticks = function (n) {
                return bo(r, n);
            };
            o.tickFormat = function (n, t) {
                return _o(r, n, t);
            };
            o.nice = function (n) {
                return o.domain(Mo(r, n));
            };
            o.exponent = function (n) {
                return arguments.length ? ((i = Eo((e = n))), (u = Eo(1 / e)), t.domain(r.map(i)), o) : e;
            };
            o.copy = function () {
                return n(t.copy(), e, r);
            };
            return mo(o, t);
        })(n.scale.linear(), 1, [0, 1]);
    }),
        (n.scale.sqrt = function () {
            return n.scale.pow().exponent(0.5);
        }),
        (n.scale.ordinal = function () {
            return (function t(e, r) {
                var i, u, o;
                function a(n) {
                    return u[((i.get(n) || ("range" === r.t ? i.set(n, e.push(n)) : NaN)) - 1) % u.length];
                }
                function l(t, r) {
                    return n.range(e.length).map(function (n) {
                        return t + r * n;
                    });
                }
                a.domain = function (n) {
                    if (!arguments.length) return e;
                    (e = []), (i = new M());
                    for (var t, u = -1, o = n.length; ++u < o; ) i.has((t = n[u])) || i.set(t, e.push(t));
                    return a[r.t].apply(a, r.a);
                };
                a.range = function (n) {
                    return arguments.length ? ((u = n), (o = 0), (r = { t: "range", a: arguments }), a) : u;
                };
                a.rangePoints = function (n, t) {
                    arguments.length < 2 && (t = 0);
                    var i = n[0],
                        c = n[1],
                        f = e.length < 2 ? ((i = (i + c) / 2), 0) : (c - i) / (e.length - 1 + t);
                    return (u = l(i + (f * t) / 2, f)), (o = 0), (r = { t: "rangePoints", a: arguments }), a;
                };
                a.rangeRoundPoints = function (n, t) {
                    arguments.length < 2 && (t = 0);
                    var i = n[0],
                        c = n[1],
                        f = e.length < 2 ? ((i = c = Math.round((i + c) / 2)), 0) : ((c - i) / (e.length - 1 + t)) | 0;
                    return (u = l(i + Math.round((f * t) / 2 + (c - i - (e.length - 1 + t) * f) / 2), f)), (o = 0), (r = { t: "rangeRoundPoints", a: arguments }), a;
                };
                a.rangeBands = function (n, t, i) {
                    arguments.length < 2 && (t = 0), arguments.length < 3 && (i = t);
                    var c = n[1] < n[0],
                        f = n[c - 0],
                        s = n[1 - c],
                        h = (s - f) / (e.length - t + 2 * i);
                    return (u = l(f + h * i, h)), c && u.reverse(), (o = h * (1 - t)), (r = { t: "rangeBands", a: arguments }), a;
                };
                a.rangeRoundBands = function (n, t, i) {
                    arguments.length < 2 && (t = 0), arguments.length < 3 && (i = t);
                    var c = n[1] < n[0],
                        f = n[c - 0],
                        s = n[1 - c],
                        h = Math.floor((s - f) / (e.length - t + 2 * i));
                    return (u = l(f + Math.round((s - f - (e.length - t) * h) / 2), h)), c && u.reverse(), (o = Math.round(h * (1 - t))), (r = { t: "rangeRoundBands", a: arguments }), a;
                };
                a.rangeBand = function () {
                    return o;
                };
                a.rangeExtent = function () {
                    return fo(r.a[0]);
                };
                a.copy = function () {
                    return t(e, r);
                };
                return a.domain(e);
            })([], { t: "range", a: [[]] });
        }),
        (n.scale.category10 = function () {
            return n.scale.ordinal().range(Ao);
        }),
        (n.scale.category20 = function () {
            return n.scale.ordinal().range(Co);
        }),
        (n.scale.category20b = function () {
            return n.scale.ordinal().range(zo);
        }),
        (n.scale.category20c = function () {
            return n.scale.ordinal().range(Lo);
        });
    var Ao = [2062260, 16744206, 2924588, 14034728, 9725885, 9197131, 14907330, 8355711, 12369186, 1556175].map(at),
        Co = [2062260, 11454440, 16744206, 16759672, 2924588, 10018698, 14034728, 16750742, 9725885, 12955861, 9197131, 12885140, 14907330, 16234194, 8355711, 13092807, 12369186, 14408589, 1556175, 10410725].map(at),
        zo = [3750777, 5395619, 7040719, 10264286, 6519097, 9216594, 11915115, 13556636, 9202993, 12426809, 15186514, 15190932, 8666169, 11356490, 14049643, 15177372, 8077683, 10834324, 13528509, 14589654].map(at),
        Lo = [3244733, 7057110, 10406625, 13032431, 15095053, 16616764, 16625259, 16634018, 3253076, 7652470, 10607003, 13101504, 7695281, 10394312, 12369372, 14342891, 6513507, 9868950, 12434877, 14277081].map(at);
    function qo() {
        return 0;
    }
    (n.scale.quantile = function () {
        return (function t(e, r) {
            var i;
            function u() {
                var t = 0,
                    u = r.length;
                for (i = []; ++t < u; ) i[t - 1] = n.quantile(e, t / u);
                return o;
            }
            function o(t) {
                if (!isNaN((t = +t))) return r[n.bisect(i, t)];
            }
            o.domain = function (n) {
                return arguments.length ? ((e = n.map(h).filter(p).sort(s)), u()) : e;
            };
            o.range = function (n) {
                return arguments.length ? ((r = n), u()) : r;
            };
            o.quantiles = function () {
                return i;
            };
            o.invertExtent = function (n) {
                return (n = r.indexOf(n)) < 0 ? [NaN, NaN] : [n > 0 ? i[n - 1] : e[0], n < i.length ? i[n] : e[e.length - 1]];
            };
            o.copy = function () {
                return t(e, r);
            };
            return u();
        })([], []);
    }),
        (n.scale.quantize = function () {
            return (function n(t, e, r) {
                var i, u;
                function o(n) {
                    return r[Math.max(0, Math.min(u, Math.floor(i * (n - t))))];
                }
                function a() {
                    return (i = r.length / (e - t)), (u = r.length - 1), o;
                }
                o.domain = function (n) {
                    return arguments.length ? ((t = +n[0]), (e = +n[n.length - 1]), a()) : [t, e];
                };
                o.range = function (n) {
                    return arguments.length ? ((r = n), a()) : r;
                };
                o.invertExtent = function (n) {
                    return [(n = (n = r.indexOf(n)) < 0 ? NaN : n / i + t), n + 1 / i];
                };
                o.copy = function () {
                    return n(t, e, r);
                };
                return a();
            })(0, 1, [0, 1]);
        }),
        (n.scale.threshold = function () {
            return (function t(e, r) {
                function i(t) {
                    if (t <= t) return r[n.bisect(e, t)];
                }
                i.domain = function (n) {
                    return arguments.length ? ((e = n), i) : e;
                };
                i.range = function (n) {
                    return arguments.length ? ((r = n), i) : r;
                };
                i.invertExtent = function (n) {
                    return (n = r.indexOf(n)), [e[n - 1], e[n]];
                };
                i.copy = function () {
                    return t(e, r);
                };
                return i;
            })([0.5], [0, 1]);
        }),
        (n.scale.identity = function () {
            return (function n(t) {
                function e(n) {
                    return +n;
                }
                e.invert = e;
                e.domain = e.range = function (n) {
                    return arguments.length ? ((t = n.map(e)), e) : t;
                };
                e.ticks = function (n) {
                    return bo(t, n);
                };
                e.tickFormat = function (n, e) {
                    return _o(t, n, e);
                };
                e.copy = function () {
                    return n(t);
                };
                return e;
            })([0, 1]);
        }),
        (n.svg = {}),
        (n.svg.arc = function () {
            var n = Ro,
                t = Do,
                e = qo,
                r = To,
                i = Po,
                u = Uo,
                o = jo;
            function a() {
                var a = Math.max(0, +n.apply(this, arguments)),
                    c = Math.max(0, +t.apply(this, arguments)),
                    f = i.apply(this, arguments) - An,
                    s = u.apply(this, arguments) - An,
                    h = Math.abs(s - f),
                    p = f > s ? 0 : 1;
                if ((c < a && ((g = c), (c = a), (a = g)), h >= En)) return l(c, p) + (a ? l(a, 1 - p) : "") + "Z";
                var g,
                    v,
                    d,
                    y,
                    m,
                    M,
                    x,
                    b,
                    _,
                    w,
                    S,
                    k,
                    N = 0,
                    E = 0,
                    A = [];
                if (((y = (+o.apply(this, arguments) || 0) / 2) && ((d = r === To ? Math.sqrt(a * a + c * c) : +r.apply(this, arguments)), p || (E *= -1), c && (E = Rn((d / c) * Math.sin(y))), a && (N = Rn((d / a) * Math.sin(y)))), c)) {
                    (m = c * Math.cos(f + E)), (M = c * Math.sin(f + E)), (x = c * Math.cos(s - E)), (b = c * Math.sin(s - E));
                    var C = Math.abs(s - f - 2 * E) <= kn ? 0 : 1;
                    if (E && (Fo(m, M, x, b) === p) ^ C) {
                        var z = (f + s) / 2;
                        (m = c * Math.cos(z)), (M = c * Math.sin(z)), (x = b = null);
                    }
                } else m = M = 0;
                if (a) {
                    (_ = a * Math.cos(s - N)), (w = a * Math.sin(s - N)), (S = a * Math.cos(f + N)), (k = a * Math.sin(f + N));
                    var L = Math.abs(f - s + 2 * N) <= kn ? 0 : 1;
                    if (N && (Fo(_, w, S, k) === 1 - p) ^ L) {
                        var q = (f + s) / 2;
                        (_ = a * Math.cos(q)), (w = a * Math.sin(q)), (S = k = null);
                    }
                } else _ = w = 0;
                if (h > wn && (g = Math.min(Math.abs(c - a) / 2, +e.apply(this, arguments))) > 0.001) {
                    v = (a < c) ^ p ? 0 : 1;
                    var T = g,
                        R = g;
                    if (h < kn) {
                        var D = null == S ? [_, w] : null == x ? [m, M] : li([m, M], [S, k], [x, b], [_, w]),
                            P = m - D[0],
                            U = M - D[1],
                            j = x - D[0],
                            F = b - D[1],
                            H = 1 / Math.sin(Math.acos((P * j + U * F) / (Math.sqrt(P * P + U * U) * Math.sqrt(j * j + F * F))) / 2),
                            O = Math.sqrt(D[0] * D[0] + D[1] * D[1]);
                        (R = Math.min(g, (a - O) / (H - 1))), (T = Math.min(g, (c - O) / (H + 1)));
                    }
                    if (null != x) {
                        var I = Ho(null == S ? [_, w] : [S, k], [m, M], c, T, p),
                            Y = Ho([x, b], [_, w], c, T, p);
                        g === T
                            ? A.push("M", I[0], "A", T, ",", T, " 0 0,", v, " ", I[1], "A", c, ",", c, " 0 ", (1 - p) ^ Fo(I[1][0], I[1][1], Y[1][0], Y[1][1]), ",", p, " ", Y[1], "A", T, ",", T, " 0 0,", v, " ", Y[0])
                            : A.push("M", I[0], "A", T, ",", T, " 0 1,", v, " ", Y[0]);
                    } else A.push("M", m, ",", M);
                    if (null != S) {
                        var Z = Ho([m, M], [S, k], a, -R, p),
                            V = Ho([_, w], null == x ? [m, M] : [x, b], a, -R, p);
                        g === R
                            ? A.push("L", V[0], "A", R, ",", R, " 0 0,", v, " ", V[1], "A", a, ",", a, " 0 ", p ^ Fo(V[1][0], V[1][1], Z[1][0], Z[1][1]), ",", 1 - p, " ", Z[1], "A", R, ",", R, " 0 0,", v, " ", Z[0])
                            : A.push("L", V[0], "A", R, ",", R, " 0 0,", v, " ", Z[0]);
                    } else A.push("L", _, ",", w);
                } else A.push("M", m, ",", M), null != x && A.push("A", c, ",", c, " 0 ", C, ",", p, " ", x, ",", b), A.push("L", _, ",", w), null != S && A.push("A", a, ",", a, " 0 ", L, ",", 1 - p, " ", S, ",", k);
                return A.push("Z"), A.join("");
            }
            function l(n, t) {
                return "M0," + n + "A" + n + "," + n + " 0 1," + t + " 0," + -n + "A" + n + "," + n + " 0 1," + t + " 0," + n;
            }
            return (
                (a.innerRadius = function (t) {
                    return arguments.length ? ((n = dt(t)), a) : n;
                }),
                (a.outerRadius = function (n) {
                    return arguments.length ? ((t = dt(n)), a) : t;
                }),
                (a.cornerRadius = function (n) {
                    return arguments.length ? ((e = dt(n)), a) : e;
                }),
                (a.padRadius = function (n) {
                    return arguments.length ? ((r = n == To ? To : dt(n)), a) : r;
                }),
                (a.startAngle = function (n) {
                    return arguments.length ? ((i = dt(n)), a) : i;
                }),
                (a.endAngle = function (n) {
                    return arguments.length ? ((u = dt(n)), a) : u;
                }),
                (a.padAngle = function (n) {
                    return arguments.length ? ((o = dt(n)), a) : o;
                }),
                (a.centroid = function () {
                    var e = (+n.apply(this, arguments) + +t.apply(this, arguments)) / 2,
                        r = (+i.apply(this, arguments) + +u.apply(this, arguments)) / 2 - An;
                    return [Math.cos(r) * e, Math.sin(r) * e];
                }),
                a
            );
        });
    var To = "auto";
    function Ro(n) {
        return n.innerRadius;
    }
    function Do(n) {
        return n.outerRadius;
    }
    function Po(n) {
        return n.startAngle;
    }
    function Uo(n) {
        return n.endAngle;
    }
    function jo(n) {
        return n && n.padAngle;
    }
    function Fo(n, t, e, r) {
        return (n - e) * t - (t - r) * n > 0 ? 0 : 1;
    }
    function Ho(n, t, e, r, i) {
        var u = n[0] - t[0],
            o = n[1] - t[1],
            a = (i ? r : -r) / Math.sqrt(u * u + o * o),
            l = a * o,
            c = -a * u,
            f = n[0] + l,
            s = n[1] + c,
            h = t[0] + l,
            p = t[1] + c,
            g = (f + h) / 2,
            v = (s + p) / 2,
            d = h - f,
            y = p - s,
            m = d * d + y * y,
            M = e - r,
            x = f * p - h * s,
            b = (y < 0 ? -1 : 1) * Math.sqrt(Math.max(0, M * M * m - x * x)),
            _ = (x * y - d * b) / m,
            w = (-x * d - y * b) / m,
            S = (x * y + d * b) / m,
            k = (-x * d + y * b) / m,
            N = _ - g,
            E = w - v,
            A = S - g,
            C = k - v;
        return (
            N * N + E * E > A * A + C * C && ((_ = S), (w = k)),
            [
                [_ - l, w - c],
                [(_ * e) / M, (w * e) / M],
            ]
        );
    }
    function Oo(n) {
        var t = ei,
            e = ri,
            r = Be,
            i = Yo,
            u = i.key,
            o = 0.7;
        function a(u) {
            var a,
                l = [],
                c = [],
                f = -1,
                s = u.length,
                h = dt(t),
                p = dt(e);
            function g() {
                l.push("M", i(n(c), o));
            }
            for (; ++f < s; ) r.call(this, (a = u[f]), f) ? c.push([+h.call(this, a, f), +p.call(this, a, f)]) : c.length && (g(), (c = []));
            return c.length && g(), l.length ? l.join("") : null;
        }
        return (
            (a.x = function (n) {
                return arguments.length ? ((t = n), a) : t;
            }),
            (a.y = function (n) {
                return arguments.length ? ((e = n), a) : e;
            }),
            (a.defined = function (n) {
                return arguments.length ? ((r = n), a) : r;
            }),
            (a.interpolate = function (n) {
                return arguments.length ? ((u = "function" == typeof n ? (i = n) : (i = Io.get(n) || Yo).key), a) : u;
            }),
            (a.tension = function (n) {
                return arguments.length ? ((o = n), a) : o;
            }),
            a
        );
    }
    n.svg.line = function () {
        return Oo(z);
    };
    var Io = n.map({
        linear: Yo,
        "linear-closed": Zo,
        step: function (n) {
            var t = 0,
                e = n.length,
                r = n[0],
                i = [r[0], ",", r[1]];
            for (; ++t < e; ) i.push("H", (r[0] + (r = n[t])[0]) / 2, "V", r[1]);
            e > 1 && i.push("H", r[0]);
            return i.join("");
        },
        "step-before": Vo,
        "step-after": Xo,
        basis: Wo,
        "basis-open": function (n) {
            if (n.length < 4) return Yo(n);
            var t,
                e = [],
                r = -1,
                i = n.length,
                u = [0],
                o = [0];
            for (; ++r < 3; ) (t = n[r]), u.push(t[0]), o.push(t[1]);
            e.push(Jo(Qo, u) + "," + Jo(Qo, o)), --r;
            for (; ++r < i; ) (t = n[r]), u.shift(), u.push(t[0]), o.shift(), o.push(t[1]), na(e, u, o);
            return e.join("");
        },
        "basis-closed": function (n) {
            var t,
                e,
                r = -1,
                i = n.length,
                u = i + 4,
                o = [],
                a = [];
            for (; ++r < 4; ) (e = n[r % i]), o.push(e[0]), a.push(e[1]);
            (t = [Jo(Qo, o), ",", Jo(Qo, a)]), --r;
            for (; ++r < u; ) (e = n[r % i]), o.shift(), o.push(e[0]), a.shift(), a.push(e[1]), na(t, o, a);
            return t.join("");
        },
        bundle: function (n, t) {
            var e = n.length - 1;
            if (e) for (var r, i, u = n[0][0], o = n[0][1], a = n[e][0] - u, l = n[e][1] - o, c = -1; ++c <= e; ) (r = n[c]), (i = c / e), (r[0] = t * r[0] + (1 - t) * (u + i * a)), (r[1] = t * r[1] + (1 - t) * (o + i * l));
            return Wo(n);
        },
        cardinal: function (n, t) {
            return n.length < 3 ? Yo(n) : n[0] + $o(n, Bo(n, t));
        },
        "cardinal-open": function (n, t) {
            return n.length < 4 ? Yo(n) : n[1] + $o(n.slice(1, -1), Bo(n, t));
        },
        "cardinal-closed": function (n, t) {
            return n.length < 3 ? Zo(n) : n[0] + $o((n.push(n[0]), n), Bo([n[n.length - 2]].concat(n, [n[1]]), t));
        },
        monotone: function (n) {
            return n.length < 3
                ? Yo(n)
                : n[0] +
                      $o(
                          n,
                          (function (n) {
                              var t,
                                  e,
                                  r,
                                  i,
                                  u = [],
                                  o = (function (n) {
                                      var t = 0,
                                          e = n.length - 1,
                                          r = [],
                                          i = n[0],
                                          u = n[1],
                                          o = (r[0] = ta(i, u));
                                      for (; ++t < e; ) r[t] = (o + (o = ta((i = u), (u = n[t + 1])))) / 2;
                                      return (r[t] = o), r;
                                  })(n),
                                  a = -1,
                                  l = n.length - 1;
                              for (; ++a < l; )
                                  (t = ta(n[a], n[a + 1])), y(t) < wn ? (o[a] = o[a + 1] = 0) : ((e = o[a] / t), (r = o[a + 1] / t), (i = e * e + r * r) > 9 && ((i = (3 * t) / Math.sqrt(i)), (o[a] = i * e), (o[a + 1] = i * r)));
                              a = -1;
                              for (; ++a <= l; ) (i = (n[Math.min(l, a + 1)][0] - n[Math.max(0, a - 1)][0]) / (6 * (1 + o[a] * o[a]))), u.push([i || 0, o[a] * i || 0]);
                              return u;
                          })(n)
                      );
        },
    });
    function Yo(n) {
        return n.length > 1 ? n.join("L") : n + "Z";
    }
    function Zo(n) {
        return n.join("L") + "Z";
    }
    function Vo(n) {
        for (var t = 0, e = n.length, r = n[0], i = [r[0], ",", r[1]]; ++t < e; ) i.push("V", (r = n[t])[1], "H", r[0]);
        return i.join("");
    }
    function Xo(n) {
        for (var t = 0, e = n.length, r = n[0], i = [r[0], ",", r[1]]; ++t < e; ) i.push("H", (r = n[t])[0], "V", r[1]);
        return i.join("");
    }
    function $o(n, t) {
        if (t.length < 1 || (n.length != t.length && n.length != t.length + 2)) return Yo(n);
        var e = n.length != t.length,
            r = "",
            i = n[0],
            u = n[1],
            o = t[0],
            a = o,
            l = 1;
        if ((e && ((r += "Q" + (u[0] - (2 * o[0]) / 3) + "," + (u[1] - (2 * o[1]) / 3) + "," + u[0] + "," + u[1]), (i = n[1]), (l = 2)), t.length > 1)) {
            (a = t[1]), (u = n[l]), l++, (r += "C" + (i[0] + o[0]) + "," + (i[1] + o[1]) + "," + (u[0] - a[0]) + "," + (u[1] - a[1]) + "," + u[0] + "," + u[1]);
            for (var c = 2; c < t.length; c++, l++) (u = n[l]), (a = t[c]), (r += "S" + (u[0] - a[0]) + "," + (u[1] - a[1]) + "," + u[0] + "," + u[1]);
        }
        if (e) {
            var f = n[l];
            r += "Q" + (u[0] + (2 * a[0]) / 3) + "," + (u[1] + (2 * a[1]) / 3) + "," + f[0] + "," + f[1];
        }
        return r;
    }
    function Bo(n, t) {
        for (var e, r = [], i = (1 - t) / 2, u = n[0], o = n[1], a = 1, l = n.length; ++a < l; ) (e = u), (u = o), (o = n[a]), r.push([i * (o[0] - e[0]), i * (o[1] - e[1])]);
        return r;
    }
    function Wo(n) {
        if (n.length < 3) return Yo(n);
        var t = 1,
            e = n.length,
            r = n[0],
            i = r[0],
            u = r[1],
            o = [i, i, i, (r = n[1])[0]],
            a = [u, u, u, r[1]],
            l = [i, ",", u, "L", Jo(Qo, o), ",", Jo(Qo, a)];
        for (n.push(n[e - 1]); ++t <= e; ) (r = n[t]), o.shift(), o.push(r[0]), a.shift(), a.push(r[1]), na(l, o, a);
        return n.pop(), l.push("L", r), l.join("");
    }
    function Jo(n, t) {
        return n[0] * t[0] + n[1] * t[1] + n[2] * t[2] + n[3] * t[3];
    }
    Io.forEach(function (n, t) {
        (t.key = n), (t.closed = /-closed$/.test(n));
    });
    var Go = [0, 2 / 3, 1 / 3, 0],
        Ko = [0, 1 / 3, 2 / 3, 0],
        Qo = [0, 1 / 6, 2 / 3, 1 / 6];
    function na(n, t, e) {
        n.push("C", Jo(Go, t), ",", Jo(Go, e), ",", Jo(Ko, t), ",", Jo(Ko, e), ",", Jo(Qo, t), ",", Jo(Qo, e));
    }
    function ta(n, t) {
        return (t[1] - n[1]) / (t[0] - n[0]);
    }
    function ea(n) {
        for (var t, e, r, i = -1, u = n.length; ++i < u; ) (e = (t = n[i])[0]), (r = t[1] - An), (t[0] = e * Math.cos(r)), (t[1] = e * Math.sin(r));
        return n;
    }
    function ra(n) {
        var t = ei,
            e = ei,
            r = 0,
            i = ri,
            u = Be,
            o = Yo,
            a = o.key,
            l = o,
            c = "L",
            f = 0.7;
        function s(a) {
            var s,
                h,
                p,
                g = [],
                v = [],
                d = [],
                y = -1,
                m = a.length,
                M = dt(t),
                x = dt(r),
                b =
                    t === e
                        ? function () {
                              return h;
                          }
                        : dt(e),
                _ =
                    r === i
                        ? function () {
                              return p;
                          }
                        : dt(i);
            function w() {
                g.push("M", o(n(d), f), c, l(n(v.reverse()), f), "Z");
            }
            for (; ++y < m; ) u.call(this, (s = a[y]), y) ? (v.push([(h = +M.call(this, s, y)), (p = +x.call(this, s, y))]), d.push([+b.call(this, s, y), +_.call(this, s, y)])) : v.length && (w(), (v = []), (d = []));
            return v.length && w(), g.length ? g.join("") : null;
        }
        return (
            (s.x = function (n) {
                return arguments.length ? ((t = e = n), s) : e;
            }),
            (s.x0 = function (n) {
                return arguments.length ? ((t = n), s) : t;
            }),
            (s.x1 = function (n) {
                return arguments.length ? ((e = n), s) : e;
            }),
            (s.y = function (n) {
                return arguments.length ? ((r = i = n), s) : i;
            }),
            (s.y0 = function (n) {
                return arguments.length ? ((r = n), s) : r;
            }),
            (s.y1 = function (n) {
                return arguments.length ? ((i = n), s) : i;
            }),
            (s.defined = function (n) {
                return arguments.length ? ((u = n), s) : u;
            }),
            (s.interpolate = function (n) {
                return arguments.length ? ((a = "function" == typeof n ? (o = n) : (o = Io.get(n) || Yo).key), (l = o.reverse || o), (c = o.closed ? "M" : "L"), s) : a;
            }),
            (s.tension = function (n) {
                return arguments.length ? ((f = n), s) : f;
            }),
            s
        );
    }
    function ia(n) {
        return n.radius;
    }
    function ua(n) {
        return [n.x, n.y];
    }
    function oa() {
        return 64;
    }
    function aa() {
        return "circle";
    }
    function la(n) {
        var t = Math.sqrt(n / kn);
        return "M0," + t + "A" + t + "," + t + " 0 1,1 0," + -t + "A" + t + "," + t + " 0 1,1 0," + t + "Z";
    }
    (n.svg.line.radial = function () {
        var n = Oo(ea);
        return (n.radius = n.x), delete n.x, (n.angle = n.y), delete n.y, n;
    }),
        (Vo.reverse = Xo),
        (Xo.reverse = Vo),
        (n.svg.area = function () {
            return ra(z);
        }),
        (n.svg.area.radial = function () {
            var n = ra(ea);
            return (n.radius = n.x), delete n.x, (n.innerRadius = n.x0), delete n.x0, (n.outerRadius = n.x1), delete n.x1, (n.angle = n.y), delete n.y, (n.startAngle = n.y0), delete n.y0, (n.endAngle = n.y1), delete n.y1, n;
        }),
        (n.svg.chord = function () {
            var n = Ir,
                t = Yr,
                e = ia,
                r = Po,
                i = Uo;
            function u(e, r) {
                var i,
                    u,
                    c = o(this, n, e, r),
                    f = o(this, t, e, r);
                return "M" + c.p0 + a(c.r, c.p1, c.a1 - c.a0) + ((u = f), (i = c).a0 == u.a0 && i.a1 == u.a1 ? l(c.r, c.p1, c.r, c.p0) : l(c.r, c.p1, f.r, f.p0) + a(f.r, f.p1, f.a1 - f.a0) + l(f.r, f.p1, c.r, c.p0)) + "Z";
            }
            function o(n, t, u, o) {
                var a = t.call(n, u, o),
                    l = e.call(n, a, o),
                    c = r.call(n, a, o) - An,
                    f = i.call(n, a, o) - An;
                return { r: l, a0: c, a1: f, p0: [l * Math.cos(c), l * Math.sin(c)], p1: [l * Math.cos(f), l * Math.sin(f)] };
            }
            function a(n, t, e) {
                return "A" + n + "," + n + " 0 " + +(e > kn) + ",1 " + t;
            }
            function l(n, t, e, r) {
                return "Q 0,0 " + r;
            }
            return (
                (u.radius = function (n) {
                    return arguments.length ? ((e = dt(n)), u) : e;
                }),
                (u.source = function (t) {
                    return arguments.length ? ((n = dt(t)), u) : n;
                }),
                (u.target = function (n) {
                    return arguments.length ? ((t = dt(n)), u) : t;
                }),
                (u.startAngle = function (n) {
                    return arguments.length ? ((r = dt(n)), u) : r;
                }),
                (u.endAngle = function (n) {
                    return arguments.length ? ((i = dt(n)), u) : i;
                }),
                u
            );
        }),
        (n.svg.diagonal = function () {
            var n = Ir,
                t = Yr,
                e = ua;
            function r(r, i) {
                var u = n.call(this, r, i),
                    o = t.call(this, r, i),
                    a = (u.y + o.y) / 2,
                    l = [u, { x: u.x, y: a }, { x: o.x, y: a }, o];
                return "M" + (l = l.map(e))[0] + "C" + l[1] + " " + l[2] + " " + l[3];
            }
            return (
                (r.source = function (t) {
                    return arguments.length ? ((n = dt(t)), r) : n;
                }),
                (r.target = function (n) {
                    return arguments.length ? ((t = dt(n)), r) : t;
                }),
                (r.projection = function (n) {
                    return arguments.length ? ((e = n), r) : e;
                }),
                r
            );
        }),
        (n.svg.diagonal.radial = function () {
            var t = n.svg.diagonal(),
                e = ua,
                r = t.projection;
            return (
                (t.projection = function (n) {
                    return arguments.length
                        ? r(
                              (function (n) {
                                  return function () {
                                      var t = n.apply(this, arguments),
                                          e = t[0],
                                          r = t[1] - An;
                                      return [e * Math.cos(r), e * Math.sin(r)];
                                  };
                              })((e = n))
                          )
                        : e;
                }),
                t
            );
        }),
        (n.svg.symbol = function () {
            var n = aa,
                t = oa;
            function e(e, r) {
                return (ca.get(n.call(this, e, r)) || la)(t.call(this, e, r));
            }
            return (
                (e.type = function (t) {
                    return arguments.length ? ((n = dt(t)), e) : n;
                }),
                (e.size = function (n) {
                    return arguments.length ? ((t = dt(n)), e) : t;
                }),
                e
            );
        });
    var ca = n.map({
        circle: la,
        cross: function (n) {
            var t = Math.sqrt(n / 5) / 2;
            return "M" + -3 * t + "," + -t + "H" + -t + "V" + -3 * t + "H" + t + "V" + -t + "H" + 3 * t + "V" + t + "H" + t + "V" + 3 * t + "H" + -t + "V" + t + "H" + -3 * t + "Z";
        },
        diamond: function (n) {
            var t = Math.sqrt(n / (2 * sa)),
                e = t * sa;
            return "M0," + -t + "L" + e + ",0 0," + t + " " + -e + ",0Z";
        },
        square: function (n) {
            var t = Math.sqrt(n) / 2;
            return "M" + -t + "," + -t + "L" + t + "," + -t + " " + t + "," + t + " " + -t + "," + t + "Z";
        },
        "triangle-down": function (n) {
            var t = Math.sqrt(n / fa),
                e = (t * fa) / 2;
            return "M0," + e + "L" + t + "," + -e + " " + -t + "," + -e + "Z";
        },
        "triangle-up": function (n) {
            var t = Math.sqrt(n / fa),
                e = (t * fa) / 2;
            return "M0," + -e + "L" + t + "," + e + " " + -t + "," + e + "Z";
        },
    });
    n.svg.symbolTypes = ca.keys();
    var fa = Math.sqrt(3),
        sa = Math.tan(30 * Cn);
    (X.transition = function (n) {
        for (var t, e, r = va || ++ma, i = ba(n), u = [], o = da || { time: Date.now(), ease: uu, delay: 0, duration: 250 }, a = -1, l = this.length; ++a < l; ) {
            u.push((t = []));
            for (var c = this[a], f = -1, s = c.length; ++f < s; ) (e = c[f]) && _a(e, f, i, r, o), t.push(e);
        }
        return ga(u, i, r);
    }),
        (X.interrupt = function (n) {
            return this.each(null == n ? ha : pa(ba(n)));
        });
    var ha = pa(ba());
    function pa(n) {
        return function () {
            var t, e, r;
            (t = this[n]) && (r = t[(e = t.active)]) && ((r.timer.c = null), (r.timer.t = NaN), --t.count ? delete t[e] : delete this[n], (t.active += 0.5), r.event && r.event.interrupt.call(this, this.__data__, r.index));
        };
    }
    function ga(n, t, e) {
        return O(n, ya), (n.namespace = t), (n.id = e), n;
    }
    var va,
        da,
        ya = [],
        ma = 0;
    function Ma(n, t, e, r) {
        var i = n.id,
            u = n.namespace;
        return fn(
            n,
            "function" == typeof e
                ? function (n, o, a) {
                      n[u][i].tween.set(t, r(e.call(n, n.__data__, o, a)));
                  }
                : ((e = r(e)),
                  function (n) {
                      n[u][i].tween.set(t, e);
                  })
        );
    }
    function xa(n) {
        return (
            null == n && (n = ""),
            function () {
                this.textContent = n;
            }
        );
    }
    function ba(n) {
        return null == n ? "__transition__" : "__transition_" + n + "__";
    }
    function _a(n, t, e, r, i) {
        var u,
            o,
            a,
            l,
            c,
            f = n[e] || (n[e] = { active: 0, count: 0 }),
            s = f[r];
        function h(e) {
            var i = f.active,
                h = f[i];
            for (var g in (h && ((h.timer.c = null), (h.timer.t = NaN), --f.count, delete f[i], h.event && h.event.interrupt.call(n, n.__data__, h.index)), f))
                if (+g < r) {
                    var v = f[g];
                    (v.timer.c = null), (v.timer.t = NaN), --f.count, delete f[g];
                }
            (o.c = p),
                St(
                    function () {
                        return o.c && p(e || 1) && ((o.c = null), (o.t = NaN)), 1;
                    },
                    0,
                    u
                ),
                (f.active = r),
                s.event && s.event.start.call(n, n.__data__, t),
                (c = []),
                s.tween.forEach(function (e, r) {
                    (r = r.call(n, n.__data__, t)) && c.push(r);
                }),
                (l = s.ease),
                (a = s.duration);
        }
        function p(i) {
            for (var u = i / a, o = l(u), h = c.length; h > 0; ) c[--h].call(n, o);
            if (u >= 1) return s.event && s.event.end.call(n, n.__data__, t), --f.count ? delete f[r] : delete n[e], 1;
        }
        s ||
            ((u = i.time),
            (o = St(
                function (n) {
                    var t = s.delay;
                    if (((o.t = t + u), t <= n)) return h(n - t);
                    o.c = h;
                },
                0,
                u
            )),
            (s = f[r] = { tween: new M(), time: u, timer: o, delay: i.delay, duration: i.duration, ease: i.ease, index: t }),
            (i = null),
            ++f.count);
    }
    (ya.call = X.call),
        (ya.empty = X.empty),
        (ya.node = X.node),
        (ya.size = X.size),
        (n.transition = function (t, e) {
            return t && t.transition ? (va ? t.transition(e) : t) : n.selection().transition(t);
        }),
        (n.transition.prototype = ya),
        (ya.select = function (n) {
            var t,
                e,
                r,
                i = this.id,
                u = this.namespace,
                o = [];
            n = $(n);
            for (var a = -1, l = this.length; ++a < l; ) {
                o.push((t = []));
                for (var c = this[a], f = -1, s = c.length; ++f < s; ) (r = c[f]) && (e = n.call(r, r.__data__, f, a)) ? ("__data__" in r && (e.__data__ = r.__data__), _a(e, f, u, i, r[u][i]), t.push(e)) : t.push(null);
            }
            return ga(o, u, i);
        }),
        (ya.selectAll = function (n) {
            var t,
                e,
                r,
                i,
                u,
                o = this.id,
                a = this.namespace,
                l = [];
            n = B(n);
            for (var c = -1, f = this.length; ++c < f; )
                for (var s = this[c], h = -1, p = s.length; ++h < p; )
                    if ((r = s[h])) {
                        (u = r[a][o]), (e = n.call(r, r.__data__, h, c)), l.push((t = []));
                        for (var g = -1, v = e.length; ++g < v; ) (i = e[g]) && _a(i, g, a, o, u), t.push(i);
                    }
            return ga(l, a, o);
        }),
        (ya.filter = function (n) {
            var t,
                e,
                r = [];
            "function" != typeof n && (n = cn(n));
            for (var i = 0, u = this.length; i < u; i++) {
                r.push((t = []));
                for (var o, a = 0, l = (o = this[i]).length; a < l; a++) (e = o[a]) && n.call(e, e.__data__, a, i) && t.push(e);
            }
            return ga(r, this.namespace, this.id);
        }),
        (ya.tween = function (n, t) {
            var e = this.id,
                r = this.namespace;
            return arguments.length < 2
                ? this.node()[r][e].tween.get(n)
                : fn(
                      this,
                      null == t
                          ? function (t) {
                                t[r][e].tween.remove(n);
                            }
                          : function (i) {
                                i[r][e].tween.set(n, t);
                            }
                  );
        }),
        (ya.attr = function (t, e) {
            if (arguments.length < 2) {
                for (e in t) this.attr(e, t[e]);
                return this;
            }
            var r = "transform" == t ? du : Ji,
                i = n.ns.qualify(t);
            function u() {
                this.removeAttribute(i);
            }
            function o() {
                this.removeAttributeNS(i.space, i.local);
            }
            return Ma(
                this,
                "attr." + t,
                e,
                i.local
                    ? function (n) {
                          return null == n
                              ? o
                              : ((n += ""),
                                function () {
                                    var t,
                                        e = this.getAttributeNS(i.space, i.local);
                                    return (
                                        e !== n &&
                                        ((t = r(e, n)),
                                        function (n) {
                                            this.setAttributeNS(i.space, i.local, t(n));
                                        })
                                    );
                                });
                      }
                    : function (n) {
                          return null == n
                              ? u
                              : ((n += ""),
                                function () {
                                    var t,
                                        e = this.getAttribute(i);
                                    return (
                                        e !== n &&
                                        ((t = r(e, n)),
                                        function (n) {
                                            this.setAttribute(i, t(n));
                                        })
                                    );
                                });
                      }
            );
        }),
        (ya.attrTween = function (t, e) {
            var r = n.ns.qualify(t);
            return this.tween(
                "attr." + t,
                r.local
                    ? function (n, t) {
                          var i = e.call(this, n, t, this.getAttributeNS(r.space, r.local));
                          return (
                              i &&
                              function (n) {
                                  this.setAttributeNS(r.space, r.local, i(n));
                              }
                          );
                      }
                    : function (n, t) {
                          var i = e.call(this, n, t, this.getAttribute(r));
                          return (
                              i &&
                              function (n) {
                                  this.setAttribute(r, i(n));
                              }
                          );
                      }
            );
        }),
        (ya.style = function (n, t, e) {
            var r = arguments.length;
            if (r < 3) {
                if ("string" != typeof n) {
                    for (e in (r < 2 && (t = ""), n)) this.style(e, n[e], t);
                    return this;
                }
                e = "";
            }
            function i() {
                this.style.removeProperty(n);
            }
            return Ma(this, "style." + n, t, function (t) {
                return null == t
                    ? i
                    : ((t += ""),
                      function () {
                          var r,
                              i = u(this).getComputedStyle(this, null).getPropertyValue(n);
                          return (
                              i !== t &&
                              ((r = Ji(i, t)),
                              function (t) {
                                  this.style.setProperty(n, r(t), e);
                              })
                          );
                      });
            });
        }),
        (ya.styleTween = function (n, t, e) {
            return (
                arguments.length < 3 && (e = ""),
                this.tween("style." + n, function (r, i) {
                    var o = t.call(this, r, i, u(this).getComputedStyle(this, null).getPropertyValue(n));
                    return (
                        o &&
                        function (t) {
                            this.style.setProperty(n, o(t), e);
                        }
                    );
                })
            );
        }),
        (ya.text = function (n) {
            return Ma(this, "text", n, xa);
        }),
        (ya.remove = function () {
            var n = this.namespace;
            return this.each("end.transition", function () {
                var t;
                this[n].count < 2 && (t = this.parentNode) && t.removeChild(this);
            });
        }),
        (ya.ease = function (t) {
            var e = this.id,
                r = this.namespace;
            return arguments.length < 1
                ? this.node()[r][e].ease
                : ("function" != typeof t && (t = n.ease.apply(n, arguments)),
                  fn(this, function (n) {
                      n[r][e].ease = t;
                  }));
        }),
        (ya.delay = function (n) {
            var t = this.id,
                e = this.namespace;
            return arguments.length < 1
                ? this.node()[e][t].delay
                : fn(
                      this,
                      "function" == typeof n
                          ? function (r, i, u) {
                                r[e][t].delay = +n.call(r, r.__data__, i, u);
                            }
                          : ((n = +n),
                            function (r) {
                                r[e][t].delay = n;
                            })
                  );
        }),
        (ya.duration = function (n) {
            var t = this.id,
                e = this.namespace;
            return arguments.length < 1
                ? this.node()[e][t].duration
                : fn(
                      this,
                      "function" == typeof n
                          ? function (r, i, u) {
                                r[e][t].duration = Math.max(1, n.call(r, r.__data__, i, u));
                            }
                          : ((n = Math.max(1, n)),
                            function (r) {
                                r[e][t].duration = n;
                            })
                  );
        }),
        (ya.each = function (t, e) {
            var r = this.id,
                i = this.namespace;
            if (arguments.length < 2) {
                var u = da,
                    o = va;
                try {
                    (va = r),
                        fn(this, function (n, e, u) {
                            (da = n[i][r]), t.call(n, n.__data__, e, u);
                        });
                } finally {
                    (da = u), (va = o);
                }
            } else
                fn(this, function (u) {
                    var o = u[i][r];
                    (o.event || (o.event = n.dispatch("start", "end", "interrupt"))).on(t, e);
                });
            return this;
        }),
        (ya.transition = function () {
            for (var n, t, e, r = this.id, i = ++ma, u = this.namespace, o = [], a = 0, l = this.length; a < l; a++) {
                o.push((n = []));
                for (var c, f = 0, s = (c = this[a]).length; f < s; f++) (t = c[f]) && _a(t, f, u, i, { time: (e = t[u][r]).time, ease: e.ease, delay: e.delay + e.duration, duration: e.duration }), n.push(t);
            }
            return ga(o, u, i);
        }),
        (n.svg.axis = function () {
            var t,
                r = n.scale.linear(),
                i = wa,
                u = 6,
                o = 6,
                a = 3,
                l = [10],
                c = null;
            function f(e) {
                e.each(function () {
                    var e,
                        f = n.select(this),
                        s = this.__chart__ || r,
                        h = (this.__chart__ = r.copy()),
                        p = null == c ? (h.ticks ? h.ticks.apply(h, l) : h.domain()) : c,
                        g = null == t ? (h.tickFormat ? h.tickFormat.apply(h, l) : z) : t,
                        v = f.selectAll(".tick").data(p, h),
                        d = v.enter().insert("g", ".domain").attr("class", "tick").style("opacity", wn),
                        y = n.transition(v.exit()).style("opacity", wn).remove(),
                        m = n.transition(v.order()).style("opacity", 1),
                        M = Math.max(u, 0) + a,
                        x = so(h),
                        b = f.selectAll(".domain").data([0]),
                        _ = (b.enter().append("path").attr("class", "domain"), n.transition(b));
                    d.append("line"), d.append("text");
                    var w,
                        S,
                        k,
                        N,
                        E = d.select("line"),
                        A = m.select("line"),
                        C = v.select("text").text(g),
                        L = d.select("text"),
                        q = m.select("text"),
                        T = "top" === i || "left" === i ? -1 : 1;
                    if (
                        ("bottom" === i || "top" === i
                            ? ((e = ka), (w = "x"), (k = "y"), (S = "x2"), (N = "y2"), C.attr("dy", T < 0 ? "0em" : ".71em").style("text-anchor", "middle"), _.attr("d", "M" + x[0] + "," + T * o + "V0H" + x[1] + "V" + T * o))
                            : ((e = Na), (w = "y"), (k = "x"), (S = "y2"), (N = "x2"), C.attr("dy", ".32em").style("text-anchor", T < 0 ? "end" : "start"), _.attr("d", "M" + T * o + "," + x[0] + "H0V" + x[1] + "H" + T * o)),
                        E.attr(N, T * u),
                        L.attr(k, T * M),
                        A.attr(S, 0).attr(N, T * u),
                        q.attr(w, 0).attr(k, T * M),
                        h.rangeBand)
                    ) {
                        var R = h,
                            D = R.rangeBand() / 2;
                        s = h = function (n) {
                            return R(n) + D;
                        };
                    } else s.rangeBand ? (s = h) : y.call(e, h, s);
                    d.call(e, s, h), m.call(e, h, h);
                });
            }
            return (
                (f.scale = function (n) {
                    return arguments.length ? ((r = n), f) : r;
                }),
                (f.orient = function (n) {
                    return arguments.length ? ((i = n in Sa ? n + "" : wa), f) : i;
                }),
                (f.ticks = function () {
                    return arguments.length ? ((l = e(arguments)), f) : l;
                }),
                (f.tickValues = function (n) {
                    return arguments.length ? ((c = n), f) : c;
                }),
                (f.tickFormat = function (n) {
                    return arguments.length ? ((t = n), f) : t;
                }),
                (f.tickSize = function (n) {
                    var t = arguments.length;
                    return t ? ((u = +n), (o = +arguments[t - 1]), f) : u;
                }),
                (f.innerTickSize = function (n) {
                    return arguments.length ? ((u = +n), f) : u;
                }),
                (f.outerTickSize = function (n) {
                    return arguments.length ? ((o = +n), f) : o;
                }),
                (f.tickPadding = function (n) {
                    return arguments.length ? ((a = +n), f) : a;
                }),
                (f.tickSubdivide = function () {
                    return arguments.length && f;
                }),
                f
            );
        });
    var wa = "bottom",
        Sa = { top: 1, right: 1, bottom: 1, left: 1 };
    function ka(n, t, e) {
        n.attr("transform", function (n) {
            var r = t(n);
            return "translate(" + (isFinite(r) ? r : e(n)) + ",0)";
        });
    }
    function Na(n, t, e) {
        n.attr("transform", function (n) {
            var r = t(n);
            return "translate(0," + (isFinite(r) ? r : e(n)) + ")";
        });
    }
    n.svg.brush = function () {
        var t,
            e,
            r = F(h, "brushstart", "brush", "brushend"),
            i = null,
            o = null,
            a = [0, 0],
            l = [0, 0],
            c = !0,
            f = !0,
            s = Aa[0];
        function h(t) {
            t.each(function () {
                var t = n.select(this).style("pointer-events", "all").style("-webkit-tap-highlight-color", "rgba(0,0,0,0)").on("mousedown.brush", d).on("touchstart.brush", d),
                    e = t.selectAll(".background").data([0]);
                e.enter().append("rect").attr("class", "background").style("visibility", "hidden").style("cursor", "crosshair"), t.selectAll(".extent").data([0]).enter().append("rect").attr("class", "extent").style("cursor", "move");
                var r = t.selectAll(".resize").data(s, z);
                r.exit().remove(),
                    r
                        .enter()
                        .append("g")
                        .attr("class", function (n) {
                            return "resize " + n;
                        })
                        .style("cursor", function (n) {
                            return Ea[n];
                        })
                        .append("rect")
                        .attr("x", function (n) {
                            return /[ew]$/.test(n) ? -3 : null;
                        })
                        .attr("y", function (n) {
                            return /^[ns]/.test(n) ? -3 : null;
                        })
                        .attr("width", 6)
                        .attr("height", 6)
                        .style("visibility", "hidden"),
                    r.style("display", h.empty() ? "none" : null);
                var u,
                    a = n.transition(t),
                    l = n.transition(e);
                i && ((u = so(i)), l.attr("x", u[0]).attr("width", u[1] - u[0]), g(a)), o && ((u = so(o)), l.attr("y", u[0]).attr("height", u[1] - u[0]), v(a)), p(a);
            });
        }
        function p(n) {
            n.selectAll(".resize").attr("transform", function (n) {
                return "translate(" + a[+/e$/.test(n)] + "," + l[+/^s/.test(n)] + ")";
            });
        }
        function g(n) {
            n.select(".extent").attr("x", a[0]), n.selectAll(".extent,.n>rect,.s>rect").attr("width", a[1] - a[0]);
        }
        function v(n) {
            n.select(".extent").attr("y", l[0]), n.selectAll(".extent,.e>rect,.w>rect").attr("height", l[1] - l[0]);
        }
        function d() {
            var s,
                d,
                y = this,
                m = n.select(n.event.target),
                M = r.of(y, arguments),
                x = n.select(y),
                b = m.datum(),
                _ = !/^(n|s)$/.test(b) && i,
                w = !/^(e|w)$/.test(b) && o,
                S = m.classed("extent"),
                k = Mn(y),
                N = n.mouse(y),
                E = n
                    .select(u(y))
                    .on("keydown.brush", function () {
                        32 == n.event.keyCode && (S || ((s = null), (N[0] -= a[1]), (N[1] -= l[1]), (S = 2)), U());
                    })
                    .on("keyup.brush", function () {
                        32 == n.event.keyCode && 2 == S && ((N[0] += a[1]), (N[1] += l[1]), (S = 0), U());
                    });
            if ((n.event.changedTouches ? E.on("touchmove.brush", z).on("touchend.brush", q) : E.on("mousemove.brush", z).on("mouseup.brush", q), x.interrupt().selectAll("*").interrupt(), S)) (N[0] = a[0] - N[0]), (N[1] = l[0] - N[1]);
            else if (b) {
                var A = +/w$/.test(b),
                    C = +/^n/.test(b);
                (d = [a[1 - A] - N[0], l[1 - C] - N[1]]), (N[0] = a[A]), (N[1] = l[C]);
            } else n.event.altKey && (s = N.slice());
            function z() {
                var t = n.mouse(y),
                    e = !1;
                d && ((t[0] += d[0]), (t[1] += d[1])),
                    S || (n.event.altKey ? (s || (s = [(a[0] + a[1]) / 2, (l[0] + l[1]) / 2]), (N[0] = a[+(t[0] < s[0])]), (N[1] = l[+(t[1] < s[1])])) : (s = null)),
                    _ && L(t, i, 0) && (g(x), (e = !0)),
                    w && L(t, o, 1) && (v(x), (e = !0)),
                    e && (p(x), M({ type: "brush", mode: S ? "move" : "resize" }));
            }
            function L(n, r, i) {
                var u,
                    o,
                    h = so(r),
                    p = h[0],
                    g = h[1],
                    v = N[i],
                    d = i ? l : a,
                    y = d[1] - d[0];
                if (
                    (S && ((p -= v), (g -= y + v)),
                    (u = (i ? f : c) ? Math.max(p, Math.min(g, n[i])) : n[i]),
                    S ? (o = (u += v) + y) : (s && (v = Math.max(p, Math.min(g, 2 * s[i] - u))), v < u ? ((o = u), (u = v)) : (o = v)),
                    d[0] != u || d[1] != o)
                )
                    return i ? (e = null) : (t = null), (d[0] = u), (d[1] = o), !0;
            }
            function q() {
                z(),
                    x
                        .style("pointer-events", "all")
                        .selectAll(".resize")
                        .style("display", h.empty() ? "none" : null),
                    n.select("body").style("cursor", null),
                    E.on("mousemove.brush", null).on("mouseup.brush", null).on("touchmove.brush", null).on("touchend.brush", null).on("keydown.brush", null).on("keyup.brush", null),
                    k(),
                    M({ type: "brushend" });
            }
            x.style("pointer-events", "none").selectAll(".resize").style("display", null), n.select("body").style("cursor", m.style("cursor")), M({ type: "brushstart" }), z();
        }
        return (
            (h.event = function (i) {
                i.each(function () {
                    var i = r.of(this, arguments),
                        u = { x: a, y: l, i: t, j: e },
                        o = this.__chart__ || u;
                    (this.__chart__ = u),
                        va
                            ? n
                                  .select(this)
                                  .transition()
                                  .each("start.brush", function () {
                                      (t = o.i), (e = o.j), (a = o.x), (l = o.y), i({ type: "brushstart" });
                                  })
                                  .tween("brush:brush", function () {
                                      var n = Gi(a, u.x),
                                          r = Gi(l, u.y);
                                      return (
                                          (t = e = null),
                                          function (t) {
                                              (a = u.x = n(t)), (l = u.y = r(t)), i({ type: "brush", mode: "resize" });
                                          }
                                      );
                                  })
                                  .each("end.brush", function () {
                                      (t = u.i), (e = u.j), i({ type: "brush", mode: "resize" }), i({ type: "brushend" });
                                  })
                            : (i({ type: "brushstart" }), i({ type: "brush", mode: "resize" }), i({ type: "brushend" }));
                });
            }),
            (h.x = function (n) {
                return arguments.length ? ((s = Aa[(!(i = n) << 1) | !o]), h) : i;
            }),
            (h.y = function (n) {
                return arguments.length ? ((s = Aa[(!i << 1) | !(o = n)]), h) : o;
            }),
            (h.clamp = function (n) {
                return arguments.length ? (i && o ? ((c = !!n[0]), (f = !!n[1])) : i ? (c = !!n) : o && (f = !!n), h) : i && o ? [c, f] : i ? c : o ? f : null;
            }),
            (h.extent = function (n) {
                var r, u, c, f, s;
                return arguments.length
                    ? (i && ((r = n[0]), (u = n[1]), o && ((r = r[0]), (u = u[0])), (t = [r, u]), i.invert && ((r = i(r)), (u = i(u))), u < r && ((s = r), (r = u), (u = s)), (r == a[0] && u == a[1]) || (a = [r, u])),
                      o && ((c = n[0]), (f = n[1]), i && ((c = c[1]), (f = f[1])), (e = [c, f]), o.invert && ((c = o(c)), (f = o(f))), f < c && ((s = c), (c = f), (f = s)), (c == l[0] && f == l[1]) || (l = [c, f])),
                      h)
                    : (i && (t ? ((r = t[0]), (u = t[1])) : ((r = a[0]), (u = a[1]), i.invert && ((r = i.invert(r)), (u = i.invert(u))), u < r && ((s = r), (r = u), (u = s)))),
                      o && (e ? ((c = e[0]), (f = e[1])) : ((c = l[0]), (f = l[1]), o.invert && ((c = o.invert(c)), (f = o.invert(f))), f < c && ((s = c), (c = f), (f = s)))),
                      i && o
                          ? [
                                [r, c],
                                [u, f],
                            ]
                          : i
                          ? [r, u]
                          : o && [c, f]);
            }),
            (h.clear = function () {
                return h.empty() || ((a = [0, 0]), (l = [0, 0]), (t = e = null)), h;
            }),
            (h.empty = function () {
                return (!!i && a[0] == a[1]) || (!!o && l[0] == l[1]);
            }),
            n.rebind(h, r, "on")
        );
    };
    var Ea = { n: "ns-resize", e: "ew-resize", s: "ns-resize", w: "ew-resize", nw: "nwse-resize", ne: "nesw-resize", se: "nwse-resize", sw: "nesw-resize" },
        Aa = [["n", "e", "s", "w", "nw", "ne", "se", "sw"], ["e", "w"], ["n", "s"], []],
        Ca = (Rt.format = ce.timeFormat),
        za = Ca.utc,
        La = za("%Y-%m-%dT%H:%M:%S.%LZ");
    function qa(n) {
        return n.toISOString();
    }
    function Ta(t, e, r) {
        function i(n) {
            return t(n);
        }
        function u(t, r) {
            var i = (t[1] - t[0]) / r,
                u = n.bisect(Da, i);
            return u == Da.length
                ? [
                      e.year,
                      xo(
                          t.map(function (n) {
                              return n / 31536e6;
                          }),
                          r
                      )[2],
                  ]
                : u
                ? e[i / Da[u - 1] < Da[u] / i ? u - 1 : u]
                : [ja, xo(t, r)[2]];
        }
        return (
            (i.invert = function (n) {
                return Ra(t.invert(n));
            }),
            (i.domain = function (n) {
                return arguments.length ? (t.domain(n), i) : t.domain().map(Ra);
            }),
            (i.nice = function (n, t) {
                var e = i.domain(),
                    r = fo(e),
                    o = null == n ? u(r, 10) : "number" == typeof n && u(r, n);
                function a(e) {
                    return !isNaN(e) && !n.range(e, Ra(+e + 1), t).length;
                }
                return (
                    o && ((n = o[0]), (t = o[1])),
                    i.domain(
                        po(
                            e,
                            t > 1
                                ? {
                                      floor: function (t) {
                                          for (; a((t = n.floor(t))); ) t = Ra(t - 1);
                                          return t;
                                      },
                                      ceil: function (t) {
                                          for (; a((t = n.ceil(t))); ) t = Ra(+t + 1);
                                          return t;
                                      },
                                  }
                                : n
                        )
                    )
                );
            }),
            (i.ticks = function (n, t) {
                var e = fo(i.domain()),
                    r = null == n ? u(e, 10) : "number" == typeof n ? u(e, n) : !n.range && [{ range: n }, t];
                return r && ((n = r[0]), (t = r[1])), n.range(e[0], Ra(+e[1] + 1), t < 1 ? 1 : t);
            }),
            (i.tickFormat = function () {
                return r;
            }),
            (i.copy = function () {
                return Ta(t.copy(), e, r);
            }),
            mo(i, t)
        );
    }
    function Ra(n) {
        return new Date(n);
    }
    (Ca.iso = Date.prototype.toISOString && +new Date("2000-01-01T00:00:00.000Z") ? qa : La),
        (qa.parse = function (n) {
            var t = new Date(n);
            return isNaN(t) ? null : t;
        }),
        (qa.toString = La.toString),
        (Rt.second = jt(
            function (n) {
                return new Dt(1e3 * Math.floor(n / 1e3));
            },
            function (n, t) {
                n.setTime(n.getTime() + 1e3 * Math.floor(t));
            },
            function (n) {
                return n.getSeconds();
            }
        )),
        (Rt.seconds = Rt.second.range),
        (Rt.seconds.utc = Rt.second.utc.range),
        (Rt.minute = jt(
            function (n) {
                return new Dt(6e4 * Math.floor(n / 6e4));
            },
            function (n, t) {
                n.setTime(n.getTime() + 6e4 * Math.floor(t));
            },
            function (n) {
                return n.getMinutes();
            }
        )),
        (Rt.minutes = Rt.minute.range),
        (Rt.minutes.utc = Rt.minute.utc.range),
        (Rt.hour = jt(
            function (n) {
                var t = n.getTimezoneOffset() / 60;
                return new Dt(36e5 * (Math.floor(n / 36e5 - t) + t));
            },
            function (n, t) {
                n.setTime(n.getTime() + 36e5 * Math.floor(t));
            },
            function (n) {
                return n.getHours();
            }
        )),
        (Rt.hours = Rt.hour.range),
        (Rt.hours.utc = Rt.hour.utc.range),
        (Rt.month = jt(
            function (n) {
                return (n = Rt.day(n)).setDate(1), n;
            },
            function (n, t) {
                n.setMonth(n.getMonth() + t);
            },
            function (n) {
                return n.getMonth();
            }
        )),
        (Rt.months = Rt.month.range),
        (Rt.months.utc = Rt.month.utc.range);
    var Da = [1e3, 5e3, 15e3, 3e4, 6e4, 3e5, 9e5, 18e5, 36e5, 108e5, 216e5, 432e5, 864e5, 1728e5, 6048e5, 2592e6, 7776e6, 31536e6],
        Pa = [
            [Rt.second, 1],
            [Rt.second, 5],
            [Rt.second, 15],
            [Rt.second, 30],
            [Rt.minute, 1],
            [Rt.minute, 5],
            [Rt.minute, 15],
            [Rt.minute, 30],
            [Rt.hour, 1],
            [Rt.hour, 3],
            [Rt.hour, 6],
            [Rt.hour, 12],
            [Rt.day, 1],
            [Rt.day, 2],
            [Rt.week, 1],
            [Rt.month, 1],
            [Rt.month, 3],
            [Rt.year, 1],
        ],
        Ua = Ca.multi([
            [
                ".%L",
                function (n) {
                    return n.getMilliseconds();
                },
            ],
            [
                ":%S",
                function (n) {
                    return n.getSeconds();
                },
            ],
            [
                "%I:%M",
                function (n) {
                    return n.getMinutes();
                },
            ],
            [
                "%I %p",
                function (n) {
                    return n.getHours();
                },
            ],
            [
                "%a %d",
                function (n) {
                    return n.getDay() && 1 != n.getDate();
                },
            ],
            [
                "%b %d",
                function (n) {
                    return 1 != n.getDate();
                },
            ],
            [
                "%B",
                function (n) {
                    return n.getMonth();
                },
            ],
            ["%Y", Be],
        ]),
        ja = {
            range: function (t, e, r) {
                return n.range(Math.ceil(t / r) * r, +e, r).map(Ra);
            },
            floor: z,
            ceil: z,
        };
    (Pa.year = Rt.year),
        (Rt.scale = function () {
            return Ta(n.scale.linear(), Pa, Ua);
        });
    var Fa = Pa.map(function (n) {
            return [n[0].utc, n[1]];
        }),
        Ha = za.multi([
            [
                ".%L",
                function (n) {
                    return n.getUTCMilliseconds();
                },
            ],
            [
                ":%S",
                function (n) {
                    return n.getUTCSeconds();
                },
            ],
            [
                "%I:%M",
                function (n) {
                    return n.getUTCMinutes();
                },
            ],
            [
                "%I %p",
                function (n) {
                    return n.getUTCHours();
                },
            ],
            [
                "%a %d",
                function (n) {
                    return n.getUTCDay() && 1 != n.getUTCDate();
                },
            ],
            [
                "%b %d",
                function (n) {
                    return 1 != n.getUTCDate();
                },
            ],
            [
                "%B",
                function (n) {
                    return n.getUTCMonth();
                },
            ],
            ["%Y", Be],
        ]);
    function Oa(n) {
        return JSON.parse(n.responseText);
    }
    function Ia(n) {
        var t = r.createRange();
        return t.selectNode(r.body), t.createContextualFragment(n.responseText);
    }
    (Fa.year = Rt.year.utc),
        (Rt.scale.utc = function () {
            return Ta(n.scale.linear(), Fa, Ha);
        }),
        (n.text = yt(function (n) {
            return n.responseText;
        })),
        (n.json = function (n, t) {
            return mt(n, "application/json", Oa, t);
        }),
        (n.html = function (n, t) {
            return mt(n, "text/html", Ia, t);
        }),
        (n.xml = yt(function (n) {
            return n.responseXML;
        })),
        "function" == typeof define && define.amd ? ((this.d3 = n), define(n)) : "object" == typeof module && module.exports ? (module.exports = n) : (this.d3 = n);
})();
