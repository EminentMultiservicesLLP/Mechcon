! function(e, n) {
    "object" == typeof exports && "object" == typeof module ? module.exports = n() : "function" == typeof define && define.amd ? define("mitchTree", [], n) : "object" == typeof exports ? exports.mitchTree = n() : (e.d3 = e.d3 || {}, e.d3.mitchTree = n())
}(global, (function() {
    return function(e) {
        var n = {};

        function t(a) {
            if (n[a]) return n[a].exports;
            var i = n[a] = {
                i: a,
                l: !1,
                exports: {}
            };
            return e[a].call(i.exports, i, i.exports, t), i.l = !0, i.exports
        }
        return t.m = e, t.c = n, t.d = function(e, n, a) {
            t.o(e, n) || Object.defineProperty(e, n, {
                enumerable: !0,
                get: a
            })
        }, t.r = function(e) {
            "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
                value: "Module"
            }), Object.defineProperty(e, "__esModule", {
                value: !0
            })
        }, t.t = function(e, n) {
            if (1 & n && (e = t(e)), 8 & n) return e;
            if (4 & n && "object" == typeof e && e && e.__esModule) return e;
            var a = Object.create(null);
            if (t.r(a), Object.defineProperty(a, "default", {
                    enumerable: !0,
                    value: e
                }), 2 & n && "string" != typeof e)
                for (var i in e) t.d(a, i, function(n) {
                    return e[n]
                }.bind(null, i));
            return a
        }, t.n = function(e) {
            var n = e && e.__esModule ? function() {
                return e.default
            } : function() {
                return e
            };
            return t.d(n, "a", n), n
        }, t.o = function(e, n) {
            return Object.prototype.hasOwnProperty.call(e, n)
        }, t.p = "./dist", t(t.s = 0)
    }([
            function(e, n, t) {
                "use strict";
                t.r(n);
                t.d(n, "cluster", (function() {
                    return a.default
                }));
                t.d(n, "hierarchy", (function() {
                    return i.default
                }));
                t.d(n, "pack", (function() {
                    return o.default
                }));
                t.d(n, "packSiblings", (function() {
                    return s.default
                }));
                t.d(n, "packEnclose", (function() {
                    return r.default
                }));
                t.d(n, "partition", (function() {
                    return u.default
                }));
                t.d(n, "stratify", (function() {
                    return l.default
                }));
                var c = t( /*! ./tree.js */ "./node_modules/d3-hierarchy/src/tree.js");
                t.d(n, "tree", (function() {
                    return c.default
                }));
                var d = t( /*! ./treemap/index.js */ "./node_modules/d3-hierarchy/src/treemap/index.js");
                t.d(n, "treemap", (function() {
                    return d.default
                }));
                var g = t( /*! ./treemap/binary.js */ "./node_modules/d3-hierarchy/src/treemap/binary.js");
                t.d(n, "treemapBinary", (function() {
                    return g.default
                }));
                var h = t( /*! ./treemap/dice.js */ "./node_modules/d3-hierarchy/src/treemap/dice.js");
                t.d(n, "treemapDice", (function() {
                    return h.default
                }));
                var f = t( /*! ./treemap/slice.js */ "./node_modules/d3-hierarchy/src/treemap/slice.js");
                t.d(n, "treemapSlice", (function() {
                    return f.default
                }));
                var m = t( /*! ./treemap/sliceDice.js */ "./node_modules/d3-hierarchy/src/treemap/sliceDice.js");
                t.d(n, "treemapSliceDice", (function() {
                    return m.default
                }));
                var p = t( /*! ./treemap/squarify.js */ "./node_modules/d3-hierarchy/src/treemap/squarify.js");
                t.d(n, "treemapSquarify", (function() {
                    return p.default
                }));
                var v = t( /*! ./treemap/resquarify.js */ "./node_modules/d3-hierarchy/src/treemap/resquarify.js");
                t.d(n, "treemapResquarify", (function() {
                    return v.default
                }))
            },
        "./node_modules/d3-hierarchy/src/pack/enclose.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/pack/enclose.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../array.js */ "./node_modules/d3-hierarchy/src/array.js");

                function i(e, n) {
                    var t, a;
                    if (r(n, e)) return [n];
                    for (t = 0; t < e.length; ++t)
                        if (o(n, e[t]) && r(l(e[t], n), e)) return [e[t], n];
                    for (t = 0; t < e.length - 1; ++t)
                        for (a = t + 1; a < e.length; ++a)
                            if (o(l(e[t], e[a]), n) && o(l(e[t], n), e[a]) && o(l(e[a], n), e[t]) && r(c(e[t], e[a], n), e)) return [e[t], e[a], n];
                    throw new Error
                }

                function o(e, n) {
                    var t = e.r - n.r,
                        a = n.x - e.x,
                        i = n.y - e.y;
                    return t < 0 || t * t < a * a + i * i
                }

                function s(e, n) {
                    var t = e.r - n.r + 1e-6,
                        a = n.x - e.x,
                        i = n.y - e.y;
                    return t > 0 && t * t > a * a + i * i
                }

                function r(e, n) {
                    for (var t = 0; t < n.length; ++t)
                        if (!s(e, n[t])) return !1;
                    return !0
                }

                function u(e) {
                    switch (e.length) {
                        case 1:
                            return {
                                x: (n = e[0]).x,
                                y: n.y,
                                r: n.r
                            };
                        case 2:
                            return l(e[0], e[1]);
                        case 3:
                            return c(e[0], e[1], e[2])
                    }
                    var n
                }

                function l(e, n) {
                    var t = e.x,
                        a = e.y,
                        i = e.r,
                        o = n.x,
                        s = n.y,
                        r = n.r,
                        u = o - t,
                        l = s - a,
                        c = r - i,
                        d = Math.sqrt(u * u + l * l);
                    return {
                        x: (t + o + u / d * c) / 2,
                        y: (a + s + l / d * c) / 2,
                        r: (d + i + r) / 2
                    }
                }

                function c(e, n, t) {
                    var a = e.x,
                        i = e.y,
                        o = e.r,
                        s = n.x,
                        r = n.y,
                        u = n.r,
                        l = t.x,
                        c = t.y,
                        d = t.r,
                        g = a - s,
                        h = a - l,
                        f = i - r,
                        m = i - c,
                        p = u - o,
                        v = d - o,
                        y = a * a + i * i - o * o,
                        _ = y - s * s - r * r + u * u,
                        b = y - l * l - c * c + d * d,
                        j = h * f - g * m,
                        x = (f * b - m * _) / (2 * j) - a,
                        w = (m * p - f * v) / j,
                        R = (h * _ - g * b) / (2 * j) - i,
                        k = (g * v - h * p) / j,
                        A = w * w + k * k - 1,
                        O = 2 * (o + x * w + R * k),
                        S = x * x + R * R - o * o,
                        C = -(A ? (O + Math.sqrt(O * O - 4 * A * S)) / (2 * A) : S / O);
                    return {
                        x: a + x + w * C,
                        y: i + R + k * C,
                        r: C
                    }
                }
                n.default = function(e) {
                    for (var n, t, o = 0, r = (e = Object(a.shuffle)(a.slice.call(e))).length, l = []; o < r;) n = e[o], t && s(t, n) ? ++o : (t = u(l = i(l, n)), o = 0);
                    return t
                }
            },
        "./node_modules/d3-hierarchy/src/pack/index.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/pack/index.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./siblings.js */ "./node_modules/d3-hierarchy/src/pack/siblings.js"),
                    i = t( /*! ../accessors.js */ "./node_modules/d3-hierarchy/src/accessors.js"),
                    o = t( /*! ../constant.js */ "./node_modules/d3-hierarchy/src/constant.js");

                function s(e) {
                    return Math.sqrt(e.value)
                }

                function r(e) {
                    return function(n) {
                        n.children || (n.r = Math.max(0, +e(n) || 0))
                    }
                }

                function u(e, n) {
                    return function(t) {
                        if (i = t.children) {
                            var i, o, s, r = i.length,
                                u = e(t) * n || 0;
                            if (u)
                                for (o = 0; o < r; ++o) i[o].r += u;
                            if (s = Object(a.packEnclose)(i), u)
                                for (o = 0; o < r; ++o) i[o].r -= u;
                            t.r = s + u
                        }
                    }
                }

                function l(e) {
                    return function(n) {
                        var t = n.parent;
                        n.r *= e, t && (n.x = t.x + e * n.x, n.y = t.y + e * n.y)
                    }
                }
                n.default = function() {
                    var e = null,
                        n = 1,
                        t = 1,
                        a = o.constantZero;

                    function c(i) {
                        return i.x = n / 2, i.y = t / 2, e ? i.eachBefore(r(e)).eachAfter(u(a, .5)).eachBefore(l(1)) : i.eachBefore(r(s)).eachAfter(u(o.constantZero, 1)).eachAfter(u(a, i.r / Math.min(n, t))).eachBefore(l(Math.min(n, t) / (2 * i.r))), i
                    }
                    return c.radius = function(n) {
                        return arguments.length ? (e = Object(i.optional)(n), c) : e
                    }, c.size = function(e) {
                        return arguments.length ? (n = +e[0], t = +e[1], c) : [n, t]
                    }, c.padding = function(e) {
                        return arguments.length ? (a = "function" == typeof e ? e : Object(o.default)(+e), c) : a
                    }, c
                }
            },
        "./node_modules/d3-hierarchy/src/pack/siblings.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/pack/siblings.js ***!
              \********************************************************/
            /*! exports provided: packEnclose, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "packEnclose", (function() {
                    return u
                }));
                var a = t( /*! ./enclose.js */ "./node_modules/d3-hierarchy/src/pack/enclose.js");

                function i(e, n, t) {
                    var a, i, o, s, r = e.x - n.x,
                        u = e.y - n.y,
                        l = r * r + u * u;
                    l ? (i = n.r + t.r, i *= i, s = e.r + t.r, i > (s *= s) ? (a = (l + s - i) / (2 * l), o = Math.sqrt(Math.max(0, s / l - a * a)), t.x = e.x - a * r - o * u, t.y = e.y - a * u + o * r) : (a = (l + i - s) / (2 * l), o = Math.sqrt(Math.max(0, i / l - a * a)), t.x = n.x + a * r - o * u, t.y = n.y + a * u + o * r)) : (t.x = n.x + t.r, t.y = n.y)
                }

                function o(e, n) {
                    var t = e.r + n.r - 1e-6,
                        a = n.x - e.x,
                        i = n.y - e.y;
                    return t > 0 && t * t > a * a + i * i
                }

                function s(e) {
                    var n = e._,
                        t = e.next._,
                        a = n.r + t.r,
                        i = (n.x * t.r + t.x * n.r) / a,
                        o = (n.y * t.r + t.y * n.r) / a;
                    return i * i + o * o
                }

                function r(e) {
                    this._ = e, this.next = null, this.previous = null
                }

                function u(e) {
                    if (!(l = e.length)) return 0;
                    var n, t, u, l, c, d, g, h, f, m, p;
                    if ((n = e[0]).x = 0, n.y = 0, !(l > 1)) return n.r;
                    if (t = e[1], n.x = -t.r, t.x = n.r, t.y = 0, !(l > 2)) return n.r + t.r;
                    i(t, n, u = e[2]), n = new r(n), t = new r(t), u = new r(u), n.next = u.previous = t, t.next = n.previous = u, u.next = t.previous = n;
                    e: for (g = 3; g < l; ++g) {
                        i(n._, t._, u = e[g]), u = new r(u), h = t.next, f = n.previous, m = t._.r, p = n._.r;
                        do {
                            if (m <= p) {
                                if (o(h._, u._)) {
                                    t = h, n.next = t, t.previous = n, --g;
                                    continue e
                                }
                                m += h._.r, h = h.next
                            } else {
                                if (o(f._, u._)) {
                                    (n = f).next = t, t.previous = n, --g;
                                    continue e
                                }
                                p += f._.r, f = f.previous
                            }
                        } while (h !== f.next);
                        for (u.previous = n, u.next = t, n.next = t.previous = t = u, c = s(n);
                            (u = u.next) !== t;)(d = s(u)) < c && (n = u, c = d);
                        t = n.next
                    }
                    for (n = [t._], u = t;
                        (u = u.next) !== t;) n.push(u._);
                    for (u = Object(a.default)(n), g = 0; g < l; ++g)(n = e[g]).x -= u.x, n.y -= u.y;
                    return u.r
                }
                n.default = function(e) {
                    return u(e), e
                }
            },
        "./node_modules/d3-hierarchy/src/partition.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/partition.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./treemap/round.js */ "./node_modules/d3-hierarchy/src/treemap/round.js"),
                    i = t( /*! ./treemap/dice.js */ "./node_modules/d3-hierarchy/src/treemap/dice.js");
                n.default = function() {
                    var e = 1,
                        n = 1,
                        t = 0,
                        o = !1;

                    function s(s) {
                        var r = s.height + 1;
                        return s.x0 = s.y0 = t, s.x1 = e, s.y1 = n / r, s.eachBefore(function(e, n) {
                            return function(a) {
                                a.children && Object(i.default)(a, a.x0, e * (a.depth + 1) / n, a.x1, e * (a.depth + 2) / n);
                                var o = a.x0,
                                    s = a.y0,
                                    r = a.x1 - t,
                                    u = a.y1 - t;
                                r < o && (o = r = (o + r) / 2), u < s && (s = u = (s + u) / 2), a.x0 = o, a.y0 = s, a.x1 = r, a.y1 = u
                            }
                        }(n, r)), o && s.eachBefore(a.default), s
                    }
                    return s.round = function(e) {
                        return arguments.length ? (o = !!e, s) : o
                    }, s.size = function(t) {
                        return arguments.length ? (e = +t[0], n = +t[1], s) : [e, n]
                    }, s.padding = function(e) {
                        return arguments.length ? (t = +e, s) : t
                    }, s
                }
            },
        "./node_modules/d3-hierarchy/src/stratify.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/stratify.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./accessors.js */ "./node_modules/d3-hierarchy/src/accessors.js"),
                    i = t( /*! ./hierarchy/index.js */ "./node_modules/d3-hierarchy/src/hierarchy/index.js"),
                    o = {
                        depth: -1
                    },
                    s = {};

                function r(e) {
                    return e.id
                }

                function u(e) {
                    return e.parentId
                }
                n.default = function() {
                    var e = r,
                        n = u;

                    function t(t) {
                        var a, r, u, l, c, d, g, h = t.length,
                            f = new Array(h),
                            m = {};
                        for (r = 0; r < h; ++r) a = t[r], c = f[r] = new i.Node(a), null != (d = e(a, r, t)) && (d += "") && (m[g = "$" + (c.id = d)] = g in m ? s : c);
                        for (r = 0; r < h; ++r)
                            if (c = f[r], null != (d = n(t[r], r, t)) && (d += "")) {
                                if (!(l = m["$" + d])) throw new Error("missing: " + d);
                                if (l === s) throw new Error("ambiguous: " + d);
                                l.children ? l.children.push(c) : l.children = [c], c.parent = l
                            } else {
                                if (u) throw new Error("multiple roots");
                                u = c
                            }
                        if (!u) throw new Error("no root");
                        if (u.parent = o, u.eachBefore((function(e) {
                                e.depth = e.parent.depth + 1, --h
                            })).eachBefore(i.computeHeight), u.parent = null, h > 0) throw new Error("cycle");
                        return u
                    }
                    return t.id = function(n) {
                        return arguments.length ? (e = Object(a.required)(n), t) : e
                    }, t.parentId = function(e) {
                        return arguments.length ? (n = Object(a.required)(e), t) : n
                    }, t
                }
            },
        "./node_modules/d3-hierarchy/src/tree.js":
            /*!***********************************************!*\
              !*** ./node_modules/d3-hierarchy/src/tree.js ***!
              \***********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./hierarchy/index.js */ "./node_modules/d3-hierarchy/src/hierarchy/index.js");

                function i(e, n) {
                    return e.parent === n.parent ? 1 : 2
                }

                function o(e) {
                    var n = e.children;
                    return n ? n[0] : e.t
                }

                function s(e) {
                    var n = e.children;
                    return n ? n[n.length - 1] : e.t
                }

                function r(e, n, t) {
                    var a = t / (n.i - e.i);
                    n.c -= a, n.s += t, e.c += a, n.z += t, n.m += t
                }

                function u(e, n, t) {
                    return e.a.parent === n.parent ? e.a : t
                }

                function l(e, n) {
                    this._ = e, this.parent = null, this.children = null, this.A = null, this.a = this, this.z = 0, this.m = 0, this.c = 0, this.s = 0, this.t = null, this.i = n
                }
                l.prototype = Object.create(a.Node.prototype), n.default = function() {
                    var e = i,
                        n = 1,
                        t = 1,
                        a = null;

                    function c(i) {
                        var o = function(e) {
                            for (var n, t, a, i, o, s = new l(e, 0), r = [s]; n = r.pop();)
                                if (a = n._.children)
                                    for (n.children = new Array(o = a.length), i = o - 1; i >= 0; --i) r.push(t = n.children[i] = new l(a[i], i)), t.parent = n;
                            return (s.parent = new l(null, 0)).children = [s], s
                        }(i);
                        if (o.eachAfter(d), o.parent.m = -o.z, o.eachBefore(g), a) i.eachBefore(h);
                        else {
                            var s = i,
                                r = i,
                                u = i;
                            i.eachBefore((function(e) {
                                e.x < s.x && (s = e), e.x > r.x && (r = e), e.depth > u.depth && (u = e)
                            }));
                            var c = s === r ? 1 : e(s, r) / 2,
                                f = c - s.x,
                                m = n / (r.x + c + f),
                                p = t / (u.depth || 1);
                            i.eachBefore((function(e) {
                                e.x = (e.x + f) * m, e.y = e.depth * p
                            }))
                        }
                        return i
                    }

                    function d(n) {
                        var t = n.children,
                            a = n.parent.children,
                            i = n.i ? a[n.i - 1] : null;
                        if (t) {
                            ! function(e) {
                                for (var n, t = 0, a = 0, i = e.children, o = i.length; --o >= 0;)(n = i[o]).z += t, n.m += t, t += n.s + (a += n.c)
                            }(n);
                            var l = (t[0].z + t[t.length - 1].z) / 2;
                            i ? (n.z = i.z + e(n._, i._), n.m = n.z - l) : n.z = l
                        } else i && (n.z = i.z + e(n._, i._));
                        n.parent.A = function(n, t, a) {
                            if (t) {
                                for (var i, l = n, c = n, d = t, g = l.parent.children[0], h = l.m, f = c.m, m = d.m, p = g.m; d = s(d), l = o(l), d && l;) g = o(g), (c = s(c)).a = n, (i = d.z + m - l.z - h + e(d._, l._)) > 0 && (r(u(d, n, a), n, i), h += i, f += i), m += d.m, h += l.m, p += g.m, f += c.m;
                                d && !s(c) && (c.t = d, c.m += m - f), l && !o(g) && (g.t = l, g.m += h - p, a = n)
                            }
                            return a
                        }(n, i, n.parent.A || a[0])
                    }

                    function g(e) {
                        e._.x = e.z + e.parent.m, e.m += e.parent.m
                    }

                    function h(e) {
                        e.x *= n, e.y = e.depth * t
                    }
                    return c.separation = function(n) {
                        return arguments.length ? (e = n, c) : e
                    }, c.size = function(e) {
                        return arguments.length ? (a = !1, n = +e[0], t = +e[1], c) : a ? null : [n, t]
                    }, c.nodeSize = function(e) {
                        return arguments.length ? (a = !0, n = +e[0], t = +e[1], c) : a ? [n, t] : null
                    }, c
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/binary.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/binary.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n, t, a, i) {
                    var o, s, r = e.children,
                        u = r.length,
                        l = new Array(u + 1);
                    for (l[0] = s = o = 0; o < u; ++o) l[o + 1] = s += r[o].value;
                    ! function e(n, t, a, i, o, s, u) {
                        if (n >= t - 1) {
                            var c = r[n];
                            return c.x0 = i, c.y0 = o, c.x1 = s, void(c.y1 = u)
                        }
                        var d = l[n],
                            g = a / 2 + d,
                            h = n + 1,
                            f = t - 1;
                        for (; h < f;) {
                            var m = h + f >>> 1;
                            l[m] < g ? h = m + 1 : f = m
                        }
                        g - l[h - 1] < l[h] - g && n + 1 < h && --h;
                        var p = l[h] - d,
                            v = a - p;
                        if (s - i > u - o) {
                            var y = (i * v + s * p) / a;
                            e(n, h, p, i, o, y, u), e(h, t, v, y, o, s, u)
                        } else {
                            var _ = (o * v + u * p) / a;
                            e(n, h, p, i, o, s, _), e(h, t, v, i, _, s, u)
                        }
                    }(0, u, e.value, n, t, a, i)
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/dice.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/dice.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n, t, a, i) {
                    for (var o, s = e.children, r = -1, u = s.length, l = e.value && (a - n) / e.value; ++r < u;)(o = s[r]).y0 = t, o.y1 = i, o.x0 = n, o.x1 = n += o.value * l
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/index.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/index.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./round.js */ "./node_modules/d3-hierarchy/src/treemap/round.js"),
                    i = t( /*! ./squarify.js */ "./node_modules/d3-hierarchy/src/treemap/squarify.js"),
                    o = t( /*! ../accessors.js */ "./node_modules/d3-hierarchy/src/accessors.js"),
                    s = t( /*! ../constant.js */ "./node_modules/d3-hierarchy/src/constant.js");
                n.default = function() {
                    var e = i.default,
                        n = !1,
                        t = 1,
                        r = 1,
                        u = [0],
                        l = s.constantZero,
                        c = s.constantZero,
                        d = s.constantZero,
                        g = s.constantZero,
                        h = s.constantZero;

                    function f(e) {
                        return e.x0 = e.y0 = 0, e.x1 = t, e.y1 = r, e.eachBefore(m), u = [0], n && e.eachBefore(a.default), e
                    }

                    function m(n) {
                        var t = u[n.depth],
                            a = n.x0 + t,
                            i = n.y0 + t,
                            o = n.x1 - t,
                            s = n.y1 - t;
                        o < a && (a = o = (a + o) / 2), s < i && (i = s = (i + s) / 2), n.x0 = a, n.y0 = i, n.x1 = o, n.y1 = s, n.children && (t = u[n.depth + 1] = l(n) / 2, a += h(n) - t, i += c(n) - t, (o -= d(n) - t) < a && (a = o = (a + o) / 2), (s -= g(n) - t) < i && (i = s = (i + s) / 2), e(n, a, i, o, s))
                    }
                    return f.round = function(e) {
                        return arguments.length ? (n = !!e, f) : n
                    }, f.size = function(e) {
                        return arguments.length ? (t = +e[0], r = +e[1], f) : [t, r]
                    }, f.tile = function(n) {
                        return arguments.length ? (e = Object(o.required)(n), f) : e
                    }, f.padding = function(e) {
                        return arguments.length ? f.paddingInner(e).paddingOuter(e) : f.paddingInner()
                    }, f.paddingInner = function(e) {
                        return arguments.length ? (l = "function" == typeof e ? e : Object(s.default)(+e), f) : l
                    }, f.paddingOuter = function(e) {
                        return arguments.length ? f.paddingTop(e).paddingRight(e).paddingBottom(e).paddingLeft(e) : f.paddingTop()
                    }, f.paddingTop = function(e) {
                        return arguments.length ? (c = "function" == typeof e ? e : Object(s.default)(+e), f) : c
                    }, f.paddingRight = function(e) {
                        return arguments.length ? (d = "function" == typeof e ? e : Object(s.default)(+e), f) : d
                    }, f.paddingBottom = function(e) {
                        return arguments.length ? (g = "function" == typeof e ? e : Object(s.default)(+e), f) : g
                    }, f.paddingLeft = function(e) {
                        return arguments.length ? (h = "function" == typeof e ? e : Object(s.default)(+e), f) : h
                    }, f
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/resquarify.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/resquarify.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./dice.js */ "./node_modules/d3-hierarchy/src/treemap/dice.js"),
                    i = t( /*! ./slice.js */ "./node_modules/d3-hierarchy/src/treemap/slice.js"),
                    o = t( /*! ./squarify.js */ "./node_modules/d3-hierarchy/src/treemap/squarify.js");
                n.default = function e(n) {
                    function t(e, t, s, r, u) {
                        if ((l = e._squarify) && l.ratio === n)
                            for (var l, c, d, g, h, f = -1, m = l.length, p = e.value; ++f < m;) {
                                for (d = (c = l[f]).children, g = c.value = 0, h = d.length; g < h; ++g) c.value += d[g].value;
                                c.dice ? Object(a.default)(c, t, s, r, s += (u - s) * c.value / p) : Object(i.default)(c, t, s, t += (r - t) * c.value / p, u), p -= c.value
                            } else e._squarify = l = Object(o.squarifyRatio)(n, e, t, s, r, u), l.ratio = n
                    }
                    return t.ratio = function(n) {
                        return e((n = +n) > 1 ? n : 1)
                    }, t
                }(o.phi)
            },
        "./node_modules/d3-hierarchy/src/treemap/round.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/round.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    e.x0 = Math.round(e.x0), e.y0 = Math.round(e.y0), e.x1 = Math.round(e.x1), e.y1 = Math.round(e.y1)
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/slice.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/slice.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n, t, a, i) {
                    for (var o, s = e.children, r = -1, u = s.length, l = e.value && (i - t) / e.value; ++r < u;)(o = s[r]).x0 = n, o.x1 = a, o.y0 = t, o.y1 = t += o.value * l
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/sliceDice.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/sliceDice.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./dice.js */ "./node_modules/d3-hierarchy/src/treemap/dice.js"),
                    i = t( /*! ./slice.js */ "./node_modules/d3-hierarchy/src/treemap/slice.js");
                n.default = function(e, n, t, o, s) {
                    (1 & e.depth ? i.default : a.default)(e, n, t, o, s)
                }
            },
        "./node_modules/d3-hierarchy/src/treemap/squarify.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-hierarchy/src/treemap/squarify.js ***!
              \***********************************************************/
            /*! exports provided: phi, squarifyRatio, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "phi", (function() {
                    return o
                })), t.d(n, "squarifyRatio", (function() {
                    return s
                }));
                var a = t( /*! ./dice.js */ "./node_modules/d3-hierarchy/src/treemap/dice.js"),
                    i = t( /*! ./slice.js */ "./node_modules/d3-hierarchy/src/treemap/slice.js"),
                    o = (1 + Math.sqrt(5)) / 2;

                function s(e, n, t, o, s, r) {
                    for (var u, l, c, d, g, h, f, m, p, v, y, _ = [], b = n.children, j = 0, x = 0, w = b.length, R = n.value; j < w;) {
                        c = s - t, d = r - o;
                        do {
                            g = b[x++].value
                        } while (!g && x < w);
                        for (h = f = g, y = g * g * (v = Math.max(d / c, c / d) / (R * e)), p = Math.max(f / y, y / h); x < w; ++x) {
                            if (g += l = b[x].value, l < h && (h = l), l > f && (f = l), y = g * g * v, (m = Math.max(f / y, y / h)) > p) {
                                g -= l;
                                break
                            }
                            p = m
                        }
                        _.push(u = {
                            value: g,
                            dice: c < d,
                            children: b.slice(j, x)
                        }), u.dice ? Object(a.default)(u, t, o, s, R ? o += d * g / R : r) : Object(i.default)(u, t, o, R ? t += c * g / R : s, r), R -= g, j = x
                    }
                    return _
                }
                n.default = function e(n) {
                    function t(e, t, a, i, o) {
                        s(n, e, t, a, i, o)
                    }
                    return t.ratio = function(n) {
                        return e((n = +n) > 1 ? n : 1)
                    }, t
                }(o)
            },
        "./node_modules/d3-interpolate/src/array.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/array.js ***!
              \**************************************************/
            /*! exports provided: default, genericArray */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "genericArray", (function() {
                    return o
                }));
                var a = t( /*! ./value.js */ "./node_modules/d3-interpolate/src/value.js"),
                    i = t( /*! ./numberArray.js */ "./node_modules/d3-interpolate/src/numberArray.js");

                function o(e, n) {
                    var t, i = n ? n.length : 0,
                        o = e ? Math.min(i, e.length) : 0,
                        s = new Array(o),
                        r = new Array(i);
                    for (t = 0; t < o; ++t) s[t] = Object(a.default)(e[t], n[t]);
                    for (; t < i; ++t) r[t] = n[t];
                    return function(e) {
                        for (t = 0; t < o; ++t) r[t] = s[t](e);
                        return r
                    }
                }
                n.default = function(e, n) {
                    return (Object(i.isNumberArray)(n) ? i.default : o)(e, n)
                }
            },
        "./node_modules/d3-interpolate/src/basis.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/basis.js ***!
              \**************************************************/
            /*! exports provided: basis, default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t, a, i) {
                    var o = e * e,
                        s = o * e;
                    return ((1 - 3 * e + 3 * o - s) * n + (4 - 6 * o + 3 * s) * t + (1 + 3 * e + 3 * o - 3 * s) * a + s * i) / 6
                }
                t.r(n), t.d(n, "basis", (function() {
                    return a
                })), n.default = function(e) {
                    var n = e.length - 1;
                    return function(t) {
                        var i = t <= 0 ? t = 0 : t >= 1 ? (t = 1, n - 1) : Math.floor(t * n),
                            o = e[i],
                            s = e[i + 1],
                            r = i > 0 ? e[i - 1] : 2 * o - s,
                            u = i < n - 1 ? e[i + 2] : 2 * s - o;
                        return a((t - i / n) * n, r, o, s, u)
                    }
                }
            },
        "./node_modules/d3-interpolate/src/basisClosed.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-interpolate/src/basisClosed.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./basis.js */ "./node_modules/d3-interpolate/src/basis.js");
                n.default = function(e) {
                    var n = e.length;
                    return function(t) {
                        var i = Math.floor(((t %= 1) < 0 ? ++t : t) * n),
                            o = e[(i + n - 1) % n],
                            s = e[i % n],
                            r = e[(i + 1) % n],
                            u = e[(i + 2) % n];
                        return Object(a.basis)((t - i / n) * n, o, s, r, u)
                    }
                }
            },
        "./node_modules/d3-interpolate/src/color.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/color.js ***!
              \**************************************************/
            /*! exports provided: hue, gamma, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "hue", (function() {
                    return o
                })), t.d(n, "gamma", (function() {
                    return s
                })), t.d(n, "default", (function() {
                    return r
                }));
                var a = t( /*! ./constant.js */ "./node_modules/d3-interpolate/src/constant.js");

                function i(e, n) {
                    return function(t) {
                        return e + t * n
                    }
                }

                function o(e, n) {
                    var t = n - e;
                    return t ? i(e, t > 180 || t < -180 ? t - 360 * Math.round(t / 360) : t) : Object(a.default)(isNaN(e) ? n : e)
                }

                function s(e) {
                    return 1 == (e = +e) ? r : function(n, t) {
                        return t - n ? function(e, n, t) {
                            return e = Math.pow(e, t), n = Math.pow(n, t) - e, t = 1 / t,
                                function(a) {
                                    return Math.pow(e + a * n, t)
                                }
                        }(n, t, e) : Object(a.default)(isNaN(n) ? t : n)
                    }
                }

                function r(e, n) {
                    var t = n - e;
                    return t ? i(e, t) : Object(a.default)(isNaN(e) ? n : e)
                }
            },
        "./node_modules/d3-interpolate/src/constant.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-interpolate/src/constant.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return e
                    }
                }
            },
        "./node_modules/d3-interpolate/src/cubehelix.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-interpolate/src/cubehelix.js ***!
              \******************************************************/
            /*! exports provided: default, cubehelixLong */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "cubehelixLong", (function() {
                    return s
                }));
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");

                function o(e) {
                    return function n(t) {
                        function o(n, o) {
                            var s = e((n = Object(a.cubehelix)(n)).h, (o = Object(a.cubehelix)(o)).h),
                                r = Object(i.default)(n.s, o.s),
                                u = Object(i.default)(n.l, o.l),
                                l = Object(i.default)(n.opacity, o.opacity);
                            return function(e) {
                                return n.h = s(e), n.s = r(e), n.l = u(Math.pow(e, t)), n.opacity = l(e), n + ""
                            }
                        }
                        return t = +t, o.gamma = n, o
                    }(1)
                }
                n.default = o(i.hue);
                var s = o(i.default)
            },
        "./node_modules/d3-interpolate/src/date.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-interpolate/src/date.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    var t = new Date;
                    return e = +e, n = +n,
                        function(a) {
                            return t.setTime(e * (1 - a) + n * a), t
                        }
                }
            },
        "./node_modules/d3-interpolate/src/discrete.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-interpolate/src/discrete.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    var n = e.length;
                    return function(t) {
                        return e[Math.max(0, Math.min(n - 1, Math.floor(t * n)))]
                    }
                }
            },
        "./node_modules/d3-interpolate/src/hcl.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-interpolate/src/hcl.js ***!
              \************************************************/
            /*! exports provided: default, hclLong */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "hclLong", (function() {
                    return s
                }));
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");

                function o(e) {
                    return function(n, t) {
                        var o = e((n = Object(a.hcl)(n)).h, (t = Object(a.hcl)(t)).h),
                            s = Object(i.default)(n.c, t.c),
                            r = Object(i.default)(n.l, t.l),
                            u = Object(i.default)(n.opacity, t.opacity);
                        return function(e) {
                            return n.h = o(e), n.c = s(e), n.l = r(e), n.opacity = u(e), n + ""
                        }
                    }
                }
                n.default = o(i.hue);
                var s = o(i.default)
            },
        "./node_modules/d3-interpolate/src/hsl.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-interpolate/src/hsl.js ***!
              \************************************************/
            /*! exports provided: default, hslLong */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "hslLong", (function() {
                    return s
                }));
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");

                function o(e) {
                    return function(n, t) {
                        var o = e((n = Object(a.hsl)(n)).h, (t = Object(a.hsl)(t)).h),
                            s = Object(i.default)(n.s, t.s),
                            r = Object(i.default)(n.l, t.l),
                            u = Object(i.default)(n.opacity, t.opacity);
                        return function(e) {
                            return n.h = o(e), n.s = s(e), n.l = r(e), n.opacity = u(e), n + ""
                        }
                    }
                }
                n.default = o(i.hue);
                var s = o(i.default)
            },
        "./node_modules/d3-interpolate/src/hue.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-interpolate/src/hue.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");
                n.default = function(e, n) {
                    var t = Object(a.hue)(+e, +n);
                    return function(e) {
                        var n = t(e);
                        return n - 360 * Math.floor(n / 360)
                    }
                }
            },
        "./node_modules/d3-interpolate/src/index.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/index.js ***!
              \**************************************************/
            /*! exports provided: interpolate, interpolateArray, interpolateBasis, interpolateBasisClosed, interpolateDate, interpolateDiscrete, interpolateHue, interpolateNumber, interpolateNumberArray, interpolateObject, interpolateRound, interpolateString, interpolateTransformCss, interpolateTransformSvg, interpolateZoom, interpolateRgb, interpolateRgbBasis, interpolateRgbBasisClosed, interpolateHsl, interpolateHslLong, interpolateLab, interpolateHcl, interpolateHclLong, interpolateCubehelix, interpolateCubehelixLong, piecewise, quantize */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./value.js */ "./node_modules/d3-interpolate/src/value.js");
                t.d(n, "interpolate", (function() {
                    return a.default
                }));
                var i = t( /*! ./array.js */ "./node_modules/d3-interpolate/src/array.js");
                t.d(n, "interpolateArray", (function() {
                    return i.default
                }));
                var o = t( /*! ./basis.js */ "./node_modules/d3-interpolate/src/basis.js");
                t.d(n, "interpolateBasis", (function() {
                    return o.default
                }));
                var s = t( /*! ./basisClosed.js */ "./node_modules/d3-interpolate/src/basisClosed.js");
                t.d(n, "interpolateBasisClosed", (function() {
                    return s.default
                }));
                var r = t( /*! ./date.js */ "./node_modules/d3-interpolate/src/date.js");
                t.d(n, "interpolateDate", (function() {
                    return r.default
                }));
                var u = t( /*! ./discrete.js */ "./node_modules/d3-interpolate/src/discrete.js");
                t.d(n, "interpolateDiscrete", (function() {
                    return u.default
                }));
                var l = t( /*! ./hue.js */ "./node_modules/d3-interpolate/src/hue.js");
                t.d(n, "interpolateHue", (function() {
                    return l.default
                }));
                var c = t( /*! ./number.js */ "./node_modules/d3-interpolate/src/number.js");
                t.d(n, "interpolateNumber", (function() {
                    return c.default
                }));
                var d = t( /*! ./numberArray.js */ "./node_modules/d3-interpolate/src/numberArray.js");
                t.d(n, "interpolateNumberArray", (function() {
                    return d.default
                }));
                var g = t( /*! ./object.js */ "./node_modules/d3-interpolate/src/object.js");
                t.d(n, "interpolateObject", (function() {
                    return g.default
                }));
                var h = t( /*! ./round.js */ "./node_modules/d3-interpolate/src/round.js");
                t.d(n, "interpolateRound", (function() {
                    return h.default
                }));
                var f = t( /*! ./string.js */ "./node_modules/d3-interpolate/src/string.js");
                t.d(n, "interpolateString", (function() {
                    return f.default
                }));
                var m = t( /*! ./transform/index.js */ "./node_modules/d3-interpolate/src/transform/index.js");
                t.d(n, "interpolateTransformCss", (function() {
                    return m.interpolateTransformCss
                })), t.d(n, "interpolateTransformSvg", (function() {
                    return m.interpolateTransformSvg
                }));
                var p = t( /*! ./zoom.js */ "./node_modules/d3-interpolate/src/zoom.js");
                t.d(n, "interpolateZoom", (function() {
                    return p.default
                }));
                var v = t( /*! ./rgb.js */ "./node_modules/d3-interpolate/src/rgb.js");
                t.d(n, "interpolateRgb", (function() {
                    return v.default
                })), t.d(n, "interpolateRgbBasis", (function() {
                    return v.rgbBasis
                })), t.d(n, "interpolateRgbBasisClosed", (function() {
                    return v.rgbBasisClosed
                }));
                var y = t( /*! ./hsl.js */ "./node_modules/d3-interpolate/src/hsl.js");
                t.d(n, "interpolateHsl", (function() {
                    return y.default
                })), t.d(n, "interpolateHslLong", (function() {
                    return y.hslLong
                }));
                var _ = t( /*! ./lab.js */ "./node_modules/d3-interpolate/src/lab.js");
                t.d(n, "interpolateLab", (function() {
                    return _.default
                }));
                var b = t( /*! ./hcl.js */ "./node_modules/d3-interpolate/src/hcl.js");
                t.d(n, "interpolateHcl", (function() {
                    return b.default
                })), t.d(n, "interpolateHclLong", (function() {
                    return b.hclLong
                }));
                var j = t( /*! ./cubehelix.js */ "./node_modules/d3-interpolate/src/cubehelix.js");
                t.d(n, "interpolateCubehelix", (function() {
                    return j.default
                })), t.d(n, "interpolateCubehelixLong", (function() {
                    return j.cubehelixLong
                }));
                var x = t( /*! ./piecewise.js */ "./node_modules/d3-interpolate/src/piecewise.js");
                t.d(n, "piecewise", (function() {
                    return x.default
                }));
                var w = t( /*! ./quantize.js */ "./node_modules/d3-interpolate/src/quantize.js");
                t.d(n, "quantize", (function() {
                    return w.default
                }))
            },
        "./node_modules/d3-interpolate/src/lab.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-interpolate/src/lab.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "default", (function() {
                    return o
                }));
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");

                function o(e, n) {
                    var t = Object(i.default)((e = Object(a.lab)(e)).l, (n = Object(a.lab)(n)).l),
                        o = Object(i.default)(e.a, n.a),
                        s = Object(i.default)(e.b, n.b),
                        r = Object(i.default)(e.opacity, n.opacity);
                    return function(n) {
                        return e.l = t(n), e.a = o(n), e.b = s(n), e.opacity = r(n), e + ""
                    }
                }
            },
        "./node_modules/d3-interpolate/src/number.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-interpolate/src/number.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    return e = +e, n = +n,
                        function(t) {
                            return e * (1 - t) + n * t
                        }
                }
            },
        "./node_modules/d3-interpolate/src/numberArray.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-interpolate/src/numberArray.js ***!
              \********************************************************/
            /*! exports provided: default, isNumberArray */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return ArrayBuffer.isView(e) && !(e instanceof DataView)
                }
                t.r(n), t.d(n, "isNumberArray", (function() {
                    return a
                })), n.default = function(e, n) {
                    n || (n = []);
                    var t, a = e ? Math.min(n.length, e.length) : 0,
                        i = n.slice();
                    return function(o) {
                        for (t = 0; t < a; ++t) i[t] = e[t] * (1 - o) + n[t] * o;
                        return i
                    }
                }
            },
        "./node_modules/d3-interpolate/src/object.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-interpolate/src/object.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./value.js */ "./node_modules/d3-interpolate/src/value.js");
                n.default = function(e, n) {
                    var t, i = {},
                        o = {};
                    for (t in null !== e && "object" == typeof e || (e = {}), null !== n && "object" == typeof n || (n = {}), n) t in e ? i[t] = Object(a.default)(e[t], n[t]) : o[t] = n[t];
                    return function(e) {
                        for (t in i) o[t] = i[t](e);
                        return o
                    }
                }
            },
        "./node_modules/d3-interpolate/src/piecewise.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-interpolate/src/piecewise.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e, n) {
                    for (var t = 0, a = n.length - 1, i = n[0], o = new Array(a < 0 ? 0 : a); t < a;) o[t] = e(i, i = n[++t]);
                    return function(e) {
                        var n = Math.max(0, Math.min(a - 1, Math.floor(e *= a)));
                        return o[n](e - n)
                    }
                }
                t.r(n), t.d(n, "default", (function() {
                    return a
                }))
            },
        "./node_modules/d3-interpolate/src/quantize.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-interpolate/src/quantize.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    for (var t = new Array(n), a = 0; a < n; ++a) t[a] = e(a / (n - 1));
                    return t
                }
            },
        "./node_modules/d3-interpolate/src/rgb.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-interpolate/src/rgb.js ***!
              \************************************************/
            /*! exports provided: default, rgbBasis, rgbBasisClosed */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "rgbBasis", (function() {
                    return u
                })), t.d(n, "rgbBasisClosed", (function() {
                    return l
                }));
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./basis.js */ "./node_modules/d3-interpolate/src/basis.js"),
                    o = t( /*! ./basisClosed.js */ "./node_modules/d3-interpolate/src/basisClosed.js"),
                    s = t( /*! ./color.js */ "./node_modules/d3-interpolate/src/color.js");

                function r(e) {
                    return function(n) {
                        var t, i, o = n.length,
                            s = new Array(o),
                            r = new Array(o),
                            u = new Array(o);
                        for (t = 0; t < o; ++t) i = Object(a.rgb)(n[t]), s[t] = i.r || 0, r[t] = i.g || 0, u[t] = i.b || 0;
                        return s = e(s), r = e(r), u = e(u), i.opacity = 1,
                            function(e) {
                                return i.r = s(e), i.g = r(e), i.b = u(e), i + ""
                            }
                    }
                }
                n.default = function e(n) {
                    var t = Object(s.gamma)(n);

                    function i(e, n) {
                        var i = t((e = Object(a.rgb)(e)).r, (n = Object(a.rgb)(n)).r),
                            o = t(e.g, n.g),
                            r = t(e.b, n.b),
                            u = Object(s.default)(e.opacity, n.opacity);
                        return function(n) {
                            return e.r = i(n), e.g = o(n), e.b = r(n), e.opacity = u(n), e + ""
                        }
                    }
                    return i.gamma = e, i
                }(1);
                var u = r(i.default),
                    l = r(o.default)
            },
        "./node_modules/d3-interpolate/src/round.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/round.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    return e = +e, n = +n,
                        function(t) {
                            return Math.round(e * (1 - t) + n * t)
                        }
                }
            },
        "./node_modules/d3-interpolate/src/string.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-interpolate/src/string.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./number.js */ "./node_modules/d3-interpolate/src/number.js"),
                    i = /[-+]?(?:\d+\.?\d*|\.?\d+)(?:[eE][-+]?\d+)?/g,
                    o = new RegExp(i.source, "g");
                n.default = function(e, n) {
                    var t, s, r, u = i.lastIndex = o.lastIndex = 0,
                        l = -1,
                        c = [],
                        d = [];
                    for (e += "", n += "";
                        (t = i.exec(e)) && (s = o.exec(n));)(r = s.index) > u && (r = n.slice(u, r), c[l] ? c[l] += r : c[++l] = r), (t = t[0]) === (s = s[0]) ? c[l] ? c[l] += s : c[++l] = s : (c[++l] = null, d.push({
                        i: l,
                        x: Object(a.default)(t, s)
                    })), u = o.lastIndex;
                    return u < n.length && (r = n.slice(u), c[l] ? c[l] += r : c[++l] = r), c.length < 2 ? d[0] ? function(e) {
                        return function(n) {
                            return e(n) + ""
                        }
                    }(d[0].x) : function(e) {
                        return function() {
                            return e
                        }
                    }(n) : (n = d.length, function(e) {
                        for (var t, a = 0; a < n; ++a) c[(t = d[a]).i] = t.x(e);
                        return c.join("")
                    })
                }
            },
        "./node_modules/d3-interpolate/src/transform/decompose.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-interpolate/src/transform/decompose.js ***!
              \****************************************************************/
            /*! exports provided: identity, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "identity", (function() {
                    return i
                }));
                var a = 180 / Math.PI,
                    i = {
                        translateX: 0,
                        translateY: 0,
                        rotate: 0,
                        skewX: 0,
                        scaleX: 1,
                        scaleY: 1
                    };
                n.default = function(e, n, t, i, o, s) {
                    var r, u, l;
                    return (r = Math.sqrt(e * e + n * n)) && (e /= r, n /= r), (l = e * t + n * i) && (t -= e * l, i -= n * l), (u = Math.sqrt(t * t + i * i)) && (t /= u, i /= u, l /= u), e * i < n * t && (e = -e, n = -n, l = -l, r = -r), {
                        translateX: o,
                        translateY: s,
                        rotate: Math.atan2(n, e) * a,
                        skewX: Math.atan(l) * a,
                        scaleX: r,
                        scaleY: u
                    }
                }
            },
        "./node_modules/d3-interpolate/src/transform/index.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-interpolate/src/transform/index.js ***!
              \************************************************************/
            /*! exports provided: interpolateTransformCss, interpolateTransformSvg */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "interpolateTransformCss", (function() {
                    return s
                })), t.d(n, "interpolateTransformSvg", (function() {
                    return r
                }));
                var a = t( /*! ../number.js */ "./node_modules/d3-interpolate/src/number.js"),
                    i = t( /*! ./parse.js */ "./node_modules/d3-interpolate/src/transform/parse.js");

                function o(e, n, t, i) {
                    function o(e) {
                        return e.length ? e.pop() + " " : ""
                    }
                    return function(s, r) {
                        var u = [],
                            l = [];
                        return s = e(s), r = e(r),
                            function(e, i, o, s, r, u) {
                                if (e !== o || i !== s) {
                                    var l = r.push("translate(", null, n, null, t);
                                    u.push({
                                        i: l - 4,
                                        x: Object(a.default)(e, o)
                                    }, {
                                        i: l - 2,
                                        x: Object(a.default)(i, s)
                                    })
                                } else(o || s) && r.push("translate(" + o + n + s + t)
                            }(s.translateX, s.translateY, r.translateX, r.translateY, u, l),
                            function(e, n, t, s) {
                                e !== n ? (e - n > 180 ? n += 360 : n - e > 180 && (e += 360), s.push({
                                    i: t.push(o(t) + "rotate(", null, i) - 2,
                                    x: Object(a.default)(e, n)
                                })) : n && t.push(o(t) + "rotate(" + n + i)
                            }(s.rotate, r.rotate, u, l),
                            function(e, n, t, s) {
                                e !== n ? s.push({
                                    i: t.push(o(t) + "skewX(", null, i) - 2,
                                    x: Object(a.default)(e, n)
                                }) : n && t.push(o(t) + "skewX(" + n + i)
                            }(s.skewX, r.skewX, u, l),
                            function(e, n, t, i, s, r) {
                                if (e !== t || n !== i) {
                                    var u = s.push(o(s) + "scale(", null, ",", null, ")");
                                    r.push({
                                        i: u - 4,
                                        x: Object(a.default)(e, t)
                                    }, {
                                        i: u - 2,
                                        x: Object(a.default)(n, i)
                                    })
                                } else 1 === t && 1 === i || s.push(o(s) + "scale(" + t + "," + i + ")")
                            }(s.scaleX, s.scaleY, r.scaleX, r.scaleY, u, l), s = r = null,
                            function(e) {
                                for (var n, t = -1, a = l.length; ++t < a;) u[(n = l[t]).i] = n.x(e);
                                return u.join("")
                            }
                    }
                }
                var s = o(i.parseCss, "px, ", "px)", "deg)"),
                    r = o(i.parseSvg, ", ", ")", ")")
            },
        "./node_modules/d3-interpolate/src/transform/parse.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-interpolate/src/transform/parse.js ***!
              \************************************************************/
            /*! exports provided: parseCss, parseSvg */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "parseCss", (function() {
                    return u
                })), t.d(n, "parseSvg", (function() {
                    return l
                }));
                var a, i, o, s, r = t( /*! ./decompose.js */ "./node_modules/d3-interpolate/src/transform/decompose.js");

                function u(e) {
                    return "none" === e ? r.identity : (a || (a = document.createElement("DIV"), i = document.documentElement, o = document.defaultView), a.style.transform = e, e = o.getComputedStyle(i.appendChild(a), null).getPropertyValue("transform"), i.removeChild(a), e = e.slice(7, -1).split(","), Object(r.default)(+e[0], +e[1], +e[2], +e[3], +e[4], +e[5]))
                }

                function l(e) {
                    return null == e ? r.identity : (s || (s = document.createElementNS("http://www.w3.org/2000/svg", "g")), s.setAttribute("transform", e), (e = s.transform.baseVal.consolidate()) ? (e = e.matrix, Object(r.default)(e.a, e.b, e.c, e.d, e.e, e.f)) : r.identity)
                }
            },
        "./node_modules/d3-interpolate/src/value.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-interpolate/src/value.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! ./rgb.js */ "./node_modules/d3-interpolate/src/rgb.js"),
                    o = t( /*! ./array.js */ "./node_modules/d3-interpolate/src/array.js"),
                    s = t( /*! ./date.js */ "./node_modules/d3-interpolate/src/date.js"),
                    r = t( /*! ./number.js */ "./node_modules/d3-interpolate/src/number.js"),
                    u = t( /*! ./object.js */ "./node_modules/d3-interpolate/src/object.js"),
                    l = t( /*! ./string.js */ "./node_modules/d3-interpolate/src/string.js"),
                    c = t( /*! ./constant.js */ "./node_modules/d3-interpolate/src/constant.js"),
                    d = t( /*! ./numberArray.js */ "./node_modules/d3-interpolate/src/numberArray.js");
                n.default = function(e, n) {
                    var t, g = typeof n;
                    return null == n || "boolean" === g ? Object(c.default)(n) : ("number" === g ? r.default : "string" === g ? (t = Object(a.color)(n)) ? (n = t, i.default) : l.default : n instanceof a.color ? i.default : n instanceof Date ? s.default : Object(d.isNumberArray)(n) ? d.default : Array.isArray(n) ? o.genericArray : "function" != typeof n.valueOf && "function" != typeof n.toString || isNaN(n) ? u.default : r.default)(e, n)
                }
            },
        "./node_modules/d3-interpolate/src/zoom.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-interpolate/src/zoom.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = Math.SQRT2;

                function i(e) {
                    return ((e = Math.exp(e)) + 1 / e) / 2
                }
                n.default = function(e, n) {
                    var t, o, s = e[0],
                        r = e[1],
                        u = e[2],
                        l = n[0],
                        c = n[1],
                        d = n[2],
                        g = l - s,
                        h = c - r,
                        f = g * g + h * h;
                    if (f < 1e-12) o = Math.log(d / u) / a, t = function(e) {
                        return [s + e * g, r + e * h, u * Math.exp(a * e * o)]
                    };
                    else {
                        var m = Math.sqrt(f),
                            p = (d * d - u * u + 4 * f) / (2 * u * 2 * m),
                            v = (d * d - u * u - 4 * f) / (2 * d * 2 * m),
                            y = Math.log(Math.sqrt(p * p + 1) - p),
                            _ = Math.log(Math.sqrt(v * v + 1) - v);
                        o = (_ - y) / a, t = function(e) {
                            var n, t = e * o,
                                l = i(y),
                                c = u / (2 * m) * (l * (n = a * t + y, ((n = Math.exp(2 * n)) - 1) / (n + 1)) - function(e) {
                                    return ((e = Math.exp(e)) - 1 / e) / 2
                                }(y));
                            return [s + c * g, r + c * h, u * l / i(a * t + y)]
                        }
                    }
                    return t.duration = 1e3 * o, t
                }
            },
        "./node_modules/d3-path/src/index.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-path/src/index.js ***!
              \*******************************************/
            /*! exports provided: path */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./path.js */ "./node_modules/d3-path/src/path.js");
                t.d(n, "path", (function() {
                    return a.default
                }))
            },
        "./node_modules/d3-path/src/path.js":
            /*!******************************************!*\
              !*** ./node_modules/d3-path/src/path.js ***!
              \******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = Math.PI,
                    i = 2 * a,
                    o = i - 1e-6;

                function s() {
                    this._x0 = this._y0 = this._x1 = this._y1 = null, this._ = ""
                }

                function r() {
                    return new s
                }
                s.prototype = r.prototype = {
                    constructor: s,
                    moveTo: function(e, n) {
                        this._ += "M" + (this._x0 = this._x1 = +e) + "," + (this._y0 = this._y1 = +n)
                    },
                    closePath: function() {
                        null !== this._x1 && (this._x1 = this._x0, this._y1 = this._y0, this._ += "Z")
                    },
                    lineTo: function(e, n) {
                        this._ += "L" + (this._x1 = +e) + "," + (this._y1 = +n)
                    },
                    quadraticCurveTo: function(e, n, t, a) {
                        this._ += "Q" + +e + "," + +n + "," + (this._x1 = +t) + "," + (this._y1 = +a)
                    },
                    bezierCurveTo: function(e, n, t, a, i, o) {
                        this._ += "C" + +e + "," + +n + "," + +t + "," + +a + "," + (this._x1 = +i) + "," + (this._y1 = +o)
                    },
                    arcTo: function(e, n, t, i, o) {
                        e = +e, n = +n, t = +t, i = +i, o = +o;
                        var s = this._x1,
                            r = this._y1,
                            u = t - e,
                            l = i - n,
                            c = s - e,
                            d = r - n,
                            g = c * c + d * d;
                        if (o < 0) throw new Error("negative radius: " + o);
                        if (null === this._x1) this._ += "M" + (this._x1 = e) + "," + (this._y1 = n);
                        else if (g > 1e-6)
                            if (Math.abs(d * u - l * c) > 1e-6 && o) {
                                var h = t - s,
                                    f = i - r,
                                    m = u * u + l * l,
                                    p = h * h + f * f,
                                    v = Math.sqrt(m),
                                    y = Math.sqrt(g),
                                    _ = o * Math.tan((a - Math.acos((m + g - p) / (2 * v * y))) / 2),
                                    b = _ / y,
                                    j = _ / v;
                                Math.abs(b - 1) > 1e-6 && (this._ += "L" + (e + b * c) + "," + (n + b * d)), this._ += "A" + o + "," + o + ",0,0," + +(d * h > c * f) + "," + (this._x1 = e + j * u) + "," + (this._y1 = n + j * l)
                            } else this._ += "L" + (this._x1 = e) + "," + (this._y1 = n);
                        else;
                    },
                    arc: function(e, n, t, s, r, u) {
                        e = +e, n = +n, u = !!u;
                        var l = (t = +t) * Math.cos(s),
                            c = t * Math.sin(s),
                            d = e + l,
                            g = n + c,
                            h = 1 ^ u,
                            f = u ? s - r : r - s;
                        if (t < 0) throw new Error("negative radius: " + t);
                        null === this._x1 ? this._ += "M" + d + "," + g : (Math.abs(this._x1 - d) > 1e-6 || Math.abs(this._y1 - g) > 1e-6) && (this._ += "L" + d + "," + g), t && (f < 0 && (f = f % i + i), f > o ? this._ += "A" + t + "," + t + ",0,1," + h + "," + (e - l) + "," + (n - c) + "A" + t + "," + t + ",0,1," + h + "," + (this._x1 = d) + "," + (this._y1 = g) : f > 1e-6 && (this._ += "A" + t + "," + t + ",0," + +(f >= a) + "," + h + "," + (this._x1 = e + t * Math.cos(r)) + "," + (this._y1 = n + t * Math.sin(r))))
                    },
                    rect: function(e, n, t, a) {
                        this._ += "M" + (this._x0 = this._x1 = +e) + "," + (this._y0 = this._y1 = +n) + "h" + +t + "v" + +a + "h" + -t + "Z"
                    },
                    toString: function() {
                        return this._
                    }
                }, n.default = r
            },
        "./node_modules/d3-selection/src/constant.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-selection/src/constant.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return e
                    }
                }
            },
        "./node_modules/d3-selection/src/create.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-selection/src/create.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./creator */ "./node_modules/d3-selection/src/creator.js"),
                    i = t( /*! ./select */ "./node_modules/d3-selection/src/select.js");
                n.default = function(e) {
                    return Object(i.default)(Object(a.default)(e).call(document.documentElement))
                }
            },
        "./node_modules/d3-selection/src/creator.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-selection/src/creator.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./namespace */ "./node_modules/d3-selection/src/namespace.js"),
                    i = t( /*! ./namespaces */ "./node_modules/d3-selection/src/namespaces.js");

                function o(e) {
                    return function() {
                        var n = this.ownerDocument,
                            t = this.namespaceURI;
                        return t === i.xhtml && n.documentElement.namespaceURI === i.xhtml ? n.createElement(e) : n.createElementNS(t, e)
                    }
                }

                function s(e) {
                    return function() {
                        return this.ownerDocument.createElementNS(e.space, e.local)
                    }
                }
                n.default = function(e) {
                    var n = Object(a.default)(e);
                    return (n.local ? s : o)(n)
                }
            },
        "./node_modules/d3-selection/src/index.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-selection/src/index.js ***!
              \************************************************/
            /*! exports provided: create, creator, local, matcher, mouse, namespace, namespaces, clientPoint, select, selectAll, selection, selector, selectorAll, style, touch, touches, window, event, customEvent */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./create */ "./node_modules/d3-selection/src/create.js");
                t.d(n, "create", (function() {
                    return a.default
                }));
                var i = t( /*! ./creator */ "./node_modules/d3-selection/src/creator.js");
                t.d(n, "creator", (function() {
                    return i.default
                }));
                var o = t( /*! ./local */ "./node_modules/d3-selection/src/local.js");
                t.d(n, "local", (function() {
                    return o.default
                }));
                var s = t( /*! ./matcher */ "./node_modules/d3-selection/src/matcher.js");
                t.d(n, "matcher", (function() {
                    return s.default
                }));
                var r = t( /*! ./mouse */ "./node_modules/d3-selection/src/mouse.js");
                t.d(n, "mouse", (function() {
                    return r.default
                }));
                var u = t( /*! ./namespace */ "./node_modules/d3-selection/src/namespace.js");
                t.d(n, "namespace", (function() {
                    return u.default
                }));
                var l = t( /*! ./namespaces */ "./node_modules/d3-selection/src/namespaces.js");
                t.d(n, "namespaces", (function() {
                    return l.default
                }));
                var c = t( /*! ./point */ "./node_modules/d3-selection/src/point.js");
                t.d(n, "clientPoint", (function() {
                    return c.default
                }));
                var d = t( /*! ./select */ "./node_modules/d3-selection/src/select.js");
                t.d(n, "select", (function() {
                    return d.default
                }));
                var g = t( /*! ./selectAll */ "./node_modules/d3-selection/src/selectAll.js");
                t.d(n, "selectAll", (function() {
                    return g.default
                }));
                var h = t( /*! ./selection/index */ "./node_modules/d3-selection/src/selection/index.js");
                t.d(n, "selection", (function() {
                    return h.default
                }));
                var f = t( /*! ./selector */ "./node_modules/d3-selection/src/selector.js");
                t.d(n, "selector", (function() {
                    return f.default
                }));
                var m = t( /*! ./selectorAll */ "./node_modules/d3-selection/src/selectorAll.js");
                t.d(n, "selectorAll", (function() {
                    return m.default
                }));
                var p = t( /*! ./selection/style */ "./node_modules/d3-selection/src/selection/style.js");
                t.d(n, "style", (function() {
                    return p.styleValue
                }));
                var v = t( /*! ./touch */ "./node_modules/d3-selection/src/touch.js");
                t.d(n, "touch", (function() {
                    return v.default
                }));
                var y = t( /*! ./touches */ "./node_modules/d3-selection/src/touches.js");
                t.d(n, "touches", (function() {
                    return y.default
                }));
                var _ = t( /*! ./window */ "./node_modules/d3-selection/src/window.js");
                t.d(n, "window", (function() {
                    return _.default
                }));
                var b = t( /*! ./selection/on */ "./node_modules/d3-selection/src/selection/on.js");
                t.d(n, "event", (function() {
                    return b.event
                })), t.d(n, "customEvent", (function() {
                    return b.customEvent
                }))
            },
        "./node_modules/d3-selection/src/local.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-selection/src/local.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "default", (function() {
                    return i
                }));
                var a = 0;

                function i() {
                    return new o
                }

                function o() {
                    this._ = "@" + (++a).toString(36)
                }
                o.prototype = i.prototype = {
                    constructor: o,
                    get: function(e) {
                        for (var n = this._; !(n in e);)
                            if (!(e = e.parentNode)) return;
                        return e[n]
                    },
                    set: function(e, n) {
                        return e[this._] = n
                    },
                    remove: function(e) {
                        return this._ in e && delete e[this._]
                    },
                    toString: function() {
                        return this._
                    }
                }
            },
        "./node_modules/d3-selection/src/matcher.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-selection/src/matcher.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return this.matches(e)
                    }
                }
            },
        "./node_modules/d3-selection/src/mouse.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-selection/src/mouse.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./sourceEvent */ "./node_modules/d3-selection/src/sourceEvent.js"),
                    i = t( /*! ./point */ "./node_modules/d3-selection/src/point.js");
                n.default = function(e) {
                    var n = Object(a.default)();
                    return n.changedTouches && (n = n.changedTouches[0]), Object(i.default)(e, n)
                }
            },
        "./node_modules/d3-selection/src/namespace.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-selection/src/namespace.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./namespaces */ "./node_modules/d3-selection/src/namespaces.js");
                n.default = function(e) {
                    var n = e += "",
                        t = n.indexOf(":");
                    return t >= 0 && "xmlns" !== (n = e.slice(0, t)) && (e = e.slice(t + 1)), a.default.hasOwnProperty(n) ? {
                        space: a.default[n],
                        local: e
                    } : e
                }
            },
        "./node_modules/d3-selection/src/namespaces.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-selection/src/namespaces.js ***!
              \*****************************************************/
            /*! exports provided: xhtml, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "xhtml", (function() {
                    return a
                }));
                var a = "http://www.w3.org/1999/xhtml";
                n.default = {
                    svg: "http://www.w3.org/2000/svg",
                    xhtml: a,
                    xlink: "http://www.w3.org/1999/xlink",
                    xml: "http://www.w3.org/XML/1998/namespace",
                    xmlns: "http://www.w3.org/2000/xmlns/"
                }
            },
        "./node_modules/d3-selection/src/point.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-selection/src/point.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    var t = e.ownerSVGElement || e;
                    if (t.createSVGPoint) {
                        var a = t.createSVGPoint();
                        return a.x = n.clientX, a.y = n.clientY, [(a = a.matrixTransform(e.getScreenCTM().inverse())).x, a.y]
                    }
                    var i = e.getBoundingClientRect();
                    return [n.clientX - i.left - e.clientLeft, n.clientY - i.top - e.clientTop]
                }
            },
        "./node_modules/d3-selection/src/select.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-selection/src/select.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./selection/index */ "./node_modules/d3-selection/src/selection/index.js");
                n.default = function(e) {
                    return "string" == typeof e ? new a.Selection([
                        [document.querySelector(e)]
                    ], [document.documentElement]) : new a.Selection([
                        [e]
                    ], a.root)
                }
            },
        "./node_modules/d3-selection/src/selectAll.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-selection/src/selectAll.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./selection/index */ "./node_modules/d3-selection/src/selection/index.js");
                n.default = function(e) {
                    return "string" == typeof e ? new a.Selection([document.querySelectorAll(e)], [document.documentElement]) : new a.Selection([null == e ? [] : e], a.root)
                }
            },
        "./node_modules/d3-selection/src/selection/append.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/append.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../creator */ "./node_modules/d3-selection/src/creator.js");
                n.default = function(e) {
                    var n = "function" == typeof e ? e : Object(a.default)(e);
                    return this.select((function() {
                        return this.appendChild(n.apply(this, arguments))
                    }))
                }
            },
        "./node_modules/d3-selection/src/selection/attr.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/attr.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../namespace */ "./node_modules/d3-selection/src/namespace.js");

                function i(e) {
                    return function() {
                        this.removeAttribute(e)
                    }
                }

                function o(e) {
                    return function() {
                        this.removeAttributeNS(e.space, e.local)
                    }
                }

                function s(e, n) {
                    return function() {
                        this.setAttribute(e, n)
                    }
                }

                function r(e, n) {
                    return function() {
                        this.setAttributeNS(e.space, e.local, n)
                    }
                }

                function u(e, n) {
                    return function() {
                        var t = n.apply(this, arguments);
                        null == t ? this.removeAttribute(e) : this.setAttribute(e, t)
                    }
                }

                function l(e, n) {
                    return function() {
                        var t = n.apply(this, arguments);
                        null == t ? this.removeAttributeNS(e.space, e.local) : this.setAttributeNS(e.space, e.local, t)
                    }
                }
                n.default = function(e, n) {
                    var t = Object(a.default)(e);
                    if (arguments.length < 2) {
                        var c = this.node();
                        return t.local ? c.getAttributeNS(t.space, t.local) : c.getAttribute(t)
                    }
                    return this.each((null == n ? t.local ? o : i : "function" == typeof n ? t.local ? l : u : t.local ? r : s)(t, n))
                }
            },
        "./node_modules/d3-selection/src/selection/call.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/call.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    var e = arguments[0];
                    return arguments[0] = this, e.apply(null, arguments), this
                }
            },
        "./node_modules/d3-selection/src/selection/classed.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/classed.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return e.trim().split(/^|\s+/)
                }

                function i(e) {
                    return e.classList || new o(e)
                }

                function o(e) {
                    this._node = e, this._names = a(e.getAttribute("class") || "")
                }

                function s(e, n) {
                    for (var t = i(e), a = -1, o = n.length; ++a < o;) t.add(n[a])
                }

                function r(e, n) {
                    for (var t = i(e), a = -1, o = n.length; ++a < o;) t.remove(n[a])
                }

                function u(e) {
                    return function() {
                        s(this, e)
                    }
                }

                function l(e) {
                    return function() {
                        r(this, e)
                    }
                }

                function c(e, n) {
                    return function() {
                        (n.apply(this, arguments) ? s : r)(this, e)
                    }
                }
                t.r(n), o.prototype = {
                    add: function(e) {
                        this._names.indexOf(e) < 0 && (this._names.push(e), this._node.setAttribute("class", this._names.join(" ")))
                    },
                    remove: function(e) {
                        var n = this._names.indexOf(e);
                        n >= 0 && (this._names.splice(n, 1), this._node.setAttribute("class", this._names.join(" ")))
                    },
                    contains: function(e) {
                        return this._names.indexOf(e) >= 0
                    }
                }, n.default = function(e, n) {
                    var t = a(e + "");
                    if (arguments.length < 2) {
                        for (var o = i(this.node()), s = -1, r = t.length; ++s < r;)
                            if (!o.contains(t[s])) return !1;
                        return !0
                    }
                    return this.each(("function" == typeof n ? c : n ? u : l)(t, n))
                }
            },
        "./node_modules/d3-selection/src/selection/clone.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/clone.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    var e = this.cloneNode(!1),
                        n = this.parentNode;
                    return n ? n.insertBefore(e, this.nextSibling) : e
                }

                function i() {
                    var e = this.cloneNode(!0),
                        n = this.parentNode;
                    return n ? n.insertBefore(e, this.nextSibling) : e
                }
                t.r(n), n.default = function(e) {
                    return this.select(e ? i : a)
                }
            },
        "./node_modules/d3-selection/src/selection/data.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/data.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js"),
                    i = t( /*! ./enter */ "./node_modules/d3-selection/src/selection/enter.js"),
                    o = t( /*! ../constant */ "./node_modules/d3-selection/src/constant.js");

                function s(e, n, t, a, o, s) {
                    for (var r, u = 0, l = n.length, c = s.length; u < c; ++u)(r = n[u]) ? (r.__data__ = s[u], a[u] = r) : t[u] = new i.EnterNode(e, s[u]);
                    for (; u < l; ++u)(r = n[u]) && (o[u] = r)
                }

                function r(e, n, t, a, o, s, r) {
                    var u, l, c, d = {},
                        g = n.length,
                        h = s.length,
                        f = new Array(g);
                    for (u = 0; u < g; ++u)(l = n[u]) && (f[u] = c = "$" + r.call(l, l.__data__, u, n), c in d ? o[u] = l : d[c] = l);
                    for (u = 0; u < h; ++u)(l = d[c = "$" + r.call(e, s[u], u, s)]) ? (a[u] = l, l.__data__ = s[u], d[c] = null) : t[u] = new i.EnterNode(e, s[u]);
                    for (u = 0; u < g; ++u)(l = n[u]) && d[f[u]] === l && (o[u] = l)
                }
                n.default = function(e, n) {
                    if (!e) return v = new Array(this.size()), h = -1, this.each((function(e) {
                        v[++h] = e
                    })), v;
                    var t = n ? r : s,
                        i = this._parents,
                        u = this._groups;
                    "function" != typeof e && (e = Object(o.default)(e));
                    for (var l = u.length, c = new Array(l), d = new Array(l), g = new Array(l), h = 0; h < l; ++h) {
                        var f = i[h],
                            m = u[h],
                            p = m.length,
                            v = e.call(f, f && f.__data__, h, i),
                            y = v.length,
                            _ = d[h] = new Array(y),
                            b = c[h] = new Array(y);
                        t(f, m, _, b, g[h] = new Array(p), v, n);
                        for (var j, x, w = 0, R = 0; w < y; ++w)
                            if (j = _[w]) {
                                for (w >= R && (R = w + 1); !(x = b[R]) && ++R < y;);
                                j._next = x || null
                            }
                    }
                    return (c = new a.Selection(c, i))._enter = d, c._exit = g, c
                }
            },
        "./node_modules/d3-selection/src/selection/datum.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/datum.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return arguments.length ? this.property("__data__", e) : this.node().__data__
                }
            },
        "./node_modules/d3-selection/src/selection/dispatch.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/dispatch.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../window */ "./node_modules/d3-selection/src/window.js");

                function i(e, n, t) {
                    var i = Object(a.default)(e),
                        o = i.CustomEvent;
                    "function" == typeof o ? o = new o(n, t) : (o = i.document.createEvent("Event"), t ? (o.initEvent(n, t.bubbles, t.cancelable), o.detail = t.detail) : o.initEvent(n, !1, !1)), e.dispatchEvent(o)
                }

                function o(e, n) {
                    return function() {
                        return i(this, e, n)
                    }
                }

                function s(e, n) {
                    return function() {
                        return i(this, e, n.apply(this, arguments))
                    }
                }
                n.default = function(e, n) {
                    return this.each(("function" == typeof n ? s : o)(e, n))
                }
            },
        "./node_modules/d3-selection/src/selection/each.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/each.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    for (var n = this._groups, t = 0, a = n.length; t < a; ++t)
                        for (var i, o = n[t], s = 0, r = o.length; s < r; ++s)(i = o[s]) && e.call(i, i.__data__, s, o);
                    return this
                }
            },
        "./node_modules/d3-selection/src/selection/empty.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/empty.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    return !this.node()
                }
            },
        "./node_modules/d3-selection/src/selection/enter.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/enter.js ***!
              \**********************************************************/
            /*! exports provided: default, EnterNode */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "EnterNode", (function() {
                    return o
                }));
                var a = t( /*! ./sparse */ "./node_modules/d3-selection/src/selection/sparse.js"),
                    i = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js");

                function o(e, n) {
                    this.ownerDocument = e.ownerDocument, this.namespaceURI = e.namespaceURI, this._next = null, this._parent = e, this.__data__ = n
                }
                n.default = function() {
                    return new i.Selection(this._enter || this._groups.map(a.default), this._parents)
                }, o.prototype = {
                    constructor: o,
                    appendChild: function(e) {
                        return this._parent.insertBefore(e, this._next)
                    },
                    insertBefore: function(e, n) {
                        return this._parent.insertBefore(e, n)
                    },
                    querySelector: function(e) {
                        return this._parent.querySelector(e)
                    },
                    querySelectorAll: function(e) {
                        return this._parent.querySelectorAll(e)
                    }
                }
            },
        "./node_modules/d3-selection/src/selection/exit.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/exit.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./sparse */ "./node_modules/d3-selection/src/selection/sparse.js"),
                    i = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js");
                n.default = function() {
                    return new i.Selection(this._exit || this._groups.map(a.default), this._parents)
                }
            },
        "./node_modules/d3-selection/src/selection/filter.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/filter.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js"),
                    i = t( /*! ../matcher */ "./node_modules/d3-selection/src/matcher.js");
                n.default = function(e) {
                    "function" != typeof e && (e = Object(i.default)(e));
                    for (var n = this._groups, t = n.length, o = new Array(t), s = 0; s < t; ++s)
                        for (var r, u = n[s], l = u.length, c = o[s] = [], d = 0; d < l; ++d)(r = u[d]) && e.call(r, r.__data__, d, u) && c.push(r);
                    return new a.Selection(o, this._parents)
                }
            },
        "./node_modules/d3-selection/src/selection/html.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/html.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    this.innerHTML = ""
                }

                function i(e) {
                    return function() {
                        this.innerHTML = e
                    }
                }

                function o(e) {
                    return function() {
                        var n = e.apply(this, arguments);
                        this.innerHTML = null == n ? "" : n
                    }
                }
                t.r(n), n.default = function(e) {
                    return arguments.length ? this.each(null == e ? a : ("function" == typeof e ? o : i)(e)) : this.node().innerHTML
                }
            },
        "./node_modules/d3-selection/src/selection/index.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/index.js ***!
              \**********************************************************/
            /*! exports provided: root, Selection, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "root", (function() {
                    return T
                })), t.d(n, "Selection", (function() {
                    return N
                }));
                var a = t( /*! ./select */ "./node_modules/d3-selection/src/selection/select.js"),
                    i = t( /*! ./selectAll */ "./node_modules/d3-selection/src/selection/selectAll.js"),
                    o = t( /*! ./filter */ "./node_modules/d3-selection/src/selection/filter.js"),
                    s = t( /*! ./data */ "./node_modules/d3-selection/src/selection/data.js"),
                    r = t( /*! ./enter */ "./node_modules/d3-selection/src/selection/enter.js"),
                    u = t( /*! ./exit */ "./node_modules/d3-selection/src/selection/exit.js"),
                    l = t( /*! ./join */ "./node_modules/d3-selection/src/selection/join.js"),
                    c = t( /*! ./merge */ "./node_modules/d3-selection/src/selection/merge.js"),
                    d = t( /*! ./order */ "./node_modules/d3-selection/src/selection/order.js"),
                    g = t( /*! ./sort */ "./node_modules/d3-selection/src/selection/sort.js"),
                    h = t( /*! ./call */ "./node_modules/d3-selection/src/selection/call.js"),
                    f = t( /*! ./nodes */ "./node_modules/d3-selection/src/selection/nodes.js"),
                    m = t( /*! ./node */ "./node_modules/d3-selection/src/selection/node.js"),
                    p = t( /*! ./size */ "./node_modules/d3-selection/src/selection/size.js"),
                    v = t( /*! ./empty */ "./node_modules/d3-selection/src/selection/empty.js"),
                    y = t( /*! ./each */ "./node_modules/d3-selection/src/selection/each.js"),
                    _ = t( /*! ./attr */ "./node_modules/d3-selection/src/selection/attr.js"),
                    b = t( /*! ./style */ "./node_modules/d3-selection/src/selection/style.js"),
                    j = t( /*! ./property */ "./node_modules/d3-selection/src/selection/property.js"),
                    x = t( /*! ./classed */ "./node_modules/d3-selection/src/selection/classed.js"),
                    w = t( /*! ./text */ "./node_modules/d3-selection/src/selection/text.js"),
                    R = t( /*! ./html */ "./node_modules/d3-selection/src/selection/html.js"),
                    k = t( /*! ./raise */ "./node_modules/d3-selection/src/selection/raise.js"),
                    A = t( /*! ./lower */ "./node_modules/d3-selection/src/selection/lower.js"),
                    O = t( /*! ./append */ "./node_modules/d3-selection/src/selection/append.js"),
                    S = t( /*! ./insert */ "./node_modules/d3-selection/src/selection/insert.js"),
                    C = t( /*! ./remove */ "./node_modules/d3-selection/src/selection/remove.js"),
                    E = t( /*! ./clone */ "./node_modules/d3-selection/src/selection/clone.js"),
                    B = t( /*! ./datum */ "./node_modules/d3-selection/src/selection/datum.js"),
                    M = t( /*! ./on */ "./node_modules/d3-selection/src/selection/on.js"),
                    D = t( /*! ./dispatch */ "./node_modules/d3-selection/src/selection/dispatch.js"),
                    T = [null];

                function N(e, n) {
                    this._groups = e, this._parents = n
                }

                function z() {
                    return new N([
                        [document.documentElement]
                    ], T)
                }
                N.prototype = z.prototype = {
                    constructor: N,
                    select: a.default,
                    selectAll: i.default,
                    filter: o.default,
                    data: s.default,
                    enter: r.default,
                    exit: u.default,
                    join: l.default,
                    merge: c.default,
                    order: d.default,
                    sort: g.default,
                    call: h.default,
                    nodes: f.default,
                    node: m.default,
                    size: p.default,
                    empty: v.default,
                    each: y.default,
                    attr: _.default,
                    style: b.default,
                    property: j.default,
                    classed: x.default,
                    text: w.default,
                    html: R.default,
                    raise: k.default,
                    lower: A.default,
                    append: O.default,
                    insert: S.default,
                    remove: C.default,
                    clone: E.default,
                    datum: B.default,
                    on: M.default,
                    dispatch: D.default
                }, n.default = z
            },
        "./node_modules/d3-selection/src/selection/insert.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/insert.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../creator */ "./node_modules/d3-selection/src/creator.js"),
                    i = t( /*! ../selector */ "./node_modules/d3-selection/src/selector.js");

                function o() {
                    return null
                }
                n.default = function(e, n) {
                    var t = "function" == typeof e ? e : Object(a.default)(e),
                        s = null == n ? o : "function" == typeof n ? n : Object(i.default)(n);
                    return this.select((function() {
                        return this.insertBefore(t.apply(this, arguments), s.apply(this, arguments) || null)
                    }))
                }
            },
        "./node_modules/d3-selection/src/selection/join.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/join.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n, t) {
                    var a = this.enter(),
                        i = this,
                        o = this.exit();
                    return a = "function" == typeof e ? e(a) : a.append(e + ""), null != n && (i = n(i)), null == t ? o.remove() : t(o), a && i ? a.merge(i).order() : i
                }
            },
        "./node_modules/d3-selection/src/selection/lower.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/lower.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    this.previousSibling && this.parentNode.insertBefore(this, this.parentNode.firstChild)
                }
                t.r(n), n.default = function() {
                    return this.each(a)
                }
            },
        "./node_modules/d3-selection/src/selection/merge.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/merge.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js");
                n.default = function(e) {
                    for (var n = this._groups, t = e._groups, i = n.length, o = t.length, s = Math.min(i, o), r = new Array(i), u = 0; u < s; ++u)
                        for (var l, c = n[u], d = t[u], g = c.length, h = r[u] = new Array(g), f = 0; f < g; ++f)(l = c[f] || d[f]) && (h[f] = l);
                    for (; u < i; ++u) r[u] = n[u];
                    return new a.Selection(r, this._parents)
                }
            },
        "./node_modules/d3-selection/src/selection/node.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/node.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    for (var e = this._groups, n = 0, t = e.length; n < t; ++n)
                        for (var a = e[n], i = 0, o = a.length; i < o; ++i) {
                            var s = a[i];
                            if (s) return s
                        }
                    return null
                }
            },
        "./node_modules/d3-selection/src/selection/nodes.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/nodes.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    var e = new Array(this.size()),
                        n = -1;
                    return this.each((function() {
                        e[++n] = this
                    })), e
                }
            },
        "./node_modules/d3-selection/src/selection/on.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/on.js ***!
              \*******************************************************/
            /*! exports provided: event, default, customEvent */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "event", (function() {
                    return i
                })), t.d(n, "customEvent", (function() {
                    return c
                }));
                var a = {},
                    i = null;
                "undefined" != typeof document && ("onmouseenter" in document.documentElement || (a = {
                    mouseenter: "mouseover",
                    mouseleave: "mouseout"
                }));

                function o(e, n, t) {
                    return e = s(e, n, t),
                        function(n) {
                            var t = n.relatedTarget;
                            t && (t === this || 8 & t.compareDocumentPosition(this)) || e.call(this, n)
                        }
                }

                function s(e, n, t) {
                    return function(a) {
                        var o = i;
                        i = a;
                        try {
                            e.call(this, this.__data__, n, t)
                        } finally {
                            i = o
                        }
                    }
                }

                function r(e) {
                    return e.trim().split(/^|\s+/).map((function(e) {
                        var n = "",
                            t = e.indexOf(".");
                        return t >= 0 && (n = e.slice(t + 1), e = e.slice(0, t)), {
                            type: e,
                            name: n
                        }
                    }))
                }

                function u(e) {
                    return function() {
                        var n = this.__on;
                        if (n) {
                            for (var t, a = 0, i = -1, o = n.length; a < o; ++a) t = n[a], e.type && t.type !== e.type || t.name !== e.name ? n[++i] = t : this.removeEventListener(t.type, t.listener, t.capture);
                            ++i ? n.length = i : delete this.__on
                        }
                    }
                }

                function l(e, n, t) {
                    var i = a.hasOwnProperty(e.type) ? o : s;
                    return function(a, o, s) {
                        var r, u = this.__on,
                            l = i(n, o, s);
                        if (u)
                            for (var c = 0, d = u.length; c < d; ++c)
                                if ((r = u[c]).type === e.type && r.name === e.name) return this.removeEventListener(r.type, r.listener, r.capture), this.addEventListener(r.type, r.listener = l, r.capture = t), void(r.value = n);
                        this.addEventListener(e.type, l, t), r = {
                            type: e.type,
                            name: e.name,
                            value: n,
                            listener: l,
                            capture: t
                        }, u ? u.push(r) : this.__on = [r]
                    }
                }

                function c(e, n, t, a) {
                    var o = i;
                    e.sourceEvent = i, i = e;
                    try {
                        return n.apply(t, a)
                    } finally {
                        i = o
                    }
                }
                n.default = function(e, n, t) {
                    var a, i, o = r(e + ""),
                        s = o.length;
                    if (!(arguments.length < 2)) {
                        for (c = n ? l : u, null == t && (t = !1), a = 0; a < s; ++a) this.each(c(o[a], n, t));
                        return this
                    }
                    var c = this.node().__on;
                    if (c)
                        for (var d, g = 0, h = c.length; g < h; ++g)
                            for (a = 0, d = c[g]; a < s; ++a)
                                if ((i = o[a]).type === d.type && i.name === d.name) return d.value
                }
            },
        "./node_modules/d3-selection/src/selection/order.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/order.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    for (var e = this._groups, n = -1, t = e.length; ++n < t;)
                        for (var a, i = e[n], o = i.length - 1, s = i[o]; --o >= 0;)(a = i[o]) && (s && 4 ^ a.compareDocumentPosition(s) && s.parentNode.insertBefore(a, s), s = a);
                    return this
                }
            },
        "./node_modules/d3-selection/src/selection/property.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/property.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return function() {
                        delete this[e]
                    }
                }

                function i(e, n) {
                    return function() {
                        this[e] = n
                    }
                }

                function o(e, n) {
                    return function() {
                        var t = n.apply(this, arguments);
                        null == t ? delete this[e] : this[e] = t
                    }
                }
                t.r(n), n.default = function(e, n) {
                    return arguments.length > 1 ? this.each((null == n ? a : "function" == typeof n ? o : i)(e, n)) : this.node()[e]
                }
            },
        "./node_modules/d3-selection/src/selection/raise.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/raise.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    this.nextSibling && this.parentNode.appendChild(this)
                }
                t.r(n), n.default = function() {
                    return this.each(a)
                }
            },
        "./node_modules/d3-selection/src/selection/remove.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/remove.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    var e = this.parentNode;
                    e && e.removeChild(this)
                }
                t.r(n), n.default = function() {
                    return this.each(a)
                }
            },
        "./node_modules/d3-selection/src/selection/select.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/select.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js"),
                    i = t( /*! ../selector */ "./node_modules/d3-selection/src/selector.js");
                n.default = function(e) {
                    "function" != typeof e && (e = Object(i.default)(e));
                    for (var n = this._groups, t = n.length, o = new Array(t), s = 0; s < t; ++s)
                        for (var r, u, l = n[s], c = l.length, d = o[s] = new Array(c), g = 0; g < c; ++g)(r = l[g]) && (u = e.call(r, r.__data__, g, l)) && ("__data__" in r && (u.__data__ = r.__data__), d[g] = u);
                    return new a.Selection(o, this._parents)
                }
            },
        "./node_modules/d3-selection/src/selection/selectAll.js":
            /*!**************************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/selectAll.js ***!
              \**************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js"),
                    i = t( /*! ../selectorAll */ "./node_modules/d3-selection/src/selectorAll.js");
                n.default = function(e) {
                    "function" != typeof e && (e = Object(i.default)(e));
                    for (var n = this._groups, t = n.length, o = [], s = [], r = 0; r < t; ++r)
                        for (var u, l = n[r], c = l.length, d = 0; d < c; ++d)(u = l[d]) && (o.push(e.call(u, u.__data__, d, l)), s.push(u));
                    return new a.Selection(o, s)
                }
            },
        "./node_modules/d3-selection/src/selection/size.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/size.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    var e = 0;
                    return this.each((function() {
                        ++e
                    })), e
                }
            },
        "./node_modules/d3-selection/src/selection/sort.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/sort.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index */ "./node_modules/d3-selection/src/selection/index.js");

                function i(e, n) {
                    return e < n ? -1 : e > n ? 1 : e >= n ? 0 : NaN
                }
                n.default = function(e) {
                    function n(n, t) {
                        return n && t ? e(n.__data__, t.__data__) : !n - !t
                    }
                    e || (e = i);
                    for (var t = this._groups, o = t.length, s = new Array(o), r = 0; r < o; ++r) {
                        for (var u, l = t[r], c = l.length, d = s[r] = new Array(c), g = 0; g < c; ++g)(u = l[g]) && (d[g] = u);
                        d.sort(n)
                    }
                    return new a.Selection(s, this._parents).order()
                }
            },
        "./node_modules/d3-selection/src/selection/sparse.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/sparse.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return new Array(e.length)
                }
            },
        "./node_modules/d3-selection/src/selection/style.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/style.js ***!
              \**********************************************************/
            /*! exports provided: default, styleValue */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "styleValue", (function() {
                    return r
                }));
                var a = t( /*! ../window */ "./node_modules/d3-selection/src/window.js");

                function i(e) {
                    return function() {
                        this.style.removeProperty(e)
                    }
                }

                function o(e, n, t) {
                    return function() {
                        this.style.setProperty(e, n, t)
                    }
                }

                function s(e, n, t) {
                    return function() {
                        var a = n.apply(this, arguments);
                        null == a ? this.style.removeProperty(e) : this.style.setProperty(e, a, t)
                    }
                }

                function r(e, n) {
                    return e.style.getPropertyValue(n) || Object(a.default)(e).getComputedStyle(e, null).getPropertyValue(n)
                }
                n.default = function(e, n, t) {
                    return arguments.length > 1 ? this.each((null == n ? i : "function" == typeof n ? s : o)(e, n, null == t ? "" : t)) : r(this.node(), e)
                }
            },
        "./node_modules/d3-selection/src/selection/text.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-selection/src/selection/text.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    this.textContent = ""
                }

                function i(e) {
                    return function() {
                        this.textContent = e
                    }
                }

                function o(e) {
                    return function() {
                        var n = e.apply(this, arguments);
                        this.textContent = null == n ? "" : n
                    }
                }
                t.r(n), n.default = function(e) {
                    return arguments.length ? this.each(null == e ? a : ("function" == typeof e ? o : i)(e)) : this.node().textContent
                }
            },
        "./node_modules/d3-selection/src/selector.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-selection/src/selector.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {}
                t.r(n), n.default = function(e) {
                    return null == e ? a : function() {
                        return this.querySelector(e)
                    }
                }
            },
        "./node_modules/d3-selection/src/selectorAll.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-selection/src/selectorAll.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    return []
                }
                t.r(n), n.default = function(e) {
                    return null == e ? a : function() {
                        return this.querySelectorAll(e)
                    }
                }
            },
        "./node_modules/d3-selection/src/sourceEvent.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-selection/src/sourceEvent.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./selection/on */ "./node_modules/d3-selection/src/selection/on.js");
                n.default = function() {
                    for (var e, n = a.event; e = n.sourceEvent;) n = e;
                    return n
                }
            },
        "./node_modules/d3-selection/src/touch.js":
            /*!************************************************!*\
              !*** ./node_modules/d3-selection/src/touch.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./sourceEvent */ "./node_modules/d3-selection/src/sourceEvent.js"),
                    i = t( /*! ./point */ "./node_modules/d3-selection/src/point.js");
                n.default = function(e, n, t) {
                    arguments.length < 3 && (t = n, n = Object(a.default)().changedTouches);
                    for (var o, s = 0, r = n ? n.length : 0; s < r; ++s)
                        if ((o = n[s]).identifier === t) return Object(i.default)(e, o);
                    return null
                }
            },
        "./node_modules/d3-selection/src/touches.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-selection/src/touches.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./sourceEvent */ "./node_modules/d3-selection/src/sourceEvent.js"),
                    i = t( /*! ./point */ "./node_modules/d3-selection/src/point.js");
                n.default = function(e, n) {
                    null == n && (n = Object(a.default)().touches);
                    for (var t = 0, o = n ? n.length : 0, s = new Array(o); t < o; ++t) s[t] = Object(i.default)(e, n[t]);
                    return s
                }
            },
        "./node_modules/d3-selection/src/window.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-selection/src/window.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return e.ownerDocument && e.ownerDocument.defaultView || e.document && e || e.defaultView
                }
            },
        "./node_modules/d3-shape/src/arc.js":
            /*!******************************************!*\
              !*** ./node_modules/d3-shape/src/arc.js ***!
              \******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-path */ "./node_modules/d3-path/src/index.js"),
                    i = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    o = t( /*! ./math.js */ "./node_modules/d3-shape/src/math.js");

                function s(e) {
                    return e.innerRadius
                }

                function r(e) {
                    return e.outerRadius
                }

                function u(e) {
                    return e.startAngle
                }

                function l(e) {
                    return e.endAngle
                }

                function c(e) {
                    return e && e.padAngle
                }

                function d(e, n, t, a, i, s, r, u) {
                    var l = t - e,
                        c = a - n,
                        d = r - i,
                        g = u - s,
                        h = g * l - d * c;
                    if (!(h * h < o.epsilon)) return [e + (h = (d * (n - s) - g * (e - i)) / h) * l, n + h * c]
                }

                function g(e, n, t, a, i, s, r) {
                    var u = e - t,
                        l = n - a,
                        c = (r ? s : -s) / Object(o.sqrt)(u * u + l * l),
                        d = c * l,
                        g = -c * u,
                        h = e + d,
                        f = n + g,
                        m = t + d,
                        p = a + g,
                        v = (h + m) / 2,
                        y = (f + p) / 2,
                        _ = m - h,
                        b = p - f,
                        j = _ * _ + b * b,
                        x = i - s,
                        w = h * p - m * f,
                        R = (b < 0 ? -1 : 1) * Object(o.sqrt)(Object(o.max)(0, x * x * j - w * w)),
                        k = (w * b - _ * R) / j,
                        A = (-w * _ - b * R) / j,
                        O = (w * b + _ * R) / j,
                        S = (-w * _ + b * R) / j,
                        C = k - v,
                        E = A - y,
                        B = O - v,
                        M = S - y;
                    return C * C + E * E > B * B + M * M && (k = O, A = S), {
                        cx: k,
                        cy: A,
                        x01: -d,
                        y01: -g,
                        x11: k * (i / x - 1),
                        y11: A * (i / x - 1)
                    }
                }
                n.default = function() {
                    var e = s,
                        n = r,
                        t = Object(i.default)(0),
                        h = null,
                        f = u,
                        m = l,
                        p = c,
                        v = null;

                    function y() {
                        var i, s, r = +e.apply(this, arguments),
                            u = +n.apply(this, arguments),
                            l = f.apply(this, arguments) - o.halfPi,
                            c = m.apply(this, arguments) - o.halfPi,
                            y = Object(o.abs)(c - l),
                            _ = c > l;
                        if (v || (v = i = Object(a.path)()), u < r && (s = u, u = r, r = s), u > o.epsilon)
                            if (y > o.tau - o.epsilon) v.moveTo(u * Object(o.cos)(l), u * Object(o.sin)(l)), v.arc(0, 0, u, l, c, !_), r > o.epsilon && (v.moveTo(r * Object(o.cos)(c), r * Object(o.sin)(c)), v.arc(0, 0, r, c, l, _));
                            else {
                                var b, j, x = l,
                                    w = c,
                                    R = l,
                                    k = c,
                                    A = y,
                                    O = y,
                                    S = p.apply(this, arguments) / 2,
                                    C = S > o.epsilon && (h ? +h.apply(this, arguments) : Object(o.sqrt)(r * r + u * u)),
                                    E = Object(o.min)(Object(o.abs)(u - r) / 2, +t.apply(this, arguments)),
                                    B = E,
                                    M = E;
                                if (C > o.epsilon) {
                                    var D = Object(o.asin)(C / r * Object(o.sin)(S)),
                                        T = Object(o.asin)(C / u * Object(o.sin)(S));
                                    (A -= 2 * D) > o.epsilon ? (R += D *= _ ? 1 : -1, k -= D) : (A = 0, R = k = (l + c) / 2), (O -= 2 * T) > o.epsilon ? (x += T *= _ ? 1 : -1, w -= T) : (O = 0, x = w = (l + c) / 2)
                                }
                                var N = u * Object(o.cos)(x),
                                    z = u * Object(o.sin)(x),
                                    F = r * Object(o.cos)(k),
                                    P = r * Object(o.sin)(k);
                                if (E > o.epsilon) {
                                    var L, I = u * Object(o.cos)(w),
                                        K = u * Object(o.sin)(w),
                                        G = r * Object(o.cos)(R),
                                        H = r * Object(o.sin)(R);
                                    if (y < o.pi && (L = d(N, z, G, H, I, K, F, P))) {
                                        var q = N - L[0],
                                            W = z - L[1],
                                            U = I - L[0],
                                            V = K - L[1],
                                            Z = 1 / Object(o.sin)(Object(o.acos)((q * U + W * V) / (Object(o.sqrt)(q * q + W * W) * Object(o.sqrt)(U * U + V * V))) / 2),
                                            Y = Object(o.sqrt)(L[0] * L[0] + L[1] * L[1]);
                                        B = Object(o.min)(E, (r - Y) / (Z - 1)), M = Object(o.min)(E, (u - Y) / (Z + 1))
                                    }
                                }
                                O > o.epsilon ? M > o.epsilon ? (b = g(G, H, N, z, u, M, _), j = g(I, K, F, P, u, M, _), v.moveTo(b.cx + b.x01, b.cy + b.y01), M < E ? v.arc(b.cx, b.cy, M, Object(o.atan2)(b.y01, b.x01), Object(o.atan2)(j.y01, j.x01), !_) : (v.arc(b.cx, b.cy, M, Object(o.atan2)(b.y01, b.x01), Object(o.atan2)(b.y11, b.x11), !_), v.arc(0, 0, u, Object(o.atan2)(b.cy + b.y11, b.cx + b.x11), Object(o.atan2)(j.cy + j.y11, j.cx + j.x11), !_), v.arc(j.cx, j.cy, M, Object(o.atan2)(j.y11, j.x11), Object(o.atan2)(j.y01, j.x01), !_))) : (v.moveTo(N, z), v.arc(0, 0, u, x, w, !_)) : v.moveTo(N, z), r > o.epsilon && A > o.epsilon ? B > o.epsilon ? (b = g(F, P, I, K, r, -B, _), j = g(N, z, G, H, r, -B, _), v.lineTo(b.cx + b.x01, b.cy + b.y01), B < E ? v.arc(b.cx, b.cy, B, Object(o.atan2)(b.y01, b.x01), Object(o.atan2)(j.y01, j.x01), !_) : (v.arc(b.cx, b.cy, B, Object(o.atan2)(b.y01, b.x01), Object(o.atan2)(b.y11, b.x11), !_), v.arc(0, 0, r, Object(o.atan2)(b.cy + b.y11, b.cx + b.x11), Object(o.atan2)(j.cy + j.y11, j.cx + j.x11), _), v.arc(j.cx, j.cy, B, Object(o.atan2)(j.y11, j.x11), Object(o.atan2)(j.y01, j.x01), !_))) : v.arc(0, 0, r, k, R, _) : v.lineTo(F, P)
                            }
                        else v.moveTo(0, 0);
                        if (v.closePath(), i) return v = null, i + "" || null
                    }
                    return y.centroid = function() {
                        var t = (+e.apply(this, arguments) + +n.apply(this, arguments)) / 2,
                            a = (+f.apply(this, arguments) + +m.apply(this, arguments)) / 2 - o.pi / 2;
                        return [Object(o.cos)(a) * t, Object(o.sin)(a) * t]
                    }, y.innerRadius = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(i.default)(+n), y) : e
                    }, y.outerRadius = function(e) {
                        return arguments.length ? (n = "function" == typeof e ? e : Object(i.default)(+e), y) : n
                    }, y.cornerRadius = function(e) {
                        return arguments.length ? (t = "function" == typeof e ? e : Object(i.default)(+e), y) : t
                    }, y.padRadius = function(e) {
                        return arguments.length ? (h = null == e ? null : "function" == typeof e ? e : Object(i.default)(+e), y) : h
                    }, y.startAngle = function(e) {
                        return arguments.length ? (f = "function" == typeof e ? e : Object(i.default)(+e), y) : f
                    }, y.endAngle = function(e) {
                        return arguments.length ? (m = "function" == typeof e ? e : Object(i.default)(+e), y) : m
                    }, y.padAngle = function(e) {
                        return arguments.length ? (p = "function" == typeof e ? e : Object(i.default)(+e), y) : p
                    }, y.context = function(e) {
                        return arguments.length ? (v = null == e ? null : e, y) : v
                    }, y
                }
            },
        "./node_modules/d3-shape/src/area.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-shape/src/area.js ***!
              \*******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-path */ "./node_modules/d3-path/src/index.js"),
                    i = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    o = t( /*! ./curve/linear.js */ "./node_modules/d3-shape/src/curve/linear.js"),
                    s = t( /*! ./line.js */ "./node_modules/d3-shape/src/line.js"),
                    r = t( /*! ./point.js */ "./node_modules/d3-shape/src/point.js");
                n.default = function() {
                    var e = r.x,
                        n = null,
                        t = Object(i.default)(0),
                        u = r.y,
                        l = Object(i.default)(!0),
                        c = null,
                        d = o.default,
                        g = null;

                    function h(i) {
                        var o, s, r, h, f, m = i.length,
                            p = !1,
                            v = new Array(m),
                            y = new Array(m);
                        for (null == c && (g = d(f = Object(a.path)())), o = 0; o <= m; ++o) {
                            if (!(o < m && l(h = i[o], o, i)) === p)
                                if (p = !p) s = o, g.areaStart(), g.lineStart();
                                else {
                                    for (g.lineEnd(), g.lineStart(), r = o - 1; r >= s; --r) g.point(v[r], y[r]);
                                    g.lineEnd(), g.areaEnd()
                                }
                            p && (v[o] = +e(h, o, i), y[o] = +t(h, o, i), g.point(n ? +n(h, o, i) : v[o], u ? +u(h, o, i) : y[o]))
                        }
                        if (f) return g = null, f + "" || null
                    }

                    function f() {
                        return Object(s.default)().defined(l).curve(d).context(c)
                    }
                    return h.x = function(t) {
                        return arguments.length ? (e = "function" == typeof t ? t : Object(i.default)(+t), n = null, h) : e
                    }, h.x0 = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(i.default)(+n), h) : e
                    }, h.x1 = function(e) {
                        return arguments.length ? (n = null == e ? null : "function" == typeof e ? e : Object(i.default)(+e), h) : n
                    }, h.y = function(e) {
                        return arguments.length ? (t = "function" == typeof e ? e : Object(i.default)(+e), u = null, h) : t
                    }, h.y0 = function(e) {
                        return arguments.length ? (t = "function" == typeof e ? e : Object(i.default)(+e), h) : t
                    }, h.y1 = function(e) {
                        return arguments.length ? (u = null == e ? null : "function" == typeof e ? e : Object(i.default)(+e), h) : u
                    }, h.lineX0 = h.lineY0 = function() {
                        return f().x(e).y(t)
                    }, h.lineY1 = function() {
                        return f().x(e).y(u)
                    }, h.lineX1 = function() {
                        return f().x(n).y(t)
                    }, h.defined = function(e) {
                        return arguments.length ? (l = "function" == typeof e ? e : Object(i.default)(!!e), h) : l
                    }, h.curve = function(e) {
                        return arguments.length ? (d = e, null != c && (g = d(c)), h) : d
                    }, h.context = function(e) {
                        return arguments.length ? (null == e ? c = g = null : g = d(c = e), h) : c
                    }, h
                }
            },
        "./node_modules/d3-shape/src/areaRadial.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/areaRadial.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./curve/radial.js */ "./node_modules/d3-shape/src/curve/radial.js"),
                    i = t( /*! ./area.js */ "./node_modules/d3-shape/src/area.js"),
                    o = t( /*! ./lineRadial.js */ "./node_modules/d3-shape/src/lineRadial.js");
                n.default = function() {
                    var e = Object(i.default)().curve(a.curveRadialLinear),
                        n = e.curve,
                        t = e.lineX0,
                        s = e.lineX1,
                        r = e.lineY0,
                        u = e.lineY1;
                    return e.angle = e.x, delete e.x, e.startAngle = e.x0, delete e.x0, e.endAngle = e.x1, delete e.x1, e.radius = e.y, delete e.y, e.innerRadius = e.y0, delete e.y0, e.outerRadius = e.y1, delete e.y1, e.lineStartAngle = function() {
                        return Object(o.lineRadial)(t())
                    }, delete e.lineX0, e.lineEndAngle = function() {
                        return Object(o.lineRadial)(s())
                    }, delete e.lineX1, e.lineInnerRadius = function() {
                        return Object(o.lineRadial)(r())
                    }, delete e.lineY0, e.lineOuterRadius = function() {
                        return Object(o.lineRadial)(u())
                    }, delete e.lineY1, e.curve = function(e) {
                        return arguments.length ? n(Object(a.default)(e)) : n()._curve
                    }, e
                }
            },
        "./node_modules/d3-shape/src/array.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-shape/src/array.js ***!
              \********************************************/
            /*! exports provided: slice */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "slice", (function() {
                    return a
                }));
                var a = Array.prototype.slice
            },
        "./node_modules/d3-shape/src/constant.js":
            /*!***********************************************!*\
              !*** ./node_modules/d3-shape/src/constant.js ***!
              \***********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return e
                    }
                }
            },
        "./node_modules/d3-shape/src/curve/basis.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/basis.js ***!
              \**************************************************/
            /*! exports provided: point, Basis, default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t) {
                    e._context.bezierCurveTo((2 * e._x0 + e._x1) / 3, (2 * e._y0 + e._y1) / 3, (e._x0 + 2 * e._x1) / 3, (e._y0 + 2 * e._y1) / 3, (e._x0 + 4 * e._x1 + n) / 6, (e._y0 + 4 * e._y1 + t) / 6)
                }

                function i(e) {
                    this._context = e
                }
                t.r(n), t.d(n, "point", (function() {
                    return a
                })), t.d(n, "Basis", (function() {
                    return i
                })), i.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._y0 = this._y1 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 3:
                                a(this, this._x1, this._y1);
                            case 2:
                                this._context.lineTo(this._x1, this._y1)
                        }(this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                break;
                            case 1:
                                this._point = 2;
                                break;
                            case 2:
                                this._point = 3, this._context.lineTo((5 * this._x0 + this._x1) / 6, (5 * this._y0 + this._y1) / 6);
                            default:
                                a(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = e, this._y0 = this._y1, this._y1 = n
                    }
                }, n.default = function(e) {
                    return new i(e)
                }
            },
        "./node_modules/d3-shape/src/curve/basisClosed.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/basisClosed.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../noop.js */ "./node_modules/d3-shape/src/noop.js"),
                    i = t( /*! ./basis.js */ "./node_modules/d3-shape/src/curve/basis.js");

                function o(e) {
                    this._context = e
                }
                o.prototype = {
                    areaStart: a.default,
                    areaEnd: a.default,
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._x3 = this._x4 = this._y0 = this._y1 = this._y2 = this._y3 = this._y4 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 1:
                                this._context.moveTo(this._x2, this._y2), this._context.closePath();
                                break;
                            case 2:
                                this._context.moveTo((this._x2 + 2 * this._x3) / 3, (this._y2 + 2 * this._y3) / 3), this._context.lineTo((this._x3 + 2 * this._x2) / 3, (this._y3 + 2 * this._y2) / 3), this._context.closePath();
                                break;
                            case 3:
                                this.point(this._x2, this._y2), this.point(this._x3, this._y3), this.point(this._x4, this._y4)
                        }
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._x2 = e, this._y2 = n;
                                break;
                            case 1:
                                this._point = 2, this._x3 = e, this._y3 = n;
                                break;
                            case 2:
                                this._point = 3, this._x4 = e, this._y4 = n, this._context.moveTo((this._x0 + 4 * this._x1 + e) / 6, (this._y0 + 4 * this._y1 + n) / 6);
                                break;
                            default:
                                Object(i.point)(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = e, this._y0 = this._y1, this._y1 = n
                    }
                }, n.default = function(e) {
                    return new o(e)
                }
            },
        "./node_modules/d3-shape/src/curve/basisOpen.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/basisOpen.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./basis.js */ "./node_modules/d3-shape/src/curve/basis.js");

                function i(e) {
                    this._context = e
                }
                i.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._y0 = this._y1 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        (this._line || 0 !== this._line && 3 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1;
                                break;
                            case 1:
                                this._point = 2;
                                break;
                            case 2:
                                this._point = 3;
                                var t = (this._x0 + 4 * this._x1 + e) / 6,
                                    i = (this._y0 + 4 * this._y1 + n) / 6;
                                this._line ? this._context.lineTo(t, i) : this._context.moveTo(t, i);
                                break;
                            case 3:
                                this._point = 4;
                            default:
                                Object(a.point)(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = e, this._y0 = this._y1, this._y1 = n
                    }
                }, n.default = function(e) {
                    return new i(e)
                }
            },
        "./node_modules/d3-shape/src/curve/bundle.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/bundle.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./basis.js */ "./node_modules/d3-shape/src/curve/basis.js");

                function i(e, n) {
                    this._basis = new a.Basis(e), this._beta = n
                }
                i.prototype = {
                    lineStart: function() {
                        this._x = [], this._y = [], this._basis.lineStart()
                    },
                    lineEnd: function() {
                        var e = this._x,
                            n = this._y,
                            t = e.length - 1;
                        if (t > 0)
                            for (var a, i = e[0], o = n[0], s = e[t] - i, r = n[t] - o, u = -1; ++u <= t;) a = u / t, this._basis.point(this._beta * e[u] + (1 - this._beta) * (i + a * s), this._beta * n[u] + (1 - this._beta) * (o + a * r));
                        this._x = this._y = null, this._basis.lineEnd()
                    },
                    point: function(e, n) {
                        this._x.push(+e), this._y.push(+n)
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return 1 === n ? new a.Basis(e) : new i(e, n)
                    }
                    return t.beta = function(n) {
                        return e(+n)
                    }, t
                }(.85)
            },
        "./node_modules/d3-shape/src/curve/cardinal.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/cardinal.js ***!
              \*****************************************************/
            /*! exports provided: point, Cardinal, default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t) {
                    e._context.bezierCurveTo(e._x1 + e._k * (e._x2 - e._x0), e._y1 + e._k * (e._y2 - e._y0), e._x2 + e._k * (e._x1 - n), e._y2 + e._k * (e._y1 - t), e._x2, e._y2)
                }

                function i(e, n) {
                    this._context = e, this._k = (1 - n) / 6
                }
                t.r(n), t.d(n, "point", (function() {
                    return a
                })), t.d(n, "Cardinal", (function() {
                    return i
                })), i.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._y0 = this._y1 = this._y2 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 2:
                                this._context.lineTo(this._x2, this._y2);
                                break;
                            case 3:
                                a(this, this._x1, this._y1)
                        }(this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                break;
                            case 1:
                                this._point = 2, this._x1 = e, this._y1 = n;
                                break;
                            case 2:
                                this._point = 3;
                            default:
                                a(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return new i(e, n)
                    }
                    return t.tension = function(n) {
                        return e(+n)
                    }, t
                }(0)
            },
        "./node_modules/d3-shape/src/curve/cardinalClosed.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/cardinalClosed.js ***!
              \***********************************************************/
            /*! exports provided: CardinalClosed, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "CardinalClosed", (function() {
                    return o
                }));
                var a = t( /*! ../noop.js */ "./node_modules/d3-shape/src/noop.js"),
                    i = t( /*! ./cardinal.js */ "./node_modules/d3-shape/src/curve/cardinal.js");

                function o(e, n) {
                    this._context = e, this._k = (1 - n) / 6
                }
                o.prototype = {
                    areaStart: a.default,
                    areaEnd: a.default,
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._x3 = this._x4 = this._x5 = this._y0 = this._y1 = this._y2 = this._y3 = this._y4 = this._y5 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 1:
                                this._context.moveTo(this._x3, this._y3), this._context.closePath();
                                break;
                            case 2:
                                this._context.lineTo(this._x3, this._y3), this._context.closePath();
                                break;
                            case 3:
                                this.point(this._x3, this._y3), this.point(this._x4, this._y4), this.point(this._x5, this._y5)
                        }
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._x3 = e, this._y3 = n;
                                break;
                            case 1:
                                this._point = 2, this._context.moveTo(this._x4 = e, this._y4 = n);
                                break;
                            case 2:
                                this._point = 3, this._x5 = e, this._y5 = n;
                                break;
                            default:
                                Object(i.point)(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return new o(e, n)
                    }
                    return t.tension = function(n) {
                        return e(+n)
                    }, t
                }(0)
            },
        "./node_modules/d3-shape/src/curve/cardinalOpen.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/cardinalOpen.js ***!
              \*********************************************************/
            /*! exports provided: CardinalOpen, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "CardinalOpen", (function() {
                    return i
                }));
                var a = t( /*! ./cardinal.js */ "./node_modules/d3-shape/src/curve/cardinal.js");

                function i(e, n) {
                    this._context = e, this._k = (1 - n) / 6
                }
                i.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._y0 = this._y1 = this._y2 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        (this._line || 0 !== this._line && 3 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1;
                                break;
                            case 1:
                                this._point = 2;
                                break;
                            case 2:
                                this._point = 3, this._line ? this._context.lineTo(this._x2, this._y2) : this._context.moveTo(this._x2, this._y2);
                                break;
                            case 3:
                                this._point = 4;
                            default:
                                Object(a.point)(this, e, n)
                        }
                        this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return new i(e, n)
                    }
                    return t.tension = function(n) {
                        return e(+n)
                    }, t
                }(0)
            },
        "./node_modules/d3-shape/src/curve/catmullRom.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/catmullRom.js ***!
              \*******************************************************/
            /*! exports provided: point, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "point", (function() {
                    return o
                }));
                var a = t( /*! ../math.js */ "./node_modules/d3-shape/src/math.js"),
                    i = t( /*! ./cardinal.js */ "./node_modules/d3-shape/src/curve/cardinal.js");

                function o(e, n, t) {
                    var i = e._x1,
                        o = e._y1,
                        s = e._x2,
                        r = e._y2;
                    if (e._l01_a > a.epsilon) {
                        var u = 2 * e._l01_2a + 3 * e._l01_a * e._l12_a + e._l12_2a,
                            l = 3 * e._l01_a * (e._l01_a + e._l12_a);
                        i = (i * u - e._x0 * e._l12_2a + e._x2 * e._l01_2a) / l, o = (o * u - e._y0 * e._l12_2a + e._y2 * e._l01_2a) / l
                    }
                    if (e._l23_a > a.epsilon) {
                        var c = 2 * e._l23_2a + 3 * e._l23_a * e._l12_a + e._l12_2a,
                            d = 3 * e._l23_a * (e._l23_a + e._l12_a);
                        s = (s * c + e._x1 * e._l23_2a - n * e._l12_2a) / d, r = (r * c + e._y1 * e._l23_2a - t * e._l12_2a) / d
                    }
                    e._context.bezierCurveTo(i, o, s, r, e._x2, e._y2)
                }

                function s(e, n) {
                    this._context = e, this._alpha = n
                }
                s.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._y0 = this._y1 = this._y2 = NaN, this._l01_a = this._l12_a = this._l23_a = this._l01_2a = this._l12_2a = this._l23_2a = this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 2:
                                this._context.lineTo(this._x2, this._y2);
                                break;
                            case 3:
                                this.point(this._x2, this._y2)
                        }(this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        if (e = +e, n = +n, this._point) {
                            var t = this._x2 - e,
                                a = this._y2 - n;
                            this._l23_a = Math.sqrt(this._l23_2a = Math.pow(t * t + a * a, this._alpha))
                        }
                        switch (this._point) {
                            case 0:
                                this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                break;
                            case 1:
                                this._point = 2;
                                break;
                            case 2:
                                this._point = 3;
                            default:
                                o(this, e, n)
                        }
                        this._l01_a = this._l12_a, this._l12_a = this._l23_a, this._l01_2a = this._l12_2a, this._l12_2a = this._l23_2a, this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return n ? new s(e, n) : new i.Cardinal(e, 0)
                    }
                    return t.alpha = function(n) {
                        return e(+n)
                    }, t
                }(.5)
            },
        "./node_modules/d3-shape/src/curve/catmullRomClosed.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/catmullRomClosed.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./cardinalClosed.js */ "./node_modules/d3-shape/src/curve/cardinalClosed.js"),
                    i = t( /*! ../noop.js */ "./node_modules/d3-shape/src/noop.js"),
                    o = t( /*! ./catmullRom.js */ "./node_modules/d3-shape/src/curve/catmullRom.js");

                function s(e, n) {
                    this._context = e, this._alpha = n
                }
                s.prototype = {
                    areaStart: i.default,
                    areaEnd: i.default,
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._x3 = this._x4 = this._x5 = this._y0 = this._y1 = this._y2 = this._y3 = this._y4 = this._y5 = NaN, this._l01_a = this._l12_a = this._l23_a = this._l01_2a = this._l12_2a = this._l23_2a = this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 1:
                                this._context.moveTo(this._x3, this._y3), this._context.closePath();
                                break;
                            case 2:
                                this._context.lineTo(this._x3, this._y3), this._context.closePath();
                                break;
                            case 3:
                                this.point(this._x3, this._y3), this.point(this._x4, this._y4), this.point(this._x5, this._y5)
                        }
                    },
                    point: function(e, n) {
                        if (e = +e, n = +n, this._point) {
                            var t = this._x2 - e,
                                a = this._y2 - n;
                            this._l23_a = Math.sqrt(this._l23_2a = Math.pow(t * t + a * a, this._alpha))
                        }
                        switch (this._point) {
                            case 0:
                                this._point = 1, this._x3 = e, this._y3 = n;
                                break;
                            case 1:
                                this._point = 2, this._context.moveTo(this._x4 = e, this._y4 = n);
                                break;
                            case 2:
                                this._point = 3, this._x5 = e, this._y5 = n;
                                break;
                            default:
                                Object(o.point)(this, e, n)
                        }
                        this._l01_a = this._l12_a, this._l12_a = this._l23_a, this._l01_2a = this._l12_2a, this._l12_2a = this._l23_2a, this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return n ? new s(e, n) : new a.CardinalClosed(e, 0)
                    }
                    return t.alpha = function(n) {
                        return e(+n)
                    }, t
                }(.5)
            },
        "./node_modules/d3-shape/src/curve/catmullRomOpen.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/catmullRomOpen.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./cardinalOpen.js */ "./node_modules/d3-shape/src/curve/cardinalOpen.js"),
                    i = t( /*! ./catmullRom.js */ "./node_modules/d3-shape/src/curve/catmullRom.js");

                function o(e, n) {
                    this._context = e, this._alpha = n
                }
                o.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._x2 = this._y0 = this._y1 = this._y2 = NaN, this._l01_a = this._l12_a = this._l23_a = this._l01_2a = this._l12_2a = this._l23_2a = this._point = 0
                    },
                    lineEnd: function() {
                        (this._line || 0 !== this._line && 3 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        if (e = +e, n = +n, this._point) {
                            var t = this._x2 - e,
                                a = this._y2 - n;
                            this._l23_a = Math.sqrt(this._l23_2a = Math.pow(t * t + a * a, this._alpha))
                        }
                        switch (this._point) {
                            case 0:
                                this._point = 1;
                                break;
                            case 1:
                                this._point = 2;
                                break;
                            case 2:
                                this._point = 3, this._line ? this._context.lineTo(this._x2, this._y2) : this._context.moveTo(this._x2, this._y2);
                                break;
                            case 3:
                                this._point = 4;
                            default:
                                Object(i.point)(this, e, n)
                        }
                        this._l01_a = this._l12_a, this._l12_a = this._l23_a, this._l01_2a = this._l12_2a, this._l12_2a = this._l23_2a, this._x0 = this._x1, this._x1 = this._x2, this._x2 = e, this._y0 = this._y1, this._y1 = this._y2, this._y2 = n
                    }
                }, n.default = function e(n) {
                    function t(e) {
                        return n ? new o(e, n) : new a.CardinalOpen(e, 0)
                    }
                    return t.alpha = function(n) {
                        return e(+n)
                    }, t
                }(.5)
            },
        "./node_modules/d3-shape/src/curve/linear.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/linear.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    this._context = e
                }
                t.r(n), a.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._point = 0
                    },
                    lineEnd: function() {
                        (this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                break;
                            case 1:
                                this._point = 2;
                            default:
                                this._context.lineTo(e, n)
                        }
                    }
                }, n.default = function(e) {
                    return new a(e)
                }
            },
        "./node_modules/d3-shape/src/curve/linearClosed.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/linearClosed.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../noop.js */ "./node_modules/d3-shape/src/noop.js");

                function i(e) {
                    this._context = e
                }
                i.prototype = {
                    areaStart: a.default,
                    areaEnd: a.default,
                    lineStart: function() {
                        this._point = 0
                    },
                    lineEnd: function() {
                        this._point && this._context.closePath()
                    },
                    point: function(e, n) {
                        e = +e, n = +n, this._point ? this._context.lineTo(e, n) : (this._point = 1, this._context.moveTo(e, n))
                    }
                }, n.default = function(e) {
                    return new i(e)
                }
            },
        "./node_modules/d3-shape/src/curve/monotone.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/monotone.js ***!
              \*****************************************************/
            /*! exports provided: monotoneX, monotoneY */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return e < 0 ? -1 : 1
                }

                function i(e, n, t) {
                    var i = e._x1 - e._x0,
                        o = n - e._x1,
                        s = (e._y1 - e._y0) / (i || o < 0 && -0),
                        r = (t - e._y1) / (o || i < 0 && -0),
                        u = (s * o + r * i) / (i + o);
                    return (a(s) + a(r)) * Math.min(Math.abs(s), Math.abs(r), .5 * Math.abs(u)) || 0
                }

                function o(e, n) {
                    var t = e._x1 - e._x0;
                    return t ? (3 * (e._y1 - e._y0) / t - n) / 2 : n
                }

                function s(e, n, t) {
                    var a = e._x0,
                        i = e._y0,
                        o = e._x1,
                        s = e._y1,
                        r = (o - a) / 3;
                    e._context.bezierCurveTo(a + r, i + r * n, o - r, s - r * t, o, s)
                }

                function r(e) {
                    this._context = e
                }

                function u(e) {
                    this._context = new l(e)
                }

                function l(e) {
                    this._context = e
                }

                function c(e) {
                    return new r(e)
                }

                function d(e) {
                    return new u(e)
                }
                t.r(n), t.d(n, "monotoneX", (function() {
                    return c
                })), t.d(n, "monotoneY", (function() {
                    return d
                })), r.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x0 = this._x1 = this._y0 = this._y1 = this._t0 = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        switch (this._point) {
                            case 2:
                                this._context.lineTo(this._x1, this._y1);
                                break;
                            case 3:
                                s(this, this._t0, o(this, this._t0))
                        }(this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line = 1 - this._line
                    },
                    point: function(e, n) {
                        var t = NaN;
                        if (n = +n, (e = +e) !== this._x1 || n !== this._y1) {
                            switch (this._point) {
                                case 0:
                                    this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                    break;
                                case 1:
                                    this._point = 2;
                                    break;
                                case 2:
                                    this._point = 3, s(this, o(this, t = i(this, e, n)), t);
                                    break;
                                default:
                                    s(this, this._t0, t = i(this, e, n))
                            }
                            this._x0 = this._x1, this._x1 = e, this._y0 = this._y1, this._y1 = n, this._t0 = t
                        }
                    }
                }, (u.prototype = Object.create(r.prototype)).point = function(e, n) {
                    r.prototype.point.call(this, n, e)
                }, l.prototype = {
                    moveTo: function(e, n) {
                        this._context.moveTo(n, e)
                    },
                    closePath: function() {
                        this._context.closePath()
                    },
                    lineTo: function(e, n) {
                        this._context.lineTo(n, e)
                    },
                    bezierCurveTo: function(e, n, t, a, i, o) {
                        this._context.bezierCurveTo(n, e, a, t, o, i)
                    }
                }
            },
        "./node_modules/d3-shape/src/curve/natural.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/natural.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    this._context = e
                }

                function i(e) {
                    var n, t, a = e.length - 1,
                        i = new Array(a),
                        o = new Array(a),
                        s = new Array(a);
                    for (i[0] = 0, o[0] = 2, s[0] = e[0] + 2 * e[1], n = 1; n < a - 1; ++n) i[n] = 1, o[n] = 4, s[n] = 4 * e[n] + 2 * e[n + 1];
                    for (i[a - 1] = 2, o[a - 1] = 7, s[a - 1] = 8 * e[a - 1] + e[a], n = 1; n < a; ++n) t = i[n] / o[n - 1], o[n] -= t, s[n] -= t * s[n - 1];
                    for (i[a - 1] = s[a - 1] / o[a - 1], n = a - 2; n >= 0; --n) i[n] = (s[n] - i[n + 1]) / o[n];
                    for (o[a - 1] = (e[a] + i[a - 1]) / 2, n = 0; n < a - 1; ++n) o[n] = 2 * e[n + 1] - i[n + 1];
                    return [i, o]
                }
                t.r(n), a.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x = [], this._y = []
                    },
                    lineEnd: function() {
                        var e = this._x,
                            n = this._y,
                            t = e.length;
                        if (t)
                            if (this._line ? this._context.lineTo(e[0], n[0]) : this._context.moveTo(e[0], n[0]), 2 === t) this._context.lineTo(e[1], n[1]);
                            else
                                for (var a = i(e), o = i(n), s = 0, r = 1; r < t; ++s, ++r) this._context.bezierCurveTo(a[0][s], o[0][s], a[1][s], o[1][s], e[r], n[r]);
                        (this._line || 0 !== this._line && 1 === t) && this._context.closePath(), this._line = 1 - this._line, this._x = this._y = null
                    },
                    point: function(e, n) {
                        this._x.push(+e), this._y.push(+n)
                    }
                }, n.default = function(e) {
                    return new a(e)
                }
            },
        "./node_modules/d3-shape/src/curve/radial.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/radial.js ***!
              \***************************************************/
            /*! exports provided: curveRadialLinear, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "curveRadialLinear", (function() {
                    return a
                })), t.d(n, "default", (function() {
                    return o
                }));
                var a = o(t( /*! ./linear.js */ "./node_modules/d3-shape/src/curve/linear.js").default);

                function i(e) {
                    this._curve = e
                }

                function o(e) {
                    function n(n) {
                        return new i(e(n))
                    }
                    return n._curve = e, n
                }
                i.prototype = {
                    areaStart: function() {
                        this._curve.areaStart()
                    },
                    areaEnd: function() {
                        this._curve.areaEnd()
                    },
                    lineStart: function() {
                        this._curve.lineStart()
                    },
                    lineEnd: function() {
                        this._curve.lineEnd()
                    },
                    point: function(e, n) {
                        this._curve.point(n * Math.sin(e), n * -Math.cos(e))
                    }
                }
            },
        "./node_modules/d3-shape/src/curve/step.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/curve/step.js ***!
              \*************************************************/
            /*! exports provided: default, stepBefore, stepAfter */
            function(e, n, t) {
                "use strict";

                function a(e, n) {
                    this._context = e, this._t = n
                }

                function i(e) {
                    return new a(e, 0)
                }

                function o(e) {
                    return new a(e, 1)
                }
                t.r(n), t.d(n, "stepBefore", (function() {
                    return i
                })), t.d(n, "stepAfter", (function() {
                    return o
                })), a.prototype = {
                    areaStart: function() {
                        this._line = 0
                    },
                    areaEnd: function() {
                        this._line = NaN
                    },
                    lineStart: function() {
                        this._x = this._y = NaN, this._point = 0
                    },
                    lineEnd: function() {
                        0 < this._t && this._t < 1 && 2 === this._point && this._context.lineTo(this._x, this._y), (this._line || 0 !== this._line && 1 === this._point) && this._context.closePath(), this._line >= 0 && (this._t = 1 - this._t, this._line = 1 - this._line)
                    },
                    point: function(e, n) {
                        switch (e = +e, n = +n, this._point) {
                            case 0:
                                this._point = 1, this._line ? this._context.lineTo(e, n) : this._context.moveTo(e, n);
                                break;
                            case 1:
                                this._point = 2;
                            default:
                                if (this._t <= 0) this._context.lineTo(this._x, n), this._context.lineTo(e, n);
                                else {
                                    var t = this._x * (1 - this._t) + e * this._t;
                                    this._context.lineTo(t, this._y), this._context.lineTo(t, n)
                                }
                        }
                        this._x = e, this._y = n
                    }
                }, n.default = function(e) {
                    return new a(e, .5)
                }
            },
        "./node_modules/d3-shape/src/descending.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/descending.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    return n < e ? -1 : n > e ? 1 : n >= e ? 0 : NaN
                }
            },
        "./node_modules/d3-shape/src/identity.js":
            /*!***********************************************!*\
              !*** ./node_modules/d3-shape/src/identity.js ***!
              \***********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return e
                }
            },
        "./node_modules/d3-shape/src/index.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-shape/src/index.js ***!
              \********************************************/
            /*! exports provided: arc, area, line, pie, areaRadial, radialArea, lineRadial, radialLine, pointRadial, linkHorizontal, linkVertical, linkRadial, symbol, symbols, symbolCircle, symbolCross, symbolDiamond, symbolSquare, symbolStar, symbolTriangle, symbolWye, curveBasisClosed, curveBasisOpen, curveBasis, curveBundle, curveCardinalClosed, curveCardinalOpen, curveCardinal, curveCatmullRomClosed, curveCatmullRomOpen, curveCatmullRom, curveLinearClosed, curveLinear, curveMonotoneX, curveMonotoneY, curveNatural, curveStep, curveStepAfter, curveStepBefore, stack, stackOffsetExpand, stackOffsetDiverging, stackOffsetNone, stackOffsetSilhouette, stackOffsetWiggle, stackOrderAppearance, stackOrderAscending, stackOrderDescending, stackOrderInsideOut, stackOrderNone, stackOrderReverse */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./arc.js */ "./node_modules/d3-shape/src/arc.js");
                t.d(n, "arc", (function() {
                    return a.default
                }));
                var i = t( /*! ./area.js */ "./node_modules/d3-shape/src/area.js");
                t.d(n, "area", (function() {
                    return i.default
                }));
                var o = t( /*! ./line.js */ "./node_modules/d3-shape/src/line.js");
                t.d(n, "line", (function() {
                    return o.default
                }));
                var s = t( /*! ./pie.js */ "./node_modules/d3-shape/src/pie.js");
                t.d(n, "pie", (function() {
                    return s.default
                }));
                var r = t( /*! ./areaRadial.js */ "./node_modules/d3-shape/src/areaRadial.js");
                t.d(n, "areaRadial", (function() {
                    return r.default
                })), t.d(n, "radialArea", (function() {
                    return r.default
                }));
                var u = t( /*! ./lineRadial.js */ "./node_modules/d3-shape/src/lineRadial.js");
                t.d(n, "lineRadial", (function() {
                    return u.default
                })), t.d(n, "radialLine", (function() {
                    return u.default
                }));
                var l = t( /*! ./pointRadial.js */ "./node_modules/d3-shape/src/pointRadial.js");
                t.d(n, "pointRadial", (function() {
                    return l.default
                }));
                var c = t( /*! ./link/index.js */ "./node_modules/d3-shape/src/link/index.js");
                t.d(n, "linkHorizontal", (function() {
                    return c.linkHorizontal
                })), t.d(n, "linkVertical", (function() {
                    return c.linkVertical
                })), t.d(n, "linkRadial", (function() {
                    return c.linkRadial
                }));
                var d = t( /*! ./symbol.js */ "./node_modules/d3-shape/src/symbol.js");
                t.d(n, "symbol", (function() {
                    return d.default
                })), t.d(n, "symbols", (function() {
                    return d.symbols
                }));
                var g = t( /*! ./symbol/circle.js */ "./node_modules/d3-shape/src/symbol/circle.js");
                t.d(n, "symbolCircle", (function() {
                    return g.default
                }));
                var h = t( /*! ./symbol/cross.js */ "./node_modules/d3-shape/src/symbol/cross.js");
                t.d(n, "symbolCross", (function() {
                    return h.default
                }));
                var f = t( /*! ./symbol/diamond.js */ "./node_modules/d3-shape/src/symbol/diamond.js");
                t.d(n, "symbolDiamond", (function() {
                    return f.default
                }));
                var m = t( /*! ./symbol/square.js */ "./node_modules/d3-shape/src/symbol/square.js");
                t.d(n, "symbolSquare", (function() {
                    return m.default
                }));
                var p = t( /*! ./symbol/star.js */ "./node_modules/d3-shape/src/symbol/star.js");
                t.d(n, "symbolStar", (function() {
                    return p.default
                }));
                var v = t( /*! ./symbol/triangle.js */ "./node_modules/d3-shape/src/symbol/triangle.js");
                t.d(n, "symbolTriangle", (function() {
                    return v.default
                }));
                var y = t( /*! ./symbol/wye.js */ "./node_modules/d3-shape/src/symbol/wye.js");
                t.d(n, "symbolWye", (function() {
                    return y.default
                }));
                var _ = t( /*! ./curve/basisClosed.js */ "./node_modules/d3-shape/src/curve/basisClosed.js");
                t.d(n, "curveBasisClosed", (function() {
                    return _.default
                }));
                var b = t( /*! ./curve/basisOpen.js */ "./node_modules/d3-shape/src/curve/basisOpen.js");
                t.d(n, "curveBasisOpen", (function() {
                    return b.default
                }));
                var j = t( /*! ./curve/basis.js */ "./node_modules/d3-shape/src/curve/basis.js");
                t.d(n, "curveBasis", (function() {
                    return j.default
                }));
                var x = t( /*! ./curve/bundle.js */ "./node_modules/d3-shape/src/curve/bundle.js");
                t.d(n, "curveBundle", (function() {
                    return x.default
                }));
                var w = t( /*! ./curve/cardinalClosed.js */ "./node_modules/d3-shape/src/curve/cardinalClosed.js");
                t.d(n, "curveCardinalClosed", (function() {
                    return w.default
                }));
                var R = t( /*! ./curve/cardinalOpen.js */ "./node_modules/d3-shape/src/curve/cardinalOpen.js");
                t.d(n, "curveCardinalOpen", (function() {
                    return R.default
                }));
                var k = t( /*! ./curve/cardinal.js */ "./node_modules/d3-shape/src/curve/cardinal.js");
                t.d(n, "curveCardinal", (function() {
                    return k.default
                }));
                var A = t( /*! ./curve/catmullRomClosed.js */ "./node_modules/d3-shape/src/curve/catmullRomClosed.js");
                t.d(n, "curveCatmullRomClosed", (function() {
                    return A.default
                }));
                var O = t( /*! ./curve/catmullRomOpen.js */ "./node_modules/d3-shape/src/curve/catmullRomOpen.js");
                t.d(n, "curveCatmullRomOpen", (function() {
                    return O.default
                }));
                var S = t( /*! ./curve/catmullRom.js */ "./node_modules/d3-shape/src/curve/catmullRom.js");
                t.d(n, "curveCatmullRom", (function() {
                    return S.default
                }));
                var C = t( /*! ./curve/linearClosed.js */ "./node_modules/d3-shape/src/curve/linearClosed.js");
                t.d(n, "curveLinearClosed", (function() {
                    return C.default
                }));
                var E = t( /*! ./curve/linear.js */ "./node_modules/d3-shape/src/curve/linear.js");
                t.d(n, "curveLinear", (function() {
                    return E.default
                }));
                var B = t( /*! ./curve/monotone.js */ "./node_modules/d3-shape/src/curve/monotone.js");
                t.d(n, "curveMonotoneX", (function() {
                    return B.monotoneX
                })), t.d(n, "curveMonotoneY", (function() {
                    return B.monotoneY
                }));
                var M = t( /*! ./curve/natural.js */ "./node_modules/d3-shape/src/curve/natural.js");
                t.d(n, "curveNatural", (function() {
                    return M.default
                }));
                var D = t( /*! ./curve/step.js */ "./node_modules/d3-shape/src/curve/step.js");
                t.d(n, "curveStep", (function() {
                    return D.default
                })), t.d(n, "curveStepAfter", (function() {
                    return D.stepAfter
                })), t.d(n, "curveStepBefore", (function() {
                    return D.stepBefore
                }));
                var T = t( /*! ./stack.js */ "./node_modules/d3-shape/src/stack.js");
                t.d(n, "stack", (function() {
                    return T.default
                }));
                var N = t( /*! ./offset/expand.js */ "./node_modules/d3-shape/src/offset/expand.js");
                t.d(n, "stackOffsetExpand", (function() {
                    return N.default
                }));
                var z = t( /*! ./offset/diverging.js */ "./node_modules/d3-shape/src/offset/diverging.js");
                t.d(n, "stackOffsetDiverging", (function() {
                    return z.default
                }));
                var F = t( /*! ./offset/none.js */ "./node_modules/d3-shape/src/offset/none.js");
                t.d(n, "stackOffsetNone", (function() {
                    return F.default
                }));
                var P = t( /*! ./offset/silhouette.js */ "./node_modules/d3-shape/src/offset/silhouette.js");
                t.d(n, "stackOffsetSilhouette", (function() {
                    return P.default
                }));
                var L = t( /*! ./offset/wiggle.js */ "./node_modules/d3-shape/src/offset/wiggle.js");
                t.d(n, "stackOffsetWiggle", (function() {
                    return L.default
                }));
                var I = t( /*! ./order/appearance.js */ "./node_modules/d3-shape/src/order/appearance.js");
                t.d(n, "stackOrderAppearance", (function() {
                    return I.default
                }));
                var K = t( /*! ./order/ascending.js */ "./node_modules/d3-shape/src/order/ascending.js");
                t.d(n, "stackOrderAscending", (function() {
                    return K.default
                }));
                var G = t( /*! ./order/descending.js */ "./node_modules/d3-shape/src/order/descending.js");
                t.d(n, "stackOrderDescending", (function() {
                    return G.default
                }));
                var H = t( /*! ./order/insideOut.js */ "./node_modules/d3-shape/src/order/insideOut.js");
                t.d(n, "stackOrderInsideOut", (function() {
                    return H.default
                }));
                var q = t( /*! ./order/none.js */ "./node_modules/d3-shape/src/order/none.js");
                t.d(n, "stackOrderNone", (function() {
                    return q.default
                }));
                var W = t( /*! ./order/reverse.js */ "./node_modules/d3-shape/src/order/reverse.js");
                t.d(n, "stackOrderReverse", (function() {
                    return W.default
                }))
            },
        "./node_modules/d3-shape/src/line.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-shape/src/line.js ***!
              \*******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-path */ "./node_modules/d3-path/src/index.js"),
                    i = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    o = t( /*! ./curve/linear.js */ "./node_modules/d3-shape/src/curve/linear.js"),
                    s = t( /*! ./point.js */ "./node_modules/d3-shape/src/point.js");
                n.default = function() {
                    var e = s.x,
                        n = s.y,
                        t = Object(i.default)(!0),
                        r = null,
                        u = o.default,
                        l = null;

                    function c(i) {
                        var o, s, c, d = i.length,
                            g = !1;
                        for (null == r && (l = u(c = Object(a.path)())), o = 0; o <= d; ++o) !(o < d && t(s = i[o], o, i)) === g && ((g = !g) ? l.lineStart() : l.lineEnd()), g && l.point(+e(s, o, i), +n(s, o, i));
                        if (c) return l = null, c + "" || null
                    }
                    return c.x = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(i.default)(+n), c) : e
                    }, c.y = function(e) {
                        return arguments.length ? (n = "function" == typeof e ? e : Object(i.default)(+e), c) : n
                    }, c.defined = function(e) {
                        return arguments.length ? (t = "function" == typeof e ? e : Object(i.default)(!!e), c) : t
                    }, c.curve = function(e) {
                        return arguments.length ? (u = e, null != r && (l = u(r)), c) : u
                    }, c.context = function(e) {
                        return arguments.length ? (null == e ? r = l = null : l = u(r = e), c) : r
                    }, c
                }
            },
        "./node_modules/d3-shape/src/lineRadial.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/lineRadial.js ***!
              \*************************************************/
            /*! exports provided: lineRadial, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "lineRadial", (function() {
                    return o
                }));
                var a = t( /*! ./curve/radial.js */ "./node_modules/d3-shape/src/curve/radial.js"),
                    i = t( /*! ./line.js */ "./node_modules/d3-shape/src/line.js");

                function o(e) {
                    var n = e.curve;
                    return e.angle = e.x, delete e.x, e.radius = e.y, delete e.y, e.curve = function(e) {
                        return arguments.length ? n(Object(a.default)(e)) : n()._curve
                    }, e
                }
                n.default = function() {
                    return o(Object(i.default)().curve(a.curveRadialLinear))
                }
            },
        "./node_modules/d3-shape/src/link/index.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/link/index.js ***!
              \*************************************************/
            /*! exports provided: linkHorizontal, linkVertical, linkRadial */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "linkHorizontal", (function() {
                    return f
                })), t.d(n, "linkVertical", (function() {
                    return m
                })), t.d(n, "linkRadial", (function() {
                    return p
                }));
                var a = t( /*! d3-path */ "./node_modules/d3-path/src/index.js"),
                    i = t( /*! ../array.js */ "./node_modules/d3-shape/src/array.js"),
                    o = t( /*! ../constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    s = t( /*! ../point.js */ "./node_modules/d3-shape/src/point.js"),
                    r = t( /*! ../pointRadial.js */ "./node_modules/d3-shape/src/pointRadial.js");

                function u(e) {
                    return e.source
                }

                function l(e) {
                    return e.target
                }

                function c(e) {
                    var n = u,
                        t = l,
                        r = s.x,
                        c = s.y,
                        d = null;

                    function g() {
                        var o, s = i.slice.call(arguments),
                            u = n.apply(this, s),
                            l = t.apply(this, s);
                        if (d || (d = o = Object(a.path)()), e(d, +r.apply(this, (s[0] = u, s)), +c.apply(this, s), +r.apply(this, (s[0] = l, s)), +c.apply(this, s)), o) return d = null, o + "" || null
                    }
                    return g.source = function(e) {
                        return arguments.length ? (n = e, g) : n
                    }, g.target = function(e) {
                        return arguments.length ? (t = e, g) : t
                    }, g.x = function(e) {
                        return arguments.length ? (r = "function" == typeof e ? e : Object(o.default)(+e), g) : r
                    }, g.y = function(e) {
                        return arguments.length ? (c = "function" == typeof e ? e : Object(o.default)(+e), g) : c
                    }, g.context = function(e) {
                        return arguments.length ? (d = null == e ? null : e, g) : d
                    }, g
                }

                function d(e, n, t, a, i) {
                    e.moveTo(n, t), e.bezierCurveTo(n = (n + a) / 2, t, n, i, a, i)
                }

                function g(e, n, t, a, i) {
                    e.moveTo(n, t), e.bezierCurveTo(n, t = (t + i) / 2, a, t, a, i)
                }

                function h(e, n, t, a, i) {
                    var o = Object(r.default)(n, t),
                        s = Object(r.default)(n, t = (t + i) / 2),
                        u = Object(r.default)(a, t),
                        l = Object(r.default)(a, i);
                    e.moveTo(o[0], o[1]), e.bezierCurveTo(s[0], s[1], u[0], u[1], l[0], l[1])
                }

                function f() {
                    return c(d)
                }

                function m() {
                    return c(g)
                }

                function p() {
                    var e = c(h);
                    return e.angle = e.x, delete e.x, e.radius = e.y, delete e.y, e
                }
            },
        "./node_modules/d3-shape/src/math.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-shape/src/math.js ***!
              \*******************************************/
            /*! exports provided: abs, atan2, cos, max, min, sin, sqrt, epsilon, pi, halfPi, tau, acos, asin */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "abs", (function() {
                    return a
                })), t.d(n, "atan2", (function() {
                    return i
                })), t.d(n, "cos", (function() {
                    return o
                })), t.d(n, "max", (function() {
                    return s
                })), t.d(n, "min", (function() {
                    return r
                })), t.d(n, "sin", (function() {
                    return u
                })), t.d(n, "sqrt", (function() {
                    return l
                })), t.d(n, "epsilon", (function() {
                    return c
                })), t.d(n, "pi", (function() {
                    return d
                })), t.d(n, "halfPi", (function() {
                    return g
                })), t.d(n, "tau", (function() {
                    return h
                })), t.d(n, "acos", (function() {
                    return f
                })), t.d(n, "asin", (function() {
                    return m
                }));
                var a = Math.abs,
                    i = Math.atan2,
                    o = Math.cos,
                    s = Math.max,
                    r = Math.min,
                    u = Math.sin,
                    l = Math.sqrt,
                    c = 1e-12,
                    d = Math.PI,
                    g = d / 2,
                    h = 2 * d;

                function f(e) {
                    return e > 1 ? 0 : e < -1 ? d : Math.acos(e)
                }

                function m(e) {
                    return e >= 1 ? g : e <= -1 ? -g : Math.asin(e)
                }
            },
        "./node_modules/d3-shape/src/noop.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-shape/src/noop.js ***!
              \*******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {}
            },
        "./node_modules/d3-shape/src/offset/diverging.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-shape/src/offset/diverging.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    if ((r = e.length) > 0)
                        for (var t, a, i, o, s, r, u = 0, l = e[n[0]].length; u < l; ++u)
                            for (o = s = 0, t = 0; t < r; ++t)(i = (a = e[n[t]][u])[1] - a[0]) > 0 ? (a[0] = o, a[1] = o += i) : i < 0 ? (a[1] = s, a[0] = s += i) : (a[0] = 0, a[1] = i)
                }
            },
        "./node_modules/d3-shape/src/offset/expand.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/offset/expand.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/offset/none.js");
                n.default = function(e, n) {
                    if ((i = e.length) > 0) {
                        for (var t, i, o, s = 0, r = e[0].length; s < r; ++s) {
                            for (o = t = 0; t < i; ++t) o += e[t][s][1] || 0;
                            if (o)
                                for (t = 0; t < i; ++t) e[t][s][1] /= o
                        }
                        Object(a.default)(e, n)
                    }
                }
            },
        "./node_modules/d3-shape/src/offset/none.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-shape/src/offset/none.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    if ((i = e.length) > 1)
                        for (var t, a, i, o = 1, s = e[n[0]], r = s.length; o < i; ++o)
                            for (a = s, s = e[n[o]], t = 0; t < r; ++t) s[t][1] += s[t][0] = isNaN(a[t][1]) ? a[t][0] : a[t][1]
                }
            },
        "./node_modules/d3-shape/src/offset/silhouette.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3-shape/src/offset/silhouette.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/offset/none.js");
                n.default = function(e, n) {
                    if ((t = e.length) > 0) {
                        for (var t, i = 0, o = e[n[0]], s = o.length; i < s; ++i) {
                            for (var r = 0, u = 0; r < t; ++r) u += e[r][i][1] || 0;
                            o[i][1] += o[i][0] = -u / 2
                        }
                        Object(a.default)(e, n)
                    }
                }
            },
        "./node_modules/d3-shape/src/offset/wiggle.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/offset/wiggle.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/offset/none.js");
                n.default = function(e, n) {
                    if ((o = e.length) > 0 && (i = (t = e[n[0]]).length) > 0) {
                        for (var t, i, o, s = 0, r = 1; r < i; ++r) {
                            for (var u = 0, l = 0, c = 0; u < o; ++u) {
                                for (var d = e[n[u]], g = d[r][1] || 0, h = (g - (d[r - 1][1] || 0)) / 2, f = 0; f < u; ++f) {
                                    var m = e[n[f]];
                                    h += (m[r][1] || 0) - (m[r - 1][1] || 0)
                                }
                                l += g, c += h * g
                            }
                            t[r - 1][1] += t[r - 1][0] = s, l && (s -= c / l)
                        }
                        t[r - 1][1] += t[r - 1][0] = s, Object(a.default)(e, n)
                    }
                }
            },
        "./node_modules/d3-shape/src/order/appearance.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-shape/src/order/appearance.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/order/none.js");

                function i(e) {
                    for (var n, t = -1, a = 0, i = e.length, o = -1 / 0; ++t < i;)(n = +e[t][1]) > o && (o = n, a = t);
                    return a
                }
                n.default = function(e) {
                    var n = e.map(i);
                    return Object(a.default)(e).sort((function(e, t) {
                        return n[e] - n[t]
                    }))
                }
            },
        "./node_modules/d3-shape/src/order/ascending.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-shape/src/order/ascending.js ***!
              \******************************************************/
            /*! exports provided: default, sum */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "sum", (function() {
                    return i
                }));
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/order/none.js");

                function i(e) {
                    for (var n, t = 0, a = -1, i = e.length; ++a < i;)(n = +e[a][1]) && (t += n);
                    return t
                }
                n.default = function(e) {
                    var n = e.map(i);
                    return Object(a.default)(e).sort((function(e, t) {
                        return n[e] - n[t]
                    }))
                }
            },
        "./node_modules/d3-shape/src/order/descending.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3-shape/src/order/descending.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./ascending.js */ "./node_modules/d3-shape/src/order/ascending.js");
                n.default = function(e) {
                    return Object(a.default)(e).reverse()
                }
            },
        "./node_modules/d3-shape/src/order/insideOut.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-shape/src/order/insideOut.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./appearance.js */ "./node_modules/d3-shape/src/order/appearance.js"),
                    i = t( /*! ./ascending.js */ "./node_modules/d3-shape/src/order/ascending.js");
                n.default = function(e) {
                    var n, t, o = e.length,
                        s = e.map(i.sum),
                        r = Object(a.default)(e),
                        u = 0,
                        l = 0,
                        c = [],
                        d = [];
                    for (n = 0; n < o; ++n) t = r[n], u < l ? (u += s[t], c.push(t)) : (l += s[t], d.push(t));
                    return d.reverse().concat(c)
                }
            },
        "./node_modules/d3-shape/src/order/none.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/order/none.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    for (var n = e.length, t = new Array(n); --n >= 0;) t[n] = n;
                    return t
                }
            },
        "./node_modules/d3-shape/src/order/reverse.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/order/reverse.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./none.js */ "./node_modules/d3-shape/src/order/none.js");
                n.default = function(e) {
                    return Object(a.default)(e).reverse()
                }
            },
        "./node_modules/d3-shape/src/pie.js":
            /*!******************************************!*\
              !*** ./node_modules/d3-shape/src/pie.js ***!
              \******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    i = t( /*! ./descending.js */ "./node_modules/d3-shape/src/descending.js"),
                    o = t( /*! ./identity.js */ "./node_modules/d3-shape/src/identity.js"),
                    s = t( /*! ./math.js */ "./node_modules/d3-shape/src/math.js");
                n.default = function() {
                    var e = o.default,
                        n = i.default,
                        t = null,
                        r = Object(a.default)(0),
                        u = Object(a.default)(s.tau),
                        l = Object(a.default)(0);

                    function c(a) {
                        var i, o, c, d, g, h = a.length,
                            f = 0,
                            m = new Array(h),
                            p = new Array(h),
                            v = +r.apply(this, arguments),
                            y = Math.min(s.tau, Math.max(-s.tau, u.apply(this, arguments) - v)),
                            _ = Math.min(Math.abs(y) / h, l.apply(this, arguments)),
                            b = _ * (y < 0 ? -1 : 1);
                        for (i = 0; i < h; ++i)(g = p[m[i] = i] = +e(a[i], i, a)) > 0 && (f += g);
                        for (null != n ? m.sort((function(e, t) {
                                return n(p[e], p[t])
                            })) : null != t && m.sort((function(e, n) {
                                return t(a[e], a[n])
                            })), i = 0, c = f ? (y - h * b) / f : 0; i < h; ++i, v = d) o = m[i], d = v + ((g = p[o]) > 0 ? g * c : 0) + b, p[o] = {
                            data: a[o],
                            index: i,
                            value: g,
                            startAngle: v,
                            endAngle: d,
                            padAngle: _
                        };
                        return p
                    }
                    return c.value = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(a.default)(+n), c) : e
                    }, c.sortValues = function(e) {
                        return arguments.length ? (n = e, t = null, c) : n
                    }, c.sort = function(e) {
                        return arguments.length ? (t = e, n = null, c) : t
                    }, c.startAngle = function(e) {
                        return arguments.length ? (r = "function" == typeof e ? e : Object(a.default)(+e), c) : r
                    }, c.endAngle = function(e) {
                        return arguments.length ? (u = "function" == typeof e ? e : Object(a.default)(+e), c) : u
                    }, c.padAngle = function(e) {
                        return arguments.length ? (l = "function" == typeof e ? e : Object(a.default)(+e), c) : l
                    }, c
                }
            },
        "./node_modules/d3-shape/src/point.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-shape/src/point.js ***!
              \********************************************/
            /*! exports provided: x, y */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return e[0]
                }

                function i(e) {
                    return e[1]
                }
                t.r(n), t.d(n, "x", (function() {
                    return a
                })), t.d(n, "y", (function() {
                    return i
                }))
            },
        "./node_modules/d3-shape/src/pointRadial.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-shape/src/pointRadial.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    return [(n = +n) * Math.cos(e -= Math.PI / 2), n * Math.sin(e)]
                }
            },
        "./node_modules/d3-shape/src/stack.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-shape/src/stack.js ***!
              \********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./array.js */ "./node_modules/d3-shape/src/array.js"),
                    i = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    o = t( /*! ./offset/none.js */ "./node_modules/d3-shape/src/offset/none.js"),
                    s = t( /*! ./order/none.js */ "./node_modules/d3-shape/src/order/none.js");

                function r(e, n) {
                    return e[n]
                }
                n.default = function() {
                    var e = Object(i.default)([]),
                        n = s.default,
                        t = o.default,
                        u = r;

                    function l(a) {
                        var i, o, s = e.apply(this, arguments),
                            r = a.length,
                            l = s.length,
                            c = new Array(l);
                        for (i = 0; i < l; ++i) {
                            for (var d, g = s[i], h = c[i] = new Array(r), f = 0; f < r; ++f) h[f] = d = [0, +u(a[f], g, f, a)], d.data = a[f];
                            h.key = g
                        }
                        for (i = 0, o = n(c); i < l; ++i) c[o[i]].index = i;
                        return t(c, o), c
                    }
                    return l.keys = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(i.default)(a.slice.call(n)), l) : e
                    }, l.value = function(e) {
                        return arguments.length ? (u = "function" == typeof e ? e : Object(i.default)(+e), l) : u
                    }, l.order = function(e) {
                        return arguments.length ? (n = null == e ? s.default : "function" == typeof e ? e : Object(i.default)(a.slice.call(e)), l) : n
                    }, l.offset = function(e) {
                        return arguments.length ? (t = null == e ? o.default : e, l) : t
                    }, l
                }
            },
        "./node_modules/d3-shape/src/symbol.js":
            /*!*********************************************!*\
              !*** ./node_modules/d3-shape/src/symbol.js ***!
              \*********************************************/
            /*! exports provided: symbols, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "symbols", (function() {
                    return g
                }));
                var a = t( /*! d3-path */ "./node_modules/d3-path/src/index.js"),
                    i = t( /*! ./symbol/circle.js */ "./node_modules/d3-shape/src/symbol/circle.js"),
                    o = t( /*! ./symbol/cross.js */ "./node_modules/d3-shape/src/symbol/cross.js"),
                    s = t( /*! ./symbol/diamond.js */ "./node_modules/d3-shape/src/symbol/diamond.js"),
                    r = t( /*! ./symbol/star.js */ "./node_modules/d3-shape/src/symbol/star.js"),
                    u = t( /*! ./symbol/square.js */ "./node_modules/d3-shape/src/symbol/square.js"),
                    l = t( /*! ./symbol/triangle.js */ "./node_modules/d3-shape/src/symbol/triangle.js"),
                    c = t( /*! ./symbol/wye.js */ "./node_modules/d3-shape/src/symbol/wye.js"),
                    d = t( /*! ./constant.js */ "./node_modules/d3-shape/src/constant.js"),
                    g = [i.default, o.default, s.default, u.default, r.default, l.default, c.default];
                n.default = function() {
                    var e = Object(d.default)(i.default),
                        n = Object(d.default)(64),
                        t = null;

                    function o() {
                        var i;
                        if (t || (t = i = Object(a.path)()), e.apply(this, arguments).draw(t, +n.apply(this, arguments)), i) return t = null, i + "" || null
                    }
                    return o.type = function(n) {
                        return arguments.length ? (e = "function" == typeof n ? n : Object(d.default)(n), o) : e
                    }, o.size = function(e) {
                        return arguments.length ? (n = "function" == typeof e ? e : Object(d.default)(+e), o) : n
                    }, o.context = function(e) {
                        return arguments.length ? (t = null == e ? null : e, o) : t
                    }, o
                }
            },
        "./node_modules/d3-shape/src/symbol/circle.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/circle.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../math.js */ "./node_modules/d3-shape/src/math.js");
                n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(n / a.pi);
                        e.moveTo(t, 0), e.arc(0, 0, t, 0, a.tau)
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/cross.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/cross.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(n / 5) / 2;
                        e.moveTo(-3 * t, -t), e.lineTo(-t, -t), e.lineTo(-t, -3 * t), e.lineTo(t, -3 * t), e.lineTo(t, -t), e.lineTo(3 * t, -t), e.lineTo(3 * t, t), e.lineTo(t, t), e.lineTo(t, 3 * t), e.lineTo(-t, 3 * t), e.lineTo(-t, t), e.lineTo(-3 * t, t), e.closePath()
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/diamond.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/diamond.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = Math.sqrt(1 / 3),
                    i = 2 * a;
                n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(n / i),
                            o = t * a;
                        e.moveTo(0, -t), e.lineTo(o, 0), e.lineTo(0, t), e.lineTo(-o, 0), e.closePath()
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/square.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/square.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(n),
                            a = -t / 2;
                        e.rect(a, a, t, t)
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/star.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/star.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../math.js */ "./node_modules/d3-shape/src/math.js"),
                    i = Math.sin(a.pi / 10) / Math.sin(7 * a.pi / 10),
                    o = Math.sin(a.tau / 10) * i,
                    s = -Math.cos(a.tau / 10) * i;
                n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(.8908130915292852 * n),
                            i = o * t,
                            r = s * t;
                        e.moveTo(0, -t), e.lineTo(i, r);
                        for (var u = 1; u < 5; ++u) {
                            var l = a.tau * u / 5,
                                c = Math.cos(l),
                                d = Math.sin(l);
                            e.lineTo(d * t, -c * t), e.lineTo(c * i - d * r, d * i + c * r)
                        }
                        e.closePath()
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/triangle.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/triangle.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = Math.sqrt(3);
                n.default = {
                    draw: function(e, n) {
                        var t = -Math.sqrt(n / (3 * a));
                        e.moveTo(0, 2 * t), e.lineTo(-a * t, -t), e.lineTo(a * t, -t), e.closePath()
                    }
                }
            },
        "./node_modules/d3-shape/src/symbol/wye.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-shape/src/symbol/wye.js ***!
              \*************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = -.5,
                    i = Math.sqrt(3) / 2,
                    o = 1 / Math.sqrt(12),
                    s = 3 * (o / 2 + 1);
                n.default = {
                    draw: function(e, n) {
                        var t = Math.sqrt(n / s),
                            r = t / 2,
                            u = t * o,
                            l = r,
                            c = t * o + t,
                            d = -l,
                            g = c;
                        e.moveTo(r, u), e.lineTo(l, c), e.lineTo(d, g), e.lineTo(a * r - i * u, i * r + a * u), e.lineTo(a * l - i * c, i * l + a * c), e.lineTo(a * d - i * g, i * d + a * g), e.lineTo(a * r + i * u, a * u - i * r), e.lineTo(a * l + i * c, a * c - i * l), e.lineTo(a * d + i * g, a * g - i * d), e.closePath()
                    }
                }
            },
        "./node_modules/d3-timer/src/index.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-timer/src/index.js ***!
              \********************************************/
            /*! exports provided: now, timer, timerFlush, timeout, interval */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./timer.js */ "./node_modules/d3-timer/src/timer.js");
                t.d(n, "now", (function() {
                    return a.now
                })), t.d(n, "timer", (function() {
                    return a.timer
                })), t.d(n, "timerFlush", (function() {
                    return a.timerFlush
                }));
                var i = t( /*! ./timeout.js */ "./node_modules/d3-timer/src/timeout.js");
                t.d(n, "timeout", (function() {
                    return i.default
                }));
                var o = t( /*! ./interval.js */ "./node_modules/d3-timer/src/interval.js");
                t.d(n, "interval", (function() {
                    return o.default
                }))
            },
        "./node_modules/d3-timer/src/interval.js":
            /*!***********************************************!*\
              !*** ./node_modules/d3-timer/src/interval.js ***!
              \***********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./timer.js */ "./node_modules/d3-timer/src/timer.js");
                n.default = function(e, n, t) {
                    var i = new a.Timer,
                        o = n;
                    return null == n ? (i.restart(e, n, t), i) : (n = +n, t = null == t ? Object(a.now)() : +t, i.restart((function a(s) {
                        s += o, i.restart(a, o += n, t), e(s)
                    }), n, t), i)
                }
            },
        "./node_modules/d3-timer/src/timeout.js":
            /*!**********************************************!*\
              !*** ./node_modules/d3-timer/src/timeout.js ***!
              \**********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./timer.js */ "./node_modules/d3-timer/src/timer.js");
                n.default = function(e, n, t) {
                    var i = new a.Timer;
                    return n = null == n ? 0 : +n, i.restart((function(t) {
                        i.stop(), e(t + n)
                    }), n, t), i
                }
            },
        "./node_modules/d3-timer/src/timer.js":
            /*!********************************************!*\
              !*** ./node_modules/d3-timer/src/timer.js ***!
              \********************************************/
            /*! exports provided: now, Timer, timer, timerFlush */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "now", (function() {
                    return h
                })), t.d(n, "Timer", (function() {
                    return m
                })), t.d(n, "timer", (function() {
                    return p
                })), t.d(n, "timerFlush", (function() {
                    return v
                }));
                var a, i, o = 0,
                    s = 0,
                    r = 0,
                    u = 0,
                    l = 0,
                    c = 0,
                    d = "object" == typeof performance && performance.now ? performance : Date,
                    g = "object" == typeof window && window.requestAnimationFrame ? window.requestAnimationFrame.bind(window) : function(e) {
                        setTimeout(e, 17)
                    };

                function h() {
                    return l || (g(f), l = d.now() + c)
                }

                function f() {
                    l = 0
                }

                function m() {
                    this._call = this._time = this._next = null
                }

                function p(e, n, t) {
                    var a = new m;
                    return a.restart(e, n, t), a
                }

                function v() {
                    h(), ++o;
                    for (var e, n = a; n;)(e = l - n._time) >= 0 && n._call.call(null, e), n = n._next;
                    --o
                }

                function y() {
                    l = (u = d.now()) + c, o = s = 0;
                    try {
                        v()
                    } finally {
                        o = 0,
                            function() {
                                var e, n, t = a,
                                    o = 1 / 0;
                                for (; t;) t._call ? (o > t._time && (o = t._time), e = t, t = t._next) : (n = t._next, t._next = null, t = e ? e._next = n : a = n);
                                i = e, b(o)
                            }(), l = 0
                    }
                }

                function _() {
                    var e = d.now(),
                        n = e - u;
                    n > 1e3 && (c -= n, u = e)
                }

                function b(e) {
                    o || (s && (s = clearTimeout(s)), e - l > 24 ? (e < 1 / 0 && (s = setTimeout(y, e - d.now() - c)), r && (r = clearInterval(r))) : (r || (u = d.now(), r = setInterval(_, 1e3)), o = 1, g(y)))
                }
                m.prototype = p.prototype = {
                    constructor: m,
                    restart: function(e, n, t) {
                        if ("function" != typeof e) throw new TypeError("callback is not a function");
                        t = (null == t ? h() : +t) + (null == n ? 0 : +n), this._next || i === this || (i ? i._next = this : a = this, i = this), this._call = e, this._time = t, b()
                    },
                    stop: function() {
                        this._call && (this._call = null, this._time = 1 / 0, b())
                    }
                }
            },
        "./node_modules/d3-transition/src/active.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3-transition/src/active.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./transition/index.js */ "./node_modules/d3-transition/src/transition/index.js"),
                    i = t( /*! ./transition/schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js"),
                    o = [null];
                n.default = function(e, n) {
                    var t, s, r = e.__transition;
                    if (r)
                        for (s in n = null == n ? null : n + "", r)
                            if ((t = r[s]).state > i.SCHEDULED && t.name === n) return new a.Transition([
                                [e]
                            ], o, n, +s);
                    return null
                }
            },
        "./node_modules/d3-transition/src/index.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3-transition/src/index.js ***!
              \*************************************************/
            /*! exports provided: transition, active, interrupt */
            function(e, n, t) {
                "use strict";
                t.r(n);
                t( /*! ./selection/index.js */ "./node_modules/d3-transition/src/selection/index.js");
                var a = t( /*! ./transition/index.js */ "./node_modules/d3-transition/src/transition/index.js");
                t.d(n, "transition", (function() {
                    return a.default
                }));
                var i = t( /*! ./active.js */ "./node_modules/d3-transition/src/active.js");
                t.d(n, "active", (function() {
                    return i.default
                }));
                var o = t( /*! ./interrupt.js */ "./node_modules/d3-transition/src/interrupt.js");
                t.d(n, "interrupt", (function() {
                    return o.default
                }))
            },
        "./node_modules/d3-transition/src/interrupt.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3-transition/src/interrupt.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./transition/schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");
                n.default = function(e, n) {
                    var t, i, o, s = e.__transition,
                        r = !0;
                    if (s) {
                        for (o in n = null == n ? null : n + "", s)(t = s[o]).name === n ? (i = t.state > a.STARTING && t.state < a.ENDING, t.state = a.ENDED, t.timer.stop(), t.on.call(i ? "interrupt" : "cancel", e, e.__data__, t.index, t.group), delete s[o]) : r = !1;
                        r && delete e.__transition
                    }
                }
            },
        "./node_modules/d3-transition/src/selection/index.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-transition/src/selection/index.js ***!
              \***********************************************************/
            /*! no exports provided */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! ./interrupt.js */ "./node_modules/d3-transition/src/selection/interrupt.js"),
                    o = t( /*! ./transition.js */ "./node_modules/d3-transition/src/selection/transition.js");
                a.selection.prototype.interrupt = i.default, a.selection.prototype.transition = o.default
            },
        "./node_modules/d3-transition/src/selection/interrupt.js":
            /*!***************************************************************!*\
              !*** ./node_modules/d3-transition/src/selection/interrupt.js ***!
              \***************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../interrupt.js */ "./node_modules/d3-transition/src/interrupt.js");
                n.default = function(e) {
                    return this.each((function() {
                        Object(a.default)(this, e)
                    }))
                }
            },
        "./node_modules/d3-transition/src/selection/transition.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-transition/src/selection/transition.js ***!
              \****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ../transition/index.js */ "./node_modules/d3-transition/src/transition/index.js"),
                    i = t( /*! ../transition/schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js"),
                    o = t( /*! d3-ease */ "./node_modules/d3-ease/src/index.js"),
                    s = t( /*! d3-timer */ "./node_modules/d3-timer/src/index.js"),
                    r = {
                        time: null,
                        delay: 0,
                        duration: 250,
                        ease: o.easeCubicInOut
                    };

                function u(e, n) {
                    for (var t; !(t = e.__transition) || !(t = t[n]);)
                        if (!(e = e.parentNode)) return r.time = Object(s.now)(), r;
                    return t
                }
                n.default = function(e) {
                    var n, t;
                    e instanceof a.Transition ? (n = e._id, e = e._name) : (n = Object(a.newId)(), (t = r).time = Object(s.now)(), e = null == e ? null : e + "");
                    for (var o = this._groups, l = o.length, c = 0; c < l; ++c)
                        for (var d, g = o[c], h = g.length, f = 0; f < h; ++f)(d = g[f]) && Object(i.default)(d, e, n, f, g, t || u(d, n));
                    return new a.Transition(o, this._parents, e, n)
                }
            },
        "./node_modules/d3-transition/src/transition/attr.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/attr.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-interpolate */ "./node_modules/d3-interpolate/src/index.js"),
                    i = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    o = t( /*! ./tween.js */ "./node_modules/d3-transition/src/transition/tween.js"),
                    s = t( /*! ./interpolate.js */ "./node_modules/d3-transition/src/transition/interpolate.js");

                function r(e) {
                    return function() {
                        this.removeAttribute(e)
                    }
                }

                function u(e) {
                    return function() {
                        this.removeAttributeNS(e.space, e.local)
                    }
                }

                function l(e, n, t) {
                    var a, i, o = t + "";
                    return function() {
                        var s = this.getAttribute(e);
                        return s === o ? null : s === a ? i : i = n(a = s, t)
                    }
                }

                function c(e, n, t) {
                    var a, i, o = t + "";
                    return function() {
                        var s = this.getAttributeNS(e.space, e.local);
                        return s === o ? null : s === a ? i : i = n(a = s, t)
                    }
                }

                function d(e, n, t) {
                    var a, i, o;
                    return function() {
                        var s, r, u = t(this);
                        if (null != u) return (s = this.getAttribute(e)) === (r = u + "") ? null : s === a && r === i ? o : (i = r, o = n(a = s, u));
                        this.removeAttribute(e)
                    }
                }

                function g(e, n, t) {
                    var a, i, o;
                    return function() {
                        var s, r, u = t(this);
                        if (null != u) return (s = this.getAttributeNS(e.space, e.local)) === (r = u + "") ? null : s === a && r === i ? o : (i = r, o = n(a = s, u));
                        this.removeAttributeNS(e.space, e.local)
                    }
                }
                n.default = function(e, n) {
                    var t = Object(i.namespace)(e),
                        h = "transform" === t ? a.interpolateTransformSvg : s.default;
                    return this.attrTween(e, "function" == typeof n ? (t.local ? g : d)(t, h, Object(o.tweenValue)(this, "attr." + e, n)) : null == n ? (t.local ? u : r)(t) : (t.local ? c : l)(t, h, n))
                }
            },
        "./node_modules/d3-transition/src/transition/attrTween.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/attrTween.js ***!
              \****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js");

                function i(e, n) {
                    return function(t) {
                        this.setAttribute(e, n.call(this, t))
                    }
                }

                function o(e, n) {
                    return function(t) {
                        this.setAttributeNS(e.space, e.local, n.call(this, t))
                    }
                }

                function s(e, n) {
                    var t, a;

                    function i() {
                        var i = n.apply(this, arguments);
                        return i !== a && (t = (a = i) && o(e, i)), t
                    }
                    return i._value = n, i
                }

                function r(e, n) {
                    var t, a;

                    function o() {
                        var o = n.apply(this, arguments);
                        return o !== a && (t = (a = o) && i(e, o)), t
                    }
                    return o._value = n, o
                }
                n.default = function(e, n) {
                    var t = "attr." + e;
                    if (arguments.length < 2) return (t = this.tween(t)) && t._value;
                    if (null == n) return this.tween(t, null);
                    if ("function" != typeof n) throw new Error;
                    var i = Object(a.namespace)(e);
                    return this.tween(t, (i.local ? s : r)(i, n))
                }
            },
        "./node_modules/d3-transition/src/transition/delay.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/delay.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");

                function i(e, n) {
                    return function() {
                        Object(a.init)(this, e).delay = +n.apply(this, arguments)
                    }
                }

                function o(e, n) {
                    return n = +n,
                        function() {
                            Object(a.init)(this, e).delay = n
                        }
                }
                n.default = function(e) {
                    var n = this._id;
                    return arguments.length ? this.each(("function" == typeof e ? i : o)(n, e)) : Object(a.get)(this.node(), n).delay
                }
            },
        "./node_modules/d3-transition/src/transition/duration.js":
            /*!***************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/duration.js ***!
              \***************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");

                function i(e, n) {
                    return function() {
                        Object(a.set)(this, e).duration = +n.apply(this, arguments)
                    }
                }

                function o(e, n) {
                    return n = +n,
                        function() {
                            Object(a.set)(this, e).duration = n
                        }
                }
                n.default = function(e) {
                    var n = this._id;
                    return arguments.length ? this.each(("function" == typeof e ? i : o)(n, e)) : Object(a.get)(this.node(), n).duration
                }
            },
        "./node_modules/d3-transition/src/transition/ease.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/ease.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");

                function i(e, n) {
                    if ("function" != typeof n) throw new Error;
                    return function() {
                        Object(a.set)(this, e).ease = n
                    }
                }
                n.default = function(e) {
                    var n = this._id;
                    return arguments.length ? this.each(i(n, e)) : Object(a.get)(this.node(), n).ease
                }
            },
        "./node_modules/d3-transition/src/transition/end.js":
            /*!**********************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/end.js ***!
              \**********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");
                n.default = function() {
                    var e, n, t = this,
                        i = t._id,
                        o = t.size();
                    return new Promise((function(s, r) {
                        var u = {
                                value: r
                            },
                            l = {
                                value: function() {
                                    0 == --o && s()
                                }
                            };
                        t.each((function() {
                            var t = Object(a.set)(this, i),
                                o = t.on;
                            o !== e && ((n = (e = o).copy())._.cancel.push(u), n._.interrupt.push(u), n._.end.push(l)), t.on = n
                        }))
                    }))
                }
            },
        "./node_modules/d3-transition/src/transition/filter.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/filter.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! ./index.js */ "./node_modules/d3-transition/src/transition/index.js");
                n.default = function(e) {
                    "function" != typeof e && (e = Object(a.matcher)(e));
                    for (var n = this._groups, t = n.length, o = new Array(t), s = 0; s < t; ++s)
                        for (var r, u = n[s], l = u.length, c = o[s] = [], d = 0; d < l; ++d)(r = u[d]) && e.call(r, r.__data__, d, u) && c.push(r);
                    return new i.Transition(o, this._parents, this._name, this._id)
                }
            },
        "./node_modules/d3-transition/src/transition/index.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/index.js ***!
              \************************************************************/
            /*! exports provided: Transition, default, newId */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "Transition", (function() {
                    return R
                })), t.d(n, "default", (function() {
                    return k
                })), t.d(n, "newId", (function() {
                    return A
                }));
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! ./attr.js */ "./node_modules/d3-transition/src/transition/attr.js"),
                    o = t( /*! ./attrTween.js */ "./node_modules/d3-transition/src/transition/attrTween.js"),
                    s = t( /*! ./delay.js */ "./node_modules/d3-transition/src/transition/delay.js"),
                    r = t( /*! ./duration.js */ "./node_modules/d3-transition/src/transition/duration.js"),
                    u = t( /*! ./ease.js */ "./node_modules/d3-transition/src/transition/ease.js"),
                    l = t( /*! ./filter.js */ "./node_modules/d3-transition/src/transition/filter.js"),
                    c = t( /*! ./merge.js */ "./node_modules/d3-transition/src/transition/merge.js"),
                    d = t( /*! ./on.js */ "./node_modules/d3-transition/src/transition/on.js"),
                    g = t( /*! ./remove.js */ "./node_modules/d3-transition/src/transition/remove.js"),
                    h = t( /*! ./select.js */ "./node_modules/d3-transition/src/transition/select.js"),
                    f = t( /*! ./selectAll.js */ "./node_modules/d3-transition/src/transition/selectAll.js"),
                    m = t( /*! ./selection.js */ "./node_modules/d3-transition/src/transition/selection.js"),
                    p = t( /*! ./style.js */ "./node_modules/d3-transition/src/transition/style.js"),
                    v = t( /*! ./styleTween.js */ "./node_modules/d3-transition/src/transition/styleTween.js"),
                    y = t( /*! ./text.js */ "./node_modules/d3-transition/src/transition/text.js"),
                    _ = t( /*! ./textTween.js */ "./node_modules/d3-transition/src/transition/textTween.js"),
                    b = t( /*! ./transition.js */ "./node_modules/d3-transition/src/transition/transition.js"),
                    j = t( /*! ./tween.js */ "./node_modules/d3-transition/src/transition/tween.js"),
                    x = t( /*! ./end.js */ "./node_modules/d3-transition/src/transition/end.js"),
                    w = 0;

                function R(e, n, t, a) {
                    this._groups = e, this._parents = n, this._name = t, this._id = a
                }

                function k(e) {
                    return Object(a.selection)().transition(e)
                }

                function A() {
                    return ++w
                }
                var O = a.selection.prototype;
                R.prototype = k.prototype = {
                    constructor: R,
                    select: h.default,
                    selectAll: f.default,
                    filter: l.default,
                    merge: c.default,
                    selection: m.default,
                    transition: b.default,
                    call: O.call,
                    nodes: O.nodes,
                    node: O.node,
                    size: O.size,
                    empty: O.empty,
                    each: O.each,
                    on: d.default,
                    attr: i.default,
                    attrTween: o.default,
                    style: p.default,
                    styleTween: v.default,
                    text: y.default,
                    textTween: _.default,
                    remove: g.default,
                    tween: j.default,
                    delay: s.default,
                    duration: r.default,
                    ease: u.default,
                    end: x.default
                }
            },
        "./node_modules/d3-transition/src/transition/interpolate.js":
            /*!******************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/interpolate.js ***!
              \******************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-color */ "./node_modules/d3-color/src/index.js"),
                    i = t( /*! d3-interpolate */ "./node_modules/d3-interpolate/src/index.js");
                n.default = function(e, n) {
                    var t;
                    return ("number" == typeof n ? i.interpolateNumber : n instanceof a.color ? i.interpolateRgb : (t = Object(a.color)(n)) ? (n = t, i.interpolateRgb) : i.interpolateString)(e, n)
                }
            },
        "./node_modules/d3-transition/src/transition/merge.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/merge.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index.js */ "./node_modules/d3-transition/src/transition/index.js");
                n.default = function(e) {
                    if (e._id !== this._id) throw new Error;
                    for (var n = this._groups, t = e._groups, i = n.length, o = t.length, s = Math.min(i, o), r = new Array(i), u = 0; u < s; ++u)
                        for (var l, c = n[u], d = t[u], g = c.length, h = r[u] = new Array(g), f = 0; f < g; ++f)(l = c[f] || d[f]) && (h[f] = l);
                    for (; u < i; ++u) r[u] = n[u];
                    return new a.Transition(r, this._parents, this._name, this._id)
                }
            },
        "./node_modules/d3-transition/src/transition/on.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/on.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");

                function i(e, n, t) {
                    var i, o, s = function(e) {
                        return (e + "").trim().split(/^|\s+/).every((function(e) {
                            var n = e.indexOf(".");
                            return n >= 0 && (e = e.slice(0, n)), !e || "start" === e
                        }))
                    }(n) ? a.init : a.set;
                    return function() {
                        var a = s(this, e),
                            r = a.on;
                        r !== i && (o = (i = r).copy()).on(n, t), a.on = o
                    }
                }
                n.default = function(e, n) {
                    var t = this._id;
                    return arguments.length < 2 ? Object(a.get)(this.node(), t).on.on(e) : this.each(i(t, e, n))
                }
            },
        "./node_modules/d3-transition/src/transition/remove.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/remove.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    return this.on("end.remove", (e = this._id, function() {
                        var n = this.parentNode;
                        for (var t in this.__transition)
                            if (+t !== e) return;
                        n && n.removeChild(this)
                    }));
                    var e
                }
            },
        "./node_modules/d3-transition/src/transition/schedule.js":
            /*!***************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/schedule.js ***!
              \***************************************************************/
            /*! exports provided: CREATED, SCHEDULED, STARTING, STARTED, RUNNING, ENDING, ENDED, default, init, set, get */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "CREATED", (function() {
                    return r
                })), t.d(n, "SCHEDULED", (function() {
                    return u
                })), t.d(n, "STARTING", (function() {
                    return l
                })), t.d(n, "STARTED", (function() {
                    return c
                })), t.d(n, "RUNNING", (function() {
                    return d
                })), t.d(n, "ENDING", (function() {
                    return g
                })), t.d(n, "ENDED", (function() {
                    return h
                })), t.d(n, "init", (function() {
                    return f
                })), t.d(n, "set", (function() {
                    return m
                })), t.d(n, "get", (function() {
                    return p
                }));
                var a = t( /*! d3-dispatch */ "./node_modules/d3-dispatch/src/index.js"),
                    i = t( /*! d3-timer */ "./node_modules/d3-timer/src/index.js"),
                    o = Object(a.dispatch)("start", "end", "cancel", "interrupt"),
                    s = [],
                    r = 0,
                    u = 1,
                    l = 2,
                    c = 3,
                    d = 4,
                    g = 5,
                    h = 6;

                function f(e, n) {
                    var t = p(e, n);
                    if (t.state > r) throw new Error("too late; already scheduled");
                    return t
                }

                function m(e, n) {
                    var t = p(e, n);
                    if (t.state > c) throw new Error("too late; already running");
                    return t
                }

                function p(e, n) {
                    var t = e.__transition;
                    if (!t || !(t = t[n])) throw new Error("transition not found");
                    return t
                }
                n.default = function(e, n, t, a, f, m) {
                    var p = e.__transition;
                    if (p) {
                        if (t in p) return
                    } else e.__transition = {};
                    ! function(e, n, t) {
                        var a, o = e.__transition;

                        function s(g) {
                            var m, p, v, y;
                            if (t.state !== u) return f();
                            for (m in o)
                                if ((y = o[m]).name === t.name) {
                                    if (y.state === c) return Object(i.timeout)(s);
                                    y.state === d ? (y.state = h, y.timer.stop(), y.on.call("interrupt", e, e.__data__, y.index, y.group), delete o[m]) : +m < n && (y.state = h, y.timer.stop(), y.on.call("cancel", e, e.__data__, y.index, y.group), delete o[m])
                                }
                            if (Object(i.timeout)((function() {
                                    t.state === c && (t.state = d, t.timer.restart(r, t.delay, t.time), r(g))
                                })), t.state = l, t.on.call("start", e, e.__data__, t.index, t.group), t.state === l) {
                                for (t.state = c, a = new Array(v = t.tween.length), m = 0, p = -1; m < v; ++m)(y = t.tween[m].value.call(e, e.__data__, t.index, t.group)) && (a[++p] = y);
                                a.length = p + 1
                            }
                        }

                        function r(n) {
                            for (var i = n < t.duration ? t.ease.call(null, n / t.duration) : (t.timer.restart(f), t.state = g, 1), o = -1, s = a.length; ++o < s;) a[o].call(e, i);
                            t.state === g && (t.on.call("end", e, e.__data__, t.index, t.group), f())
                        }

                        function f() {
                            for (var a in t.state = h, t.timer.stop(), delete o[n], o) return;
                            delete e.__transition
                        }
                        o[n] = t, t.timer = Object(i.timer)((function(e) {
                            t.state = u, t.timer.restart(s, t.delay, t.time), t.delay <= e && s(e - t.delay)
                        }), 0, t.time)
                    }(e, t, {
                        name: n,
                        index: a,
                        group: f,
                        on: o,
                        tween: s,
                        time: m.time,
                        delay: m.delay,
                        duration: m.duration,
                        ease: m.ease,
                        timer: null,
                        state: r
                    })
                }
            },
        "./node_modules/d3-transition/src/transition/select.js":
            /*!*************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/select.js ***!
              \*************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! ./index.js */ "./node_modules/d3-transition/src/transition/index.js"),
                    o = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");
                n.default = function(e) {
                    var n = this._name,
                        t = this._id;
                    "function" != typeof e && (e = Object(a.selector)(e));
                    for (var s = this._groups, r = s.length, u = new Array(r), l = 0; l < r; ++l)
                        for (var c, d, g = s[l], h = g.length, f = u[l] = new Array(h), m = 0; m < h; ++m)(c = g[m]) && (d = e.call(c, c.__data__, m, g)) && ("__data__" in c && (d.__data__ = c.__data__), f[m] = d, Object(o.default)(f[m], n, t, m, f, Object(o.get)(c, t)));
                    return new i.Transition(u, this._parents, n, t)
                }
            },
        "./node_modules/d3-transition/src/transition/selectAll.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/selectAll.js ***!
              \****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! ./index.js */ "./node_modules/d3-transition/src/transition/index.js"),
                    o = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");
                n.default = function(e) {
                    var n = this._name,
                        t = this._id;
                    "function" != typeof e && (e = Object(a.selectorAll)(e));
                    for (var s = this._groups, r = s.length, u = [], l = [], c = 0; c < r; ++c)
                        for (var d, g = s[c], h = g.length, f = 0; f < h; ++f)
                            if (d = g[f]) {
                                for (var m, p = e.call(d, d.__data__, f, g), v = Object(o.get)(d, t), y = 0, _ = p.length; y < _; ++y)(m = p[y]) && Object(o.default)(m, n, t, y, p, v);
                                u.push(p), l.push(d)
                            }
                    return new i.Transition(u, l, n, t)
                }
            },
        "./node_modules/d3-transition/src/transition/selection.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/selection.js ***!
              \****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js").selection.prototype.constructor;
                n.default = function() {
                    return new a(this._groups, this._parents)
                }
            },
        "./node_modules/d3-transition/src/transition/style.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/style.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-interpolate */ "./node_modules/d3-interpolate/src/index.js"),
                    i = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    o = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js"),
                    s = t( /*! ./tween.js */ "./node_modules/d3-transition/src/transition/tween.js"),
                    r = t( /*! ./interpolate.js */ "./node_modules/d3-transition/src/transition/interpolate.js");

                function u(e) {
                    return function() {
                        this.style.removeProperty(e)
                    }
                }
                n.default = function(e, n, t) {
                    var l = "transform" == (e += "") ? a.interpolateTransformCss : r.default;
                    return null == n ? this.styleTween(e, function(e, n) {
                        var t, a, o;
                        return function() {
                            var s = Object(i.style)(this, e),
                                r = (this.style.removeProperty(e), Object(i.style)(this, e));
                            return s === r ? null : s === t && r === a ? o : o = n(t = s, a = r)
                        }
                    }(e, l)).on("end.style." + e, u(e)) : "function" == typeof n ? this.styleTween(e, function(e, n, t) {
                        var a, o, s;
                        return function() {
                            var r = Object(i.style)(this, e),
                                u = t(this),
                                l = u + "";
                            return null == u && (this.style.removeProperty(e), l = u = Object(i.style)(this, e)), r === l ? null : r === a && l === o ? s : (o = l, s = n(a = r, u))
                        }
                    }(e, l, Object(s.tweenValue)(this, "style." + e, n))).each(function(e, n) {
                        var t, a, i, s, r = "style." + n,
                            l = "end." + r;
                        return function() {
                            var c = Object(o.set)(this, e),
                                d = c.on,
                                g = null == c.value[r] ? s || (s = u(n)) : void 0;
                            d === t && i === g || (a = (t = d).copy()).on(l, i = g), c.on = a
                        }
                    }(this._id, e)) : this.styleTween(e, function(e, n, t) {
                        var a, o, s = t + "";
                        return function() {
                            var r = Object(i.style)(this, e);
                            return r === s ? null : r === a ? o : o = n(a = r, t)
                        }
                    }(e, l, n), t).on("end.style." + e, null)
                }
            },
        "./node_modules/d3-transition/src/transition/styleTween.js":
            /*!*****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/styleTween.js ***!
              \*****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t) {
                    return function(a) {
                        this.style.setProperty(e, n.call(this, a), t)
                    }
                }

                function i(e, n, t) {
                    var i, o;

                    function s() {
                        var s = n.apply(this, arguments);
                        return s !== o && (i = (o = s) && a(e, s, t)), i
                    }
                    return s._value = n, s
                }
                t.r(n), n.default = function(e, n, t) {
                    var a = "style." + (e += "");
                    if (arguments.length < 2) return (a = this.tween(a)) && a._value;
                    if (null == n) return this.tween(a, null);
                    if ("function" != typeof n) throw new Error;
                    return this.tween(a, i(e, n, null == t ? "" : t))
                }
            },
        "./node_modules/d3-transition/src/transition/text.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/text.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./tween.js */ "./node_modules/d3-transition/src/transition/tween.js");
                n.default = function(e) {
                    return this.tween("text", "function" == typeof e ? function(e) {
                        return function() {
                            var n = e(this);
                            this.textContent = null == n ? "" : n
                        }
                    }(Object(a.tweenValue)(this, "text", e)) : function(e) {
                        return function() {
                            this.textContent = e
                        }
                    }(null == e ? "" : e + ""))
                }
            },
        "./node_modules/d3-transition/src/transition/textTween.js":
            /*!****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/textTween.js ***!
              \****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return function(n) {
                        this.textContent = e.call(this, n)
                    }
                }

                function i(e) {
                    var n, t;

                    function i() {
                        var i = e.apply(this, arguments);
                        return i !== t && (n = (t = i) && a(i)), n
                    }
                    return i._value = e, i
                }
                t.r(n), n.default = function(e) {
                    var n = "text";
                    if (arguments.length < 1) return (n = this.tween(n)) && n._value;
                    if (null == e) return this.tween(n, null);
                    if ("function" != typeof e) throw new Error;
                    return this.tween(n, i(e))
                }
            },
        "./node_modules/d3-transition/src/transition/transition.js":
            /*!*****************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/transition.js ***!
              \*****************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./index.js */ "./node_modules/d3-transition/src/transition/index.js"),
                    i = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");
                n.default = function() {
                    for (var e = this._name, n = this._id, t = Object(a.newId)(), o = this._groups, s = o.length, r = 0; r < s; ++r)
                        for (var u, l = o[r], c = l.length, d = 0; d < c; ++d)
                            if (u = l[d]) {
                                var g = Object(i.get)(u, n);
                                Object(i.default)(u, e, t, d, l, {
                                    time: g.time + g.delay + g.duration,
                                    delay: 0,
                                    duration: g.duration,
                                    ease: g.ease
                                })
                            }
                    return new a.Transition(o, this._parents, e, t)
                }
            },
        "./node_modules/d3-transition/src/transition/tween.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3-transition/src/transition/tween.js ***!
              \************************************************************/
            /*! exports provided: default, tweenValue */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "tweenValue", (function() {
                    return s
                }));
                var a = t( /*! ./schedule.js */ "./node_modules/d3-transition/src/transition/schedule.js");

                function i(e, n) {
                    var t, i;
                    return function() {
                        var o = Object(a.set)(this, e),
                            s = o.tween;
                        if (s !== t)
                            for (var r = 0, u = (i = t = s).length; r < u; ++r)
                                if (i[r].name === n) {
                                    (i = i.slice()).splice(r, 1);
                                    break
                                }
                        o.tween = i
                    }
                }

                function o(e, n, t) {
                    var i, o;
                    if ("function" != typeof t) throw new Error;
                    return function() {
                        var s = Object(a.set)(this, e),
                            r = s.tween;
                        if (r !== i) {
                            o = (i = r).slice();
                            for (var u = {
                                    name: n,
                                    value: t
                                }, l = 0, c = o.length; l < c; ++l)
                                if (o[l].name === n) {
                                    o[l] = u;
                                    break
                                }
                            l === c && o.push(u)
                        }
                        s.tween = o
                    }
                }

                function s(e, n, t) {
                    var i = e._id;
                    return e.each((function() {
                            var e = Object(a.set)(this, i);
                            (e.value || (e.value = {}))[n] = t.apply(this, arguments)
                        })),
                        function(e) {
                            return Object(a.get)(e, i).value[n]
                        }
                }
                n.default = function(e, n) {
                    var t = this._id;
                    if (e += "", arguments.length < 2) {
                        for (var s, r = Object(a.get)(this.node(), t).tween, u = 0, l = r.length; u < l; ++u)
                            if ((s = r[u]).name === e) return s.value;
                        return null
                    }
                    return this.each((null == n ? i : o)(t, e, n))
                }
            },
        "./node_modules/d3-zoom/src/constant.js":
            /*!**********************************************!*\
              !*** ./node_modules/d3-zoom/src/constant.js ***!
              \**********************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return e
                    }
                }
            },
        "./node_modules/d3-zoom/src/event.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-zoom/src/event.js ***!
              \*******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t) {
                    this.target = e, this.type = n, this.transform = t
                }
                t.r(n), t.d(n, "default", (function() {
                    return a
                }))
            },
        "./node_modules/d3-zoom/src/index.js":
            /*!*******************************************!*\
              !*** ./node_modules/d3-zoom/src/index.js ***!
              \*******************************************/
            /*! exports provided: zoom, zoomTransform, zoomIdentity */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./zoom.js */ "./node_modules/d3-zoom/src/zoom.js");
                t.d(n, "zoom", (function() {
                    return a.default
                }));
                var i = t( /*! ./transform.js */ "./node_modules/d3-zoom/src/transform.js");
                t.d(n, "zoomTransform", (function() {
                    return i.default
                })), t.d(n, "zoomIdentity", (function() {
                    return i.identity
                }))
            },
        "./node_modules/d3-zoom/src/noevent.js":
            /*!*********************************************!*\
              !*** ./node_modules/d3-zoom/src/noevent.js ***!
              \*********************************************/
            /*! exports provided: nopropagation, default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "nopropagation", (function() {
                    return i
                }));
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js");

                function i() {
                    a.event.stopImmediatePropagation()
                }
                n.default = function() {
                    a.event.preventDefault(), a.event.stopImmediatePropagation()
                }
            },
        "./node_modules/d3-zoom/src/transform.js":
            /*!***********************************************!*\
              !*** ./node_modules/d3-zoom/src/transform.js ***!
              \***********************************************/
            /*! exports provided: Transform, identity, default */
            function(e, n, t) {
                "use strict";

                function a(e, n, t) {
                    this.k = e, this.x = n, this.y = t
                }
                t.r(n), t.d(n, "Transform", (function() {
                    return a
                })), t.d(n, "identity", (function() {
                    return i
                })), t.d(n, "default", (function() {
                    return o
                })), a.prototype = {
                    constructor: a,
                    scale: function(e) {
                        return 1 === e ? this : new a(this.k * e, this.x, this.y)
                    },
                    translate: function(e, n) {
                        return 0 === e & 0 === n ? this : new a(this.k, this.x + this.k * e, this.y + this.k * n)
                    },
                    apply: function(e) {
                        return [e[0] * this.k + this.x, e[1] * this.k + this.y]
                    },
                    applyX: function(e) {
                        return e * this.k + this.x
                    },
                    applyY: function(e) {
                        return e * this.k + this.y
                    },
                    invert: function(e) {
                        return [(e[0] - this.x) / this.k, (e[1] - this.y) / this.k]
                    },
                    invertX: function(e) {
                        return (e - this.x) / this.k
                    },
                    invertY: function(e) {
                        return (e - this.y) / this.k
                    },
                    rescaleX: function(e) {
                        return e.copy().domain(e.range().map(this.invertX, this).map(e.invert, e))
                    },
                    rescaleY: function(e) {
                        return e.copy().domain(e.range().map(this.invertY, this).map(e.invert, e))
                    },
                    toString: function() {
                        return "translate(" + this.x + "," + this.y + ") scale(" + this.k + ")"
                    }
                };
                var i = new a(1, 0, 0);

                function o(e) {
                    for (; !e.__zoom;)
                        if (!(e = e.parentNode)) return i;
                    return e.__zoom
                }
                o.prototype = a.prototype
            },
        "./node_modules/d3-zoom/src/zoom.js":
            /*!******************************************!*\
              !*** ./node_modules/d3-zoom/src/zoom.js ***!
              \******************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-dispatch */ "./node_modules/d3-dispatch/src/index.js"),
                    i = t( /*! d3-drag */ "./node_modules/d3-drag/src/index.js"),
                    o = t( /*! d3-interpolate */ "./node_modules/d3-interpolate/src/index.js"),
                    s = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    r = t( /*! d3-transition */ "./node_modules/d3-transition/src/index.js"),
                    u = t( /*! ./constant.js */ "./node_modules/d3-zoom/src/constant.js"),
                    l = t( /*! ./event.js */ "./node_modules/d3-zoom/src/event.js"),
                    c = t( /*! ./transform.js */ "./node_modules/d3-zoom/src/transform.js"),
                    d = t( /*! ./noevent.js */ "./node_modules/d3-zoom/src/noevent.js");

                function g() {
                    return !s.event.ctrlKey && !s.event.button
                }

                function h() {
                    var e = this;
                    return e instanceof SVGElement ? (e = e.ownerSVGElement || e).hasAttribute("viewBox") ? [
                        [(e = e.viewBox.baseVal).x, e.y],
                        [e.x + e.width, e.y + e.height]
                    ] : [
                        [0, 0],
                        [e.width.baseVal.value, e.height.baseVal.value]
                    ] : [
                        [0, 0],
                        [e.clientWidth, e.clientHeight]
                    ]
                }

                function f() {
                    return this.__zoom || c.identity
                }

                function m() {
                    return -s.event.deltaY * (1 === s.event.deltaMode ? .05 : s.event.deltaMode ? 1 : .002)
                }

                function p() {
                    return navigator.maxTouchPoints || "ontouchstart" in this
                }

                function v(e, n, t) {
                    var a = e.invertX(n[0][0]) - t[0][0],
                        i = e.invertX(n[1][0]) - t[1][0],
                        o = e.invertY(n[0][1]) - t[0][1],
                        s = e.invertY(n[1][1]) - t[1][1];
                    return e.translate(i > a ? (a + i) / 2 : Math.min(0, a) || Math.max(0, i), s > o ? (o + s) / 2 : Math.min(0, o) || Math.max(0, s))
                }
                n.default = function() {
                    var e, n, t = g,
                        y = h,
                        _ = v,
                        b = m,
                        j = p,
                        x = [0, 1 / 0],
                        w = [
                            [-1 / 0, -1 / 0],
                            [1 / 0, 1 / 0]
                        ],
                        R = 250,
                        k = o.interpolateZoom,
                        A = Object(a.dispatch)("start", "zoom", "end"),
                        O = 0;

                    function S(e) {
                        e.property("__zoom", f).on("wheel.zoom", N).on("mousedown.zoom", z).on("dblclick.zoom", F).filter(j).on("touchstart.zoom", P).on("touchmove.zoom", L).on("touchend.zoom touchcancel.zoom", I).style("touch-action", "none").style("-webkit-tap-highlight-color", "rgba(0,0,0,0)")
                    }

                    function C(e, n) {
                        return (n = Math.max(x[0], Math.min(x[1], n))) === e.k ? e : new c.Transform(n, e.x, e.y)
                    }

                    function E(e, n, t) {
                        var a = n[0] - t[0] * e.k,
                            i = n[1] - t[1] * e.k;
                        return a === e.x && i === e.y ? e : new c.Transform(e.k, a, i)
                    }

                    function B(e) {
                        return [(+e[0][0] + +e[1][0]) / 2, (+e[0][1] + +e[1][1]) / 2]
                    }

                    function M(e, n, t) {
                        e.on("start.zoom", (function() {
                            D(this, arguments).start()
                        })).on("interrupt.zoom end.zoom", (function() {
                            D(this, arguments).end()
                        })).tween("zoom", (function() {
                            var e = this,
                                a = arguments,
                                i = D(e, a),
                                o = y.apply(e, a),
                                s = null == t ? B(o) : "function" == typeof t ? t.apply(e, a) : t,
                                r = Math.max(o[1][0] - o[0][0], o[1][1] - o[0][1]),
                                u = e.__zoom,
                                l = "function" == typeof n ? n.apply(e, a) : n,
                                d = k(u.invert(s).concat(r / u.k), l.invert(s).concat(r / l.k));
                            return function(e) {
                                if (1 === e) e = l;
                                else {
                                    var n = d(e),
                                        t = r / n[2];
                                    e = new c.Transform(t, s[0] - n[0] * t, s[1] - n[1] * t)
                                }
                                i.zoom(null, e)
                            }
                        }))
                    }

                    function D(e, n, t) {
                        return !t && e.__zooming || new T(e, n)
                    }

                    function T(e, n) {
                        this.that = e, this.args = n, this.active = 0, this.extent = y.apply(e, n), this.taps = 0
                    }

                    function N() {
                        if (t.apply(this, arguments)) {
                            var e = D(this, arguments),
                                n = this.__zoom,
                                a = Math.max(x[0], Math.min(x[1], n.k * Math.pow(2, b.apply(this, arguments)))),
                                i = Object(s.mouse)(this);
                            if (e.wheel) e.mouse[0][0] === i[0] && e.mouse[0][1] === i[1] || (e.mouse[1] = n.invert(e.mouse[0] = i)), clearTimeout(e.wheel);
                            else {
                                if (n.k === a) return;
                                e.mouse = [i, n.invert(i)], Object(r.interrupt)(this), e.start()
                            }
                            Object(d.default)(), e.wheel = setTimeout(o, 150), e.zoom("mouse", _(E(C(n, a), e.mouse[0], e.mouse[1]), e.extent, w))
                        }

                        function o() {
                            e.wheel = null, e.end()
                        }
                    }

                    function z() {
                        if (!n && t.apply(this, arguments)) {
                            var e = D(this, arguments, !0),
                                a = Object(s.select)(s.event.view).on("mousemove.zoom", c, !0).on("mouseup.zoom", g, !0),
                                o = Object(s.mouse)(this),
                                u = s.event.clientX,
                                l = s.event.clientY;
                            Object(i.dragDisable)(s.event.view), Object(d.nopropagation)(), e.mouse = [o, this.__zoom.invert(o)], Object(r.interrupt)(this), e.start()
                        }

                        function c() {
                            if (Object(d.default)(), !e.moved) {
                                var n = s.event.clientX - u,
                                    t = s.event.clientY - l;
                                e.moved = n * n + t * t > O
                            }
                            e.zoom("mouse", _(E(e.that.__zoom, e.mouse[0] = Object(s.mouse)(e.that), e.mouse[1]), e.extent, w))
                        }

                        function g() {
                            a.on("mousemove.zoom mouseup.zoom", null), Object(i.dragEnable)(s.event.view, e.moved), Object(d.default)(), e.end()
                        }
                    }

                    function F() {
                        if (t.apply(this, arguments)) {
                            var e = this.__zoom,
                                n = Object(s.mouse)(this),
                                a = e.invert(n),
                                i = e.k * (s.event.shiftKey ? .5 : 2),
                                o = _(E(C(e, i), n, a), y.apply(this, arguments), w);
                            Object(d.default)(), R > 0 ? Object(s.select)(this).transition().duration(R).call(M, o, n) : Object(s.select)(this).call(S.transform, o)
                        }
                    }

                    function P() {
                        if (t.apply(this, arguments)) {
                            var n, a, i, o, u = s.event.touches,
                                l = u.length,
                                c = D(this, arguments, s.event.changedTouches.length === l);
                            for (Object(d.nopropagation)(), a = 0; a < l; ++a) i = u[a], o = [o = Object(s.touch)(this, u, i.identifier), this.__zoom.invert(o), i.identifier], c.touch0 ? c.touch1 || c.touch0[2] === o[2] || (c.touch1 = o, c.taps = 0) : (c.touch0 = o, n = !0, c.taps = 1 + !!e);
                            e && (e = clearTimeout(e)), n && (c.taps < 2 && (e = setTimeout((function() {
                                e = null
                            }), 500)), Object(r.interrupt)(this), c.start())
                        }
                    }

                    function L() {
                        if (this.__zooming) {
                            var n, t, a, i, o = D(this, arguments),
                                r = s.event.changedTouches,
                                u = r.length;
                            for (Object(d.default)(), e && (e = clearTimeout(e)), o.taps = 0, n = 0; n < u; ++n) t = r[n], a = Object(s.touch)(this, r, t.identifier), o.touch0 && o.touch0[2] === t.identifier ? o.touch0[0] = a : o.touch1 && o.touch1[2] === t.identifier && (o.touch1[0] = a);
                            if (t = o.that.__zoom, o.touch1) {
                                var l = o.touch0[0],
                                    c = o.touch0[1],
                                    g = o.touch1[0],
                                    h = o.touch1[1],
                                    f = (f = g[0] - l[0]) * f + (f = g[1] - l[1]) * f,
                                    m = (m = h[0] - c[0]) * m + (m = h[1] - c[1]) * m;
                                t = C(t, Math.sqrt(f / m)), a = [(l[0] + g[0]) / 2, (l[1] + g[1]) / 2], i = [(c[0] + h[0]) / 2, (c[1] + h[1]) / 2]
                            } else {
                                if (!o.touch0) return;
                                a = o.touch0[0], i = o.touch0[1]
                            }
                            o.zoom("touch", _(E(t, a, i), o.extent, w))
                        }
                    }

                    function I() {
                        if (this.__zooming) {
                            var e, t, a = D(this, arguments),
                                i = s.event.changedTouches,
                                o = i.length;
                            for (Object(d.nopropagation)(), n && clearTimeout(n), n = setTimeout((function() {
                                    n = null
                                }), 500), e = 0; e < o; ++e) t = i[e], a.touch0 && a.touch0[2] === t.identifier ? delete a.touch0 : a.touch1 && a.touch1[2] === t.identifier && delete a.touch1;
                            if (a.touch1 && !a.touch0 && (a.touch0 = a.touch1, delete a.touch1), a.touch0) a.touch0[1] = this.__zoom.invert(a.touch0[0]);
                            else if (a.end(), 2 === a.taps) {
                                var r = Object(s.select)(this).on("dblclick.zoom");
                                r && r.apply(this, arguments)
                            }
                        }
                    }
                    return S.transform = function(e, n, t) {
                        var a = e.selection ? e.selection() : e;
                        a.property("__zoom", f), e !== a ? M(e, n, t) : a.interrupt().each((function() {
                            D(this, arguments).start().zoom(null, "function" == typeof n ? n.apply(this, arguments) : n).end()
                        }))
                    }, S.scaleBy = function(e, n, t) {
                        S.scaleTo(e, (function() {
                            var e = this.__zoom.k,
                                t = "function" == typeof n ? n.apply(this, arguments) : n;
                            return e * t
                        }), t)
                    }, S.scaleTo = function(e, n, t) {
                        S.transform(e, (function() {
                            var e = y.apply(this, arguments),
                                a = this.__zoom,
                                i = null == t ? B(e) : "function" == typeof t ? t.apply(this, arguments) : t,
                                o = a.invert(i),
                                s = "function" == typeof n ? n.apply(this, arguments) : n;
                            return _(E(C(a, s), i, o), e, w)
                        }), t)
                    }, S.translateBy = function(e, n, t) {
                        S.transform(e, (function() {
                            return _(this.__zoom.translate("function" == typeof n ? n.apply(this, arguments) : n, "function" == typeof t ? t.apply(this, arguments) : t), y.apply(this, arguments), w)
                        }))
                    }, S.translateTo = function(e, n, t, a) {
                        S.transform(e, (function() {
                            var e = y.apply(this, arguments),
                                i = this.__zoom,
                                o = null == a ? B(e) : "function" == typeof a ? a.apply(this, arguments) : a;
                            return _(c.identity.translate(o[0], o[1]).scale(i.k).translate("function" == typeof n ? -n.apply(this, arguments) : -n, "function" == typeof t ? -t.apply(this, arguments) : -t), e, w)
                        }), a)
                    }, T.prototype = {
                        start: function() {
                            return 1 == ++this.active && (this.that.__zooming = this, this.emit("start")), this
                        },
                        zoom: function(e, n) {
                            return this.mouse && "mouse" !== e && (this.mouse[1] = n.invert(this.mouse[0])), this.touch0 && "touch" !== e && (this.touch0[1] = n.invert(this.touch0[0])), this.touch1 && "touch" !== e && (this.touch1[1] = n.invert(this.touch1[0])), this.that.__zoom = n, this.emit("zoom"), this
                        },
                        end: function() {
                            return 0 == --this.active && (delete this.that.__zooming, this.emit("end")), this
                        },
                        emit: function(e) {
                            Object(s.customEvent)(new l.default(S, e, this.that.__zoom), A.apply, A, [e, this.that, this.args])
                        }
                    }, S.wheelDelta = function(e) {
                        return arguments.length ? (b = "function" == typeof e ? e : Object(u.default)(+e), S) : b
                    }, S.filter = function(e) {
                        return arguments.length ? (t = "function" == typeof e ? e : Object(u.default)(!!e), S) : t
                    }, S.touchable = function(e) {
                        return arguments.length ? (j = "function" == typeof e ? e : Object(u.default)(!!e), S) : j
                    }, S.extent = function(e) {
                        return arguments.length ? (y = "function" == typeof e ? e : Object(u.default)([
                            [+e[0][0], +e[0][1]],
                            [+e[1][0], +e[1][1]]
                        ]), S) : y
                    }, S.scaleExtent = function(e) {
                        return arguments.length ? (x[0] = +e[0], x[1] = +e[1], S) : [x[0], x[1]]
                    }, S.translateExtent = function(e) {
                        return arguments.length ? (w[0][0] = +e[0][0], w[1][0] = +e[1][0], w[0][1] = +e[0][1], w[1][1] = +e[1][1], S) : [
                            [w[0][0], w[0][1]],
                            [w[1][0], w[1][1]]
                        ]
                    }, S.constrain = function(e) {
                        return arguments.length ? (_ = e, S) : _
                    }, S.duration = function(e) {
                        return arguments.length ? (R = +e, S) : R
                    }, S.interpolate = function(e) {
                        return arguments.length ? (k = e, S) : k
                    }, S.on = function() {
                        var e = A.on.apply(A, arguments);
                        return e === A ? S : e
                    }, S.clickDistance = function(e) {
                        return arguments.length ? (O = (e = +e) * e, S) : Math.sqrt(O)
                    }, S
                }
            },
        "./node_modules/d3plus-common/es/index.js":
            /*!************************************************!*\
              !*** ./node_modules/d3plus-common/es/index.js ***!
              \************************************************/
            /*! exports provided: accessor, assign, attrize, BaseClass, closest, configPrep, constant, elem, findLocale, isObject, merge, parseSides, prefix, RESET, stylize, unique, uuid */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./src/accessor */ "./node_modules/d3plus-common/es/src/accessor.js");
                t.d(n, "accessor", (function() {
                    return a.default
                }));
                var i = t( /*! ./src/assign */ "./node_modules/d3plus-common/es/src/assign.js");
                t.d(n, "assign", (function() {
                    return i.default
                }));
                var o = t( /*! ./src/attrize */ "./node_modules/d3plus-common/es/src/attrize.js");
                t.d(n, "attrize", (function() {
                    return o.default
                }));
                var s = t( /*! ./src/BaseClass */ "./node_modules/d3plus-common/es/src/BaseClass.js");
                t.d(n, "BaseClass", (function() {
                    return s.default
                }));
                var r = t( /*! ./src/closest */ "./node_modules/d3plus-common/es/src/closest.js");
                t.d(n, "closest", (function() {
                    return r.default
                }));
                var u = t( /*! ./src/configPrep */ "./node_modules/d3plus-common/es/src/configPrep.js");
                t.d(n, "configPrep", (function() {
                    return u.default
                }));
                var l = t( /*! ./src/constant */ "./node_modules/d3plus-common/es/src/constant.js");
                t.d(n, "constant", (function() {
                    return l.default
                }));
                var c = t( /*! ./src/elem */ "./node_modules/d3plus-common/es/src/elem.js");
                t.d(n, "elem", (function() {
                    return c.default
                }));
                var d = t( /*! ./src/findLocale */ "./node_modules/d3plus-common/es/src/findLocale.js");
                t.d(n, "findLocale", (function() {
                    return d.default
                }));
                var g = t( /*! ./src/isObject */ "./node_modules/d3plus-common/es/src/isObject.js");
                t.d(n, "isObject", (function() {
                    return g.default
                }));
                var h = t( /*! ./src/merge */ "./node_modules/d3plus-common/es/src/merge.js");
                t.d(n, "merge", (function() {
                    return h.default
                }));
                var f = t( /*! ./src/parseSides */ "./node_modules/d3plus-common/es/src/parseSides.js");
                t.d(n, "parseSides", (function() {
                    return f.default
                }));
                var m = t( /*! ./src/prefix */ "./node_modules/d3plus-common/es/src/prefix.js");
                t.d(n, "prefix", (function() {
                    return m.default
                }));
                var p = t( /*! ./src/RESET */ "./node_modules/d3plus-common/es/src/RESET.js");
                t.d(n, "RESET", (function() {
                    return p.default
                }));
                var v = t( /*! ./src/stylize */ "./node_modules/d3plus-common/es/src/stylize.js");
                t.d(n, "stylize", (function() {
                    return v.default
                }));
                var y = t( /*! ./src/unique */ "./node_modules/d3plus-common/es/src/unique.js");
                t.d(n, "unique", (function() {
                    return y.default
                }));
                var _ = t( /*! ./src/uuid */ "./node_modules/d3plus-common/es/src/uuid.js");
                t.d(n, "uuid", (function() {
                    return _.default
                }))
            },
        "./node_modules/d3plus-common/es/src/BaseClass.js":
            /*!********************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/BaseClass.js ***!
              \********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "default", (function() {
                    return g
                }));
                var a = t( /*! ./assign */ "./node_modules/d3plus-common/es/src/assign.js"),
                    i = t( /*! ./findLocale */ "./node_modules/d3plus-common/es/src/findLocale.js"),
                    o = t( /*! ./isObject */ "./node_modules/d3plus-common/es/src/isObject.js"),
                    s = t( /*! ./uuid */ "./node_modules/d3plus-common/es/src/uuid.js"),
                    r = t( /*! ./RESET */ "./node_modules/d3plus-common/es/src/RESET.js"),
                    u = t( /*! ./locales/index */ "./node_modules/d3plus-common/es/src/locales/index.js");

                function l(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function c(e, n) {
                    if (Object(o.default)(e))
                        for (var t in e)
                            if ({}.hasOwnProperty.call(e, t) && !t.startsWith("_")) {
                                var a = n && Object(o.default)(n) ? n[t] : void 0;
                                e[t] === r.default ? a ? e[t] = a : delete e[t] : Object(o.default)(e[t]) && c(e[t], a)
                            }
                }

                function d(e) {
                    var n = [];
                    do {
                        n = n.concat(Object.getOwnPropertyNames(e)), e = Object.getPrototypeOf(e)
                    } while (e && e !== Object.prototype);
                    return n.filter((function(e) {
                        return 0 !== e.indexOf("_") && !["config", "constructor", "render"].includes(e)
                    }))
                }
                var g = function() {
                    function e() {
                        var n = this;
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, e), this._locale = "en-US", this._on = {}, this._translate = function(e) {
                            var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : n._locale,
                                a = u.default[t];
                            return a && a[e] ? a[e] : e
                        }, this._uuid = Object(s.default)()
                    }
                    var n, t, g;
                    return n = e, (t = [{
                        key: "config",
                        value: function(e) {
                            var n = this;
                            if (!this._configDefault) {
                                var t = {};
                                d(this.__proto__).forEach((function(e) {
                                    var i = n[e]();
                                    t[e] = Object(o.default)(i) ? Object(a.default)({}, i) : i
                                })), this._configDefault = t
                            }
                            if (arguments.length) {
                                for (var i in e)
                                    if ({}.hasOwnProperty.call(e, i) && i in this) {
                                        var s = e[i];
                                        s === r.default ? "on" === i ? this._on = this._configDefault[i] : this[i](this._configDefault[i]) : (c(s, this._configDefault[i]), this[i](s))
                                    }
                                return this
                            }
                            var u = {};
                            return d(this.__proto__).forEach((function(e) {
                                u[e] = n[e]()
                            })), u
                        }
                    }, {
                        key: "locale",
                        value: function(e) {
                            return arguments.length ? (this._locale = Object(i.default)(e), this) : this._locale
                        }
                    }, {
                        key: "on",
                        value: function(e, n) {
                            return 2 === arguments.length ? (this._on[e] = n, this) : arguments.length ? "string" == typeof e ? this._on[e] : (this._on = Object.assign({}, this._on, e), this) : this._on
                        }
                    }, {
                        key: "translate",
                        value: function(e) {
                            return arguments.length ? (this._translate = e, this) : this._translate
                        }
                    }, {
                        key: "shapeConfig",
                        value: function(e) {
                            return arguments.length ? (this._shapeConfig = Object(a.default)(this._shapeConfig, e), this) : this._shapeConfig
                        }
                    }]) && l(n.prototype, t), g && l(n, g), e
                }()
            },
        "./node_modules/d3plus-common/es/src/RESET.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/RESET.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = "D3PLUS-COMMON-RESET"
            },
        "./node_modules/d3plus-common/es/src/accessor.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/accessor.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e, n) {
                    return void 0 === n ? function(n) {
                        return n[e]
                    } : function(t) {
                        return void 0 === t[e] ? n : t[e]
                    }
                }
            },
        "./node_modules/d3plus-common/es/src/assign.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/assign.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./isObject */ "./node_modules/d3plus-common/es/src/isObject.js");

                function i(e) {
                    return "undefined" == typeof window || e !== window && e !== document
                }
                n.default = function e() {
                    for (var n = arguments, t = arguments.length <= 0 ? void 0 : arguments[0], o = function(o) {
                            var s = o < 0 || n.length <= o ? void 0 : n[o];
                            Object.keys(s).forEach((function(n) {
                                var o = s[n];
                                Object(a.default)(o) && i(o) ? t.hasOwnProperty(n) && Object(a.default)(t[n]) ? t[n] = e({}, t[n], o) : t[n] = e({}, o) : Array.isArray(o) ? t[n] = o.slice() : t[n] = o
                            }))
                        }, s = 1; s < arguments.length; s++) o(s);
                    return t
                }
            },
        "./node_modules/d3plus-common/es/src/attrize.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/attrize.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {};
                    for (var t in n)({}).hasOwnProperty.call(n, t) && e.attr(t, n[t])
                }
            },
        "./node_modules/d3plus-common/es/src/closest.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/closest.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : [];
                    if (n && n instanceof Array && n.length) return n.reduce((function(n, t) {
                        return Math.abs(t - e) < Math.abs(n - e) ? t : n
                    }))
                }
            },
        "./node_modules/d3plus-common/es/src/configPrep.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/configPrep.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return (a = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function i() {
                    var e = this,
                        n = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : this._shapeConfig,
                        t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "shape",
                        i = arguments.length > 2 && void 0 !== arguments[2] && arguments[2],
                        o = {
                            duration: this._duration,
                            on: {}
                        },
                        s = function(n) {
                            return function(t, a, i) {
                                for (var o; t.__d3plus__;) o && (t.__d3plusParent__ = o), o = t, a = t.i, t = t.data || t.feature;
                                return n.bind(e)(t, a, i || o)
                            }
                        },
                        r = function(e, n) {
                            for (var a in n)({}.hasOwnProperty.call(n, a) && !a.includes(".") || a.includes(".".concat(t))) && (e.on[a] = s(n[a]))
                        },
                        u = function e(n) {
                            return n.map((function(n) {
                                return n instanceof Array ? e(n) : "object" === a(n) ? l({}, n) : "function" == typeof n ? s(n) : n
                            }))
                        },
                        l = function e(n, t) {
                            for (var i in t)({}).hasOwnProperty.call(t, i) && ("on" === i ? r(n, t[i]) : "function" == typeof t[i] ? n[i] = s(t[i]) : t[i] instanceof Array ? n[i] = u(t[i]) : "object" === a(t[i]) ? (n[i] = {
                                on: {}
                            }, e(n[i], t[i])) : n[i] = t[i])
                        };
                    return l(o, n), this._on && r(o, this._on), i && n[i] && (l(o, n[i]), n[i].on && r(o, n[i].on)), o
                }
                t.r(n), t.d(n, "default", (function() {
                    return i
                }))
            },
        "./node_modules/d3plus-common/es/src/constant.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/constant.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return function() {
                        return e
                    }
                }
            },
        "./node_modules/d3plus-common/es/src/elem.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/elem.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! d3-transition */ "./node_modules/d3-transition/src/index.js"),
                    o = t( /*! ./attrize */ "./node_modules/d3plus-common/es/src/attrize.js");
                n.default = function(e, n) {
                    n = Object.assign({}, {
                        condition: !0,
                        enter: {},
                        exit: {},
                        parent: Object(a.select)("body"),
                        transition: Object(i.transition)().duration(0),
                        update: {}
                    }, n);
                    var t = /\.([^#]+)/g.exec(e),
                        s = /#([^\.]+)/g.exec(e),
                        r = /^([^.^#]+)/g.exec(e)[1],
                        u = n.parent.selectAll(e.includes(":") ? e.split(":")[1] : e).data(n.condition ? [null] : []),
                        l = u.enter().append(r).call(o.default, n.enter);
                    s && l.attr("id", s[1]), t && l.attr("class", t[1]), u.exit().transition(n.transition).call(o.default, n.exit).remove();
                    var c = l.merge(u);
                    return c.transition(n.transition).call(o.default, n.update), c
                }
            },
        "./node_modules/d3plus-common/es/src/findLocale.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/findLocale.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! windows-locale */ "./node_modules/windows-locale/index.json"),
                    i = t( /*! iso639-codes */ "./node_modules/iso639-codes/index.json");

                function o(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }
                var s = [],
                    r = Object.keys(i);
                Object.keys(a).map((function(e) {
                    var n, t = a[e],
                        u = r.find((function(e) {
                            return e.toLowerCase() === t.language.toLowerCase()
                        }));
                    t.location && u && s.push((o(n = {}, "name", t.language), o(n, "location", t.location), o(n, "tag", t.tag), o(n, "lcid", t.id), o(n, "iso639-2", i[u]["iso639-2"]), o(n, "iso639-1", i[u]["iso639-1"]), n))
                }));
                var u = {
                    ar: "ar-SA",
                    ca: "ca-ES",
                    da: "da-DK",
                    en: "en-US",
                    ko: "ko-KR",
                    pa: "pa-IN",
                    pt: "pt-BR",
                    sv: "sv-SE"
                };
                n.default = function(e) {
                    if ("string" != typeof e || 5 === e.length) return e;
                    if (u[e]) return u[e];
                    var n = s.filter((function(n) {
                        return n["iso639-1"] === e
                    }));
                    return n.length ? 1 === n.length ? n[0].tag : n.find((function(n) {
                        return n.tag === "".concat(e, "-").concat(e.toUpperCase())
                    })) ? "".concat(e, "-").concat(e.toUpperCase()) : n[0].tag : e
                }
            },
        "./node_modules/d3plus-common/es/src/isObject.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/isObject.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return (a = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }
                t.r(n), n.default = function(e) {
                    return !(!e || "object" !== a(e) || "undefined" != typeof window && (e === window || e === window.document || e instanceof Element) || Array.isArray(e))
                }
            },
        "./node_modules/d3plus-common/es/src/locales/es-ES.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/locales/es-ES.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = {
                    and: "y",
                    Back: "Atrás",
                    "Click to Expand": "Clic para Ampliar",
                    "Click to Hide": "Clic para Ocultar",
                    "Click to Highlight": "Clic para Resaltar",
                    "Click to Reset": "Clic para Restablecer",
                    Download: "Descargar",
                    "Loading Visualization": "Cargando Visualización",
                    "No Data Available": "Datos No Disponibles",
                    "Powered by D3plus": "Funciona con D3plus",
                    Share: "Porcentaje",
                    "Shift+Click to Hide": "Mayús+Clic para Ocultar",
                    Total: "Total",
                    Values: "Valores"
                }
            },
        "./node_modules/d3plus-common/es/src/locales/index.js":
            /*!************************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/locales/index.js ***!
              \************************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./es-ES */ "./node_modules/d3plus-common/es/src/locales/es-ES.js");
                n.default = {
                    "es-ES": a.default
                }
            },
        "./node_modules/d3plus-common/es/src/merge.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/merge.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-array */ "./node_modules/d3-array/src/index.js"),
                    i = t( /*! d3-collection */ "./node_modules/d3-collection/src/index.js"),
                    o = t( /*! ./unique */ "./node_modules/d3plus-common/es/src/unique.js");
                n.default = function e(n) {
                    var t = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {},
                        s = Object(o.default)(Object(a.merge)(n.map((function(e) {
                            return Object(i.keys)(e)
                        })))),
                        r = {};
                    return s.forEach((function(i) {
                        var s, u = n.map((function(e) {
                            return e[i]
                        }));
                        if (t[i]) s = t[i](u);
                        else {
                            var l = u.map((function(e) {
                                return e || !1 === e ? e.constructor : e
                            })).filter((function(e) {
                                return void 0 !== e
                            }));
                            l.length ? l.indexOf(Array) >= 0 ? (s = Object(a.merge)(u.map((function(e) {
                                return e instanceof Array ? e : [e]
                            }))), 1 === (s = Object(o.default)(s)).length && (s = s[0])) : l.indexOf(String) >= 0 ? 1 === (s = Object(o.default)(u)).length && (s = s[0]) : l.indexOf(Number) >= 0 ? s = Object(a.sum)(u) : l.indexOf(Object) >= 0 ? s = 1 === (s = Object(o.default)(u.filter((function(e) {
                                return e
                            })))).length ? s[0] : e(s) : 1 === (s = Object(o.default)(u.filter((function(e) {
                                return void 0 !== e
                            })))).length && (s = s[0]) : s = void 0
                        }
                        r[i] = s
                    })), r
                }
            },
        "./node_modules/d3plus-common/es/src/parseSides.js":
            /*!*********************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/parseSides.js ***!
              \*********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    var n;
                    return 1 === (n = "number" == typeof e ? [e] : e.split(/\s+/)).length ? n = [n[0], n[0], n[0], n[0]] : 2 === n.length ? n = n.concat(n) : 3 === n.length && n.push(n[1]), ["top", "right", "bottom", "left"].reduce((function(e, t, a) {
                        var i = parseFloat(n[a]);
                        return e[t] = i || 0, e
                    }), {})
                }
            },
        "./node_modules/d3plus-common/es/src/prefix.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/prefix.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function() {
                    return "-webkit-transform" in document.body.style ? "-webkit-" : "-moz-transform" in document.body.style ? "-moz-" : "-ms-transform" in document.body.style ? "-ms-" : "-o-transform" in document.body.style ? "-o-" : ""
                }
            },
        "./node_modules/d3plus-common/es/src/stylize.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/stylize.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {};
                    for (var t in n)({}).hasOwnProperty.call(n, t) && e.style(t, n[t])
                }
            },
        "./node_modules/d3plus-common/es/src/unique.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/unique.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return e.filter((function(e, n, t) {
                        return t.indexOf(e) === n
                    }))
                }
            },
        "./node_modules/d3plus-common/es/src/uuid.js":
            /*!***************************************************!*\
              !*** ./node_modules/d3plus-common/es/src/uuid.js ***!
              \***************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a() {
                    return Math.floor(65536 * (1 + Math.random())).toString(16).substring(1)
                }
                t.r(n), n.default = function() {
                    return "".concat(a()).concat(a(), "-").concat(a(), "-").concat(a(), "-").concat(a(), "-").concat(a()).concat(a()).concat(a())
                }
            },
        "./node_modules/d3plus-text/es/index.js":
            /*!**********************************************!*\
              !*** ./node_modules/d3plus-text/es/index.js ***!
              \**********************************************/
            /*! exports provided: fontExists, rtl, stringify, strip, TextBox, textSplit, textWidth, textWrap, titleCase, trim, trimLeft, trimRight */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./src/fontExists */ "./node_modules/d3plus-text/es/src/fontExists.js");
                t.d(n, "fontExists", (function() {
                    return a.default
                }));
                var i = t( /*! ./src/rtl */ "./node_modules/d3plus-text/es/src/rtl.js");
                t.d(n, "rtl", (function() {
                    return i.default
                }));
                var o = t( /*! ./src/stringify */ "./node_modules/d3plus-text/es/src/stringify.js");
                t.d(n, "stringify", (function() {
                    return o.default
                }));
                var s = t( /*! ./src/strip */ "./node_modules/d3plus-text/es/src/strip.js");
                t.d(n, "strip", (function() {
                    return s.default
                }));
                var r = t( /*! ./src/TextBox */ "./node_modules/d3plus-text/es/src/TextBox.js");
                t.d(n, "TextBox", (function() {
                    return r.default
                }));
                var u = t( /*! ./src/textSplit */ "./node_modules/d3plus-text/es/src/textSplit.js");
                t.d(n, "textSplit", (function() {
                    return u.default
                }));
                var l = t( /*! ./src/textWidth */ "./node_modules/d3plus-text/es/src/textWidth.js");
                t.d(n, "textWidth", (function() {
                    return l.default
                }));
                var c = t( /*! ./src/textWrap */ "./node_modules/d3plus-text/es/src/textWrap.js");
                t.d(n, "textWrap", (function() {
                    return c.default
                }));
                var d = t( /*! ./src/titleCase */ "./node_modules/d3plus-text/es/src/titleCase.js");
                t.d(n, "titleCase", (function() {
                    return d.default
                }));
                var g = t( /*! ./src/trim */ "./node_modules/d3plus-text/es/src/trim.js");
                t.d(n, "trim", (function() {
                    return g.trim
                })), t.d(n, "trimLeft", (function() {
                    return g.trimLeft
                })), t.d(n, "trimRight", (function() {
                    return g.trimRight
                }))
            },
        "./node_modules/d3plus-text/es/src/TextBox.js":
            /*!****************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/TextBox.js ***!
              \****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "default", (function() {
                    return b
                }));
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! d3-transition */ "./node_modules/d3-transition/src/index.js"),
                    o = t( /*! d3-array */ "./node_modules/d3-array/src/index.js"),
                    s = t( /*! d3plus-common */ "./node_modules/d3plus-common/es/index.js"),
                    r = t( /*! ./fontExists */ "./node_modules/d3plus-text/es/src/fontExists.js"),
                    u = t( /*! ./rtl */ "./node_modules/d3plus-text/es/src/rtl.js"),
                    l = t( /*! ./strip */ "./node_modules/d3plus-text/es/src/strip.js"),
                    c = t( /*! ./textSplit */ "./node_modules/d3plus-text/es/src/textSplit.js"),
                    d = t( /*! ./textWidth */ "./node_modules/d3plus-text/es/src/textWidth.js"),
                    g = t( /*! ./textWrap */ "./node_modules/d3plus-text/es/src/textWrap.js"),
                    h = t( /*! ./trim */ "./node_modules/d3plus-text/es/src/trim.js");

                function f(e) {
                    return (f = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function m(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function p(e, n) {
                    return !n || "object" !== f(n) && "function" != typeof n ? function(e) {
                        if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                        return e
                    }(e) : n
                }

                function v(e) {
                    return (v = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function y(e, n) {
                    return (y = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }
                var _ = {
                        i: "font-style: italic;",
                        em: "font-style: italic;",
                        b: "font-weight: bold;",
                        strong: "font-weight: bold;"
                    },
                    b = function(e) {
                        function n() {
                            var e;
                            return function(e, n) {
                                if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                            }(this, n), (e = p(this, v(n).call(this)))._ariaHidden = Object(s.constant)("false"), e._delay = 0, e._duration = 0, e._ellipsis = function(e, n) {
                                return n ? "".concat(e.replace(/\.|,$/g, ""), "...") : ""
                            }, e._fontColor = Object(s.constant)("black"), e._fontFamily = Object(s.constant)(["Roboto", "Helvetica Neue", "HelveticaNeue", "Helvetica", "Arial", "sans-serif"]), e._fontMax = Object(s.constant)(50), e._fontMin = Object(s.constant)(8), e._fontOpacity = Object(s.constant)(1), e._fontResize = Object(s.constant)(!1), e._fontSize = Object(s.constant)(10), e._fontWeight = Object(s.constant)(400), e._height = Object(s.accessor)("height", 200), e._html = _, e._id = function(e, n) {
                                return e.id || "".concat(n)
                            }, e._lineHeight = function(n, t) {
                                return 1.2 * e._fontSize(n, t)
                            }, e._maxLines = Object(s.constant)(null), e._on = {}, e._overflow = Object(s.constant)(!1), e._padding = Object(s.constant)(0), e._pointerEvents = Object(s.constant)("auto"), e._rotate = Object(s.constant)(0), e._rotateAnchor = function(e) {
                                return [e.w / 2, e.h / 2]
                            }, e._split = c.default, e._text = Object(s.accessor)("text"), e._textAnchor = Object(s.constant)("start"), e._verticalAlign = Object(s.constant)("top"), e._width = Object(s.accessor)("width", 200), e._x = Object(s.accessor)("x", 0), e._y = Object(s.accessor)("y", 0), e
                        }
                        var t, f, b;
                        return function(e, n) {
                            if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                            e.prototype = Object.create(n && n.prototype, {
                                constructor: {
                                    value: e,
                                    writable: !0,
                                    configurable: !0
                                }
                            }), n && y(e, n)
                        }(n, e), t = n, (f = [{
                            key: "render",
                            value: function(e) {
                                var n = this;
                                void 0 === this._select && this.select(Object(a.select)("body").append("svg").style("width", "".concat(window.innerWidth, "px")).style("height", "".concat(window.innerHeight, "px")).node());
                                var t = this,
                                    c = this._select.selectAll(".d3plus-textBox").data(this._data.reduce((function(e, a, i) {
                                        var u = n._text(a, i);
                                        if (void 0 === u) return e;
                                        u = Object(h.trim)(u);
                                        var l, c, f = n._fontResize(a, i),
                                            m = n._lineHeight(a, i) / n._fontSize(a, i),
                                            p = f ? n._fontMax(a, i) : n._fontSize(a, i),
                                            v = f ? p * m : n._lineHeight(a, i),
                                            y = 1,
                                            _ = [],
                                            b = {
                                                "font-family": Object(r.default)(n._fontFamily(a, i)),
                                                "font-size": p,
                                                "font-weight": n._fontWeight(a, i),
                                                "line-height": v
                                            },
                                            j = Object(s.parseSides)(n._padding(a, i)),
                                            x = n._height(a, i) - (j.top + j.bottom),
                                            w = n._width(a, i) - (j.left + j.right),
                                            R = Object(g.default)().fontFamily(b["font-family"]).fontSize(p).fontWeight(b["font-weight"]).lineHeight(v).maxLines(n._maxLines(a, i)).height(x).overflow(n._overflow(a, i)).width(w).split(n._split),
                                            k = n._fontMax(a, i),
                                            A = n._fontMin(a, i),
                                            O = n._verticalAlign(a, i),
                                            S = n._split(u, i);
                                        if (w > A && (x > v || f && x > A * m)) {
                                            if (f) {
                                                l = Object(d.default)(S, b);
                                                var C = 1.165 + w / x * .1,
                                                    E = w * x,
                                                    B = Object(o.max)(l),
                                                    M = Object(o.sum)(l, (function(e) {
                                                        return e * v
                                                    })) * C;
                                                if (B > w || M > E) {
                                                    var D = Math.sqrt(E / M),
                                                        T = w / B,
                                                        N = Object(o.min)([D, T]);
                                                    p = Math.floor(p * N)
                                                }
                                                var z = Math.floor(.8 * x);
                                                p > z && (p = z)
                                            }! function e() {
                                                var n = function() {
                                                    y < 1 ? _ = [t._ellipsis("", y)] : _[y - 1] = t._ellipsis(_[y - 1], y)
                                                };
                                                if (p = Object(o.max)([p, A]), p = Object(o.min)([p, k]), f && (v = p * m, R.fontSize(p).lineHeight(v), b["font-size"] = p, b["line-height"] = v), c = R(u), _ = c.lines.filter((function(e) {
                                                        return "" !== e
                                                    })), y = _.length, c.truncated)
                                                    if (f) {
                                                        if (--p < A) return p = A, void n();
                                                        e()
                                                    } else n()
                                            }()
                                        }
                                        if (_.length) {
                                            var F = y * v,
                                                P = n._rotate(a, i),
                                                L = 0 === P ? "top" === O ? 0 : "middle" === O ? x / 2 - F / 2 : x - F : 0;
                                            L -= .1 * v, e.push({
                                                aH: n._ariaHidden(a, i),
                                                data: a,
                                                i: i,
                                                lines: _,
                                                fC: n._fontColor(a, i),
                                                fF: b["font-family"],
                                                fO: n._fontOpacity(a, i),
                                                fW: b["font-weight"],
                                                id: n._id(a, i),
                                                tA: n._textAnchor(a, i),
                                                vA: n._verticalAlign(a, i),
                                                widths: c.widths,
                                                fS: p,
                                                lH: v,
                                                w: w,
                                                h: x,
                                                r: P,
                                                x: n._x(a, i) + j.left,
                                                y: n._y(a, i) + L + j.top
                                            })
                                        }
                                        return e
                                    }), []), (function(e) {
                                        return n._id(e.data, e.i)
                                    })),
                                    f = Object(i.transition)().duration(this._duration);

                                function m(e) {
                                    e.attr("transform", (function(e, n) {
                                        var a = t._rotateAnchor(e, n);
                                        return "translate(".concat(e.x, ", ").concat(e.y, ") rotate(").concat(e.r, ", ").concat(a[0], ", ").concat(a[1], ")")
                                    }))
                                }
                                0 === this._duration ? c.exit().remove() : (c.exit().transition().delay(this._duration).remove(), c.exit().selectAll("text").transition(f).attr("opacity", 0).style("opacity", 0));
                                var p = c.enter().append("g").attr("class", "d3plus-textBox").attr("id", (function(e) {
                                        return "d3plus-textBox-".concat(Object(l.default)(e.id))
                                    })).call(m).merge(c),
                                    v = Object(u.default)();
                                p.style("pointer-events", (function(e) {
                                    return n._pointerEvents(e.data, e.i)
                                })).each((function(e) {
                                    function n(e) {
                                        e[t._html ? "html" : "text"]((function(e) {
                                            return Object(h.trimRight)(e).replace(/&([^\;&]*)/g, (function(e, n) {
                                                return "amp" === n ? e : "&amp;".concat(n)
                                            })).replace(/<([^A-z^/]+)/g, (function(e, n) {
                                                return "&lt;".concat(n)
                                            })).replace(/<$/g, "&lt;").replace(/(<[^>^\/]+>)([^<^>]+)$/g, (function(e, n, t) {
                                                return "".concat(n).concat(t).concat(n.replace("<", "</"))
                                            })).replace(/^([^<^>]+)(<\/[^>]+>)/g, (function(e, n, t) {
                                                return "".concat(t.replace("</", "<")).concat(n).concat(t)
                                            })).replace(/<([A-z]+)[^>]*>([^<^>]+)<\/[^>]+>/g, (function(e, n, a) {
                                                var i = t._html[n] ? '<tspan style="'.concat(t._html[n], '">') : "";
                                                return "".concat(i.length ? i : "").concat(a).concat(i.length ? "</tspan>" : "")
                                            }))
                                        }))
                                    }

                                    function i(n) {
                                        n.attr("aria-hidden", e.aH).attr("dir", v ? "rtl" : "ltr").attr("fill", e.fC).attr("text-anchor", e.tA).attr("font-family", e.fF).style("font-family", e.fF).attr("font-size", "".concat(e.fS, "px")).style("font-size", "".concat(e.fS, "px")).attr("font-weight", e.fW).style("font-weight", e.fW).attr("x", "".concat("middle" === e.tA ? e.w / 2 : v ? "start" === e.tA ? e.w : 0 : "end" === e.tA ? e.w : 2 * Math.sin(Math.PI * e.r / 180), "px")).attr("y", (function(n, t) {
                                            return 0 === e.r || "top" === e.vA ? "".concat((t + 1) * e.lH - (e.lH - e.fS), "px") : "middle" === e.vA ? "".concat((e.h + e.fS) / 2 - (e.lH - e.fS) + (t - e.lines.length / 2 + .5) * e.lH, "px") : "".concat(e.h - 2 * (e.lH - e.fS) - (e.lines.length - (t + 1)) * e.lH + 2 * Math.cos(Math.PI * e.r / 180), "px")
                                        }))
                                    }
                                    var o = Object(a.select)(this).selectAll("text").data(e.lines);
                                    0 === t._duration ? (o.call(n).call(i), o.exit().remove(), o.enter().append("text").attr("dominant-baseline", "alphabetic").style("baseline-shift", "0%").attr("unicode-bidi", "bidi-override").call(n).call(i).attr("opacity", e.fO).style("opacity", e.fO)) : (o.call(n).transition(f).call(i), o.exit().transition(f).attr("opacity", 0).remove(), o.enter().append("text").attr("dominant-baseline", "alphabetic").style("baseline-shift", "0%").attr("opacity", 0).style("opacity", 0).call(n).call(i).merge(o).transition(f).delay(t._delay).call(i).attr("opacity", e.fO).style("opacity", e.fO))
                                })).transition(f).call(m);
                                for (var y = Object.keys(this._on), _ = y.reduce((function(e, t) {
                                        return e[t] = function(e, a) {
                                            return n._on[t](e.data, a)
                                        }, e
                                    }), {}), b = 0; b < y.length; b++) p.on(y[b], _[y[b]]);
                                return e && setTimeout(e, this._duration + 100), this
                            }
                        }, {
                            key: "ariaHidden",
                            value: function(e) {
                                return void 0 !== e ? (this._ariaHidden = "function" == typeof e ? e : Object(s.constant)(e), this) : this._ariaHidden
                            }
                        }, {
                            key: "data",
                            value: function(e) {
                                return arguments.length ? (this._data = e, this) : this._data
                            }
                        }, {
                            key: "delay",
                            value: function(e) {
                                return arguments.length ? (this._delay = e, this) : this._delay
                            }
                        }, {
                            key: "duration",
                            value: function(e) {
                                return arguments.length ? (this._duration = e, this) : this._duration
                            }
                        }, {
                            key: "ellipsis",
                            value: function(e) {
                                return arguments.length ? (this._ellipsis = "function" == typeof e ? e : Object(s.constant)(e), this) : this._ellipsis
                            }
                        }, {
                            key: "fontColor",
                            value: function(e) {
                                return arguments.length ? (this._fontColor = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontColor
                            }
                        }, {
                            key: "fontFamily",
                            value: function(e) {
                                return arguments.length ? (this._fontFamily = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontFamily
                            }
                        }, {
                            key: "fontMax",
                            value: function(e) {
                                return arguments.length ? (this._fontMax = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontMax
                            }
                        }, {
                            key: "fontMin",
                            value: function(e) {
                                return arguments.length ? (this._fontMin = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontMin
                            }
                        }, {
                            key: "fontOpacity",
                            value: function(e) {
                                return arguments.length ? (this._fontOpacity = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontOpacity
                            }
                        }, {
                            key: "fontResize",
                            value: function(e) {
                                return arguments.length ? (this._fontResize = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontResize
                            }
                        }, {
                            key: "fontSize",
                            value: function(e) {
                                return arguments.length ? (this._fontSize = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontSize
                            }
                        }, {
                            key: "fontWeight",
                            value: function(e) {
                                return arguments.length ? (this._fontWeight = "function" == typeof e ? e : Object(s.constant)(e), this) : this._fontWeight
                            }
                        }, {
                            key: "height",
                            value: function(e) {
                                return arguments.length ? (this._height = "function" == typeof e ? e : Object(s.constant)(e), this) : this._height
                            }
                        }, {
                            key: "html",
                            value: function(e) {
                                return arguments.length ? (this._html = "boolean" == typeof e ? !!e && _ : e, this) : this._html
                            }
                        }, {
                            key: "id",
                            value: function(e) {
                                return arguments.length ? (this._id = "function" == typeof e ? e : Object(s.constant)(e), this) : this._id
                            }
                        }, {
                            key: "lineHeight",
                            value: function(e) {
                                return arguments.length ? (this._lineHeight = "function" == typeof e ? e : Object(s.constant)(e), this) : this._lineHeight
                            }
                        }, {
                            key: "maxLines",
                            value: function(e) {
                                return arguments.length ? (this._maxLines = "function" == typeof e ? e : Object(s.constant)(e), this) : this._maxLines
                            }
                        }, {
                            key: "overflow",
                            value: function(e) {
                                return arguments.length ? (this._overflow = "function" == typeof e ? e : Object(s.constant)(e), this) : this._overflow
                            }
                        }, {
                            key: "padding",
                            value: function(e) {
                                return arguments.length ? (this._padding = "function" == typeof e ? e : Object(s.constant)(e), this) : this._padding
                            }
                        }, {
                            key: "pointerEvents",
                            value: function(e) {
                                return arguments.length ? (this._pointerEvents = "function" == typeof e ? e : Object(s.constant)(e), this) : this._pointerEvents
                            }
                        }, {
                            key: "rotate",
                            value: function(e) {
                                return arguments.length ? (this._rotate = "function" == typeof e ? e : Object(s.constant)(e), this) : this._rotate
                            }
                        }, {
                            key: "rotateAnchor",
                            value: function(e) {
                                return arguments.length ? (this._rotateAnchor = "function" == typeof e ? e : Object(s.constant)(e), this) : this._rotateAnchor
                            }
                        }, {
                            key: "select",
                            value: function(e) {
                                return arguments.length ? (this._select = Object(a.select)(e), this) : this._select
                            }
                        }, {
                            key: "split",
                            value: function(e) {
                                return arguments.length ? (this._split = e, this) : this._split
                            }
                        }, {
                            key: "text",
                            value: function(e) {
                                return arguments.length ? (this._text = "function" == typeof e ? e : Object(s.constant)(e), this) : this._text
                            }
                        }, {
                            key: "textAnchor",
                            value: function(e) {
                                return arguments.length ? (this._textAnchor = "function" == typeof e ? e : Object(s.constant)(e), this) : this._textAnchor
                            }
                        }, {
                            key: "verticalAlign",
                            value: function(e) {
                                return arguments.length ? (this._verticalAlign = "function" == typeof e ? e : Object(s.constant)(e), this) : this._verticalAlign
                            }
                        }, {
                            key: "width",
                            value: function(e) {
                                return arguments.length ? (this._width = "function" == typeof e ? e : Object(s.constant)(e), this) : this._width
                            }
                        }, {
                            key: "x",
                            value: function(e) {
                                return arguments.length ? (this._x = "function" == typeof e ? e : Object(s.constant)(e), this) : this._x
                            }
                        }, {
                            key: "y",
                            value: function(e) {
                                return arguments.length ? (this._y = "function" == typeof e ? e : Object(s.constant)(e), this) : this._y
                            }
                        }]) && m(t.prototype, f), b && m(t, b), n
                    }(s.BaseClass)
            },
        "./node_modules/d3plus-text/es/src/combiningMarks.js":
            /*!***********************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/combiningMarks.js ***!
              \***********************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                n.default = ["u0903", "u093B", "u093E", "u093F", "u0940", "u0949", "u094A", "u094B", "u094C", "u094E", "u094F", "u0982", "u0983", "u09BE", "u09BF", "u09C0", "u09C7", "u09C8", "u09CB", "u09CC", "u09D7", "u0A03", "u0A3E", "u0A3F", "u0A40", "u0A83", "u0ABE", "u0ABF", "u0AC0", "u0AC9", "u0ACB", "u0ACC", "u0B02", "u0B03", "u0B3E", "u0B40", "u0B47", "u0B48", "u0B4B", "u0B4C", "u0B57", "u0BBE", "u0BBF", "u0BC1", "u0BC2", "u0BC6", "u0BC7", "u0BC8", "u0BCA", "u0BCB", "u0BCC", "u0BD7", "u0C01", "u0C02", "u0C03", "u0C41", "u0C42", "u0C43", "u0C44", "u0C82", "u0C83", "u0CBE", "u0CC0", "u0CC1", "u0CC2", "u0CC3", "u0CC4", "u0CC7", "u0CC8", "u0CCA", "u0CCB", "u0CD5", "u0CD6", "u0D02", "u0D03", "u0D3E", "u0D3F", "u0D40", "u0D46", "u0D47", "u0D48", "u0D4A", "u0D4B", "u0D4C", "u0D57", "u0D82", "u0D83", "u0DCF", "u0DD0", "u0DD1", "u0DD8", "u0DD9", "u0DDA", "u0DDB", "u0DDC", "u0DDD", "u0DDE", "u0DDF", "u0DF2", "u0DF3", "u0F3E", "u0F3F", "u0F7F", "u102B", "u102C", "u1031", "u1038", "u103B", "u103C", "u1056", "u1057", "u1062", "u1063", "u1064", "u1067", "u1068", "u1069", "u106A", "u106B", "u106C", "u106D", "u1083", "u1084", "u1087", "u1088", "u1089", "u108A", "u108B", "u108C", "u108F", "u109A", "u109B", "u109C", "u17B6", "u17BE", "u17BF", "u17C0", "u17C1", "u17C2", "u17C3", "u17C4", "u17C5", "u17C7", "u17C8", "u1923", "u1924", "u1925", "u1926", "u1929", "u192A", "u192B", "u1930", "u1931", "u1933", "u1934", "u1935", "u1936", "u1937", "u1938", "u1A19", "u1A1A", "u1A55", "u1A57", "u1A61", "u1A63", "u1A64", "u1A6D", "u1A6E", "u1A6F", "u1A70", "u1A71", "u1A72", "u1B04", "u1B35", "u1B3B", "u1B3D", "u1B3E", "u1B3F", "u1B40", "u1B41", "u1B43", "u1B44", "u1B82", "u1BA1", "u1BA6", "u1BA7", "u1BAA", "u1BE7", "u1BEA", "u1BEB", "u1BEC", "u1BEE", "u1BF2", "u1BF3", "u1C24", "u1C25", "u1C26", "u1C27", "u1C28", "u1C29", "u1C2A", "u1C2B", "u1C34", "u1C35", "u1CE1", "u1CF2", "u1CF3", "u302E", "u302F", "uA823", "uA824", "uA827", "uA880", "uA881", "uA8B4", "uA8B5", "uA8B6", "uA8B7", "uA8B8", "uA8B9", "uA8BA", "uA8BB", "uA8BC", "uA8BD", "uA8BE", "uA8BF", "uA8C0", "uA8C1", "uA8C2", "uA8C3", "uA952", "uA953", "uA983", "uA9B4", "uA9B5", "uA9BA", "uA9BB", "uA9BD", "uA9BE", "uA9BF", "uA9C0", "uAA2F", "uAA30", "uAA33", "uAA34", "uAA4D", "uAA7B", "uAA7D", "uAAEB", "uAAEE", "uAAEF", "uAAF5", "uABE3", "uABE4", "uABE6", "uABE7", "uABE9", "uABEA", "uABEC"].concat(["u0300", "u0301", "u0302", "u0303", "u0304", "u0305", "u0306", "u0307", "u0308", "u0309", "u030A", "u030B", "u030C", "u030D", "u030E", "u030F", "u0310", "u0311", "u0312", "u0313", "u0314", "u0315", "u0316", "u0317", "u0318", "u0319", "u031A", "u031B", "u031C", "u031D", "u031E", "u031F", "u0320", "u0321", "u0322", "u0323", "u0324", "u0325", "u0326", "u0327", "u0328", "u0329", "u032A", "u032B", "u032C", "u032D", "u032E", "u032F", "u0330", "u0331", "u0332", "u0333", "u0334", "u0335", "u0336", "u0337", "u0338", "u0339", "u033A", "u033B", "u033C", "u033D", "u033E", "u033F", "u0340", "u0341", "u0342", "u0343", "u0344", "u0345", "u0346", "u0347", "u0348", "u0349", "u034A", "u034B", "u034C", "u034D", "u034E", "u034F", "u0350", "u0351", "u0352", "u0353", "u0354", "u0355", "u0356", "u0357", "u0358", "u0359", "u035A", "u035B", "u035C", "u035D", "u035E", "u035F", "u0360", "u0361", "u0362", "u0363", "u0364", "u0365", "u0366", "u0367", "u0368", "u0369", "u036A", "u036B", "u036C", "u036D", "u036E", "u036F", "u0483", "u0484", "u0485", "u0486", "u0487", "u0591", "u0592", "u0593", "u0594", "u0595", "u0596", "u0597", "u0598", "u0599", "u059A", "u059B", "u059C", "u059D", "u059E", "u059F", "u05A0", "u05A1", "u05A2", "u05A3", "u05A4", "u05A5", "u05A6", "u05A7", "u05A8", "u05A9", "u05AA", "u05AB", "u05AC", "u05AD", "u05AE", "u05AF", "u05B0", "u05B1", "u05B2", "u05B3", "u05B4", "u05B5", "u05B6", "u05B7", "u05B8", "u05B9", "u05BA", "u05BB", "u05BC", "u05BD", "u05BF", "u05C1", "u05C2", "u05C4", "u05C5", "u05C7", "u0610", "u0611", "u0612", "u0613", "u0614", "u0615", "u0616", "u0617", "u0618", "u0619", "u061A", "u064B", "u064C", "u064D", "u064E", "u064F", "u0650", "u0651", "u0652", "u0653", "u0654", "u0655", "u0656", "u0657", "u0658", "u0659", "u065A", "u065B", "u065C", "u065D", "u065E", "u065F", "u0670", "u06D6", "u06D7", "u06D8", "u06D9", "u06DA", "u06DB", "u06DC", "u06DF", "u06E0", "u06E1", "u06E2", "u06E3", "u06E4", "u06E7", "u06E8", "u06EA", "u06EB", "u06EC", "u06ED", "u0711", "u0730", "u0731", "u0732", "u0733", "u0734", "u0735", "u0736", "u0737", "u0738", "u0739", "u073A", "u073B", "u073C", "u073D", "u073E", "u073F", "u0740", "u0741", "u0742", "u0743", "u0744", "u0745", "u0746", "u0747", "u0748", "u0749", "u074A", "u07A6", "u07A7", "u07A8", "u07A9", "u07AA", "u07AB", "u07AC", "u07AD", "u07AE", "u07AF", "u07B0", "u07EB", "u07EC", "u07ED", "u07EE", "u07EF", "u07F0", "u07F1", "u07F2", "u07F3", "u0816", "u0817", "u0818", "u0819", "u081B", "u081C", "u081D", "u081E", "u081F", "u0820", "u0821", "u0822", "u0823", "u0825", "u0826", "u0827", "u0829", "u082A", "u082B", "u082C", "u082D", "u0859", "u085A", "u085B", "u08E3", "u08E4", "u08E5", "u08E6", "u08E7", "u08E8", "u08E9", "u08EA", "u08EB", "u08EC", "u08ED", "u08EE", "u08EF", "u08F0", "u08F1", "u08F2", "u08F3", "u08F4", "u08F5", "u08F6", "u08F7", "u08F8", "u08F9", "u08FA", "u08FB", "u08FC", "u08FD", "u08FE", "u08FF", "u0900", "u0901", "u0902", "u093A", "u093C", "u0941", "u0942", "u0943", "u0944", "u0945", "u0946", "u0947", "u0948", "u094D", "u0951", "u0952", "u0953", "u0954", "u0955", "u0956", "u0957", "u0962", "u0963", "u0981", "u09BC", "u09C1", "u09C2", "u09C3", "u09C4", "u09CD", "u09E2", "u09E3", "u0A01", "u0A02", "u0A3C", "u0A41", "u0A42", "u0A47", "u0A48", "u0A4B", "u0A4C", "u0A4D", "u0A51", "u0A70", "u0A71", "u0A75", "u0A81", "u0A82", "u0ABC", "u0AC1", "u0AC2", "u0AC3", "u0AC4", "u0AC5", "u0AC7", "u0AC8", "u0ACD", "u0AE2", "u0AE3", "u0B01", "u0B3C", "u0B3F", "u0B41", "u0B42", "u0B43", "u0B44", "u0B4D", "u0B56", "u0B62", "u0B63", "u0B82", "u0BC0", "u0BCD", "u0C00", "u0C3E", "u0C3F", "u0C40", "u0C46", "u0C47", "u0C48", "u0C4A", "u0C4B", "u0C4C", "u0C4D", "u0C55", "u0C56", "u0C62", "u0C63", "u0C81", "u0CBC", "u0CBF", "u0CC6", "u0CCC", "u0CCD", "u0CE2", "u0CE3", "u0D01", "u0D41", "u0D42", "u0D43", "u0D44", "u0D4D", "u0D62", "u0D63", "u0DCA", "u0DD2", "u0DD3", "u0DD4", "u0DD6", "u0E31", "u0E34", "u0E35", "u0E36", "u0E37", "u0E38", "u0E39", "u0E3A", "u0E47", "u0E48", "u0E49", "u0E4A", "u0E4B", "u0E4C", "u0E4D", "u0E4E", "u0EB1", "u0EB4", "u0EB5", "u0EB6", "u0EB7", "u0EB8", "u0EB9", "u0EBB", "u0EBC", "u0EC8", "u0EC9", "u0ECA", "u0ECB", "u0ECC", "u0ECD", "u0F18", "u0F19", "u0F35", "u0F37", "u0F39", "u0F71", "u0F72", "u0F73", "u0F74", "u0F75", "u0F76", "u0F77", "u0F78", "u0F79", "u0F7A", "u0F7B", "u0F7C", "u0F7D", "u0F7E", "u0F80", "u0F81", "u0F82", "u0F83", "u0F84", "u0F86", "u0F87", "u0F8D", "u0F8E", "u0F8F", "u0F90", "u0F91", "u0F92", "u0F93", "u0F94", "u0F95", "u0F96", "u0F97", "u0F99", "u0F9A", "u0F9B", "u0F9C", "u0F9D", "u0F9E", "u0F9F", "u0FA0", "u0FA1", "u0FA2", "u0FA3", "u0FA4", "u0FA5", "u0FA6", "u0FA7", "u0FA8", "u0FA9", "u0FAA", "u0FAB", "u0FAC", "u0FAD", "u0FAE", "u0FAF", "u0FB0", "u0FB1", "u0FB2", "u0FB3", "u0FB4", "u0FB5", "u0FB6", "u0FB7", "u0FB8", "u0FB9", "u0FBA", "u0FBB", "u0FBC", "u0FC6", "u102D", "u102E", "u102F", "u1030", "u1032", "u1033", "u1034", "u1035", "u1036", "u1037", "u1039", "u103A", "u103D", "u103E", "u1058", "u1059", "u105E", "u105F", "u1060", "u1071", "u1072", "u1073", "u1074", "u1082", "u1085", "u1086", "u108D", "u109D", "u135D", "u135E", "u135F", "u1712", "u1713", "u1714", "u1732", "u1733", "u1734", "u1752", "u1753", "u1772", "u1773", "u17B4", "u17B5", "u17B7", "u17B8", "u17B9", "u17BA", "u17BB", "u17BC", "u17BD", "u17C6", "u17C9", "u17CA", "u17CB", "u17CC", "u17CD", "u17CE", "u17CF", "u17D0", "u17D1", "u17D2", "u17D3", "u17DD", "u180B", "u180C", "u180D", "u18A9", "u1920", "u1921", "u1922", "u1927", "u1928", "u1932", "u1939", "u193A", "u193B", "u1A17", "u1A18", "u1A1B", "u1A56", "u1A58", "u1A59", "u1A5A", "u1A5B", "u1A5C", "u1A5D", "u1A5E", "u1A60", "u1A62", "u1A65", "u1A66", "u1A67", "u1A68", "u1A69", "u1A6A", "u1A6B", "u1A6C", "u1A73", "u1A74", "u1A75", "u1A76", "u1A77", "u1A78", "u1A79", "u1A7A", "u1A7B", "u1A7C", "u1A7F", "u1AB0", "u1AB1", "u1AB2", "u1AB3", "u1AB4", "u1AB5", "u1AB6", "u1AB7", "u1AB8", "u1AB9", "u1ABA", "u1ABB", "u1ABC", "u1ABD", "u1B00", "u1B01", "u1B02", "u1B03", "u1B34", "u1B36", "u1B37", "u1B38", "u1B39", "u1B3A", "u1B3C", "u1B42", "u1B6B", "u1B6C", "u1B6D", "u1B6E", "u1B6F", "u1B70", "u1B71", "u1B72", "u1B73", "u1B80", "u1B81", "u1BA2", "u1BA3", "u1BA4", "u1BA5", "u1BA8", "u1BA9", "u1BAB", "u1BAC", "u1BAD", "u1BE6", "u1BE8", "u1BE9", "u1BED", "u1BEF", "u1BF0", "u1BF1", "u1C2C", "u1C2D", "u1C2E", "u1C2F", "u1C30", "u1C31", "u1C32", "u1C33", "u1C36", "u1C37", "u1CD0", "u1CD1", "u1CD2", "u1CD4", "u1CD5", "u1CD6", "u1CD7", "u1CD8", "u1CD9", "u1CDA", "u1CDB", "u1CDC", "u1CDD", "u1CDE", "u1CDF", "u1CE0", "u1CE2", "u1CE3", "u1CE4", "u1CE5", "u1CE6", "u1CE7", "u1CE8", "u1CED", "u1CF4", "u1CF8", "u1CF9", "u1DC0", "u1DC1", "u1DC2", "u1DC3", "u1DC4", "u1DC5", "u1DC6", "u1DC7", "u1DC8", "u1DC9", "u1DCA", "u1DCB", "u1DCC", "u1DCD", "u1DCE", "u1DCF", "u1DD0", "u1DD1", "u1DD2", "u1DD3", "u1DD4", "u1DD5", "u1DD6", "u1DD7", "u1DD8", "u1DD9", "u1DDA", "u1DDB", "u1DDC", "u1DDD", "u1DDE", "u1DDF", "u1DE0", "u1DE1", "u1DE2", "u1DE3", "u1DE4", "u1DE5", "u1DE6", "u1DE7", "u1DE8", "u1DE9", "u1DEA", "u1DEB", "u1DEC", "u1DED", "u1DEE", "u1DEF", "u1DF0", "u1DF1", "u1DF2", "u1DF3", "u1DF4", "u1DF5", "u1DFC", "u1DFD", "u1DFE", "u1DFF", "u20D0", "u20D1", "u20D2", "u20D3", "u20D4", "u20D5", "u20D6", "u20D7", "u20D8", "u20D9", "u20DA", "u20DB", "u20DC", "u20E1", "u20E5", "u20E6", "u20E7", "u20E8", "u20E9", "u20EA", "u20EB", "u20EC", "u20ED", "u20EE", "u20EF", "u20F0", "u2CEF", "u2CF0", "u2CF1", "u2D7F", "u2DE0", "u2DE1", "u2DE2", "u2DE3", "u2DE4", "u2DE5", "u2DE6", "u2DE7", "u2DE8", "u2DE9", "u2DEA", "u2DEB", "u2DEC", "u2DED", "u2DEE", "u2DEF", "u2DF0", "u2DF1", "u2DF2", "u2DF3", "u2DF4", "u2DF5", "u2DF6", "u2DF7", "u2DF8", "u2DF9", "u2DFA", "u2DFB", "u2DFC", "u2DFD", "u2DFE", "u2DFF", "u302A", "u302B", "u302C", "u302D", "u3099", "u309A", "uA66F", "uA674", "uA675", "uA676", "uA677", "uA678", "uA679", "uA67A", "uA67B", "uA67C", "uA67D", "uA69E", "uA69F", "uA6F0", "uA6F1", "uA802", "uA806", "uA80B", "uA825", "uA826", "uA8C4", "uA8E0", "uA8E1", "uA8E2", "uA8E3", "uA8E4", "uA8E5", "uA8E6", "uA8E7", "uA8E8", "uA8E9", "uA8EA", "uA8EB", "uA8EC", "uA8ED", "uA8EE", "uA8EF", "uA8F0", "uA8F1", "uA926", "uA927", "uA928", "uA929", "uA92A", "uA92B", "uA92C", "uA92D", "uA947", "uA948", "uA949", "uA94A", "uA94B", "uA94C", "uA94D", "uA94E", "uA94F", "uA950", "uA951", "uA980", "uA981", "uA982", "uA9B3", "uA9B6", "uA9B7", "uA9B8", "uA9B9", "uA9BC", "uA9E5", "uAA29", "uAA2A", "uAA2B", "uAA2C", "uAA2D", "uAA2E", "uAA31", "uAA32", "uAA35", "uAA36", "uAA43", "uAA4C", "uAA7C", "uAAB0", "uAAB2", "uAAB3", "uAAB4", "uAAB7", "uAAB8", "uAABE", "uAABF", "uAAC1", "uAAEC", "uAAED", "uAAF6", "uABE5", "uABE8", "uABED", "uFB1E", "uFE00", "uFE01", "uFE02", "uFE03", "uFE04", "uFE05", "uFE06", "uFE07", "uFE08", "uFE09", "uFE0A", "uFE0B", "uFE0C", "uFE0D", "uFE0E", "uFE0F", "uFE20", "uFE21", "uFE22", "uFE23", "uFE24", "uFE25", "uFE26", "uFE27", "uFE28", "uFE29", "uFE2A", "uFE2B", "uFE2C", "uFE2D", "uFE2E", "uFE2F"])
            },
        "./node_modules/d3plus-text/es/src/fontExists.js":
            /*!*******************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/fontExists.js ***!
              \*******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a, i, o, s, r = t( /*! ./textWidth */ "./node_modules/d3plus-text/es/src/textWidth.js"),
                    u = t( /*! ./trim */ "./node_modules/d3plus-text/es/src/trim.js"),
                    l = "abcdefghiABCDEFGHI_!@#$%^&*()_+1234567890",
                    c = {};
                n.default = function(e) {
                    a || (a = Object(r.default)(l, {
                        "font-family": "DejaVuSans",
                        "font-size": 32
                    }), i = Object(r.default)(l, {
                        "font-family": "-apple-system",
                        "font-size": 32
                    }), o = Object(r.default)(l, {
                        "font-family": "monospace",
                        "font-size": 32
                    }), s = Object(r.default)(l, {
                        "font-family": "sans-serif",
                        "font-size": 32
                    })), e instanceof Array || (e = e.split(",")), e = e.map((function(e) {
                        return Object(u.trim)(e)
                    }));
                    for (var n = 0; n < e.length; n++) {
                        var t = e[n];
                        if (c[t] || ["-apple-system", "monospace", "sans-serif", "DejaVuSans"].includes(t)) return t;
                        if (!1 !== c[t]) {
                            var d = Object(r.default)(l, {
                                "font-family": t,
                                "font-size": 32
                            });
                            if (c[t] = d !== o, c[t] && (c[t] = d !== s), i && c[t] && (c[t] = d !== i), a && c[t] && (c[t] = d !== a), c[t]) return t
                        }
                    }
                    return !1
                }
            },
        "./node_modules/d3plus-text/es/src/rtl.js":
            /*!************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/rtl.js ***!
              \************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js");
                n.default = function() {
                    return "rtl" === Object(a.select)("html").attr("dir") || "rtl" === Object(a.select)("body").attr("dir") || "rtl" === Object(a.select)("html").style("direction") || "rtl" === Object(a.select)("body").style("direction")
                }
            },
        "./node_modules/d3plus-text/es/src/stringify.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/stringify.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n), n.default = function(e) {
                    return void 0 === e ? e = "undefined" : "string" == typeof e || e instanceof String || (e = JSON.stringify(e)), e
                }
            },
        "./node_modules/d3plus-text/es/src/strip.js":
            /*!**************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/strip.js ***!
              \**************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = [
                    [/[\300-\305]/g, "A"],
                    [/[\340-\345]/g, "a"],
                    [/[\306]/g, "AE"],
                    [/[\346]/g, "ae"],
                    [/[\337]/g, "B"],
                    [/[\307]/g, "C"],
                    [/[\347]/g, "c"],
                    [/[\320\336\376]/g, "D"],
                    [/[\360]/g, "d"],
                    [/[\310-\313]/g, "E"],
                    [/[\350-\353]/g, "e"],
                    [/[\314-\317]/g, "I"],
                    [/[\354-\357]/g, "i"],
                    [/[\321]/g, "N"],
                    [/[\361]/g, "n"],
                    [/[\u014c\322-\326\330]/g, "O"],
                    [/[\u014d\362-\366\370]/g, "o"],
                    [/[\u016a\331-\334]/g, "U"],
                    [/[\u016b\371-\374]/g, "u"],
                    [/[\327]/g, "x"],
                    [/[\335]/g, "Y"],
                    [/[\375\377]/g, "y"]
                ];
                n.default = function(e) {
                    return "".concat(e).replace(/[^A-Za-z0-9\-_]/g, (function(e) {
                        if (" " === e) return "-";
                        for (var n = !1, t = 0; t < a.length; t++)
                            if (new RegExp(a[t][0]).test(e)) {
                                n = a[t][1];
                                break
                            }
                        return n || ""
                    }))
                }
            },
        "./node_modules/d3plus-text/es/src/textSplit.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/textSplit.js ***!
              \******************************************************/
            /*! exports provided: default, prefixChars, splitChars, splitWords, suffixChars */
            function(e, n, t) {
                "use strict";
                t.r(n), t.d(n, "prefixChars", (function() {
                    return r
                })), t.d(n, "splitChars", (function() {
                    return s
                })), t.d(n, "splitWords", (function() {
                    return c
                })), t.d(n, "suffixChars", (function() {
                    return u
                }));
                var a = t( /*! ./stringify */ "./node_modules/d3plus-text/es/src/stringify.js"),
                    i = t( /*! ./combiningMarks */ "./node_modules/d3plus-text/es/src/combiningMarks.js"),
                    o = t( /*! d3-array */ "./node_modules/d3-array/src/index.js"),
                    s = ["-", ";", ":", "&", "|", "u0E2F", "u0EAF", "u0EC6", "u0ECC", "u104A", "u104B", "u104C", "u104D", "u104E", "u104F", "u2013", "u2014", "u2027", "u3000", "u3001", "u3002", "uFF0C", "uFF5E"],
                    r = ["'", "<", "(", "{", "[", "u00AB", "u300A", "u3008"],
                    u = ["'", ">", ")", "}", "]", ".", "!", "?", "/", "u00BB", "u300B", "u3009"].concat(s),
                    l = "က-ဪဿ-၉ၐ-ၕ㐀-龿぀-ゟ゠-ヿ＀-＋－-｝｟-ﾟ㐀-䶿ກ-ຮະ-ໄ່-໋ໍ-ໝ",
                    c = new RegExp("(\\".concat(s.join("|\\"), ")*[^\\s|\\").concat(s.join("|\\"), "]*(\\").concat(s.join("|\\"), ")*"), "g"),
                    d = new RegExp("[".concat(l, "]")),
                    g = new RegExp("(\\".concat(r.join("|\\"), ")*[").concat(l, "](\\").concat(u.join("|\\"), "|\\").concat(i.default.join("|\\"), ")*|[a-z0-9]+"), "gi");
                n.default = function(e) {
                    return d.test(e) ? Object(o.merge)(Object(a.default)(e).match(c).map((function(e) {
                        return d.test(e) ? e.match(g) : [e]
                    }))) : Object(a.default)(e).match(c).filter((function(e) {
                        return e.length
                    }))
                }
            },
        "./node_modules/d3plus-text/es/src/textWidth.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/textWidth.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    if ("" === e.replace(/\s+/g, "")) return e;
                    var n = (new DOMParser).parseFromString(e.replace(/<[^>]+>/g, ""), "text/html");
                    return n.documentElement ? n.documentElement.textContent : e
                }
                t.r(n), n.default = function(e, n) {
                    n = Object.assign({
                        "font-size": 10,
                        "font-family": "sans-serif",
                        "font-style": "normal",
                        "font-weight": 400,
                        "font-variant": "normal"
                    }, n);
                    var t = document.createElement("canvas").getContext("2d"),
                        i = [];
                    return i.push(n["font-style"]), i.push(n["font-variant"]), i.push(n["font-weight"]), i.push("string" == typeof n["font-size"] ? n["font-size"] : "".concat(n["font-size"], "px")), i.push(n["font-family"]), t.font = i.join(" "), e instanceof Array ? e.map((function(e) {
                        return t.measureText(a(e)).width
                    })) : t.measureText(a(e)).width
                }
            },
        "./node_modules/d3plus-text/es/src/textWrap.js":
            /*!*****************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/textWrap.js ***!
              \*****************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./textWidth */ "./node_modules/d3plus-text/es/src/textWidth.js"),
                    i = t( /*! ./textSplit */ "./node_modules/d3plus-text/es/src/textSplit.js"),
                    o = t( /*! ./stringify */ "./node_modules/d3plus-text/es/src/stringify.js"),
                    s = t( /*! ./trim */ "./node_modules/d3plus-text/es/src/trim.js");
                n.default = function() {
                    var e, n = "sans-serif",
                        t = 10,
                        r = 400,
                        u = 200,
                        l = null,
                        c = !1,
                        d = i.default,
                        g = 200;

                    function h(i) {
                        i = Object(o.default)(i), void 0 === e && (e = Math.ceil(1.4 * t));
                        for (var h = d(i), f = {
                                "font-family": n,
                                "font-size": t,
                                "font-weight": r,
                                "line-height": e
                            }, m = 1, p = "", v = !1, y = 0, _ = [], b = Object(a.default)(h, f), j = Object(a.default)(" ", f), x = 0; x < h.length; x++) {
                            var w = h[x],
                                R = b[h.indexOf(w)];
                            if (w += i.slice(p.length + w.length).match("^( |\n)*", "g")[0], "\n" === p.slice(-1) || y + R > g) {
                                if (!x && !c) {
                                    v = !0;
                                    break
                                }
                                if (_.length >= m && (_[m - 1] = Object(s.trimRight)(_[m - 1])), m++, e * m > u || R > g && !c || l && m > l) {
                                    v = !0;
                                    break
                                }
                                y = 0, _.push(w)
                            } else x ? _[m - 1] += w : _[0] = w;
                            p += w, y += R, y += w.match(/[\s]*$/g)[0].length * j
                        }
                        return {
                            lines: _,
                            sentence: i,
                            truncated: v,
                            widths: Object(a.default)(_, f),
                            words: h
                        }
                    }
                    return h.fontFamily = function(e) {
                        return arguments.length ? (n = e, h) : n
                    }, h.fontSize = function(e) {
                        return arguments.length ? (t = e, h) : t
                    }, h.fontWeight = function(e) {
                        return arguments.length ? (r = e, h) : r
                    }, h.height = function(e) {
                        return arguments.length ? (u = e, h) : u
                    }, h.lineHeight = function(n) {
                        return arguments.length ? (e = n, h) : e
                    }, h.maxLines = function(e) {
                        return arguments.length ? (l = e, h) : l
                    }, h.overflow = function(e) {
                        return arguments.length ? (c = e, h) : c
                    }, h.split = function(e) {
                        return arguments.length ? (d = e, h) : d
                    }, h.width = function(e) {
                        return arguments.length ? (g = e, h) : g
                    }, h
                }
            },
        "./node_modules/d3plus-text/es/src/titleCase.js":
            /*!******************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/titleCase.js ***!
              \******************************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./textSplit */ "./node_modules/d3plus-text/es/src/textSplit.js"),
                    i = ["a", "an", "and", "as", "at", "but", "by", "for", "from", "if", "in", "into", "near", "nor", "of", "on", "onto", "or", "per", "that", "the", "to", "with", "via", "vs", "vs."],
                    o = ["CEO", "CFO", "CNC", "COO", "CPU", "GDP", "HVAC", "ID", "IT", "R&D", "TV", "UI"];
                n.default = function(e) {
                    if (void 0 === e) return "";
                    var n = i.map((function(e) {
                            return e.toLowerCase()
                        })),
                        t = o.slice(),
                        s = (t = t.concat(t.map((function(e) {
                            return "".concat(e, "s")
                        })))).map((function(e) {
                            return e.toLowerCase()
                        })),
                        r = Object(a.default)(e);
                    return r.map((function(e, i) {
                        if (e) {
                            var o = e.toLowerCase(),
                                u = a.suffixChars.includes(o.charAt(o.length - 1)) ? o.slice(0, -1) : o,
                                l = s.indexOf(u);
                            return l >= 0 ? t[l] : n.includes(u) && 0 !== i && i !== r.length - 1 ? o : e.charAt(0).toUpperCase() + e.substr(1).toLowerCase()
                        }
                        return ""
                    })).reduce((function(n, t, a) {
                        return a && " " === e.charAt(n.length) && (n += " "), n += t
                    }), "")
                }
            },
        "./node_modules/d3plus-text/es/src/trim.js":
            /*!*************************************************!*\
              !*** ./node_modules/d3plus-text/es/src/trim.js ***!
              \*************************************************/
            /*! exports provided: trim, trimLeft, trimRight */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return e.toString().replace(/^\s+|\s+$/g, "")
                }

                function i(e) {
                    return e.toString().replace(/^\s+/, "")
                }

                function o(e) {
                    return e.toString().replace(/\s+$/, "")
                }
                t.r(n), t.d(n, "trim", (function() {
                    return a
                })), t.d(n, "trimLeft", (function() {
                    return i
                })), t.d(n, "trimRight", (function() {
                    return o
                }))
            },
        "./node_modules/iso639-codes/index.json":
            /*!**********************************************!*\
              !*** ./node_modules/iso639-codes/index.json ***!
              \**********************************************/
            /*! exports provided: Abkhazian, Achinese, Acoli, Adangme, Adygei, Adyghe, Afar, Afrihili, Afrikaans, Afro-Asiatic languages, Ainu, Akan, Akkadian, Albanian, Alemannic, Aleut, Algonquian languages, Alsatian, Altaic languages, Amharic, Angika, Apache languages, Arabic, Aragonese, Arapaho, Arawak, Armenian, Aromanian, Artificial languages, Arumanian, Assamese, Asturian, Asturleonese, Athapascan languages, Australian languages, Austronesian languages, Avaric, Avestan, Awadhi, Aymara, Azerbaijani, Bable, Balinese, Baltic languages, Baluchi, Bambara, Bamileke languages, Banda languages, Bantu languages, Basa, Bashkir, Basque, Batak languages, Bedawiyet, Beja, Belarusian, Bemba, Bengali, Berber languages, Bhojpuri, Bihari languages, Bikol, Bilin, Bini, Bislama, Blin, Bliss, Blissymbolics, Blissymbols, Bokmål, Norwegian, Bosnian, Braj, Breton, Buginese, Bulgarian, Buriat, Burmese, Caddo, Castilian, Catalan, Caucasian languages, Cebuano, Celtic languages, Central American Indian languages, Central Khmer, Chagatai, Chamic languages, Chamorro, Chechen, Cherokee, Chewa, Cheyenne, Chibcha, Chichewa, Chinese, Chinook jargon, Chipewyan, Choctaw, Chuang, Church Slavic, Church Slavonic, Chuukese, Chuvash, Classical Nepal Bhasa, Classical Newari, Classical Syriac, Cook Islands Maori, Coptic, Cornish, Corsican, Cree, Creek, Creoles and pidgins, Creoles and pidgins, English based, Creoles and pidgins, French-based, Creoles and pidgins, Portuguese-based, Crimean Tatar, Crimean Turkish, Croatian, Cushitic languages, Czech, Dakota, Danish, Dargwa, Delaware, Dene Suline, Dhivehi, Dimili, Dimli, Dinka, Divehi, Dogri, Dogrib, Dravidian languages, Duala, Dutch, Dutch, Middle (ca.1050-1350), Dyula, Dzongkha, Eastern Frisian, Edo, Efik, Egyptian (Ancient), Ekajuk, Elamite, English, English, Middle (1100-1500), English, Old (ca.450-1100), Erzya, Esperanto, Estonian, Ewe, Ewondo, Fang, Fanti, Faroese, Fijian, Filipino, Finnish, Finno-Ugrian languages, Flemish, Fon, French, French, Middle (ca.1400-1600), French, Old (842-ca.1400), Friulian, Fulah, Ga, Gaelic, Galibi Carib, Galician, Ganda, Gayo, Gbaya, Geez, Georgian, German, German, Low, German, Middle High (ca.1050-1500), German, Old High (ca.750-1050), Germanic languages, Gikuyu, Gilbertese, Gondi, Gorontalo, Gothic, Grebo, Greek, Ancient (to 1453), Greek, Modern (1453-), Greenlandic, Guarani, Gujarati, Gwich'in, Haida, Haitian, Haitian Creole, Hausa, Hawaiian, Hebrew, Herero, Hiligaynon, Himachali languages, Hindi, Hiri Motu, Hittite, Hmong, Hungarian, Hupa, Iban, Icelandic, Ido, Igbo, Ijo languages, Iloko, Imperial Aramaic (700-300 BCE), Inari Sami, Indic languages, Indo-European languages, Indonesian, Ingush, Interlingua (International Auxiliary Language Association), Interlingue, Inuktitut, Inupiaq, Iranian languages, Irish, Irish, Middle (900-1200), Irish, Old (to 900), Iroquoian languages, Italian, Japanese, Javanese, Jingpho, Judeo-Arabic, Judeo-Persian, Kabardian, Kabyle, Kachin, Kalaallisut, Kalmyk, Kamba, Kannada, Kanuri, Kapampangan, Kara-Kalpak, Karachay-Balkar, Karelian, Karen languages, Kashmiri, Kashubian, Kawi, Kazakh, Khasi, Khoisan languages, Khotanese, Kikuyu, Kimbundu, Kinyarwanda, Kirdki, Kirghiz, Kirmanjki, Klingon, Komi, Kongo, Konkani, Korean, Kosraean, Kpelle, Kru languages, Kuanyama, Kumyk, Kurdish, Kurukh, Kutenai, Kwanyama, Kyrgyz, Ladino, Lahnda, Lamba, Land Dayak languages, Lao, Latin, Latvian, Leonese, Letzeburgesch, Lezghian, Limburgan, Limburger, Limburgish, Lingala, Lithuanian, Lojban, Low German, Low Saxon, Lower Sorbian, Lozi, Luba-Katanga, Luba-Lulua, Luiseno, Lule Sami, Lunda, Luo (Kenya and Tanzania), Lushai, Luxembourgish, Macedo-Romanian, Macedonian, Madurese, Magahi, Maithili, Makasar, Malagasy, Malay, Malayalam, Maldivian, Maltese, Manchu, Mandar, Mandingo, Manipuri, Manobo languages, Manx, Maori, Mapuche, Mapudungun, Marathi, Mari, Marshallese, Marwari, Masai, Mayan languages, Mende, Mi'kmaq, Micmac, Minangkabau, Mirandese, Mohawk, Moksha, Moldavian, Moldovan, Mon-Khmer languages, Mong, Mongo, Mongolian, Montenegrin, Mossi, Multiple languages, Munda languages, N'Ko, Nahuatl languages, Nauru, Navaho, Navajo, Ndebele, North, Ndebele, South, Ndonga, Neapolitan, Nepal Bhasa, Nepali, Newari, Nias, Niger-Kordofanian languages, Nilo-Saharan languages, Niuean, No linguistic content, Nogai, Norse, Old, North American Indian languages, North Ndebele, Northern Frisian, Northern Sami, Northern Sotho, Norwegian, Norwegian Bokmål, Norwegian Nynorsk, Not applicable, Nubian languages, Nuosu, Nyamwezi, Nyanja, Nyankole, Nynorsk, Norwegian, Nyoro, Nzima, Occidental, Occitan (post 1500), Occitan, Old (to 1500), Official Aramaic (700-300 BCE), Oirat, Ojibwa, Old Bulgarian, Old Church Slavonic, Old Newari, Old Slavonic, Oriya, Oromo, Osage, Ossetian, Ossetic, Otomian languages, Pahlavi, Palauan, Pali, Pampanga, Pangasinan, Panjabi, Papiamento, Papuan languages, Pashto, Pedi, Persian, Persian, Old (ca.600-400 B.C.), Philippine languages, Phoenician, Pilipino, Pohnpeian, Polish, Portuguese, Prakrit languages, Provençal, Old (to 1500), Punjabi, Pushto, Quechua, Rajasthani, Rapanui, Rarotongan, Reserved for local use, Romance languages, Romanian, Romansh, Romany, Rundi, Russian, Sakan, Salishan languages, Samaritan Aramaic, Sami languages, Samoan, Sandawe, Sango, Sanskrit, Santali, Sardinian, Sasak, Saxon, Low, Scots, Scottish Gaelic, Selkup, Semitic languages, Sepedi, Serbian, Serer, Shan, Shona, Sichuan Yi, Sicilian, Sidamo, Sign Languages, Siksika, Sindhi, Sinhala, Sinhalese, Sino-Tibetan languages, Siouan languages, Skolt Sami, Slave (Athapascan), Slavic languages, Slovak, Slovenian, Sogdian, Somali, Songhai languages, Soninke, Sorbian languages, Sotho, Northern, Sotho, Southern, South American Indian languages, South Ndebele, Southern Altai, Southern Sami, Spanish, Sranan Tongo, Standard Moroccan Tamazight, Sukuma, Sumerian, Sundanese, Susu, Swahili, Swati, Swedish, Swiss German, Syriac, Tagalog, Tahitian, Tai languages, Tajik, Tamashek, Tamil, Tatar, Telugu, Tereno, Tetum, Thai, Tibetan, Tigre, Tigrinya, Timne, Tiv, tlhIngan-Hol, Tlingit, Tok Pisin, Tokelau, Tonga (Nyasa), Tonga (Tonga Islands), Tsimshian, Tsonga, Tswana, Tumbuka, Tupi languages, Turkish, Turkish, Ottoman (1500-1928), Turkmen, Tuvalu, Tuvinian, Twi, Udmurt, Ugaritic, Uighur, Ukrainian, Umbundu, Uncoded languages, Undetermined, Upper Sorbian, Urdu, Uyghur, Uzbek, Vai, Valencian, Venda, Vietnamese, Volapük, Votic, Wakashan languages, Walloon, Waray, Washo, Welsh, Western Frisian, Western Pahari languages, Wolaitta, Wolaytta, Wolof, Xhosa, Yakut, Yao, Yapese, Yiddish, Yoruba, Yupik languages, Zande languages, Zapotec, Zaza, Zazaki, Zenaga, Zhuang, Zulu, Zuni, default */
            function(e) {
                e.exports = JSON.parse('{"Abkhazian":{"name":"Abkhazian","names":["Abkhazian"],"iso639-2":"abk","iso639-1":"ab"},"Achinese":{"name":"Achinese","names":["Achinese"],"iso639-2":"ace","iso639-1":null},"Acoli":{"name":"Acoli","names":["Acoli"],"iso639-2":"ach","iso639-1":null},"Adangme":{"name":"Adangme","names":["Adangme"],"iso639-2":"ada","iso639-1":null},"Adygei":{"name":"Adygei","names":["Adyghe","Adygei"],"iso639-2":"ady","iso639-1":null},"Adyghe":{"name":"Adyghe","names":["Adyghe","Adygei"],"iso639-2":"ady","iso639-1":null},"Afar":{"name":"Afar","names":["Afar"],"iso639-2":"aar","iso639-1":"aa"},"Afrihili":{"name":"Afrihili","names":["Afrihili"],"iso639-2":"afh","iso639-1":null},"Afrikaans":{"name":"Afrikaans","names":["Afrikaans"],"iso639-2":"afr","iso639-1":"af"},"Afro-Asiatic languages":{"name":"Afro-Asiatic languages","names":["Afro-Asiatic languages"],"iso639-2":"afa","iso639-1":null},"Ainu":{"name":"Ainu","names":["Ainu"],"iso639-2":"ain","iso639-1":null},"Akan":{"name":"Akan","names":["Akan"],"iso639-2":"aka","iso639-1":"ak"},"Akkadian":{"name":"Akkadian","names":["Akkadian"],"iso639-2":"akk","iso639-1":null},"Albanian":{"name":"Albanian","names":["Albanian"],"iso639-2":"alb/sqi","iso639-1":"sq"},"Alemannic":{"name":"Alemannic","names":["Swiss German","Alemannic","Alsatian"],"iso639-2":"gsw","iso639-1":null},"Aleut":{"name":"Aleut","names":["Aleut"],"iso639-2":"ale","iso639-1":null},"Algonquian languages":{"name":"Algonquian languages","names":["Algonquian languages"],"iso639-2":"alg","iso639-1":null},"Alsatian":{"name":"Alsatian","names":["Swiss German","Alemannic","Alsatian"],"iso639-2":"gsw","iso639-1":null},"Altaic languages":{"name":"Altaic languages","names":["Altaic languages"],"iso639-2":"tut","iso639-1":null},"Amharic":{"name":"Amharic","names":["Amharic"],"iso639-2":"amh","iso639-1":"am"},"Angika":{"name":"Angika","names":["Angika"],"iso639-2":"anp","iso639-1":null},"Apache languages":{"name":"Apache languages","names":["Apache languages"],"iso639-2":"apa","iso639-1":null},"Arabic":{"name":"Arabic","names":["Arabic"],"iso639-2":"ara","iso639-1":"ar"},"Aragonese":{"name":"Aragonese","names":["Aragonese"],"iso639-2":"arg","iso639-1":"an"},"Arapaho":{"name":"Arapaho","names":["Arapaho"],"iso639-2":"arp","iso639-1":null},"Arawak":{"name":"Arawak","names":["Arawak"],"iso639-2":"arw","iso639-1":null},"Armenian":{"name":"Armenian","names":["Armenian"],"iso639-2":"arm/hye","iso639-1":"hy"},"Aromanian":{"name":"Aromanian","names":["Aromanian","Arumanian","Macedo-Romanian"],"iso639-2":"rup","iso639-1":null},"Artificial languages":{"name":"Artificial languages","names":["Artificial languages"],"iso639-2":"art","iso639-1":null},"Arumanian":{"name":"Arumanian","names":["Aromanian","Arumanian","Macedo-Romanian"],"iso639-2":"rup","iso639-1":null},"Assamese":{"name":"Assamese","names":["Assamese"],"iso639-2":"asm","iso639-1":"as"},"Asturian":{"name":"Asturian","names":["Asturian","Bable","Leonese","Asturleonese"],"iso639-2":"ast","iso639-1":null},"Asturleonese":{"name":"Asturleonese","names":["Asturian","Bable","Leonese","Asturleonese"],"iso639-2":"ast","iso639-1":null},"Athapascan languages":{"name":"Athapascan languages","names":["Athapascan languages"],"iso639-2":"ath","iso639-1":null},"Australian languages":{"name":"Australian languages","names":["Australian languages"],"iso639-2":"aus","iso639-1":null},"Austronesian languages":{"name":"Austronesian languages","names":["Austronesian languages"],"iso639-2":"map","iso639-1":null},"Avaric":{"name":"Avaric","names":["Avaric"],"iso639-2":"ava","iso639-1":"av"},"Avestan":{"name":"Avestan","names":["Avestan"],"iso639-2":"ave","iso639-1":"ae"},"Awadhi":{"name":"Awadhi","names":["Awadhi"],"iso639-2":"awa","iso639-1":null},"Aymara":{"name":"Aymara","names":["Aymara"],"iso639-2":"aym","iso639-1":"ay"},"Azerbaijani":{"name":"Azerbaijani","names":["Azerbaijani"],"iso639-2":"aze","iso639-1":"az"},"Bable":{"name":"Bable","names":["Asturian","Bable","Leonese","Asturleonese"],"iso639-2":"ast","iso639-1":null},"Balinese":{"name":"Balinese","names":["Balinese"],"iso639-2":"ban","iso639-1":null},"Baltic languages":{"name":"Baltic languages","names":["Baltic languages"],"iso639-2":"bat","iso639-1":null},"Baluchi":{"name":"Baluchi","names":["Baluchi"],"iso639-2":"bal","iso639-1":null},"Bambara":{"name":"Bambara","names":["Bambara"],"iso639-2":"bam","iso639-1":"bm"},"Bamileke languages":{"name":"Bamileke languages","names":["Bamileke languages"],"iso639-2":"bai","iso639-1":null},"Banda languages":{"name":"Banda languages","names":["Banda languages"],"iso639-2":"bad","iso639-1":null},"Bantu languages":{"name":"Bantu languages","names":["Bantu languages"],"iso639-2":"bnt","iso639-1":null},"Basa":{"name":"Basa","names":["Basa"],"iso639-2":"bas","iso639-1":null},"Bashkir":{"name":"Bashkir","names":["Bashkir"],"iso639-2":"bak","iso639-1":"ba"},"Basque":{"name":"Basque","names":["Basque"],"iso639-2":"baq/eus","iso639-1":"eu"},"Batak languages":{"name":"Batak languages","names":["Batak languages"],"iso639-2":"btk","iso639-1":null},"Bedawiyet":{"name":"Bedawiyet","names":["Beja","Bedawiyet"],"iso639-2":"bej","iso639-1":null},"Beja":{"name":"Beja","names":["Beja","Bedawiyet"],"iso639-2":"bej","iso639-1":null},"Belarusian":{"name":"Belarusian","names":["Belarusian"],"iso639-2":"bel","iso639-1":"be"},"Bemba":{"name":"Bemba","names":["Bemba"],"iso639-2":"bem","iso639-1":null},"Bengali":{"name":"Bengali","names":["Bengali"],"iso639-2":"ben","iso639-1":"bn"},"Berber languages":{"name":"Berber languages","names":["Berber languages"],"iso639-2":"ber","iso639-1":null},"Bhojpuri":{"name":"Bhojpuri","names":["Bhojpuri"],"iso639-2":"bho","iso639-1":null},"Bihari languages":{"name":"Bihari languages","names":["Bihari languages"],"iso639-2":"bih","iso639-1":"bh"},"Bikol":{"name":"Bikol","names":["Bikol"],"iso639-2":"bik","iso639-1":null},"Bilin":{"name":"Bilin","names":["Blin","Bilin"],"iso639-2":"byn","iso639-1":null},"Bini":{"name":"Bini","names":["Bini","Edo"],"iso639-2":"bin","iso639-1":null},"Bislama":{"name":"Bislama","names":["Bislama"],"iso639-2":"bis","iso639-1":"bi"},"Blin":{"name":"Blin","names":["Blin","Bilin"],"iso639-2":"byn","iso639-1":null},"Bliss":{"name":"Bliss","names":["Blissymbols","Blissymbolics","Bliss"],"iso639-2":"zbl","iso639-1":null},"Blissymbolics":{"name":"Blissymbolics","names":["Blissymbols","Blissymbolics","Bliss"],"iso639-2":"zbl","iso639-1":null},"Blissymbols":{"name":"Blissymbols","names":["Blissymbols","Blissymbolics","Bliss"],"iso639-2":"zbl","iso639-1":null},"Bokmål, Norwegian":{"name":"Bokmål, Norwegian","names":["Bokmål, Norwegian","Norwegian Bokmål"],"iso639-2":"nob","iso639-1":"nb"},"Bosnian":{"name":"Bosnian","names":["Bosnian"],"iso639-2":"bos","iso639-1":"bs"},"Braj":{"name":"Braj","names":["Braj"],"iso639-2":"bra","iso639-1":null},"Breton":{"name":"Breton","names":["Breton"],"iso639-2":"bre","iso639-1":"br"},"Buginese":{"name":"Buginese","names":["Buginese"],"iso639-2":"bug","iso639-1":null},"Bulgarian":{"name":"Bulgarian","names":["Bulgarian"],"iso639-2":"bul","iso639-1":"bg"},"Buriat":{"name":"Buriat","names":["Buriat"],"iso639-2":"bua","iso639-1":null},"Burmese":{"name":"Burmese","names":["Burmese"],"iso639-2":"bur/mya","iso639-1":"my"},"Caddo":{"name":"Caddo","names":["Caddo"],"iso639-2":"cad","iso639-1":null},"Castilian":{"name":"Castilian","names":["Spanish","Castilian"],"iso639-2":"spa","iso639-1":"es"},"Catalan":{"name":"Catalan","names":["Catalan","Valencian"],"iso639-2":"cat","iso639-1":"ca"},"Caucasian languages":{"name":"Caucasian languages","names":["Caucasian languages"],"iso639-2":"cau","iso639-1":null},"Cebuano":{"name":"Cebuano","names":["Cebuano"],"iso639-2":"ceb","iso639-1":null},"Celtic languages":{"name":"Celtic languages","names":["Celtic languages"],"iso639-2":"cel","iso639-1":null},"Central American Indian languages":{"name":"Central American Indian languages","names":["Central American Indian languages"],"iso639-2":"cai","iso639-1":null},"Central Khmer":{"name":"Central Khmer","names":["Central Khmer"],"iso639-2":"khm","iso639-1":"km"},"Chagatai":{"name":"Chagatai","names":["Chagatai"],"iso639-2":"chg","iso639-1":null},"Chamic languages":{"name":"Chamic languages","names":["Chamic languages"],"iso639-2":"cmc","iso639-1":null},"Chamorro":{"name":"Chamorro","names":["Chamorro"],"iso639-2":"cha","iso639-1":"ch"},"Chechen":{"name":"Chechen","names":["Chechen"],"iso639-2":"che","iso639-1":"ce"},"Cherokee":{"name":"Cherokee","names":["Cherokee"],"iso639-2":"chr","iso639-1":null},"Chewa":{"name":"Chewa","names":["Chichewa","Chewa","Nyanja"],"iso639-2":"nya","iso639-1":"ny"},"Cheyenne":{"name":"Cheyenne","names":["Cheyenne"],"iso639-2":"chy","iso639-1":null},"Chibcha":{"name":"Chibcha","names":["Chibcha"],"iso639-2":"chb","iso639-1":null},"Chichewa":{"name":"Chichewa","names":["Chichewa","Chewa","Nyanja"],"iso639-2":"nya","iso639-1":"ny"},"Chinese":{"name":"Chinese","names":["Chinese"],"iso639-2":"chi/zho","iso639-1":"zh"},"Chinook jargon":{"name":"Chinook jargon","names":["Chinook jargon"],"iso639-2":"chn","iso639-1":null},"Chipewyan":{"name":"Chipewyan","names":["Chipewyan","Dene Suline"],"iso639-2":"chp","iso639-1":null},"Choctaw":{"name":"Choctaw","names":["Choctaw"],"iso639-2":"cho","iso639-1":null},"Chuang":{"name":"Chuang","names":["Zhuang","Chuang"],"iso639-2":"zha","iso639-1":"za"},"Church Slavic":{"name":"Church Slavic","names":["Church Slavic","Old Slavonic","Church Slavonic","Old Bulgarian","Old Church Slavonic"],"iso639-2":"chu","iso639-1":"cu"},"Church Slavonic":{"name":"Church Slavonic","names":["Church Slavic","Old Slavonic","Church Slavonic","Old Bulgarian","Old Church Slavonic"],"iso639-2":"chu","iso639-1":"cu"},"Chuukese":{"name":"Chuukese","names":["Chuukese"],"iso639-2":"chk","iso639-1":null},"Chuvash":{"name":"Chuvash","names":["Chuvash"],"iso639-2":"chv","iso639-1":"cv"},"Classical Nepal Bhasa":{"name":"Classical Nepal Bhasa","names":["Classical Newari","Old Newari","Classical Nepal Bhasa"],"iso639-2":"nwc","iso639-1":null},"Classical Newari":{"name":"Classical Newari","names":["Classical Newari","Old Newari","Classical Nepal Bhasa"],"iso639-2":"nwc","iso639-1":null},"Classical Syriac":{"name":"Classical Syriac","names":["Classical Syriac"],"iso639-2":"syc","iso639-1":null},"Cook Islands Maori":{"name":"Cook Islands Maori","names":["Rarotongan","Cook Islands Maori"],"iso639-2":"rar","iso639-1":null},"Coptic":{"name":"Coptic","names":["Coptic"],"iso639-2":"cop","iso639-1":null},"Cornish":{"name":"Cornish","names":["Cornish"],"iso639-2":"cor","iso639-1":"kw"},"Corsican":{"name":"Corsican","names":["Corsican"],"iso639-2":"cos","iso639-1":"co"},"Cree":{"name":"Cree","names":["Cree"],"iso639-2":"cre","iso639-1":"cr"},"Creek":{"name":"Creek","names":["Creek"],"iso639-2":"mus","iso639-1":null},"Creoles and pidgins":{"name":"Creoles and pidgins","names":["Creoles and pidgins"],"iso639-2":"crp","iso639-1":null},"Creoles and pidgins, English based":{"name":"Creoles and pidgins, English based","names":["Creoles and pidgins, English based"],"iso639-2":"cpe","iso639-1":null},"Creoles and pidgins, French-based":{"name":"Creoles and pidgins, French-based","names":["Creoles and pidgins, French-based"],"iso639-2":"cpf","iso639-1":null},"Creoles and pidgins, Portuguese-based":{"name":"Creoles and pidgins, Portuguese-based","names":["Creoles and pidgins, Portuguese-based"],"iso639-2":"cpp","iso639-1":null},"Crimean Tatar":{"name":"Crimean Tatar","names":["Crimean Tatar","Crimean Turkish"],"iso639-2":"crh","iso639-1":null},"Crimean Turkish":{"name":"Crimean Turkish","names":["Crimean Tatar","Crimean Turkish"],"iso639-2":"crh","iso639-1":null},"Croatian":{"name":"Croatian","names":["Croatian"],"iso639-2":"hrv","iso639-1":"hr"},"Cushitic languages":{"name":"Cushitic languages","names":["Cushitic languages"],"iso639-2":"cus","iso639-1":null},"Czech":{"name":"Czech","names":["Czech"],"iso639-2":"cze/ces","iso639-1":"cs"},"Dakota":{"name":"Dakota","names":["Dakota"],"iso639-2":"dak","iso639-1":null},"Danish":{"name":"Danish","names":["Danish"],"iso639-2":"dan","iso639-1":"da"},"Dargwa":{"name":"Dargwa","names":["Dargwa"],"iso639-2":"dar","iso639-1":null},"Delaware":{"name":"Delaware","names":["Delaware"],"iso639-2":"del","iso639-1":null},"Dene Suline":{"name":"Dene Suline","names":["Chipewyan","Dene Suline"],"iso639-2":"chp","iso639-1":null},"Dhivehi":{"name":"Dhivehi","names":["Divehi","Dhivehi","Maldivian"],"iso639-2":"div","iso639-1":"dv"},"Dimili":{"name":"Dimili","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Dimli":{"name":"Dimli","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Dinka":{"name":"Dinka","names":["Dinka"],"iso639-2":"din","iso639-1":null},"Divehi":{"name":"Divehi","names":["Divehi","Dhivehi","Maldivian"],"iso639-2":"div","iso639-1":"dv"},"Dogri":{"name":"Dogri","names":["Dogri"],"iso639-2":"doi","iso639-1":null},"Dogrib":{"name":"Dogrib","names":["Dogrib"],"iso639-2":"dgr","iso639-1":null},"Dravidian languages":{"name":"Dravidian languages","names":["Dravidian languages"],"iso639-2":"dra","iso639-1":null},"Duala":{"name":"Duala","names":["Duala"],"iso639-2":"dua","iso639-1":null},"Dutch":{"name":"Dutch","names":["Dutch","Flemish"],"iso639-2":"dut/nld","iso639-1":"nl"},"Dutch, Middle (ca.1050-1350)":{"name":"Dutch, Middle (ca.1050-1350)","names":["Dutch, Middle (ca.1050-1350)"],"iso639-2":"dum","iso639-1":null},"Dyula":{"name":"Dyula","names":["Dyula"],"iso639-2":"dyu","iso639-1":null},"Dzongkha":{"name":"Dzongkha","names":["Dzongkha"],"iso639-2":"dzo","iso639-1":"dz"},"Eastern Frisian":{"name":"Eastern Frisian","names":["Eastern Frisian"],"iso639-2":"frs","iso639-1":null},"Edo":{"name":"Edo","names":["Bini","Edo"],"iso639-2":"bin","iso639-1":null},"Efik":{"name":"Efik","names":["Efik"],"iso639-2":"efi","iso639-1":null},"Egyptian (Ancient)":{"name":"Egyptian (Ancient)","names":["Egyptian (Ancient)"],"iso639-2":"egy","iso639-1":null},"Ekajuk":{"name":"Ekajuk","names":["Ekajuk"],"iso639-2":"eka","iso639-1":null},"Elamite":{"name":"Elamite","names":["Elamite"],"iso639-2":"elx","iso639-1":null},"English":{"name":"English","names":["English"],"iso639-2":"eng","iso639-1":"en"},"English, Middle (1100-1500)":{"name":"English, Middle (1100-1500)","names":["English, Middle (1100-1500)"],"iso639-2":"enm","iso639-1":null},"English, Old (ca.450-1100)":{"name":"English, Old (ca.450-1100)","names":["English, Old (ca.450-1100)"],"iso639-2":"ang","iso639-1":null},"Erzya":{"name":"Erzya","names":["Erzya"],"iso639-2":"myv","iso639-1":null},"Esperanto":{"name":"Esperanto","names":["Esperanto"],"iso639-2":"epo","iso639-1":"eo"},"Estonian":{"name":"Estonian","names":["Estonian"],"iso639-2":"est","iso639-1":"et"},"Ewe":{"name":"Ewe","names":["Ewe"],"iso639-2":"ewe","iso639-1":"ee"},"Ewondo":{"name":"Ewondo","names":["Ewondo"],"iso639-2":"ewo","iso639-1":null},"Fang":{"name":"Fang","names":["Fang"],"iso639-2":"fan","iso639-1":null},"Fanti":{"name":"Fanti","names":["Fanti"],"iso639-2":"fat","iso639-1":null},"Faroese":{"name":"Faroese","names":["Faroese"],"iso639-2":"fao","iso639-1":"fo"},"Fijian":{"name":"Fijian","names":["Fijian"],"iso639-2":"fij","iso639-1":"fj"},"Filipino":{"name":"Filipino","names":["Filipino","Pilipino"],"iso639-2":"fil","iso639-1":null},"Finnish":{"name":"Finnish","names":["Finnish"],"iso639-2":"fin","iso639-1":"fi"},"Finno-Ugrian languages":{"name":"Finno-Ugrian languages","names":["Finno-Ugrian languages"],"iso639-2":"fiu","iso639-1":null},"Flemish":{"name":"Flemish","names":["Dutch","Flemish"],"iso639-2":"dut/nld","iso639-1":"nl"},"Fon":{"name":"Fon","names":["Fon"],"iso639-2":"fon","iso639-1":null},"French":{"name":"French","names":["French"],"iso639-2":"fre/fra","iso639-1":"fr"},"French, Middle (ca.1400-1600)":{"name":"French, Middle (ca.1400-1600)","names":["French, Middle (ca.1400-1600)"],"iso639-2":"frm","iso639-1":null},"French, Old (842-ca.1400)":{"name":"French, Old (842-ca.1400)","names":["French, Old (842-ca.1400)"],"iso639-2":"fro","iso639-1":null},"Friulian":{"name":"Friulian","names":["Friulian"],"iso639-2":"fur","iso639-1":null},"Fulah":{"name":"Fulah","names":["Fulah"],"iso639-2":"ful","iso639-1":"ff"},"Ga":{"name":"Ga","names":["Ga"],"iso639-2":"gaa","iso639-1":null},"Gaelic":{"name":"Gaelic","names":["Gaelic","Scottish Gaelic"],"iso639-2":"gla","iso639-1":"gd"},"Galibi Carib":{"name":"Galibi Carib","names":["Galibi Carib"],"iso639-2":"car","iso639-1":null},"Galician":{"name":"Galician","names":["Galician"],"iso639-2":"glg","iso639-1":"gl"},"Ganda":{"name":"Ganda","names":["Ganda"],"iso639-2":"lug","iso639-1":"lg"},"Gayo":{"name":"Gayo","names":["Gayo"],"iso639-2":"gay","iso639-1":null},"Gbaya":{"name":"Gbaya","names":["Gbaya"],"iso639-2":"gba","iso639-1":null},"Geez":{"name":"Geez","names":["Geez"],"iso639-2":"gez","iso639-1":null},"Georgian":{"name":"Georgian","names":["Georgian"],"iso639-2":"geo/kat","iso639-1":"ka"},"German":{"name":"German","names":["German"],"iso639-2":"ger/deu","iso639-1":"de"},"German, Low":{"name":"German, Low","names":["Low German","Low Saxon","German, Low","Saxon, Low"],"iso639-2":"nds","iso639-1":null},"German, Middle High (ca.1050-1500)":{"name":"German, Middle High (ca.1050-1500)","names":["German, Middle High (ca.1050-1500)"],"iso639-2":"gmh","iso639-1":null},"German, Old High (ca.750-1050)":{"name":"German, Old High (ca.750-1050)","names":["German, Old High (ca.750-1050)"],"iso639-2":"goh","iso639-1":null},"Germanic languages":{"name":"Germanic languages","names":["Germanic languages"],"iso639-2":"gem","iso639-1":null},"Gikuyu":{"name":"Gikuyu","names":["Kikuyu","Gikuyu"],"iso639-2":"kik","iso639-1":"ki"},"Gilbertese":{"name":"Gilbertese","names":["Gilbertese"],"iso639-2":"gil","iso639-1":null},"Gondi":{"name":"Gondi","names":["Gondi"],"iso639-2":"gon","iso639-1":null},"Gorontalo":{"name":"Gorontalo","names":["Gorontalo"],"iso639-2":"gor","iso639-1":null},"Gothic":{"name":"Gothic","names":["Gothic"],"iso639-2":"got","iso639-1":null},"Grebo":{"name":"Grebo","names":["Grebo"],"iso639-2":"grb","iso639-1":null},"Greek, Ancient (to 1453)":{"name":"Greek, Ancient (to 1453)","names":["Greek, Ancient (to 1453)"],"iso639-2":"grc","iso639-1":null},"Greek, Modern (1453-)":{"name":"Greek, Modern (1453-)","names":["Greek, Modern (1453-)"],"iso639-2":"gre/ell","iso639-1":"el"},"Greenlandic":{"name":"Greenlandic","names":["Kalaallisut","Greenlandic"],"iso639-2":"kal","iso639-1":"kl"},"Guarani":{"name":"Guarani","names":["Guarani"],"iso639-2":"grn","iso639-1":"gn"},"Gujarati":{"name":"Gujarati","names":["Gujarati"],"iso639-2":"guj","iso639-1":"gu"},"Gwich\'in":{"name":"Gwich\'in","names":["Gwich\'in"],"iso639-2":"gwi","iso639-1":null},"Haida":{"name":"Haida","names":["Haida"],"iso639-2":"hai","iso639-1":null},"Haitian":{"name":"Haitian","names":["Haitian","Haitian Creole"],"iso639-2":"hat","iso639-1":"ht"},"Haitian Creole":{"name":"Haitian Creole","names":["Haitian","Haitian Creole"],"iso639-2":"hat","iso639-1":"ht"},"Hausa":{"name":"Hausa","names":["Hausa"],"iso639-2":"hau","iso639-1":"ha"},"Hawaiian":{"name":"Hawaiian","names":["Hawaiian"],"iso639-2":"haw","iso639-1":null},"Hebrew":{"name":"Hebrew","names":["Hebrew"],"iso639-2":"heb","iso639-1":"he"},"Herero":{"name":"Herero","names":["Herero"],"iso639-2":"her","iso639-1":"hz"},"Hiligaynon":{"name":"Hiligaynon","names":["Hiligaynon"],"iso639-2":"hil","iso639-1":null},"Himachali languages":{"name":"Himachali languages","names":["Himachali languages","Western Pahari languages"],"iso639-2":"him","iso639-1":null},"Hindi":{"name":"Hindi","names":["Hindi"],"iso639-2":"hin","iso639-1":"hi"},"Hiri Motu":{"name":"Hiri Motu","names":["Hiri Motu"],"iso639-2":"hmo","iso639-1":"ho"},"Hittite":{"name":"Hittite","names":["Hittite"],"iso639-2":"hit","iso639-1":null},"Hmong":{"name":"Hmong","names":["Hmong","Mong"],"iso639-2":"hmn","iso639-1":null},"Hungarian":{"name":"Hungarian","names":["Hungarian"],"iso639-2":"hun","iso639-1":"hu"},"Hupa":{"name":"Hupa","names":["Hupa"],"iso639-2":"hup","iso639-1":null},"Iban":{"name":"Iban","names":["Iban"],"iso639-2":"iba","iso639-1":null},"Icelandic":{"name":"Icelandic","names":["Icelandic"],"iso639-2":"ice/isl","iso639-1":"is"},"Ido":{"name":"Ido","names":["Ido"],"iso639-2":"ido","iso639-1":"io"},"Igbo":{"name":"Igbo","names":["Igbo"],"iso639-2":"ibo","iso639-1":"ig"},"Ijo languages":{"name":"Ijo languages","names":["Ijo languages"],"iso639-2":"ijo","iso639-1":null},"Iloko":{"name":"Iloko","names":["Iloko"],"iso639-2":"ilo","iso639-1":null},"Imperial Aramaic (700-300 BCE)":{"name":"Imperial Aramaic (700-300 BCE)","names":["Official Aramaic (700-300 BCE)","Imperial Aramaic (700-300 BCE)"],"iso639-2":"arc","iso639-1":null},"Inari Sami":{"name":"Inari Sami","names":["Inari Sami"],"iso639-2":"smn","iso639-1":null},"Indic languages":{"name":"Indic languages","names":["Indic languages"],"iso639-2":"inc","iso639-1":null},"Indo-European languages":{"name":"Indo-European languages","names":["Indo-European languages"],"iso639-2":"ine","iso639-1":null},"Indonesian":{"name":"Indonesian","names":["Indonesian"],"iso639-2":"ind","iso639-1":"id"},"Ingush":{"name":"Ingush","names":["Ingush"],"iso639-2":"inh","iso639-1":null},"Interlingua (International Auxiliary Language Association)":{"name":"Interlingua (International Auxiliary Language Association)","names":["Interlingua (International Auxiliary Language Association)"],"iso639-2":"ina","iso639-1":"ia"},"Interlingue":{"name":"Interlingue","names":["Interlingue","Occidental"],"iso639-2":"ile","iso639-1":"ie"},"Inuktitut":{"name":"Inuktitut","names":["Inuktitut"],"iso639-2":"iku","iso639-1":"iu"},"Inupiaq":{"name":"Inupiaq","names":["Inupiaq"],"iso639-2":"ipk","iso639-1":"ik"},"Iranian languages":{"name":"Iranian languages","names":["Iranian languages"],"iso639-2":"ira","iso639-1":null},"Irish":{"name":"Irish","names":["Irish"],"iso639-2":"gle","iso639-1":"ga"},"Irish, Middle (900-1200)":{"name":"Irish, Middle (900-1200)","names":["Irish, Middle (900-1200)"],"iso639-2":"mga","iso639-1":null},"Irish, Old (to 900)":{"name":"Irish, Old (to 900)","names":["Irish, Old (to 900)"],"iso639-2":"sga","iso639-1":null},"Iroquoian languages":{"name":"Iroquoian languages","names":["Iroquoian languages"],"iso639-2":"iro","iso639-1":null},"Italian":{"name":"Italian","names":["Italian"],"iso639-2":"ita","iso639-1":"it"},"Japanese":{"name":"Japanese","names":["Japanese"],"iso639-2":"jpn","iso639-1":"ja"},"Javanese":{"name":"Javanese","names":["Javanese"],"iso639-2":"jav","iso639-1":"jv"},"Jingpho":{"name":"Jingpho","names":["Kachin","Jingpho"],"iso639-2":"kac","iso639-1":null},"Judeo-Arabic":{"name":"Judeo-Arabic","names":["Judeo-Arabic"],"iso639-2":"jrb","iso639-1":null},"Judeo-Persian":{"name":"Judeo-Persian","names":["Judeo-Persian"],"iso639-2":"jpr","iso639-1":null},"Kabardian":{"name":"Kabardian","names":["Kabardian"],"iso639-2":"kbd","iso639-1":null},"Kabyle":{"name":"Kabyle","names":["Kabyle"],"iso639-2":"kab","iso639-1":null},"Kachin":{"name":"Kachin","names":["Kachin","Jingpho"],"iso639-2":"kac","iso639-1":null},"Kalaallisut":{"name":"Kalaallisut","names":["Kalaallisut","Greenlandic"],"iso639-2":"kal","iso639-1":"kl"},"Kalmyk":{"name":"Kalmyk","names":["Kalmyk","Oirat"],"iso639-2":"xal","iso639-1":null},"Kamba":{"name":"Kamba","names":["Kamba"],"iso639-2":"kam","iso639-1":null},"Kannada":{"name":"Kannada","names":["Kannada"],"iso639-2":"kan","iso639-1":"kn"},"Kanuri":{"name":"Kanuri","names":["Kanuri"],"iso639-2":"kau","iso639-1":"kr"},"Kapampangan":{"name":"Kapampangan","names":["Pampanga","Kapampangan"],"iso639-2":"pam","iso639-1":null},"Kara-Kalpak":{"name":"Kara-Kalpak","names":["Kara-Kalpak"],"iso639-2":"kaa","iso639-1":null},"Karachay-Balkar":{"name":"Karachay-Balkar","names":["Karachay-Balkar"],"iso639-2":"krc","iso639-1":null},"Karelian":{"name":"Karelian","names":["Karelian"],"iso639-2":"krl","iso639-1":null},"Karen languages":{"name":"Karen languages","names":["Karen languages"],"iso639-2":"kar","iso639-1":null},"Kashmiri":{"name":"Kashmiri","names":["Kashmiri"],"iso639-2":"kas","iso639-1":"ks"},"Kashubian":{"name":"Kashubian","names":["Kashubian"],"iso639-2":"csb","iso639-1":null},"Kawi":{"name":"Kawi","names":["Kawi"],"iso639-2":"kaw","iso639-1":null},"Kazakh":{"name":"Kazakh","names":["Kazakh"],"iso639-2":"kaz","iso639-1":"kk"},"Khasi":{"name":"Khasi","names":["Khasi"],"iso639-2":"kha","iso639-1":null},"Khoisan languages":{"name":"Khoisan languages","names":["Khoisan languages"],"iso639-2":"khi","iso639-1":null},"Khotanese":{"name":"Khotanese","names":["Khotanese","Sakan"],"iso639-2":"kho","iso639-1":null},"Kikuyu":{"name":"Kikuyu","names":["Kikuyu","Gikuyu"],"iso639-2":"kik","iso639-1":"ki"},"Kimbundu":{"name":"Kimbundu","names":["Kimbundu"],"iso639-2":"kmb","iso639-1":null},"Kinyarwanda":{"name":"Kinyarwanda","names":["Kinyarwanda"],"iso639-2":"kin","iso639-1":"rw"},"Kirdki":{"name":"Kirdki","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Kirghiz":{"name":"Kirghiz","names":["Kirghiz","Kyrgyz"],"iso639-2":"kir","iso639-1":"ky"},"Kirmanjki":{"name":"Kirmanjki","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Klingon":{"name":"Klingon","names":["Klingon","tlhIngan-Hol"],"iso639-2":"tlh","iso639-1":null},"Komi":{"name":"Komi","names":["Komi"],"iso639-2":"kom","iso639-1":"kv"},"Kongo":{"name":"Kongo","names":["Kongo"],"iso639-2":"kon","iso639-1":"kg"},"Konkani":{"name":"Konkani","names":["Konkani"],"iso639-2":"kok","iso639-1":null},"Korean":{"name":"Korean","names":["Korean"],"iso639-2":"kor","iso639-1":"ko"},"Kosraean":{"name":"Kosraean","names":["Kosraean"],"iso639-2":"kos","iso639-1":null},"Kpelle":{"name":"Kpelle","names":["Kpelle"],"iso639-2":"kpe","iso639-1":null},"Kru languages":{"name":"Kru languages","names":["Kru languages"],"iso639-2":"kro","iso639-1":null},"Kuanyama":{"name":"Kuanyama","names":["Kuanyama","Kwanyama"],"iso639-2":"kua","iso639-1":"kj"},"Kumyk":{"name":"Kumyk","names":["Kumyk"],"iso639-2":"kum","iso639-1":null},"Kurdish":{"name":"Kurdish","names":["Kurdish"],"iso639-2":"kur","iso639-1":"ku"},"Kurukh":{"name":"Kurukh","names":["Kurukh"],"iso639-2":"kru","iso639-1":null},"Kutenai":{"name":"Kutenai","names":["Kutenai"],"iso639-2":"kut","iso639-1":null},"Kwanyama":{"name":"Kwanyama","names":["Kuanyama","Kwanyama"],"iso639-2":"kua","iso639-1":"kj"},"Kyrgyz":{"name":"Kyrgyz","names":["Kirghiz","Kyrgyz"],"iso639-2":"kir","iso639-1":"ky"},"Ladino":{"name":"Ladino","names":["Ladino"],"iso639-2":"lad","iso639-1":null},"Lahnda":{"name":"Lahnda","names":["Lahnda"],"iso639-2":"lah","iso639-1":null},"Lamba":{"name":"Lamba","names":["Lamba"],"iso639-2":"lam","iso639-1":null},"Land Dayak languages":{"name":"Land Dayak languages","names":["Land Dayak languages"],"iso639-2":"day","iso639-1":null},"Lao":{"name":"Lao","names":["Lao"],"iso639-2":"lao","iso639-1":"lo"},"Latin":{"name":"Latin","names":["Latin"],"iso639-2":"lat","iso639-1":"la"},"Latvian":{"name":"Latvian","names":["Latvian"],"iso639-2":"lav","iso639-1":"lv"},"Leonese":{"name":"Leonese","names":["Asturian","Bable","Leonese","Asturleonese"],"iso639-2":"ast","iso639-1":null},"Letzeburgesch":{"name":"Letzeburgesch","names":["Luxembourgish","Letzeburgesch"],"iso639-2":"ltz","iso639-1":"lb"},"Lezghian":{"name":"Lezghian","names":["Lezghian"],"iso639-2":"lez","iso639-1":null},"Limburgan":{"name":"Limburgan","names":["Limburgan","Limburger","Limburgish"],"iso639-2":"lim","iso639-1":"li"},"Limburger":{"name":"Limburger","names":["Limburgan","Limburger","Limburgish"],"iso639-2":"lim","iso639-1":"li"},"Limburgish":{"name":"Limburgish","names":["Limburgan","Limburger","Limburgish"],"iso639-2":"lim","iso639-1":"li"},"Lingala":{"name":"Lingala","names":["Lingala"],"iso639-2":"lin","iso639-1":"ln"},"Lithuanian":{"name":"Lithuanian","names":["Lithuanian"],"iso639-2":"lit","iso639-1":"lt"},"Lojban":{"name":"Lojban","names":["Lojban"],"iso639-2":"jbo","iso639-1":null},"Low German":{"name":"Low German","names":["Low German","Low Saxon","German, Low","Saxon, Low"],"iso639-2":"nds","iso639-1":null},"Low Saxon":{"name":"Low Saxon","names":["Low German","Low Saxon","German, Low","Saxon, Low"],"iso639-2":"nds","iso639-1":null},"Lower Sorbian":{"name":"Lower Sorbian","names":["Lower Sorbian"],"iso639-2":"dsb","iso639-1":null},"Lozi":{"name":"Lozi","names":["Lozi"],"iso639-2":"loz","iso639-1":null},"Luba-Katanga":{"name":"Luba-Katanga","names":["Luba-Katanga"],"iso639-2":"lub","iso639-1":"lu"},"Luba-Lulua":{"name":"Luba-Lulua","names":["Luba-Lulua"],"iso639-2":"lua","iso639-1":null},"Luiseno":{"name":"Luiseno","names":["Luiseno"],"iso639-2":"lui","iso639-1":null},"Lule Sami":{"name":"Lule Sami","names":["Lule Sami"],"iso639-2":"smj","iso639-1":null},"Lunda":{"name":"Lunda","names":["Lunda"],"iso639-2":"lun","iso639-1":null},"Luo (Kenya and Tanzania)":{"name":"Luo (Kenya and Tanzania)","names":["Luo (Kenya and Tanzania)"],"iso639-2":"luo","iso639-1":null},"Lushai":{"name":"Lushai","names":["Lushai"],"iso639-2":"lus","iso639-1":null},"Luxembourgish":{"name":"Luxembourgish","names":["Luxembourgish","Letzeburgesch"],"iso639-2":"ltz","iso639-1":"lb"},"Macedo-Romanian":{"name":"Macedo-Romanian","names":["Aromanian","Arumanian","Macedo-Romanian"],"iso639-2":"rup","iso639-1":null},"Macedonian":{"name":"Macedonian","names":["Macedonian"],"iso639-2":"mac/mkd","iso639-1":"mk"},"Madurese":{"name":"Madurese","names":["Madurese"],"iso639-2":"mad","iso639-1":null},"Magahi":{"name":"Magahi","names":["Magahi"],"iso639-2":"mag","iso639-1":null},"Maithili":{"name":"Maithili","names":["Maithili"],"iso639-2":"mai","iso639-1":null},"Makasar":{"name":"Makasar","names":["Makasar"],"iso639-2":"mak","iso639-1":null},"Malagasy":{"name":"Malagasy","names":["Malagasy"],"iso639-2":"mlg","iso639-1":"mg"},"Malay":{"name":"Malay","names":["Malay"],"iso639-2":"may/msa","iso639-1":"ms"},"Malayalam":{"name":"Malayalam","names":["Malayalam"],"iso639-2":"mal","iso639-1":"ml"},"Maldivian":{"name":"Maldivian","names":["Divehi","Dhivehi","Maldivian"],"iso639-2":"div","iso639-1":"dv"},"Maltese":{"name":"Maltese","names":["Maltese"],"iso639-2":"mlt","iso639-1":"mt"},"Manchu":{"name":"Manchu","names":["Manchu"],"iso639-2":"mnc","iso639-1":null},"Mandar":{"name":"Mandar","names":["Mandar"],"iso639-2":"mdr","iso639-1":null},"Mandingo":{"name":"Mandingo","names":["Mandingo"],"iso639-2":"man","iso639-1":null},"Manipuri":{"name":"Manipuri","names":["Manipuri"],"iso639-2":"mni","iso639-1":null},"Manobo languages":{"name":"Manobo languages","names":["Manobo languages"],"iso639-2":"mno","iso639-1":null},"Manx":{"name":"Manx","names":["Manx"],"iso639-2":"glv","iso639-1":"gv"},"Maori":{"name":"Maori","names":["Maori"],"iso639-2":"mao/mri","iso639-1":"mi"},"Mapuche":{"name":"Mapuche","names":["Mapudungun","Mapuche"],"iso639-2":"arn","iso639-1":null},"Mapudungun":{"name":"Mapudungun","names":["Mapudungun","Mapuche"],"iso639-2":"arn","iso639-1":null},"Marathi":{"name":"Marathi","names":["Marathi"],"iso639-2":"mar","iso639-1":"mr"},"Mari":{"name":"Mari","names":["Mari"],"iso639-2":"chm","iso639-1":null},"Marshallese":{"name":"Marshallese","names":["Marshallese"],"iso639-2":"mah","iso639-1":"mh"},"Marwari":{"name":"Marwari","names":["Marwari"],"iso639-2":"mwr","iso639-1":null},"Masai":{"name":"Masai","names":["Masai"],"iso639-2":"mas","iso639-1":null},"Mayan languages":{"name":"Mayan languages","names":["Mayan languages"],"iso639-2":"myn","iso639-1":null},"Mende":{"name":"Mende","names":["Mende"],"iso639-2":"men","iso639-1":null},"Mi\'kmaq":{"name":"Mi\'kmaq","names":["Mi\'kmaq","Micmac"],"iso639-2":"mic","iso639-1":null},"Micmac":{"name":"Micmac","names":["Mi\'kmaq","Micmac"],"iso639-2":"mic","iso639-1":null},"Minangkabau":{"name":"Minangkabau","names":["Minangkabau"],"iso639-2":"min","iso639-1":null},"Mirandese":{"name":"Mirandese","names":["Mirandese"],"iso639-2":"mwl","iso639-1":null},"Mohawk":{"name":"Mohawk","names":["Mohawk"],"iso639-2":"moh","iso639-1":null},"Moksha":{"name":"Moksha","names":["Moksha"],"iso639-2":"mdf","iso639-1":null},"Moldavian":{"name":"Moldavian","names":["Romanian","Moldavian","Moldovan"],"iso639-2":"rum/ron","iso639-1":"ro"},"Moldovan":{"name":"Moldovan","names":["Romanian","Moldavian","Moldovan"],"iso639-2":"rum/ron","iso639-1":"ro"},"Mon-Khmer languages":{"name":"Mon-Khmer languages","names":["Mon-Khmer languages"],"iso639-2":"mkh","iso639-1":null},"Mong":{"name":"Mong","names":["Hmong","Mong"],"iso639-2":"hmn","iso639-1":null},"Mongo":{"name":"Mongo","names":["Mongo"],"iso639-2":"lol","iso639-1":null},"Mongolian":{"name":"Mongolian","names":["Mongolian"],"iso639-2":"mon","iso639-1":"mn"},"Montenegrin":{"name":"Montenegrin","names":["Montenegrin"],"iso639-2":"cnr","iso639-1":null},"Mossi":{"name":"Mossi","names":["Mossi"],"iso639-2":"mos","iso639-1":null},"Multiple languages":{"name":"Multiple languages","names":["Multiple languages"],"iso639-2":"mul","iso639-1":null},"Munda languages":{"name":"Munda languages","names":["Munda languages"],"iso639-2":"mun","iso639-1":null},"N\'Ko":{"name":"N\'Ko","names":["N\'Ko"],"iso639-2":"nqo","iso639-1":null},"Nahuatl languages":{"name":"Nahuatl languages","names":["Nahuatl languages"],"iso639-2":"nah","iso639-1":null},"Nauru":{"name":"Nauru","names":["Nauru"],"iso639-2":"nau","iso639-1":"na"},"Navaho":{"name":"Navaho","names":["Navajo","Navaho"],"iso639-2":"nav","iso639-1":"nv"},"Navajo":{"name":"Navajo","names":["Navajo","Navaho"],"iso639-2":"nav","iso639-1":"nv"},"Ndebele, North":{"name":"Ndebele, North","names":["Ndebele, North","North Ndebele"],"iso639-2":"nde","iso639-1":"nd"},"Ndebele, South":{"name":"Ndebele, South","names":["Ndebele, South","South Ndebele"],"iso639-2":"nbl","iso639-1":"nr"},"Ndonga":{"name":"Ndonga","names":["Ndonga"],"iso639-2":"ndo","iso639-1":"ng"},"Neapolitan":{"name":"Neapolitan","names":["Neapolitan"],"iso639-2":"nap","iso639-1":null},"Nepal Bhasa":{"name":"Nepal Bhasa","names":["Nepal Bhasa","Newari"],"iso639-2":"new","iso639-1":null},"Nepali":{"name":"Nepali","names":["Nepali"],"iso639-2":"nep","iso639-1":"ne"},"Newari":{"name":"Newari","names":["Nepal Bhasa","Newari"],"iso639-2":"new","iso639-1":null},"Nias":{"name":"Nias","names":["Nias"],"iso639-2":"nia","iso639-1":null},"Niger-Kordofanian languages":{"name":"Niger-Kordofanian languages","names":["Niger-Kordofanian languages"],"iso639-2":"nic","iso639-1":null},"Nilo-Saharan languages":{"name":"Nilo-Saharan languages","names":["Nilo-Saharan languages"],"iso639-2":"ssa","iso639-1":null},"Niuean":{"name":"Niuean","names":["Niuean"],"iso639-2":"niu","iso639-1":null},"No linguistic content":{"name":"No linguistic content","names":["No linguistic content","Not applicable"],"iso639-2":"zxx","iso639-1":null},"Nogai":{"name":"Nogai","names":["Nogai"],"iso639-2":"nog","iso639-1":null},"Norse, Old":{"name":"Norse, Old","names":["Norse, Old"],"iso639-2":"non","iso639-1":null},"North American Indian languages":{"name":"North American Indian languages","names":["North American Indian languages"],"iso639-2":"nai","iso639-1":null},"North Ndebele":{"name":"North Ndebele","names":["Ndebele, North","North Ndebele"],"iso639-2":"nde","iso639-1":"nd"},"Northern Frisian":{"name":"Northern Frisian","names":["Northern Frisian"],"iso639-2":"frr","iso639-1":null},"Northern Sami":{"name":"Northern Sami","names":["Northern Sami"],"iso639-2":"sme","iso639-1":"se"},"Northern Sotho":{"name":"Northern Sotho","names":["Pedi","Sepedi","Northern Sotho"],"iso639-2":"nso","iso639-1":null},"Norwegian":{"name":"Norwegian","names":["Norwegian"],"iso639-2":"nor","iso639-1":"no"},"Norwegian Bokmål":{"name":"Norwegian Bokmål","names":["Bokmål, Norwegian","Norwegian Bokmål"],"iso639-2":"nob","iso639-1":"nb"},"Norwegian Nynorsk":{"name":"Norwegian Nynorsk","names":["Norwegian Nynorsk","Nynorsk, Norwegian"],"iso639-2":"nno","iso639-1":"nn"},"Not applicable":{"name":"Not applicable","names":["No linguistic content","Not applicable"],"iso639-2":"zxx","iso639-1":null},"Nubian languages":{"name":"Nubian languages","names":["Nubian languages"],"iso639-2":"nub","iso639-1":null},"Nuosu":{"name":"Nuosu","names":["Sichuan Yi","Nuosu"],"iso639-2":"iii","iso639-1":"ii"},"Nyamwezi":{"name":"Nyamwezi","names":["Nyamwezi"],"iso639-2":"nym","iso639-1":null},"Nyanja":{"name":"Nyanja","names":["Chichewa","Chewa","Nyanja"],"iso639-2":"nya","iso639-1":"ny"},"Nyankole":{"name":"Nyankole","names":["Nyankole"],"iso639-2":"nyn","iso639-1":null},"Nynorsk, Norwegian":{"name":"Nynorsk, Norwegian","names":["Norwegian Nynorsk","Nynorsk, Norwegian"],"iso639-2":"nno","iso639-1":"nn"},"Nyoro":{"name":"Nyoro","names":["Nyoro"],"iso639-2":"nyo","iso639-1":null},"Nzima":{"name":"Nzima","names":["Nzima"],"iso639-2":"nzi","iso639-1":null},"Occidental":{"name":"Occidental","names":["Interlingue","Occidental"],"iso639-2":"ile","iso639-1":"ie"},"Occitan (post 1500)":{"name":"Occitan (post 1500)","names":["Occitan (post 1500)"],"iso639-2":"oci","iso639-1":"oc"},"Occitan, Old (to 1500)":{"name":"Occitan, Old (to 1500)","names":["Provençal, Old (to 1500)","Occitan, Old (to 1500)"],"iso639-2":"pro","iso639-1":null},"Official Aramaic (700-300 BCE)":{"name":"Official Aramaic (700-300 BCE)","names":["Official Aramaic (700-300 BCE)","Imperial Aramaic (700-300 BCE)"],"iso639-2":"arc","iso639-1":null},"Oirat":{"name":"Oirat","names":["Kalmyk","Oirat"],"iso639-2":"xal","iso639-1":null},"Ojibwa":{"name":"Ojibwa","names":["Ojibwa"],"iso639-2":"oji","iso639-1":"oj"},"Old Bulgarian":{"name":"Old Bulgarian","names":["Church Slavic","Old Slavonic","Church Slavonic","Old Bulgarian","Old Church Slavonic"],"iso639-2":"chu","iso639-1":"cu"},"Old Church Slavonic":{"name":"Old Church Slavonic","names":["Church Slavic","Old Slavonic","Church Slavonic","Old Bulgarian","Old Church Slavonic"],"iso639-2":"chu","iso639-1":"cu"},"Old Newari":{"name":"Old Newari","names":["Classical Newari","Old Newari","Classical Nepal Bhasa"],"iso639-2":"nwc","iso639-1":null},"Old Slavonic":{"name":"Old Slavonic","names":["Church Slavic","Old Slavonic","Church Slavonic","Old Bulgarian","Old Church Slavonic"],"iso639-2":"chu","iso639-1":"cu"},"Oriya":{"name":"Oriya","names":["Oriya"],"iso639-2":"ori","iso639-1":"or"},"Oromo":{"name":"Oromo","names":["Oromo"],"iso639-2":"orm","iso639-1":"om"},"Osage":{"name":"Osage","names":["Osage"],"iso639-2":"osa","iso639-1":null},"Ossetian":{"name":"Ossetian","names":["Ossetian","Ossetic"],"iso639-2":"oss","iso639-1":"os"},"Ossetic":{"name":"Ossetic","names":["Ossetian","Ossetic"],"iso639-2":"oss","iso639-1":"os"},"Otomian languages":{"name":"Otomian languages","names":["Otomian languages"],"iso639-2":"oto","iso639-1":null},"Pahlavi":{"name":"Pahlavi","names":["Pahlavi"],"iso639-2":"pal","iso639-1":null},"Palauan":{"name":"Palauan","names":["Palauan"],"iso639-2":"pau","iso639-1":null},"Pali":{"name":"Pali","names":["Pali"],"iso639-2":"pli","iso639-1":"pi"},"Pampanga":{"name":"Pampanga","names":["Pampanga","Kapampangan"],"iso639-2":"pam","iso639-1":null},"Pangasinan":{"name":"Pangasinan","names":["Pangasinan"],"iso639-2":"pag","iso639-1":null},"Panjabi":{"name":"Panjabi","names":["Panjabi","Punjabi"],"iso639-2":"pan","iso639-1":"pa"},"Papiamento":{"name":"Papiamento","names":["Papiamento"],"iso639-2":"pap","iso639-1":null},"Papuan languages":{"name":"Papuan languages","names":["Papuan languages"],"iso639-2":"paa","iso639-1":null},"Pashto":{"name":"Pashto","names":["Pushto","Pashto"],"iso639-2":"pus","iso639-1":"ps"},"Pedi":{"name":"Pedi","names":["Pedi","Sepedi","Northern Sotho"],"iso639-2":"nso","iso639-1":null},"Persian":{"name":"Persian","names":["Persian"],"iso639-2":"per/fas","iso639-1":"fa"},"Persian, Old (ca.600-400 B.C.)":{"name":"Persian, Old (ca.600-400 B.C.)","names":["Persian, Old (ca.600-400 B.C.)"],"iso639-2":"peo","iso639-1":null},"Philippine languages":{"name":"Philippine languages","names":["Philippine languages"],"iso639-2":"phi","iso639-1":null},"Phoenician":{"name":"Phoenician","names":["Phoenician"],"iso639-2":"phn","iso639-1":null},"Pilipino":{"name":"Pilipino","names":["Filipino","Pilipino"],"iso639-2":"fil","iso639-1":null},"Pohnpeian":{"name":"Pohnpeian","names":["Pohnpeian"],"iso639-2":"pon","iso639-1":null},"Polish":{"name":"Polish","names":["Polish"],"iso639-2":"pol","iso639-1":"pl"},"Portuguese":{"name":"Portuguese","names":["Portuguese"],"iso639-2":"por","iso639-1":"pt"},"Prakrit languages":{"name":"Prakrit languages","names":["Prakrit languages"],"iso639-2":"pra","iso639-1":null},"Provençal, Old (to 1500)":{"name":"Provençal, Old (to 1500)","names":["Provençal, Old (to 1500)","Occitan, Old (to 1500)"],"iso639-2":"pro","iso639-1":null},"Punjabi":{"name":"Punjabi","names":["Panjabi","Punjabi"],"iso639-2":"pan","iso639-1":"pa"},"Pushto":{"name":"Pushto","names":["Pushto","Pashto"],"iso639-2":"pus","iso639-1":"ps"},"Quechua":{"name":"Quechua","names":["Quechua"],"iso639-2":"que","iso639-1":"qu"},"Rajasthani":{"name":"Rajasthani","names":["Rajasthani"],"iso639-2":"raj","iso639-1":null},"Rapanui":{"name":"Rapanui","names":["Rapanui"],"iso639-2":"rap","iso639-1":null},"Rarotongan":{"name":"Rarotongan","names":["Rarotongan","Cook Islands Maori"],"iso639-2":"rar","iso639-1":null},"Reserved for local use":{"name":"Reserved for local use","names":["Reserved for local use"],"iso639-2":"qaa-qtz","iso639-1":null},"Romance languages":{"name":"Romance languages","names":["Romance languages"],"iso639-2":"roa","iso639-1":null},"Romanian":{"name":"Romanian","names":["Romanian","Moldavian","Moldovan"],"iso639-2":"rum/ron","iso639-1":"ro"},"Romansh":{"name":"Romansh","names":["Romansh"],"iso639-2":"roh","iso639-1":"rm"},"Romany":{"name":"Romany","names":["Romany"],"iso639-2":"rom","iso639-1":null},"Rundi":{"name":"Rundi","names":["Rundi"],"iso639-2":"run","iso639-1":"rn"},"Russian":{"name":"Russian","names":["Russian"],"iso639-2":"rus","iso639-1":"ru"},"Sakan":{"name":"Sakan","names":["Khotanese","Sakan"],"iso639-2":"kho","iso639-1":null},"Salishan languages":{"name":"Salishan languages","names":["Salishan languages"],"iso639-2":"sal","iso639-1":null},"Samaritan Aramaic":{"name":"Samaritan Aramaic","names":["Samaritan Aramaic"],"iso639-2":"sam","iso639-1":null},"Sami languages":{"name":"Sami languages","names":["Sami languages"],"iso639-2":"smi","iso639-1":null},"Samoan":{"name":"Samoan","names":["Samoan"],"iso639-2":"smo","iso639-1":"sm"},"Sandawe":{"name":"Sandawe","names":["Sandawe"],"iso639-2":"sad","iso639-1":null},"Sango":{"name":"Sango","names":["Sango"],"iso639-2":"sag","iso639-1":"sg"},"Sanskrit":{"name":"Sanskrit","names":["Sanskrit"],"iso639-2":"san","iso639-1":"sa"},"Santali":{"name":"Santali","names":["Santali"],"iso639-2":"sat","iso639-1":null},"Sardinian":{"name":"Sardinian","names":["Sardinian"],"iso639-2":"srd","iso639-1":"sc"},"Sasak":{"name":"Sasak","names":["Sasak"],"iso639-2":"sas","iso639-1":null},"Saxon, Low":{"name":"Saxon, Low","names":["Low German","Low Saxon","German, Low","Saxon, Low"],"iso639-2":"nds","iso639-1":null},"Scots":{"name":"Scots","names":["Scots"],"iso639-2":"sco","iso639-1":null},"Scottish Gaelic":{"name":"Scottish Gaelic","names":["Gaelic","Scottish Gaelic"],"iso639-2":"gla","iso639-1":"gd"},"Selkup":{"name":"Selkup","names":["Selkup"],"iso639-2":"sel","iso639-1":null},"Semitic languages":{"name":"Semitic languages","names":["Semitic languages"],"iso639-2":"sem","iso639-1":null},"Sepedi":{"name":"Sepedi","names":["Pedi","Sepedi","Northern Sotho"],"iso639-2":"nso","iso639-1":null},"Serbian":{"name":"Serbian","names":["Serbian"],"iso639-2":"srp","iso639-1":"sr"},"Serer":{"name":"Serer","names":["Serer"],"iso639-2":"srr","iso639-1":null},"Shan":{"name":"Shan","names":["Shan"],"iso639-2":"shn","iso639-1":null},"Shona":{"name":"Shona","names":["Shona"],"iso639-2":"sna","iso639-1":"sn"},"Sichuan Yi":{"name":"Sichuan Yi","names":["Sichuan Yi","Nuosu"],"iso639-2":"iii","iso639-1":"ii"},"Sicilian":{"name":"Sicilian","names":["Sicilian"],"iso639-2":"scn","iso639-1":null},"Sidamo":{"name":"Sidamo","names":["Sidamo"],"iso639-2":"sid","iso639-1":null},"Sign Languages":{"name":"Sign Languages","names":["Sign Languages"],"iso639-2":"sgn","iso639-1":null},"Siksika":{"name":"Siksika","names":["Siksika"],"iso639-2":"bla","iso639-1":null},"Sindhi":{"name":"Sindhi","names":["Sindhi"],"iso639-2":"snd","iso639-1":"sd"},"Sinhala":{"name":"Sinhala","names":["Sinhala","Sinhalese"],"iso639-2":"sin","iso639-1":"si"},"Sinhalese":{"name":"Sinhalese","names":["Sinhala","Sinhalese"],"iso639-2":"sin","iso639-1":"si"},"Sino-Tibetan languages":{"name":"Sino-Tibetan languages","names":["Sino-Tibetan languages"],"iso639-2":"sit","iso639-1":null},"Siouan languages":{"name":"Siouan languages","names":["Siouan languages"],"iso639-2":"sio","iso639-1":null},"Skolt Sami":{"name":"Skolt Sami","names":["Skolt Sami"],"iso639-2":"sms","iso639-1":null},"Slave (Athapascan)":{"name":"Slave (Athapascan)","names":["Slave (Athapascan)"],"iso639-2":"den","iso639-1":null},"Slavic languages":{"name":"Slavic languages","names":["Slavic languages"],"iso639-2":"sla","iso639-1":null},"Slovak":{"name":"Slovak","names":["Slovak"],"iso639-2":"slo/slk","iso639-1":"sk"},"Slovenian":{"name":"Slovenian","names":["Slovenian"],"iso639-2":"slv","iso639-1":"sl"},"Sogdian":{"name":"Sogdian","names":["Sogdian"],"iso639-2":"sog","iso639-1":null},"Somali":{"name":"Somali","names":["Somali"],"iso639-2":"som","iso639-1":"so"},"Songhai languages":{"name":"Songhai languages","names":["Songhai languages"],"iso639-2":"son","iso639-1":null},"Soninke":{"name":"Soninke","names":["Soninke"],"iso639-2":"snk","iso639-1":null},"Sorbian languages":{"name":"Sorbian languages","names":["Sorbian languages"],"iso639-2":"wen","iso639-1":null},"Sotho, Northern":{"name":"Sotho, Northern","names":["Pedi","Sepedi","Northern Sotho"],"iso639-2":"nso","iso639-1":null},"Sotho, Southern":{"name":"Sotho, Southern","names":["Sotho, Southern"],"iso639-2":"sot","iso639-1":"st"},"South American Indian languages":{"name":"South American Indian languages","names":["South American Indian languages"],"iso639-2":"sai","iso639-1":null},"South Ndebele":{"name":"South Ndebele","names":["Ndebele, South","South Ndebele"],"iso639-2":"nbl","iso639-1":"nr"},"Southern Altai":{"name":"Southern Altai","names":["Southern Altai"],"iso639-2":"alt","iso639-1":null},"Southern Sami":{"name":"Southern Sami","names":["Southern Sami"],"iso639-2":"sma","iso639-1":null},"Spanish":{"name":"Spanish","names":["Spanish","Castilian"],"iso639-2":"spa","iso639-1":"es"},"Sranan Tongo":{"name":"Sranan Tongo","names":["Sranan Tongo"],"iso639-2":"srn","iso639-1":null},"Standard Moroccan Tamazight":{"name":"Standard Moroccan Tamazight","names":["Standard Moroccan Tamazight"],"iso639-2":"zgh","iso639-1":null},"Sukuma":{"name":"Sukuma","names":["Sukuma"],"iso639-2":"suk","iso639-1":null},"Sumerian":{"name":"Sumerian","names":["Sumerian"],"iso639-2":"sux","iso639-1":null},"Sundanese":{"name":"Sundanese","names":["Sundanese"],"iso639-2":"sun","iso639-1":"su"},"Susu":{"name":"Susu","names":["Susu"],"iso639-2":"sus","iso639-1":null},"Swahili":{"name":"Swahili","names":["Swahili"],"iso639-2":"swa","iso639-1":"sw"},"Swati":{"name":"Swati","names":["Swati"],"iso639-2":"ssw","iso639-1":"ss"},"Swedish":{"name":"Swedish","names":["Swedish"],"iso639-2":"swe","iso639-1":"sv"},"Swiss German":{"name":"Swiss German","names":["Swiss German","Alemannic","Alsatian"],"iso639-2":"gsw","iso639-1":null},"Syriac":{"name":"Syriac","names":["Syriac"],"iso639-2":"syr","iso639-1":null},"Tagalog":{"name":"Tagalog","names":["Tagalog"],"iso639-2":"tgl","iso639-1":"tl"},"Tahitian":{"name":"Tahitian","names":["Tahitian"],"iso639-2":"tah","iso639-1":"ty"},"Tai languages":{"name":"Tai languages","names":["Tai languages"],"iso639-2":"tai","iso639-1":null},"Tajik":{"name":"Tajik","names":["Tajik"],"iso639-2":"tgk","iso639-1":"tg"},"Tamashek":{"name":"Tamashek","names":["Tamashek"],"iso639-2":"tmh","iso639-1":null},"Tamil":{"name":"Tamil","names":["Tamil"],"iso639-2":"tam","iso639-1":"ta"},"Tatar":{"name":"Tatar","names":["Tatar"],"iso639-2":"tat","iso639-1":"tt"},"Telugu":{"name":"Telugu","names":["Telugu"],"iso639-2":"tel","iso639-1":"te"},"Tereno":{"name":"Tereno","names":["Tereno"],"iso639-2":"ter","iso639-1":null},"Tetum":{"name":"Tetum","names":["Tetum"],"iso639-2":"tet","iso639-1":null},"Thai":{"name":"Thai","names":["Thai"],"iso639-2":"tha","iso639-1":"th"},"Tibetan":{"name":"Tibetan","names":["Tibetan"],"iso639-2":"tib/bod","iso639-1":"bo"},"Tigre":{"name":"Tigre","names":["Tigre"],"iso639-2":"tig","iso639-1":null},"Tigrinya":{"name":"Tigrinya","names":["Tigrinya"],"iso639-2":"tir","iso639-1":"ti"},"Timne":{"name":"Timne","names":["Timne"],"iso639-2":"tem","iso639-1":null},"Tiv":{"name":"Tiv","names":["Tiv"],"iso639-2":"tiv","iso639-1":null},"tlhIngan-Hol":{"name":"tlhIngan-Hol","names":["Klingon","tlhIngan-Hol"],"iso639-2":"tlh","iso639-1":null},"Tlingit":{"name":"Tlingit","names":["Tlingit"],"iso639-2":"tli","iso639-1":null},"Tok Pisin":{"name":"Tok Pisin","names":["Tok Pisin"],"iso639-2":"tpi","iso639-1":null},"Tokelau":{"name":"Tokelau","names":["Tokelau"],"iso639-2":"tkl","iso639-1":null},"Tonga (Nyasa)":{"name":"Tonga (Nyasa)","names":["Tonga (Nyasa)"],"iso639-2":"tog","iso639-1":null},"Tonga (Tonga Islands)":{"name":"Tonga (Tonga Islands)","names":["Tonga (Tonga Islands)"],"iso639-2":"ton","iso639-1":"to"},"Tsimshian":{"name":"Tsimshian","names":["Tsimshian"],"iso639-2":"tsi","iso639-1":null},"Tsonga":{"name":"Tsonga","names":["Tsonga"],"iso639-2":"tso","iso639-1":"ts"},"Tswana":{"name":"Tswana","names":["Tswana"],"iso639-2":"tsn","iso639-1":"tn"},"Tumbuka":{"name":"Tumbuka","names":["Tumbuka"],"iso639-2":"tum","iso639-1":null},"Tupi languages":{"name":"Tupi languages","names":["Tupi languages"],"iso639-2":"tup","iso639-1":null},"Turkish":{"name":"Turkish","names":["Turkish"],"iso639-2":"tur","iso639-1":"tr"},"Turkish, Ottoman (1500-1928)":{"name":"Turkish, Ottoman (1500-1928)","names":["Turkish, Ottoman (1500-1928)"],"iso639-2":"ota","iso639-1":null},"Turkmen":{"name":"Turkmen","names":["Turkmen"],"iso639-2":"tuk","iso639-1":"tk"},"Tuvalu":{"name":"Tuvalu","names":["Tuvalu"],"iso639-2":"tvl","iso639-1":null},"Tuvinian":{"name":"Tuvinian","names":["Tuvinian"],"iso639-2":"tyv","iso639-1":null},"Twi":{"name":"Twi","names":["Twi"],"iso639-2":"twi","iso639-1":"tw"},"Udmurt":{"name":"Udmurt","names":["Udmurt"],"iso639-2":"udm","iso639-1":null},"Ugaritic":{"name":"Ugaritic","names":["Ugaritic"],"iso639-2":"uga","iso639-1":null},"Uighur":{"name":"Uighur","names":["Uighur","Uyghur"],"iso639-2":"uig","iso639-1":"ug"},"Ukrainian":{"name":"Ukrainian","names":["Ukrainian"],"iso639-2":"ukr","iso639-1":"uk"},"Umbundu":{"name":"Umbundu","names":["Umbundu"],"iso639-2":"umb","iso639-1":null},"Uncoded languages":{"name":"Uncoded languages","names":["Uncoded languages"],"iso639-2":"mis","iso639-1":null},"Undetermined":{"name":"Undetermined","names":["Undetermined"],"iso639-2":"und","iso639-1":null},"Upper Sorbian":{"name":"Upper Sorbian","names":["Upper Sorbian"],"iso639-2":"hsb","iso639-1":null},"Urdu":{"name":"Urdu","names":["Urdu"],"iso639-2":"urd","iso639-1":"ur"},"Uyghur":{"name":"Uyghur","names":["Uighur","Uyghur"],"iso639-2":"uig","iso639-1":"ug"},"Uzbek":{"name":"Uzbek","names":["Uzbek"],"iso639-2":"uzb","iso639-1":"uz"},"Vai":{"name":"Vai","names":["Vai"],"iso639-2":"vai","iso639-1":null},"Valencian":{"name":"Valencian","names":["Catalan","Valencian"],"iso639-2":"cat","iso639-1":"ca"},"Venda":{"name":"Venda","names":["Venda"],"iso639-2":"ven","iso639-1":"ve"},"Vietnamese":{"name":"Vietnamese","names":["Vietnamese"],"iso639-2":"vie","iso639-1":"vi"},"Volapük":{"name":"Volapük","names":["Volapük"],"iso639-2":"vol","iso639-1":"vo"},"Votic":{"name":"Votic","names":["Votic"],"iso639-2":"vot","iso639-1":null},"Wakashan languages":{"name":"Wakashan languages","names":["Wakashan languages"],"iso639-2":"wak","iso639-1":null},"Walloon":{"name":"Walloon","names":["Walloon"],"iso639-2":"wln","iso639-1":"wa"},"Waray":{"name":"Waray","names":["Waray"],"iso639-2":"war","iso639-1":null},"Washo":{"name":"Washo","names":["Washo"],"iso639-2":"was","iso639-1":null},"Welsh":{"name":"Welsh","names":["Welsh"],"iso639-2":"wel/cym","iso639-1":"cy"},"Western Frisian":{"name":"Western Frisian","names":["Western Frisian"],"iso639-2":"fry","iso639-1":"fy"},"Western Pahari languages":{"name":"Western Pahari languages","names":["Himachali languages","Western Pahari languages"],"iso639-2":"him","iso639-1":null},"Wolaitta":{"name":"Wolaitta","names":["Wolaitta","Wolaytta"],"iso639-2":"wal","iso639-1":null},"Wolaytta":{"name":"Wolaytta","names":["Wolaitta","Wolaytta"],"iso639-2":"wal","iso639-1":null},"Wolof":{"name":"Wolof","names":["Wolof"],"iso639-2":"wol","iso639-1":"wo"},"Xhosa":{"name":"Xhosa","names":["Xhosa"],"iso639-2":"xho","iso639-1":"xh"},"Yakut":{"name":"Yakut","names":["Yakut"],"iso639-2":"sah","iso639-1":null},"Yao":{"name":"Yao","names":["Yao"],"iso639-2":"yao","iso639-1":null},"Yapese":{"name":"Yapese","names":["Yapese"],"iso639-2":"yap","iso639-1":null},"Yiddish":{"name":"Yiddish","names":["Yiddish"],"iso639-2":"yid","iso639-1":"yi"},"Yoruba":{"name":"Yoruba","names":["Yoruba"],"iso639-2":"yor","iso639-1":"yo"},"Yupik languages":{"name":"Yupik languages","names":["Yupik languages"],"iso639-2":"ypk","iso639-1":null},"Zande languages":{"name":"Zande languages","names":["Zande languages"],"iso639-2":"znd","iso639-1":null},"Zapotec":{"name":"Zapotec","names":["Zapotec"],"iso639-2":"zap","iso639-1":null},"Zaza":{"name":"Zaza","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Zazaki":{"name":"Zazaki","names":["Zaza","Dimili","Dimli","Kirdki","Kirmanjki","Zazaki"],"iso639-2":"zza","iso639-1":null},"Zenaga":{"name":"Zenaga","names":["Zenaga"],"iso639-2":"zen","iso639-1":null},"Zhuang":{"name":"Zhuang","names":["Zhuang","Chuang"],"iso639-2":"zha","iso639-1":"za"},"Zulu":{"name":"Zulu","names":["Zulu"],"iso639-2":"zul","iso639-1":"zu"},"Zuni":{"name":"Zuni","names":["Zuni"],"iso639-2":"zun","iso639-1":null}}')
            },
        "./node_modules/windows-locale/index.json":
            /*!************************************************!*\
              !*** ./node_modules/windows-locale/index.json ***!
              \************************************************/
            /*! exports provided: aa, aa-dj, aa-er, aa-et, af, af-na, af-za, agq, agq-cm, ak, ak-gh, sq, sq-al, sq-mk, gsw, gsw-fr, gsw-li, gsw-ch, am, am-et, ar, ar-dz, ar-bh, ar-td, ar-km, ar-dj, ar-eg, ar-er, ar-iq, ar-il, ar-jo, ar-kw, ar-lb, ar-ly, ar-mr, ar-ma, ar-om, ar-ps, ar-qa, ar-sa, ar-so, ar-ss, ar-sd, ar-sy, ar-tn, ar-ae, ar-001, ar-ye, hy, hy-am, as, as-in, ast, ast-es, asa, asa-tz, az-cyrl, az-cyrl-az, az, az-latn, az-latn-az, ksf, ksf-cm, bm, bm-latn-ml, bn, bn-bd, bn-in, bas, bas-cm, ba, ba-ru, eu, eu-es, be, be-by, bem, bem-zm, bez, bez-tz, byn, byn-er, brx, brx-in, bs-cyrl, bs-cyrl-ba, bs-latn, bs, bs-latn-ba, br, br-fr, bg, bg-bg, my, my-mm, ca, ca-ad, ca-fr, ca-it, ca-es, tzm-latn-, ku, ku-arab, ku-arab-iq, cd-ru, chr, chr-cher, chr-cher-us, cgg, cgg-ug, zh-hans, zh, zh-cn, zh-sg, zh-hant, zh-hk, zh-mo, zh-tw, cu-ru, swc, swc-cd, kw, kw-gb, co, co-fr, hr,, hr-hr, hr-ba, cs, cs-cz, da, da-dk, da-gl, prs, prs-af, dv, dv-mv, dua, dua-cm, nl, nl-aw, nl-be, nl-bq, nl-cw, nl-nl, nl-sx, nl-sr, dz, dz-bt, ebu, ebu-ke, en, en-as, en-ai, en-ag, en-au, en-at, en-bs, en-bb, en-be, en-bz, en-bm, en-bw, en-io, en-vg, en-bi, en-cm, en-ca, en-029, en-ky, en-cx, en-cc, en-ck, en-cy, en-dk, en-dm, en-er, en-150, en-fk, en-fi, en-fj, en-gm, en-de, en-gh, en-gi, en-gd, en-gu, en-gg, en-gy, en-hk, en-in, en-ie, en-im, en-il, en-jm, en-je, en-ke, en-ki, en-ls, en-lr, en-mo, en-mg, en-mw, en-my, en-mt, en-mh, en-mu, en-fm, en-ms, en-na, en-nr, en-nl, en-nz, en-ng, en-nu, en-nf, en-mp, en-pk, en-pw, en-pg, en-pn, en-pr, en-ph, en-rw, en-kn, en-lc, en-vc, en-ws, en-sc, en-sl, en-sg, en-sx, en-si, en-sb, en-za, en-ss, en-sh, en-sd, en-sz, en-se, en-ch, en-tz, en-tk, en-to, en-tt, en-tc, en-tv, en-ug, en-gb, en-us, en-um, en-vi, en-vu, en-001, en-zm, en-zw, eo, eo-001, et, et-ee, ee, ee-gh, ee-tg, ewo, ewo-cm, fo, fo-dk, fo-fo, fil, fil-ph, fi, fi-fi, fr, fr-dz, fr-be, fr-bj, fr-bf, fr-bi, fr-cm, fr-ca, fr-cf, fr-td, fr-km, fr-cg, fr-cd, fr-ci, fr-dj, fr-gq, fr-fr, fr-gf, fr-pf, fr-ga, fr-gp, fr-gn, fr-ht, fr-lu, fr-mg, fr-ml, fr-mq, fr-mr, fr-mu, fr-yt, fr-ma, fr-nc, fr-ne, fr-mc, fr-re, fr-rw, fr-bl, fr-mf, fr-pm, fr-sn, fr-sc, fr-ch, fr-sy, fr-tg, fr-tn, fr-vu, fr-wf, fy, fy-nl, fur, fur-it, ff, ff-latn, ff-latn-bf, ff-cm, ff-latn-cm, ff-latn-gm, ff-latn-gh, ff-gn, ff-latn-gn, ff-latn-gw, ff-latn-lr, ff-mr, ff-latn-mr, ff-latn-ne, ff-ng, ff-latn-ng, ff-latn-sn, ff-latn-sl, gl, gl-es, lg, lg-ug, ka, ka-ge, de, de-at, de-be, de-de, de-it, de-li, de-lu, de-ch, el, el-cy, el-gr, kl, kl-gl, gn, gn-py, gu, gu-in, guz, guz-ke, ha, ha-latn, ha-latn-gh, ha-latn-ne, ha-latn-ng, haw, haw-us, he, he-il, hi, hi-in, hu, hu-hu, is, is-is, ig, ig-ng, id, id-id, ia, ia-fr, ia-001, iu, iu-latn, iu-latn-ca, iu-cans, iu-cans-ca, ga, ga-ie, it, it-it, it-sm, it-ch, it-va, ja, ja-jp, jv, jv-latn, jv-latn-id, dyo, dyo-sn, kea, kea-cv, kab, kab-dz, kkj, kkj-cm, kln, kln-ke, kam, kam-ke, kn, kn-in, ks, ks-arab, ks-arab-in, kk, kk-kz, km, km-kh, quc, quc-latn-gt, ki, ki-ke, rw, rw-rw, sw, sw-ke, sw-tz, sw-ug, kok, kok-in, ko, ko-kr, ko-kp, khq, khq-ml, ses, ses-ml, nmg, nmg-cm, ky, ky-kg, ku-arab-ir, lkt, lkt-us, lag, lag-tz, lo, lo-la, lv, lv-lv, ln, ln-ao, ln-cf, ln-cg, ln-cd, lt, lt-lt, nds, nds-de, nds-nl, dsb, dsb-de, lu, lu-cd, luo, luo-ke, lb, lb-lu, luy, luy-ke, mk, mk-mk, jmc, jmc-tz, mgh, mgh-mz, kde, kde-tz, mg, mg-mg, ms, ms-bn, ms-my, ml, ml-in, mt, mt-mt, gv, gv-im, mi, mi-nz, arn, arn-cl, mr, mr-in, mas, mas-ke, mas-tz, mzn-ir, mer, mer-ke, mgo, mgo-cm, moh, moh-ca, mn, mn-cyrl, mn-mn, mn-mong, mn-mong-, mfe, mfe-mu, mua, mua-cm, nqo, nqo-gn, naq, naq-na, ne, ne-in, ne-np, nnh, nnh-cm, jgo, jgo-cm, lrc-iq, lrc-ir, nd, nd-zw, no, nb, nb-no, nn, nn-no, nb-sj, nus, nus-sd, nus-ss, nyn, nyn-ug, oc, oc-fr, or, or-in, om, om-et, om-ke, os, os-ge, os-ru, ps, ps-af, fa, fa-af, fa-ir, pl, pl-pl, pt, pt-ao, pt-br, pt-cv, pt-gq, pt-gw, pt-lu, pt-mo, pt-mz, pt-pt, pt-st, pt-ch, pt-tl, prg-001, qps-ploca, qps-ploc, qps-plocm, pa, pa-arab, pa-in, pa-arab-pk, quz, quz-bo, quz-ec, quz-pe, ksh, ksh-de, ro, ro-md, ro-ro, rm, rm-ch, rof, rof-tz, rn, rn-bi, ru, ru-by, ru-kz, ru-kg, ru-md, ru-ru, ru-ua, rwk, rwk-tz, ssy, ssy-er, sah, sah-ru, saq, saq-ke, smn, smn-fi, smj, smj-no, smj-se, se, se-fi, se-no, se-se, sms, sms-fi, sma, sma-no, sma-se, sg, sg-cf, sbp, sbp-tz, sa, sa-in, gd, gd-gb, seh, seh-mz, sr-cyrl, sr-cyrl-ba, sr-cyrl-me, sr-cyrl-rs, sr-cyrl-cs, sr-latn, sr, sr-latn-ba, sr-latn-me, sr-latn-rs, sr-latn-cs, nso, nso-za, tn, tn-bw, tn-za, ksb, ksb-tz, sn, sn-latn, sn-latn-zw, sd, sd-arab, sd-arab-pk, si, si-lk, sk, sk-sk, sl, sl-si, xog, xog-ug, so, so-dj, so-et, so-ke, so-so, st, st-za, nr, nr-za, st-ls, es, es-ar, es-bz, es-ve, es-bo, es-br, es-cl, es-co, es-cr, es-cu, es-do, es-ec, es-sv, es-gq, es-gt, es-hn, es-419, es-mx, es-ni, es-pa, es-py, es-pe, es-ph, es-pr, es-es_tradnl, es-es, es-us, es-uy, zgh, zgh-tfng-ma, zgh-tfng, ss, ss-za, ss-sz, sv, sv-ax, sv-fi, sv-se, syr, syr-sy, shi, shi-tfng, shi-tfng-ma, shi-latn, shi-latn-ma, dav, dav-ke, tg, tg-cyrl, tg-cyrl-tj, tzm, tzm-latn, tzm-latn-dz, ta, ta-in, ta-my, ta-sg, ta-lk, twq, twq-ne, tt, tt-ru, te, te-in, teo, teo-ke, teo-ug, th, th-th, bo, bo-in, bo-cn, tig, tig-er, ti, ti-er, ti-et, to, to-to, ts, ts-za, tr, tr-cy, tr-tr, tk, tk-tm, uk, uk-ua, hsb, hsb-de, ur, ur-in, ur-pk, ug, ug-cn, uz-arab, uz-arab-af, uz-cyrl, uz-cyrl-uz, uz, uz-latn, uz-latn-uz, vai, vai-vaii, vai-vaii-lr, vai-latn-lr, vai-latn, ca-es-, ve, ve-za, vi, vi-vn, vo, vo-001, vun, vun-tz, wae, wae-ch, cy, cy-gb, wal, wal-et, wo, wo-sn, xh, xh-za, yav, yav-cm, ii, ii-cn, yo, yo-bj, yo-ng, dje, dje-ne, zu, zu-za, default */
            function(e) {
                e.exports = JSON.parse('{"aa":{"language":"Afar","location":null,"id":4096,"tag":"aa","version":"Release 10"},"aa-dj":{"language":"Afar","location":"Djibouti","id":4096,"tag":"aa-DJ","version":"Release 10"},"aa-er":{"language":"Afar","location":"Eritrea","id":4096,"tag":"aa-ER","version":"Release 10"},"aa-et":{"language":"Afar","location":"Ethiopia","id":4096,"tag":"aa-ET","version":"Release 10"},"af":{"language":"Afrikaans","location":null,"id":54,"tag":"af","version":"Release 7"},"af-na":{"language":"Afrikaans","location":"Namibia","id":4096,"tag":"af-NA","version":"Release 10"},"af-za":{"language":"Afrikaans","location":"South Africa","id":1078,"tag":"af-ZA","version":"Release B"},"agq":{"language":"Aghem","location":null,"id":4096,"tag":"agq","version":"Release 10"},"agq-cm":{"language":"Aghem","location":"Cameroon","id":4096,"tag":"agq-CM","version":"Release 10"},"ak":{"language":"Akan","location":null,"id":4096,"tag":"ak","version":"Release 10"},"ak-gh":{"language":"Akan","location":"Ghana","id":4096,"tag":"ak-GH","version":"Release 10"},"sq":{"language":"Albanian","location":null,"id":28,"tag":"sq","version":"Release 7"},"sq-al":{"language":"Albanian","location":"Albania","id":1052,"tag":"sq-AL","version":"Release B"},"sq-mk":{"language":"Albanian","location":"North Macedonia","id":4096,"tag":"sq-MK","version":"Release 10"},"gsw":{"language":"Alsatian","location":null,"id":132,"tag":"gsw","version":"Release 7"},"gsw-fr":{"language":"Alsatian","location":"France","id":1156,"tag":"gsw-FR","version":"Release V"},"gsw-li":{"language":"Alsatian","location":"Liechtenstein","id":4096,"tag":"gsw-LI","version":"Release 10"},"gsw-ch":{"language":"Alsatian","location":"Switzerland","id":4096,"tag":"gsw-CH","version":"Release 10"},"am":{"language":"Amharic","location":null,"id":94,"tag":"am","version":"Release 7"},"am-et":{"language":"Amharic","location":"Ethiopia","id":1118,"tag":"am-ET","version":"Release V"},"ar":{"language":"Arabic","location":null,"id":1,"tag":"ar","version":"Release 7"},"ar-dz":{"language":"Arabic","location":"Algeria","id":5121,"tag":"ar-DZ","version":"Release B"},"ar-bh":{"language":"Arabic","location":"Bahrain","id":15361,"tag":"ar-BH","version":"Release B"},"ar-td":{"language":"Arabic","location":"Chad","id":4096,"tag":"ar-TD","version":"Release 10"},"ar-km":{"language":"Arabic","location":"Comoros","id":4096,"tag":"ar-KM","version":"Release 10"},"ar-dj":{"language":"Arabic","location":"Djibouti","id":4096,"tag":"ar-DJ","version":"Release 10"},"ar-eg":{"language":"Arabic","location":"Egypt","id":3073,"tag":"ar-EG","version":"Release B"},"ar-er":{"language":"Arabic","location":"Eritrea","id":4096,"tag":"ar-ER","version":"Release 10"},"ar-iq":{"language":"Arabic","location":"Iraq","id":2049,"tag":"ar-IQ","version":"Release B"},"ar-il":{"language":"Arabic","location":"Israel","id":4096,"tag":"ar-IL","version":"Release 10"},"ar-jo":{"language":"Arabic","location":"Jordan","id":11265,"tag":"ar-JO","version":"Release B"},"ar-kw":{"language":"Arabic","location":"Kuwait","id":13313,"tag":"ar-KW","version":"Release B"},"ar-lb":{"language":"Arabic","location":"Lebanon","id":12289,"tag":"ar-LB","version":"Release B"},"ar-ly":{"language":"Arabic","location":"Libya","id":4097,"tag":"ar-LY","version":"Release B"},"ar-mr":{"language":"Arabic","location":"Mauritania","id":4096,"tag":"ar-MR","version":"Release 10"},"ar-ma":{"language":"Arabic","location":"Morocco","id":6145,"tag":"ar-MA","version":"Release B"},"ar-om":{"language":"Arabic","location":"Oman","id":8193,"tag":"ar-OM","version":"Release B"},"ar-ps":{"language":"Arabic","location":"Palestinian Authority","id":4096,"tag":"ar-PS","version":"Release 10"},"ar-qa":{"language":"Arabic","location":"Qatar","id":16385,"tag":"ar-QA","version":"Release B"},"ar-sa":{"language":"Arabic","location":"Saudi Arabia","id":1025,"tag":"ar-SA","version":"Release B"},"ar-so":{"language":"Arabic","location":"Somalia","id":4096,"tag":"ar-SO","version":"Release 10"},"ar-ss":{"language":"Arabic","location":"South Sudan","id":4096,"tag":"ar-SS","version":"Release 10"},"ar-sd":{"language":"Arabic","location":"Sudan","id":4096,"tag":"ar-SD","version":"Release 10"},"ar-sy":{"language":"Arabic","location":"Syria","id":10241,"tag":"ar-SY","version":"Release B"},"ar-tn":{"language":"Arabic","location":"Tunisia","id":7169,"tag":"ar-TN","version":"Release B"},"ar-ae":{"language":"Arabic","location":"U.A.E.","id":14337,"tag":"ar-AE","version":"Release B"},"ar-001":{"language":"Arabic","location":"World","id":4096,"tag":"ar-001","version":"Release 10"},"ar-ye":{"language":"Arabic","location":"Yemen","id":9217,"tag":"ar-YE","version":"Release B"},"hy":{"language":"Armenian","location":null,"id":43,"tag":"hy","version":"Release 7"},"hy-am":{"language":"Armenian","location":"Armenia","id":1067,"tag":"hy-AM","version":"Release C"},"as":{"language":"Assamese","location":null,"id":77,"tag":"as","version":"Release 7"},"as-in":{"language":"Assamese","location":"India","id":1101,"tag":"as-IN","version":"Release V"},"ast":{"language":"Asturian","location":null,"id":4096,"tag":"ast","version":"Release 10"},"ast-es":{"language":"Asturian","location":"Spain","id":4096,"tag":"ast-ES","version":"Release 10"},"asa":{"language":"Asu","location":null,"id":4096,"tag":"asa","version":"Release 10"},"asa-tz":{"language":"Asu","location":"Tanzania","id":4096,"tag":"asa-TZ","version":"Release 10"},"az-cyrl":{"language":"Azerbaijani (Cyrillic)","location":null,"id":29740,"tag":"az-Cyrl","version":"Windows 7"},"az-cyrl-az":{"language":"Azerbaijani (Cyrillic)","location":"Azerbaijan","id":2092,"tag":"az-Cyrl-AZ","version":"Release C"},"az":{"language":"Azerbaijani (Latin)","location":null,"id":44,"tag":"az","version":"Release 7"},"az-latn":{"language":"Azerbaijani (Latin)","location":null,"id":30764,"tag":"az-Latn","version":"Windows 7"},"az-latn-az":{"language":"Azerbaijani (Latin)","location":"Azerbaijan","id":1068,"tag":"az-Latn-AZ","version":"Release C"},"ksf":{"language":"Bafia","location":null,"id":4096,"tag":"ksf","version":"Release 10"},"ksf-cm":{"language":"Bafia","location":"Cameroon","id":4096,"tag":"ksf-CM","version":"Release 10"},"bm":{"language":"Bamanankan","location":null,"id":4096,"tag":"bm","version":"Release 10"},"bm-latn-ml":{"language":"Bamanankan (Latin)","location":"Mali","id":4096,"tag":"bm-Latn-ML","version":"Release 10"},"bn":{"language":"Bangla","location":null,"id":69,"tag":"bn","version":"Release 7"},"bn-bd":{"language":"Bangla","location":"Bangladesh","id":2117,"tag":"bn-BD","version":"Release V"},"bn-in":{"language":"Bangla","location":"India","id":1093,"tag":"bn-IN","version":"Release E1"},"bas":{"language":"Basaa","location":null,"id":4096,"tag":"bas","version":"Release 10"},"bas-cm":{"language":"Basaa","location":"Cameroon","id":4096,"tag":"bas-CM","version":"Release 10"},"ba":{"language":"Bashkir","location":null,"id":109,"tag":"ba","version":"Release 7"},"ba-ru":{"language":"Bashkir","location":"Russia","id":1133,"tag":"ba-RU","version":"Release V"},"eu":{"language":"Basque","location":null,"id":45,"tag":"eu","version":"Release 7"},"eu-es":{"language":"Basque","location":"Spain","id":1069,"tag":"eu-ES","version":"Release B"},"be":{"language":"Belarusian","location":null,"id":35,"tag":"be","version":"Release 7"},"be-by":{"language":"Belarusian","location":"Belarus","id":1059,"tag":"be-BY","version":"Release B"},"bem":{"language":"Bemba","location":null,"id":4096,"tag":"bem","version":"Release 10"},"bem-zm":{"language":"Bemba","location":"Zambia","id":4096,"tag":"bem-ZM","version":"Release 10"},"bez":{"language":"Bena","location":null,"id":4096,"tag":"bez","version":"Release 10"},"bez-tz":{"language":"Bena","location":"Tanzania","id":4096,"tag":"bez-TZ","version":"Release 10"},"byn":{"language":"Blin","location":null,"id":4096,"tag":"byn","version":"Release 10"},"byn-er":{"language":"Blin","location":"Eritrea","id":4096,"tag":"byn-ER","version":"Release 10"},"brx":{"language":"Bodo","location":null,"id":4096,"tag":"brx","version":"Release 10"},"brx-in":{"language":"Bodo","location":"India","id":4096,"tag":"brx-IN","version":"Release 10"},"bs-cyrl":{"language":"Bosnian (Cyrillic)","location":null,"id":25626,"tag":"bs-Cyrl","version":"Windows 7"},"bs-cyrl-ba":{"language":"Bosnian (Cyrillic)","location":"Bosnia and Herzegovina","id":8218,"tag":"bs-Cyrl-BA","version":"Release E1"},"bs-latn":{"language":"Bosnian (Latin)","location":null,"id":26650,"tag":"bs-Latn","version":"Windows 7"},"bs":{"language":"Bosnian (Latin)","location":null,"id":30746,"tag":"bs","version":"Release 7"},"bs-latn-ba":{"language":"Bosnian (Latin)","location":"Bosnia and Herzegovina","id":5146,"tag":"bs-Latn-BA","version":"Release E1"},"br":{"language":"Breton","location":null,"id":126,"tag":"br","version":"Release 7"},"br-fr":{"language":"Breton","location":"France","id":1150,"tag":"br-FR","version":"Release V"},"bg":{"language":"Bulgarian","location":null,"id":2,"tag":"bg","version":"Release 7"},"bg-bg":{"language":"Bulgarian","location":"Bulgaria","id":1026,"tag":"bg-BG","version":"Release B"},"my":{"language":"Burmese","location":null,"id":85,"tag":"my","version":"Release 8.1"},"my-mm":{"language":"Burmese","location":"Myanmar","id":1109,"tag":"my-MM","version":"Release 8.1"},"ca":{"language":"Catalan","location":null,"id":3,"tag":"ca","version":"Release 7"},"ca-ad":{"language":"Catalan","location":"Andorra","id":4096,"tag":"ca-AD","version":"Release 10"},"ca-fr":{"language":"Catalan","location":"France","id":4096,"tag":"ca-FR","version":"Release 10"},"ca-it":{"language":"Catalan","location":"Italy","id":4096,"tag":"ca-IT","version":"Release 10"},"ca-es":{"language":"Catalan","location":"Spain","id":1027,"tag":"ca-ES","version":"Release B"},"tzm-latn-":{"language":"Central Atlas Tamazight ","location":"Morocco","id":4096,"tag":"tzm-Latn-","version":"Release 10"},"ku":{"language":"Central Kurdish","location":null,"id":146,"tag":"ku","version":"Release 8"},"ku-arab":{"language":"Central Kurdish","location":null,"id":31890,"tag":"ku-Arab","version":"Release 8"},"ku-arab-iq":{"language":"Central Kurdish","location":"Iraq","id":1170,"tag":"ku-Arab-IQ","version":"Release 8"},"cd-ru":{"language":"Chechen","location":"Russia","id":4096,"tag":"cd-RU","version":"Release 10.1"},"chr":{"language":"Cherokee","location":null,"id":92,"tag":"chr","version":"Release 8"},"chr-cher":{"language":"Cherokee","location":null,"id":31836,"tag":"chr-Cher","version":"Release 8"},"chr-cher-us":{"language":"Cherokee","location":"United States","id":1116,"tag":"chr-Cher-US","version":"Release 8"},"cgg":{"language":"Chiga","location":null,"id":4096,"tag":"cgg","version":"Release 10"},"cgg-ug":{"language":"Chiga","location":"Uganda","id":4096,"tag":"cgg-UG","version":"Release 10"},"zh-hans":{"language":"Chinese (Simplified)","location":null,"id":4,"tag":"zh-Hans","version":"Release A"},"zh":{"language":"Chinese (Simplified)","location":null,"id":30724,"tag":"zh","version":"Windows 7"},"zh-cn":{"language":"Chinese (Simplified)","location":"People\'s Republic of China","id":2052,"tag":"zh-CN","version":"Release A"},"zh-sg":{"language":"Chinese (Simplified)","location":"Singapore","id":4100,"tag":"zh-SG","version":"Release A"},"zh-hant":{"language":"Chinese (Traditional)","location":null,"id":31748,"tag":"zh-Hant","version":"Release A"},"zh-hk":{"language":"Chinese (Traditional)","location":"Hong Kong S.A.R.","id":3076,"tag":"zh-HK","version":"Release A"},"zh-mo":{"language":"Chinese (Traditional)","location":"Macao S.A.R.","id":5124,"tag":"zh-MO","version":"Release D"},"zh-tw":{"language":"Chinese (Traditional)","location":"Taiwan","id":1028,"tag":"zh-TW","version":"Release A"},"cu-ru":{"language":"Church Slavic","location":"Russia","id":4096,"tag":"cu-RU","version":"Release 10.1"},"swc":{"language":"Congo Swahili","location":null,"id":4096,"tag":"swc","version":"Release 10"},"swc-cd":{"language":"Congo Swahili","location":"Congo DRC","id":4096,"tag":"swc-CD","version":"Release 10"},"kw":{"language":"Cornish","location":null,"id":4096,"tag":"kw","version":"Release 10"},"kw-gb":{"language":"Cornish","location":"United Kingdom","id":4096,"tag":"kw-GB","version":"Release 10"},"co":{"language":"Corsican","location":null,"id":131,"tag":"co","version":"Release 7"},"co-fr":{"language":"Corsican","location":"France","id":1155,"tag":"co-FR","version":"Release V"},"hr,":{"language":"Croatian","location":null,"id":26,"tag":"hr,","version":"Release 7"},"hr-hr":{"language":"Croatian","location":"Croatia","id":1050,"tag":"hr-HR","version":"Release A"},"hr-ba":{"language":"Croatian (Latin)","location":"Bosnia and Herzegovina","id":4122,"tag":"hr-BA","version":"Release E1"},"cs":{"language":"Czech","location":null,"id":5,"tag":"cs","version":"Release 7"},"cs-cz":{"language":"Czech","location":"Czech Republic","id":1029,"tag":"cs-CZ","version":"Release A"},"da":{"language":"Danish","location":null,"id":6,"tag":"da","version":"Release 7"},"da-dk":{"language":"Danish","location":"Denmark","id":1030,"tag":"da-DK","version":"Release A"},"da-gl":{"language":"Danish","location":"Greenland","id":4096,"tag":"da-GL","version":"Release 10"},"prs":{"language":"Dari","location":null,"id":140,"tag":"prs","version":"Release 7"},"prs-af":{"language":"Dari","location":"Afghanistan","id":1164,"tag":"prs-AF","version":"Release V"},"dv":{"language":"Divehi","location":null,"id":101,"tag":"dv","version":"Release 7"},"dv-mv":{"language":"Divehi","location":"Maldives","id":1125,"tag":"dv-MV","version":"ReleaseD"},"dua":{"language":"Duala","location":null,"id":4096,"tag":"dua","version":"Release 10"},"dua-cm":{"language":"Duala","location":"Cameroon","id":4096,"tag":"dua-CM","version":"Release 10"},"nl":{"language":"Dutch","location":null,"id":19,"tag":"nl","version":"Release 7"},"nl-aw":{"language":"Dutch","location":"Aruba","id":4096,"tag":"nl-AW","version":"Release 10"},"nl-be":{"language":"Dutch","location":"Belgium","id":2067,"tag":"nl-BE","version":"Release A"},"nl-bq":{"language":"Dutch","location":"Bonaire, Sint Eustatius and Saba","id":4096,"tag":"nl-BQ","version":"Release 10"},"nl-cw":{"language":"Dutch","location":"Curaçao","id":4096,"tag":"nl-CW","version":"Release 10"},"nl-nl":{"language":"Dutch","location":"Netherlands","id":1043,"tag":"nl-NL","version":"Release A"},"nl-sx":{"language":"Dutch","location":"Sint Maarten","id":4096,"tag":"nl-SX","version":"Release 10"},"nl-sr":{"language":"Dutch","location":"Suriname","id":4096,"tag":"nl-SR","version":"Release 10"},"dz":{"language":"Dzongkha","location":null,"id":4096,"tag":"dz","version":"Release 10"},"dz-bt":{"language":"Dzongkha","location":"Bhutan","id":3153,"tag":"dz-BT","version":"Release 10"},"ebu":{"language":"Embu","location":null,"id":4096,"tag":"ebu","version":"Release 10"},"ebu-ke":{"language":"Embu","location":"Kenya","id":4096,"tag":"ebu-KE","version":"Release 10"},"en":{"language":"English","location":null,"id":9,"tag":"en","version":"Release 7"},"en-as":{"language":"English","location":"American Samoa","id":4096,"tag":"en-AS","version":"Release 10"},"en-ai":{"language":"English","location":"Anguilla","id":4096,"tag":"en-AI","version":"Release 10"},"en-ag":{"language":"English","location":"Antigua and Barbuda","id":4096,"tag":"en-AG","version":"Release 10"},"en-au":{"language":"English","location":"Australia","id":3081,"tag":"en-AU","version":"Release A"},"en-at":{"language":"English","location":"Austria","id":4096,"tag":"en-AT","version":"Release 10.1"},"en-bs":{"language":"English","location":"Bahamas","id":4096,"tag":"en-BS","version":"Release 10"},"en-bb":{"language":"English","location":"Barbados","id":4096,"tag":"en-BB","version":"Release 10"},"en-be":{"language":"English","location":"Belgium","id":4096,"tag":"en-BE","version":"Release 10"},"en-bz":{"language":"English","location":"Belize","id":10249,"tag":"en-BZ","version":"Release B"},"en-bm":{"language":"English","location":"Bermuda","id":4096,"tag":"en-BM","version":"Release 10"},"en-bw":{"language":"English","location":"Botswana","id":4096,"tag":"en-BW","version":"Release 10"},"en-io":{"language":"English","location":"British Indian Ocean Territory","id":4096,"tag":"en-IO","version":"Release 10"},"en-vg":{"language":"English","location":"British Virgin Islands","id":4096,"tag":"en-VG","version":"Release 10"},"en-bi":{"language":"English","location":"Burundi","id":4096,"tag":"en-BI","version":"Release 10.1"},"en-cm":{"language":"English","location":"Cameroon","id":4096,"tag":"en-CM","version":"Release 10"},"en-ca":{"language":"English","location":"Canada","id":4105,"tag":"en-CA","version":"Release A"},"en-029":{"language":"English","location":"Caribbean","id":9225,"tag":"en-029","version":"Release B"},"en-ky":{"language":"English","location":"Cayman Islands","id":4096,"tag":"en-KY","version":"Release 10"},"en-cx":{"language":"English","location":"Christmas Island","id":4096,"tag":"en-CX","version":"Release 10"},"en-cc":{"language":"English","location":"Cocos [Keeling] Islands","id":4096,"tag":"en-CC","version":"Release 10"},"en-ck":{"language":"English","location":"Cook Islands","id":4096,"tag":"en-CK","version":"Release 10"},"en-cy":{"language":"English","location":"Cyprus","id":4096,"tag":"en-CY","version":"Release 10.1"},"en-dk":{"language":"English","location":"Denmark","id":4096,"tag":"en-DK","version":"Release 10.1"},"en-dm":{"language":"English","location":"Dominica","id":4096,"tag":"en-DM","version":"Release 10"},"en-er":{"language":"English","location":"Eritrea","id":4096,"tag":"en-ER","version":"Release 10"},"en-150":{"language":"English","location":"Europe","id":4096,"tag":"en-150","version":"Release 10"},"en-fk":{"language":"English","location":"Falkland Islands","id":4096,"tag":"en-FK","version":"Release 10"},"en-fi":{"language":"English","location":"Finland","id":4096,"tag":"en-FI","version":"Release 10.1"},"en-fj":{"language":"English","location":"Fiji","id":4096,"tag":"en-FJ","version":"Release 10"},"en-gm":{"language":"English","location":"Gambia","id":4096,"tag":"en-GM","version":"Release 10"},"en-de":{"language":"English","location":"Germany","id":4096,"tag":"en-DE","version":"Release 10.1"},"en-gh":{"language":"English","location":"Ghana","id":4096,"tag":"en-GH","version":"Release 10"},"en-gi":{"language":"English","location":"Gibraltar","id":4096,"tag":"en-GI","version":"Release 10"},"en-gd":{"language":"English","location":"Grenada","id":4096,"tag":"en-GD","version":"Release 10"},"en-gu":{"language":"English","location":"Guam","id":4096,"tag":"en-GU","version":"Release 10"},"en-gg":{"language":"English","location":"Guernsey","id":4096,"tag":"en-GG","version":"Release 10"},"en-gy":{"language":"English","location":"Guyana","id":4096,"tag":"en-GY","version":"Release 10"},"en-hk":{"language":"English","location":"Hong Kong","id":15369,"tag":"en-HK","version":"Release 8.1"},"en-in":{"language":"English","location":"India","id":16393,"tag":"en-IN","version":"Release V"},"en-ie":{"language":"English","location":"Ireland","id":6153,"tag":"en-IE","version":"Release A"},"en-im":{"language":"English","location":"Isle of Man","id":4096,"tag":"en-IM","version":"Release 10"},"en-il":{"language":"English","location":"Israel","id":4096,"tag":"en-IL","version":"Release 10.1"},"en-jm":{"language":"English","location":"Jamaica","id":8201,"tag":"en-JM","version":"Release B"},"en-je":{"language":"English","location":"Jersey","id":4096,"tag":"en-JE","version":"Release 10"},"en-ke":{"language":"English","location":"Kenya","id":4096,"tag":"en-KE","version":"Release 10"},"en-ki":{"language":"English","location":"Kiribati","id":4096,"tag":"en-KI","version":"Release 10"},"en-ls":{"language":"English","location":"Lesotho","id":4096,"tag":"en-LS","version":"Release 10"},"en-lr":{"language":"English","location":"Liberia","id":4096,"tag":"en-LR","version":"Release 10"},"en-mo":{"language":"English","location":"Macao SAR","id":4096,"tag":"en-MO","version":"Release 10"},"en-mg":{"language":"English","location":"Madagascar","id":4096,"tag":"en-MG","version":"Release 10"},"en-mw":{"language":"English","location":"Malawi","id":4096,"tag":"en-MW","version":"Release 10"},"en-my":{"language":"English","location":"Malaysia","id":17417,"tag":"en-MY","version":"Release V"},"en-mt":{"language":"English","location":"Malta","id":4096,"tag":"en-MT","version":"Release 10"},"en-mh":{"language":"English","location":"Marshall Islands","id":4096,"tag":"en-MH","version":"Release 10"},"en-mu":{"language":"English","location":"Mauritius","id":4096,"tag":"en-MU","version":"Release 10"},"en-fm":{"language":"English","location":"Micronesia","id":4096,"tag":"en-FM","version":"Release 10"},"en-ms":{"language":"English","location":"Montserrat","id":4096,"tag":"en-MS","version":"Release 10"},"en-na":{"language":"English","location":"Namibia","id":4096,"tag":"en-NA","version":"Release 10"},"en-nr":{"language":"English","location":"Nauru","id":4096,"tag":"en-NR","version":"Release 10"},"en-nl":{"language":"English","location":"Netherlands","id":4096,"tag":"en-NL","version":"Release 10.1"},"en-nz":{"language":"English","location":"New Zealand","id":5129,"tag":"en-NZ","version":"Release A"},"en-ng":{"language":"English","location":"Nigeria","id":4096,"tag":"en-NG","version":"Release 10"},"en-nu":{"language":"English","location":"Niue","id":4096,"tag":"en-NU","version":"Release 10"},"en-nf":{"language":"English","location":"Norfolk Island","id":4096,"tag":"en-NF","version":"Release 10"},"en-mp":{"language":"English","location":"Northern Mariana Islands","id":4096,"tag":"en-MP","version":"Release 10"},"en-pk":{"language":"English","location":"Pakistan","id":4096,"tag":"en-PK","version":"Release 10"},"en-pw":{"language":"English","location":"Palau","id":4096,"tag":"en-PW","version":"Release 10"},"en-pg":{"language":"English","location":"Papua New Guinea","id":4096,"tag":"en-PG","version":"Release 10"},"en-pn":{"language":"English","location":"Pitcairn Islands","id":4096,"tag":"en-PN","version":"Release 10"},"en-pr":{"language":"English","location":"Puerto Rico","id":4096,"tag":"en-PR","version":"Release 10"},"en-ph":{"language":"English","location":"Republic of the Philippines","id":13321,"tag":"en-PH","version":"Release C"},"en-rw":{"language":"English","location":"Rwanda","id":4096,"tag":"en-RW","version":"Release 10"},"en-kn":{"language":"English","location":"Saint Kitts and Nevis","id":4096,"tag":"en-KN","version":"Release 10"},"en-lc":{"language":"English","location":"Saint Lucia","id":4096,"tag":"en-LC","version":"Release 10"},"en-vc":{"language":"English","location":"Saint Vincent and the Grenadines","id":4096,"tag":"en-VC","version":"Release 10"},"en-ws":{"language":"English","location":"Samoa","id":4096,"tag":"en-WS","version":"Release 10"},"en-sc":{"language":"English","location":"Seychelles","id":4096,"tag":"en-SC","version":"Release 10"},"en-sl":{"language":"English","location":"Sierra Leone","id":4096,"tag":"en-SL","version":"Release 10"},"en-sg":{"language":"English","location":"Singapore","id":18441,"tag":"en-SG","version":"Release V"},"en-sx":{"language":"English","location":"Sint Maarten","id":4096,"tag":"en-SX","version":"Release 10"},"en-si":{"language":"English","location":"Slovenia","id":4096,"tag":"en-SI","version":"Release 10.1"},"en-sb":{"language":"English","location":"Solomon Islands","id":4096,"tag":"en-SB","version":"Release 10"},"en-za":{"language":"English","location":"South Africa","id":7177,"tag":"en-ZA","version":"Release B"},"en-ss":{"language":"English","location":"South Sudan","id":4096,"tag":"en-SS","version":"Release 10"},"en-sh":{"language":"English","location":"St Helena, Ascension, Tristan da ","id":4096,"tag":"en-SH","version":"Release 10"},"en-sd":{"language":"English","location":"Sudan","id":4096,"tag":"en-SD","version":"Release 10"},"en-sz":{"language":"English","location":"Swaziland","id":4096,"tag":"en-SZ","version":"Release 10"},"en-se":{"language":"English","location":"Sweden","id":4096,"tag":"en-SE","version":"Release 10.1"},"en-ch":{"language":"English","location":"Switzerland","id":4096,"tag":"en-CH","version":"Release 10.1"},"en-tz":{"language":"English","location":"Tanzania","id":4096,"tag":"en-TZ","version":"Release 10"},"en-tk":{"language":"English","location":"Tokelau","id":4096,"tag":"en-TK","version":"Release 10"},"en-to":{"language":"English","location":"Tonga","id":4096,"tag":"en-TO","version":"Release 10"},"en-tt":{"language":"English","location":"Trinidad and Tobago","id":11273,"tag":"en-TT","version":"Release B"},"en-tc":{"language":"English","location":"Turks and Caicos Islands","id":4096,"tag":"en-TC","version":"Release 10"},"en-tv":{"language":"English","location":"Tuvalu","id":4096,"tag":"en-TV","version":"Release 10"},"en-ug":{"language":"English","location":"Uganda","id":4096,"tag":"en-UG","version":"Release 10"},"en-gb":{"language":"English","location":"United Kingdom","id":2057,"tag":"en-GB","version":"Release A"},"en-us":{"language":"English","location":"United States","id":1033,"tag":"en-US","version":"Release A"},"en-um":{"language":"English","location":"US Minor Outlying Islands","id":4096,"tag":"en-UM","version":"Release 10"},"en-vi":{"language":"English","location":"US Virgin Islands","id":4096,"tag":"en-VI","version":"Release 10"},"en-vu":{"language":"English","location":"Vanuatu","id":4096,"tag":"en-VU","version":"Release 10"},"en-001":{"language":"English","location":"World","id":4096,"tag":"en-001","version":"Release 10"},"en-zm":{"language":"English","location":"Zambia","id":4096,"tag":"en-ZM","version":"Release 10"},"en-zw":{"language":"English","location":"Zimbabwe","id":12297,"tag":"en-ZW","version":"Release C"},"eo":{"language":"Esperanto","location":null,"id":4096,"tag":"eo","version":"Release 10"},"eo-001":{"language":"Esperanto","location":"World","id":4096,"tag":"eo-001","version":"Release 10"},"et":{"language":"Estonian","location":null,"id":37,"tag":"et","version":"Release 7"},"et-ee":{"language":"Estonian","location":"Estonia","id":1061,"tag":"et-EE","version":"Release B"},"ee":{"language":"Ewe","location":null,"id":4096,"tag":"ee","version":"Release 10"},"ee-gh":{"language":"Ewe","location":"Ghana","id":4096,"tag":"ee-GH","version":"Release 10"},"ee-tg":{"language":"Ewe","location":"Togo","id":4096,"tag":"ee-TG","version":"Release 10"},"ewo":{"language":"Ewondo","location":null,"id":4096,"tag":"ewo","version":"Release 10"},"ewo-cm":{"language":"Ewondo","location":"Cameroon","id":4096,"tag":"ewo-CM","version":"Release 10"},"fo":{"language":"Faroese","location":null,"id":56,"tag":"fo","version":"Release 7"},"fo-dk":{"language":"Faroese","location":"Denmark","id":4096,"tag":"fo-DK","version":"Release 10.1"},"fo-fo":{"language":"Faroese","location":"Faroe Islands","id":1080,"tag":"fo-FO","version":"Release B"},"fil":{"language":"Filipino","location":null,"id":100,"tag":"fil","version":"Release 7"},"fil-ph":{"language":"Filipino","location":"Philippines","id":1124,"tag":"fil-PH","version":"Release E2"},"fi":{"language":"Finnish","location":null,"id":11,"tag":"fi","version":"Release 7"},"fi-fi":{"language":"Finnish","location":"Finland","id":1035,"tag":"fi-FI","version":"Release A"},"fr":{"language":"French","location":null,"id":12,"tag":"fr","version":"Release 7"},"fr-dz":{"language":"French","location":"Algeria","id":4096,"tag":"fr-DZ","version":"Release 10"},"fr-be":{"language":"French","location":"Belgium","id":2060,"tag":"fr-BE","version":"Release A"},"fr-bj":{"language":"French","location":"Benin","id":4096,"tag":"fr-BJ","version":"Release 10"},"fr-bf":{"language":"French","location":"Burkina Faso","id":4096,"tag":"fr-BF","version":"Release 10"},"fr-bi":{"language":"French","location":"Burundi","id":4096,"tag":"fr-BI","version":"Release 10"},"fr-cm":{"language":"French","location":"Cameroon","id":11276,"tag":"fr-CM","version":"Release 8.1"},"fr-ca":{"language":"French","location":"Canada","id":3084,"tag":"fr-CA","version":"Release A"},"fr-cf":{"language":"French","location":"Central African Republic","id":4096,"tag":"fr-CF","version":"Release 10"},"fr-td":{"language":"French","location":"Chad","id":4096,"tag":"fr-TD","version":"Release 10"},"fr-km":{"language":"French","location":"Comoros","id":4096,"tag":"fr-KM","version":"Release 10"},"fr-cg":{"language":"French","location":"Congo","id":4096,"tag":"fr-CG","version":"Release 10"},"fr-cd":{"language":"French","location":"Congo, DRC","id":9228,"tag":"fr-CD","version":"Release 8.1"},"fr-ci":{"language":"French","location":"Côte d\'Ivoire","id":12300,"tag":"fr-CI","version":"Release 8.1"},"fr-dj":{"language":"French","location":"Djibouti","id":4096,"tag":"fr-DJ","version":"Release 10"},"fr-gq":{"language":"French","location":"Equatorial Guinea","id":4096,"tag":"fr-GQ","version":"Release 10"},"fr-fr":{"language":"French","location":"France","id":1036,"tag":"fr-FR","version":"Release A"},"fr-gf":{"language":"French","location":"French Guiana","id":4096,"tag":"fr-GF","version":"Release 10"},"fr-pf":{"language":"French","location":"French Polynesia","id":4096,"tag":"fr-PF","version":"Release 10"},"fr-ga":{"language":"French","location":"Gabon","id":4096,"tag":"fr-GA","version":"Release 10"},"fr-gp":{"language":"French","location":"Guadeloupe","id":4096,"tag":"fr-GP","version":"Release 10"},"fr-gn":{"language":"French","location":"Guinea","id":4096,"tag":"fr-GN","version":"Release 10"},"fr-ht":{"language":"French","location":"Haiti","id":15372,"tag":"fr-HT","version":"Release 8.1"},"fr-lu":{"language":"French","location":"Luxembourg","id":5132,"tag":"fr-LU","version":"Release A"},"fr-mg":{"language":"French","location":"Madagascar","id":4096,"tag":"fr-MG","version":"Release 10"},"fr-ml":{"language":"French","location":"Mali","id":13324,"tag":"fr-ML","version":"Release 8.1"},"fr-mq":{"language":"French","location":"Martinique","id":4096,"tag":"fr-MQ","version":"Release 10"},"fr-mr":{"language":"French","location":"Mauritania","id":4096,"tag":"fr-MR","version":"Release 10"},"fr-mu":{"language":"French","location":"Mauritius","id":4096,"tag":"fr-MU","version":"Release 10"},"fr-yt":{"language":"French","location":"Mayotte","id":4096,"tag":"fr-YT","version":"Release 10"},"fr-ma":{"language":"French","location":"Morocco","id":14348,"tag":"fr-MA","version":"Release 8.1"},"fr-nc":{"language":"French","location":"New Caledonia","id":4096,"tag":"fr-NC","version":"Release 10"},"fr-ne":{"language":"French","location":"Niger","id":4096,"tag":"fr-NE","version":"Release 10"},"fr-mc":{"language":"French","location":"Principality of Monaco","id":6156,"tag":"fr-MC","version":"Release A"},"fr-re":{"language":"French","location":"Reunion","id":8204,"tag":"fr-RE","version":"Release 8.1"},"fr-rw":{"language":"French","location":"Rwanda","id":4096,"tag":"fr-RW","version":"Release 10"},"fr-bl":{"language":"French","location":"Saint Barthélemy","id":4096,"tag":"fr-BL","version":"Release 10"},"fr-mf":{"language":"French","location":"Saint Martin","id":4096,"tag":"fr-MF","version":"Release 10"},"fr-pm":{"language":"French","location":"Saint Pierre and Miquelon","id":4096,"tag":"fr-PM","version":"Release 10"},"fr-sn":{"language":"French","location":"Senegal","id":10252,"tag":"fr-SN","version":"Release 8.1"},"fr-sc":{"language":"French","location":"Seychelles","id":4096,"tag":"fr-SC","version":"Release 10"},"fr-ch":{"language":"French","location":"Switzerland","id":4108,"tag":"fr-CH","version":"Release A"},"fr-sy":{"language":"French","location":"Syria","id":4096,"tag":"fr-SY","version":"Release 10"},"fr-tg":{"language":"French","location":"Togo","id":4096,"tag":"fr-TG","version":"Release 10"},"fr-tn":{"language":"French","location":"Tunisia","id":4096,"tag":"fr-TN","version":"Release 10"},"fr-vu":{"language":"French","location":"Vanuatu","id":4096,"tag":"fr-VU","version":"Release 10"},"fr-wf":{"language":"French","location":"Wallis and Futuna","id":4096,"tag":"fr-WF","version":"Release 10"},"fy":{"language":"Frisian","location":null,"id":98,"tag":"fy","version":"Release 7"},"fy-nl":{"language":"Frisian","location":"Netherlands","id":1122,"tag":"fy-NL","version":"Release E2"},"fur":{"language":"Friulian","location":null,"id":4096,"tag":"fur","version":"Release 10"},"fur-it":{"language":"Friulian","location":"Italy","id":4096,"tag":"fur-IT","version":"Release 10"},"ff":{"language":"Fulah","location":null,"id":103,"tag":"ff","version":"Release 8"},"ff-latn":{"language":"Fulah (Latin)","location":null,"id":31847,"tag":"ff-Latn","version":"Release 8"},"ff-latn-bf":{"language":"Fulah (Latin)","location":"Burkina Faso","id":4096,"tag":"ff-Latn-BF","version":"Release 10.4"},"ff-cm":{"language":"Fulah","location":"Cameroon","id":4096,"tag":"ff-CM","version":"Release 10"},"ff-latn-cm":{"language":"Fulah (Latin)","location":"Cameroon","id":4096,"tag":"ff-Latn-CM","version":"Release 10.4"},"ff-latn-gm":{"language":"Fulah (Latin)","location":"Gambia","id":4096,"tag":"ff-Latn-GM","version":"Release 10.4"},"ff-latn-gh":{"language":"Fulah (Latin)","location":"Ghana","id":4096,"tag":"ff-Latn-GH","version":"Release 10.4"},"ff-gn":{"language":"Fulah","location":"Guinea","id":4096,"tag":"ff-GN","version":"Release 10"},"ff-latn-gn":{"language":"Fulah (Latin)","location":"Guinea","id":4096,"tag":"ff-Latn-GN","version":"Release 10.4"},"ff-latn-gw":{"language":"Fulah (Latin)","location":"Guinea-Bissau","id":4096,"tag":"ff-Latn-GW","version":"Release 10.4"},"ff-latn-lr":{"language":"Fulah (Latin)","location":"Liberia","id":4096,"tag":"ff-Latn-LR","version":"Release 10.4"},"ff-mr":{"language":"Fulah","location":"Mauritania","id":4096,"tag":"ff-MR","version":"Release 10"},"ff-latn-mr":{"language":"Fulah (Latin)","location":"Mauritania","id":4096,"tag":"ff-Latn-MR","version":"Release 10.4"},"ff-latn-ne":{"language":"Fulah (Latin)","location":"Niger","id":4096,"tag":"ff-Latn-NE","version":"Release 10.4"},"ff-ng":{"language":"Fulah","location":"Nigeria","id":4096,"tag":"ff-NG","version":"Release 10"},"ff-latn-ng":{"language":"Fulah (Latin)","location":"Nigeria","id":4096,"tag":"ff-Latn-NG","version":"Release 10.4"},"ff-latn-sn":{"language":"Fulah","location":"Senegal","id":2151,"tag":"ff-Latn-SN","version":"Release 8"},"ff-latn-sl":{"language":"Fulah (Latin)","location":"Sierra Leone","id":4096,"tag":"ff-Latn-SL","version":"Release 10.4"},"gl":{"language":"Galician","location":null,"id":86,"tag":"gl","version":"Release 7"},"gl-es":{"language":"Galician","location":"Spain","id":1110,"tag":"gl-ES","version":"Release D"},"lg":{"language":"Ganda","location":null,"id":4096,"tag":"lg","version":"Release 10"},"lg-ug":{"language":"Ganda","location":"Uganda","id":4096,"tag":"lg-UG","version":"Release 10"},"ka":{"language":"Georgian","location":null,"id":55,"tag":"ka","version":"Release 7"},"ka-ge":{"language":"Georgian","location":"Georgia","id":1079,"tag":"ka-GE","version":"Release C"},"de":{"language":"German","location":null,"id":7,"tag":"de","version":"Release 7"},"de-at":{"language":"German","location":"Austria","id":3079,"tag":"de-AT","version":"Release A"},"de-be":{"language":"German","location":"Belgium","id":4096,"tag":"de-BE","version":"Release 10"},"de-de":{"language":"German","location":"Germany","id":1031,"tag":"de-DE","version":"Release A"},"de-it":{"language":"German","location":"Italy","id":4096,"tag":"de-IT","version":"Release 10.2"},"de-li":{"language":"German","location":"Liechtenstein","id":5127,"tag":"de-LI","version":"Release B"},"de-lu":{"language":"German","location":"Luxembourg","id":4103,"tag":"de-LU","version":"Release B"},"de-ch":{"language":"German","location":"Switzerland","id":2055,"tag":"de-CH","version":"Release A"},"el":{"language":"Greek","location":null,"id":8,"tag":"el","version":"Release 7"},"el-cy":{"language":"Greek","location":"Cyprus","id":4096,"tag":"el-CY","version":"Release 10"},"el-gr":{"language":"Greek","location":"Greece","id":1032,"tag":"el-GR","version":"Release A"},"kl":{"language":"Greenlandic","location":null,"id":111,"tag":"kl","version":"Release 7"},"kl-gl":{"language":"Greenlandic","location":"Greenland","id":1135,"tag":"kl-GL","version":"Release V"},"gn":{"language":"Guarani","location":null,"id":116,"tag":"gn","version":"Release 8.1"},"gn-py":{"language":"Guarani","location":"Paraguay","id":1140,"tag":"gn-PY","version":"Release 8.1"},"gu":{"language":"Gujarati","location":null,"id":71,"tag":"gu","version":"Release 7"},"gu-in":{"language":"Gujarati","location":"India","id":1095,"tag":"gu-IN","version":"Release D"},"guz":{"language":"Gusii","location":null,"id":4096,"tag":"guz","version":"Release 10"},"guz-ke":{"language":"Gusii","location":"Kenya","id":4096,"tag":"guz-KE","version":"Release 10"},"ha":{"language":"Hausa (Latin)","location":null,"id":104,"tag":"ha","version":"Release 7"},"ha-latn":{"language":"Hausa (Latin)","location":null,"id":31848,"tag":"ha-Latn","version":"Windows 7"},"ha-latn-gh":{"language":"Hausa (Latin)","location":"Ghana","id":4096,"tag":"ha-Latn-GH","version":"Release 10"},"ha-latn-ne":{"language":"Hausa (Latin)","location":"Niger","id":4096,"tag":"ha-Latn-NE","version":"Release 10"},"ha-latn-ng":{"language":"Hausa (Latin)","location":"Nigeria","id":1128,"tag":"ha-Latn-NG","version":"Release V"},"haw":{"language":"Hawaiian","location":null,"id":117,"tag":"haw","version":"Release 8"},"haw-us":{"language":"Hawaiian","location":"United States","id":1141,"tag":"haw-US","version":"Release 8"},"he":{"language":"Hebrew","location":null,"id":13,"tag":"he","version":"Release 7"},"he-il":{"language":"Hebrew","location":"Israel","id":1037,"tag":"he-IL","version":"Release B"},"hi":{"language":"Hindi","location":null,"id":57,"tag":"hi","version":"Release 7"},"hi-in":{"language":"Hindi","location":"India","id":1081,"tag":"hi-IN","version":"Release C"},"hu":{"language":"Hungarian","location":null,"id":14,"tag":"hu","version":"Release 7"},"hu-hu":{"language":"Hungarian","location":"Hungary","id":1038,"tag":"hu-HU","version":"Release A"},"is":{"language":"Icelandic","location":null,"id":15,"tag":"is","version":"Release 7"},"is-is":{"language":"Icelandic","location":"Iceland","id":1039,"tag":"is-IS","version":"Release A"},"ig":{"language":"Igbo","location":null,"id":112,"tag":"ig","version":"Release 7"},"ig-ng":{"language":"Igbo","location":"Nigeria","id":1136,"tag":"ig-NG","version":"Release V"},"id":{"language":"Indonesian","location":null,"id":33,"tag":"id","version":"Release 7"},"id-id":{"language":"Indonesian","location":"Indonesia","id":1057,"tag":"id-ID","version":"Release B"},"ia":{"language":"Interlingua","location":null,"id":4096,"tag":"ia","version":"Release 10"},"ia-fr":{"language":"Interlingua","location":"France","id":4096,"tag":"ia-FR","version":"Release 10"},"ia-001":{"language":"Interlingua","location":"World","id":4096,"tag":"ia-001","version":"Release 10"},"iu":{"language":"Inuktitut (Latin)","location":null,"id":93,"tag":"iu","version":"Release 7"},"iu-latn":{"language":"Inuktitut (Latin)","location":null,"id":31837,"tag":"iu-Latn","version":"Windows 7"},"iu-latn-ca":{"language":"Inuktitut (Latin)","location":"Canada","id":2141,"tag":"iu-Latn-CA","version":"Release E2"},"iu-cans":{"language":"Inuktitut (Syllabics)","location":null,"id":30813,"tag":"iu-Cans","version":"Windows 7"},"iu-cans-ca":{"language":"Inuktitut (Syllabics)","location":"Canada","id":1117,"tag":"iu-Cans-CA","version":"Release V"},"ga":{"language":"Irish","location":null,"id":60,"tag":"ga","version":"Windows 7"},"ga-ie":{"language":"Irish","location":"Ireland","id":2108,"tag":"ga-IE","version":"Release E2"},"it":{"language":"Italian","location":null,"id":16,"tag":"it","version":"Release 7"},"it-it":{"language":"Italian","location":"Italy","id":1040,"tag":"it-IT","version":"Release A"},"it-sm":{"language":"Italian","location":"San Marino","id":4096,"tag":"it-SM","version":"Release 10"},"it-ch":{"language":"Italian","location":"Switzerland","id":2064,"tag":"it-CH","version":"Release A"},"it-va":{"language":"Italian","location":"Vatican City","id":4096,"tag":"it-VA","version":"Release 10.3"},"ja":{"language":"Japanese","location":null,"id":17,"tag":"ja","version":"Release 7"},"ja-jp":{"language":"Japanese","location":"Japan","id":1041,"tag":"ja-JP","version":"Release A"},"jv":{"language":"Javanese","location":null,"id":4096,"tag":"jv","version":"Release 8.1"},"jv-latn":{"language":"Javanese","location":"Latin","id":4096,"tag":"jv-Latn","version":"Release 8.1"},"jv-latn-id":{"language":"Javanese","location":"Latin, Indonesia","id":4096,"tag":"jv-Latn-ID","version":"Release 8.1"},"dyo":{"language":"Jola-Fonyi","location":null,"id":4096,"tag":"dyo","version":"Release 10"},"dyo-sn":{"language":"Jola-Fonyi","location":"Senegal","id":4096,"tag":"dyo-SN","version":"Release 10"},"kea":{"language":"Kabuverdianu","location":null,"id":4096,"tag":"kea","version":"Release 10"},"kea-cv":{"language":"Kabuverdianu","location":"Cabo Verde","id":4096,"tag":"kea-CV","version":"Release 10"},"kab":{"language":"Kabyle","location":null,"id":4096,"tag":"kab","version":"Release 10"},"kab-dz":{"language":"Kabyle","location":"Algeria","id":4096,"tag":"kab-DZ","version":"Release 10"},"kkj":{"language":"Kako","location":null,"id":4096,"tag":"kkj","version":"Release 10"},"kkj-cm":{"language":"Kako","location":"Cameroon","id":4096,"tag":"kkj-CM","version":"Release 10"},"kln":{"language":"Kalenjin","location":null,"id":4096,"tag":"kln","version":"Release 10"},"kln-ke":{"language":"Kalenjin","location":"Kenya","id":4096,"tag":"kln-KE","version":"Release 10"},"kam":{"language":"Kamba","location":null,"id":4096,"tag":"kam","version":"Release 10"},"kam-ke":{"language":"Kamba","location":"Kenya","id":4096,"tag":"kam-KE","version":"Release 10"},"kn":{"language":"Kannada","location":null,"id":75,"tag":"kn","version":"Release 7"},"kn-in":{"language":"Kannada","location":"India","id":1099,"tag":"kn-IN","version":"Release D"},"ks":{"language":"Kashmiri","location":null,"id":96,"tag":"ks","version":"Release 10"},"ks-arab":{"language":"Kashmiri","location":"Perso-Arabic","id":1120,"tag":"ks-Arab","version":"Release 10"},"ks-arab-in":{"language":"Kashmiri","location":"Perso-Arabic","id":4096,"tag":"ks-Arab-IN","version":"Release 10"},"kk":{"language":"Kazakh","location":null,"id":63,"tag":"kk","version":"Release 7"},"kk-kz":{"language":"Kazakh","location":"Kazakhstan","id":1087,"tag":"kk-KZ","version":"Release C"},"km":{"language":"Khmer","location":null,"id":83,"tag":"km","version":"Release 7"},"km-kh":{"language":"Khmer","location":"Cambodia","id":1107,"tag":"km-KH","version":"Release V"},"quc":{"language":"K\'iche","location":null,"id":134,"tag":"quc","version":"Release 10"},"quc-latn-gt":{"language":"K\'iche","location":"Guatemala","id":1158,"tag":"quc-Latn-GT","version":"Release 10"},"ki":{"language":"Kikuyu","location":null,"id":4096,"tag":"ki","version":"Release 10"},"ki-ke":{"language":"Kikuyu","location":"Kenya","id":4096,"tag":"ki-KE","version":"Release 10"},"rw":{"language":"Kinyarwanda","location":null,"id":135,"tag":"rw","version":"Release 7"},"rw-rw":{"language":"Kinyarwanda","location":"Rwanda","id":1159,"tag":"rw-RW","version":"Release V"},"sw":{"language":"Kiswahili","location":null,"id":65,"tag":"sw","version":"Release 7"},"sw-ke":{"language":"Kiswahili","location":"Kenya","id":1089,"tag":"sw-KE","version":"Release C"},"sw-tz":{"language":"Kiswahili","location":"Tanzania","id":4096,"tag":"sw-TZ","version":"Release 10"},"sw-ug":{"language":"Kiswahili","location":"Uganda","id":4096,"tag":"sw-UG","version":"Release 10"},"kok":{"language":"Konkani","location":null,"id":87,"tag":"kok","version":"Release 7"},"kok-in":{"language":"Konkani","location":"India","id":1111,"tag":"kok-IN","version":"Release C"},"ko":{"language":"Korean","location":null,"id":18,"tag":"ko","version":"Release 7"},"ko-kr":{"language":"Korean","location":"Korea","id":1042,"tag":"ko-KR","version":"Release A"},"ko-kp":{"language":"Korean","location":"North Korea","id":4096,"tag":"ko-KP","version":"Release 10.1"},"khq":{"language":"Koyra Chiini","location":null,"id":4096,"tag":"khq","version":"Release 10"},"khq-ml":{"language":"Koyra Chiini","location":"Mali","id":4096,"tag":"khq-ML","version":"Release 10"},"ses":{"language":"Koyraboro Senni","location":null,"id":4096,"tag":"ses","version":"Release 10"},"ses-ml":{"language":"Koyraboro Senni","location":"Mali","id":4096,"tag":"ses-ML","version":"Release 10"},"nmg":{"language":"Kwasio","location":null,"id":4096,"tag":"nmg","version":"Release 10"},"nmg-cm":{"language":"Kwasio","location":"Cameroon","id":4096,"tag":"nmg-CM","version":"Release 10"},"ky":{"language":"Kyrgyz","location":null,"id":64,"tag":"ky","version":"Release 7"},"ky-kg":{"language":"Kyrgyz","location":"Kyrgyzstan","id":1088,"tag":"ky-KG","version":"Release D"},"ku-arab-ir":{"language":"Kurdish","location":"Perso-Arabic, Iran","id":4096,"tag":"ku-Arab-IR","version":"Release 10.1"},"lkt":{"language":"Lakota","location":null,"id":4096,"tag":"lkt","version":"Release 10"},"lkt-us":{"language":"Lakota","location":"United States","id":4096,"tag":"lkt-US","version":"Release 10"},"lag":{"language":"Langi","location":null,"id":4096,"tag":"lag","version":"Release 10"},"lag-tz":{"language":"Langi","location":"Tanzania","id":4096,"tag":"lag-TZ","version":"Release 10"},"lo":{"language":"Lao","location":null,"id":84,"tag":"lo","version":"Release 7"},"lo-la":{"language":"Lao","location":"Lao P.D.R.","id":1108,"tag":"lo-LA","version":"Release V"},"lv":{"language":"Latvian","location":null,"id":38,"tag":"lv","version":"Release 7"},"lv-lv":{"language":"Latvian","location":"Latvia","id":1062,"tag":"lv-LV","version":"Release B"},"ln":{"language":"Lingala","location":null,"id":4096,"tag":"ln","version":"Release 10"},"ln-ao":{"language":"Lingala","location":"Angola","id":4096,"tag":"ln-AO","version":"Release 10"},"ln-cf":{"language":"Lingala","location":"Central African Republic","id":4096,"tag":"ln-CF","version":"Release 10"},"ln-cg":{"language":"Lingala","location":"Congo","id":4096,"tag":"ln-CG","version":"Release 10"},"ln-cd":{"language":"Lingala","location":"Congo DRC","id":4096,"tag":"ln-CD","version":"Release 10"},"lt":{"language":"Lithuanian","location":null,"id":39,"tag":"lt","version":"Release 7"},"lt-lt":{"language":"Lithuanian","location":"Lithuania","id":1063,"tag":"lt-LT","version":"Release B"},"nds":{"language":"Low German","location":null,"id":4096,"tag":"nds","version":"Release 10.2"},"nds-de":{"language":"Low German ","location":"Germany","id":4096,"tag":"nds-DE","version":"Release 10.2"},"nds-nl":{"language":"Low German","location":"Netherlands","id":4096,"tag":"nds-NL","version":"Release 10.2"},"dsb":{"language":"Lower Sorbian","location":null,"id":31790,"tag":"dsb","version":"Windows 7"},"dsb-de":{"language":"Lower Sorbian","location":"Germany","id":2094,"tag":"dsb-DE","version":"Release V"},"lu":{"language":"Luba-Katanga","location":null,"id":4096,"tag":"lu","version":"Release 10"},"lu-cd":{"language":"Luba-Katanga","location":"Congo DRC","id":4096,"tag":"lu-CD","version":"Release 10"},"luo":{"language":"Luo","location":null,"id":4096,"tag":"luo","version":"Release 10"},"luo-ke":{"language":"Luo","location":"Kenya","id":4096,"tag":"luo-KE","version":"Release 10"},"lb":{"language":"Luxembourgish","location":null,"id":110,"tag":"lb","version":"Release 7"},"lb-lu":{"language":"Luxembourgish","location":"Luxembourg","id":1134,"tag":"lb-LU","version":"Release E2"},"luy":{"language":"Luyia","location":null,"id":4096,"tag":"luy","version":"Release 10"},"luy-ke":{"language":"Luyia","location":"Kenya","id":4096,"tag":"luy-KE","version":"Release 10"},"mk":{"language":"Macedonian","location":null,"id":47,"tag":"mk","version":"Release 7"},"mk-mk":{"language":"Macedonian","location":"North Macedonia ","id":1071,"tag":"mk-MK","version":"Release C"},"jmc":{"language":"Machame","location":null,"id":4096,"tag":"jmc","version":"Release 10"},"jmc-tz":{"language":"Machame","location":"Tanzania","id":4096,"tag":"jmc-TZ","version":"Release 10"},"mgh":{"language":"Makhuwa-Meetto","location":null,"id":4096,"tag":"mgh","version":"Release 10"},"mgh-mz":{"language":"Makhuwa-Meetto","location":"Mozambique","id":4096,"tag":"mgh-MZ","version":"Release 10"},"kde":{"language":"Makonde","location":null,"id":4096,"tag":"kde","version":"Release 10"},"kde-tz":{"language":"Makonde","location":"Tanzania","id":4096,"tag":"kde-TZ","version":"Release 10"},"mg":{"language":"Malagasy","location":null,"id":4096,"tag":"mg","version":"Release 8.1"},"mg-mg":{"language":"Malagasy","location":"Madagascar","id":4096,"tag":"mg-MG","version":"Release 8.1"},"ms":{"language":"Malay","location":null,"id":62,"tag":"ms","version":"Release 7"},"ms-bn":{"language":"Malay","location":"Brunei Darussalam","id":2110,"tag":"ms-BN","version":"Release C"},"ms-my":{"language":"Malay","location":"Malaysia","id":1086,"tag":"ms-MY","version":"Release C"},"ml":{"language":"Malayalam","location":null,"id":76,"tag":"ml","version":"Release 7"},"ml-in":{"language":"Malayalam","location":"India","id":1100,"tag":"ml-IN","version":"Release E1"},"mt":{"language":"Maltese","location":null,"id":58,"tag":"mt","version":"Release 7"},"mt-mt":{"language":"Maltese","location":"Malta","id":1082,"tag":"mt-MT","version":"Release E1"},"gv":{"language":"Manx","location":null,"id":4096,"tag":"gv","version":"Release 10"},"gv-im":{"language":"Manx","location":"Isle of Man","id":4096,"tag":"gv-IM","version":"Release 10"},"mi":{"language":"Maori","location":null,"id":129,"tag":"mi","version":"Release 7"},"mi-nz":{"language":"Maori","location":"New Zealand","id":1153,"tag":"mi-NZ","version":"Release E1"},"arn":{"language":"Mapudungun","location":null,"id":122,"tag":"arn","version":"Release 7"},"arn-cl":{"language":"Mapudungun","location":"Chile","id":1146,"tag":"arn-CL","version":"Release E2"},"mr":{"language":"Marathi","location":null,"id":78,"tag":"mr","version":"Release 7"},"mr-in":{"language":"Marathi","location":"India","id":1102,"tag":"mr-IN","version":"Release C"},"mas":{"language":"Masai","location":null,"id":4096,"tag":"mas","version":"Release 10"},"mas-ke":{"language":"Masai","location":"Kenya","id":4096,"tag":"mas-KE","version":"Release 10"},"mas-tz":{"language":"Masai","location":"Tanzania","id":4096,"tag":"mas-TZ","version":"Release 10"},"mzn-ir":{"language":"Mazanderani","location":"Iran","id":4096,"tag":"mzn-IR","version":"Release 10.1"},"mer":{"language":"Meru","location":null,"id":4096,"tag":"mer","version":"Release 10"},"mer-ke":{"language":"Meru","location":"Kenya","id":4096,"tag":"mer-KE","version":"Release 10"},"mgo":{"language":"Meta\'","location":null,"id":4096,"tag":"mgo","version":"Release 10"},"mgo-cm":{"language":"Meta\'","location":"Cameroon","id":4096,"tag":"mgo-CM","version":"Release 10"},"moh":{"language":"Mohawk","location":null,"id":124,"tag":"moh","version":"Release 7"},"moh-ca":{"language":"Mohawk","location":"Canada","id":1148,"tag":"moh-CA","version":"Release E2"},"mn":{"language":"Mongolian (Cyrillic)","location":null,"id":80,"tag":"mn","version":"Release 7"},"mn-cyrl":{"language":"Mongolian (Cyrillic)","location":null,"id":30800,"tag":"mn-Cyrl","version":"Windows 7"},"mn-mn":{"language":"Mongolian (Cyrillic)","location":"Mongolia","id":1104,"tag":"mn-MN","version":"Release D"},"mn-mong":{"language":"Mongolian (Traditional ","location":null,"id":31824,"tag":"mn-Mong","version":"Windows 7"},"mn-mong-":{"language":"Mongolian (Traditional ","location":"Mongolia","id":3152,"tag":"mn-Mong-","version":"Windows 7"},"mfe":{"language":"Morisyen","location":null,"id":4096,"tag":"mfe","version":"Release 10"},"mfe-mu":{"language":"Morisyen","location":"Mauritius","id":4096,"tag":"mfe-MU","version":"Release 10"},"mua":{"language":"Mundang","location":null,"id":4096,"tag":"mua","version":"Release 10"},"mua-cm":{"language":"Mundang","location":"Cameroon","id":4096,"tag":"mua-CM","version":"Release 10"},"nqo":{"language":"N\'ko","location":null,"id":4096,"tag":"nqo","version":"Release 8.1"},"nqo-gn":{"language":"N\'ko","location":"Guinea","id":4096,"tag":"nqo-GN","version":"Release 8.1"},"naq":{"language":"Nama","location":null,"id":4096,"tag":"naq","version":"Release 10"},"naq-na":{"language":"Nama","location":"Namibia","id":4096,"tag":"naq-NA","version":"Release 10"},"ne":{"language":"Nepali","location":null,"id":97,"tag":"ne","version":"Release 7"},"ne-in":{"language":"Nepali","location":"India","id":2145,"tag":"ne-IN","version":"Release 8.1"},"ne-np":{"language":"Nepali","location":"Nepal","id":1121,"tag":"ne-NP","version":"Release E2"},"nnh":{"language":"Ngiemboon","location":null,"id":4096,"tag":"nnh","version":"Release 10"},"nnh-cm":{"language":"Ngiemboon","location":"Cameroon","id":4096,"tag":"nnh-CM","version":"Release 10"},"jgo":{"language":"Ngomba","location":null,"id":4096,"tag":"jgo","version":"Release 10"},"jgo-cm":{"language":"Ngomba","location":"Cameroon","id":4096,"tag":"jgo-CM","version":"Release 10"},"lrc-iq":{"language":"Northern Luri","location":"Iraq","id":4096,"tag":"lrc-IQ","version":"Release 10.1"},"lrc-ir":{"language":"Northern Luri","location":"Iran","id":4096,"tag":"lrc-IR","version":"Release 10.1"},"nd":{"language":"North Ndebele","location":null,"id":4096,"tag":"nd","version":"Release 10"},"nd-zw":{"language":"North Ndebele","location":"Zimbabwe","id":4096,"tag":"nd-ZW","version":"Release 10"},"no":{"language":"Norwegian (Bokmal)","location":null,"id":20,"tag":"no","version":"Release 7"},"nb":{"language":"Norwegian (Bokmal)","location":null,"id":31764,"tag":"nb","version":"Release 7"},"nb-no":{"language":"Norwegian (Bokmal)","location":"Norway","id":1044,"tag":"nb-NO","version":"Release A"},"nn":{"language":"Norwegian (Nynorsk)","location":null,"id":30740,"tag":"nn","version":"Release 7"},"nn-no":{"language":"Norwegian (Nynorsk)","location":"Norway","id":2068,"tag":"nn-NO","version":"Release A"},"nb-sj":{"language":"Norwegian Bokmål","location":"Svalbard and Jan Mayen","id":4096,"tag":"nb-SJ","version":"Release 10"},"nus":{"language":"Nuer","location":null,"id":4096,"tag":"nus","version":"Release 10"},"nus-sd":{"language":"Nuer","location":"Sudan","id":4096,"tag":"nus-SD","version":"Release 10"},"nus-ss":{"language":"Nuer","location":"South Sudan","id":4096,"tag":"nus-SS","version":"Release 10.1"},"nyn":{"language":"Nyankole","location":null,"id":4096,"tag":"nyn","version":"Release 10"},"nyn-ug":{"language":"Nyankole","location":"Uganda","id":4096,"tag":"nyn-UG","version":"Release 10"},"oc":{"language":"Occitan","location":null,"id":130,"tag":"oc","version":"Release 7"},"oc-fr":{"language":"Occitan","location":"France","id":1154,"tag":"oc-FR","version":"Release V"},"or":{"language":"Odia","location":null,"id":72,"tag":"or","version":"Release 7"},"or-in":{"language":"Odia","location":"India","id":1096,"tag":"or-IN","version":"Release V"},"om":{"language":"Oromo","location":null,"id":114,"tag":"om","version":"Release 8.1"},"om-et":{"language":"Oromo","location":"Ethiopia","id":1138,"tag":"om-ET","version":"Release 8.1"},"om-ke":{"language":"Oromo","location":"Kenya","id":4096,"tag":"om-KE","version":"Release 10"},"os":{"language":"Ossetian","location":null,"id":4096,"tag":"os","version":"Release 10"},"os-ge":{"language":"Ossetian","location":"Cyrillic, Georgia","id":4096,"tag":"os-GE","version":"Release 10"},"os-ru":{"language":"Ossetian","location":"Cyrillic, Russia","id":4096,"tag":"os-RU","version":"Release 10"},"ps":{"language":"Pashto","location":null,"id":99,"tag":"ps","version":"Release 7"},"ps-af":{"language":"Pashto","location":"Afghanistan","id":1123,"tag":"ps-AF","version":"Release E2"},"fa":{"language":"Persian","location":null,"id":41,"tag":"fa","version":"Release 7"},"fa-af":{"language":"Persian","location":"Afghanistan","id":4096,"tag":"fa-AF","version":"Release 10"},"fa-ir":{"language":"Persian","location":"Iran","id":1065,"tag":"fa-IR","version":"Release B"},"pl":{"language":"Polish","location":null,"id":21,"tag":"pl","version":"Release 7"},"pl-pl":{"language":"Polish","location":"Poland","id":1045,"tag":"pl-PL","version":"Release A"},"pt":{"language":"Portuguese","location":null,"id":22,"tag":"pt","version":"Release 7"},"pt-ao":{"language":"Portuguese","location":"Angola","id":4096,"tag":"pt-AO","version":"Release 8.1"},"pt-br":{"language":"Portuguese","location":"Brazil","id":1046,"tag":"pt-BR","version":"ReleaseA"},"pt-cv":{"language":"Portuguese","location":"Cabo Verde","id":4096,"tag":"pt-CV","version":"Release 10"},"pt-gq":{"language":"Portuguese","location":"Equatorial Guinea","id":4096,"tag":"pt-GQ","version":"Release 10.2"},"pt-gw":{"language":"Portuguese","location":"Guinea-Bissau","id":4096,"tag":"pt-GW","version":"Release 10"},"pt-lu":{"language":"Portuguese","location":"Luxembourg","id":4096,"tag":"pt-LU","version":"Release 10.2"},"pt-mo":{"language":"Portuguese","location":"Macao SAR","id":4096,"tag":"pt-MO","version":"Release 10"},"pt-mz":{"language":"Portuguese","location":"Mozambique","id":4096,"tag":"pt-MZ","version":"Release 10"},"pt-pt":{"language":"Portuguese","location":"Portugal","id":2070,"tag":"pt-PT","version":"Release A"},"pt-st":{"language":"Portuguese","location":"São Tomé and Príncipe","id":4096,"tag":"pt-ST","version":"Release 10"},"pt-ch":{"language":"Portuguese","location":"Switzerland","id":4096,"tag":"pt-CH","version":"Release 10.2"},"pt-tl":{"language":"Portuguese","location":"Timor-Leste","id":4096,"tag":"pt-TL","version":"Release 10"},"prg-001":{"language":"Prussian","location":null,"id":4096,"tag":"prg-001","version":"Release 10.1"},"qps-ploca":{"language":"Pseudo Language","location":"Pseudo locale for east Asian/complex ","id":1534,"tag":"qps-ploca","version":"Release 7"},"qps-ploc":{"language":"Pseudo Language","location":"Pseudo locale used for localization ","id":1281,"tag":"qps-ploc","version":"Release 7"},"qps-plocm":{"language":"Pseudo Language","location":"Pseudo locale used for localization ","id":2559,"tag":"qps-plocm","version":"Release 7"},"pa":{"language":"Punjabi","location":null,"id":70,"tag":"pa","version":"Release 7"},"pa-arab":{"language":"Punjabi","location":null,"id":31814,"tag":"pa-Arab","version":"Release 8"},"pa-in":{"language":"Punjabi","location":"India","id":1094,"tag":"pa-IN","version":"Release D"},"pa-arab-pk":{"language":"Punjabi","location":"Islamic Republic of Pakistan","id":2118,"tag":"pa-Arab-PK","version":"Release 8"},"quz":{"language":"Quechua","location":null,"id":107,"tag":"quz","version":"Release 7"},"quz-bo":{"language":"Quechua","location":"Bolivia","id":1131,"tag":"quz-BO","version":"Release E1"},"quz-ec":{"language":"Quechua","location":"Ecuador","id":2155,"tag":"quz-EC","version":"Release E1"},"quz-pe":{"language":"Quechua","location":"Peru","id":3179,"tag":"quz-PE","version":"Release E1"},"ksh":{"language":"Ripuarian","location":null,"id":4096,"tag":"ksh","version":"Release 10"},"ksh-de":{"language":"Ripuarian","location":"Germany","id":4096,"tag":"ksh-DE","version":"Release 10"},"ro":{"language":"Romanian","location":null,"id":24,"tag":"ro","version":"Release 7"},"ro-md":{"language":"Romanian","location":"Moldova","id":2072,"tag":"ro-MD","version":"Release 8.1"},"ro-ro":{"language":"Romanian","location":"Romania","id":1048,"tag":"ro-RO","version":"Release A"},"rm":{"language":"Romansh","location":null,"id":23,"tag":"rm","version":"Release 7"},"rm-ch":{"language":"Romansh","location":"Switzerland","id":1047,"tag":"rm-CH","version":"Release E2"},"rof":{"language":"Rombo","location":null,"id":4096,"tag":"rof","version":"Release 10"},"rof-tz":{"language":"Rombo","location":"Tanzania","id":4096,"tag":"rof-TZ","version":"Release 10"},"rn":{"language":"Rundi","location":null,"id":4096,"tag":"rn","version":"Release 10"},"rn-bi":{"language":"Rundi","location":"Burundi","id":4096,"tag":"rn-BI","version":"Release 10"},"ru":{"language":"Russian","location":null,"id":25,"tag":"ru","version":"Release 7"},"ru-by":{"language":"Russian","location":"Belarus","id":4096,"tag":"ru-BY","version":"Release 10"},"ru-kz":{"language":"Russian","location":"Kazakhstan","id":4096,"tag":"ru-KZ","version":"Release 10"},"ru-kg":{"language":"Russian","location":"Kyrgyzstan","id":4096,"tag":"ru-KG","version":"Release 10"},"ru-md":{"language":"Russian","location":"Moldova","id":2073,"tag":"ru-MD","version":"Release 10"},"ru-ru":{"language":"Russian","location":"Russia","id":1049,"tag":"ru-RU","version":"Release A"},"ru-ua":{"language":"Russian","location":"Ukraine","id":4096,"tag":"ru-UA","version":"Release 10"},"rwk":{"language":"Rwa","location":null,"id":4096,"tag":"rwk","version":"Release 10"},"rwk-tz":{"language":"Rwa","location":"Tanzania","id":4096,"tag":"rwk-TZ","version":"Release 10"},"ssy":{"language":"Saho","location":null,"id":4096,"tag":"ssy","version":"Release 10"},"ssy-er":{"language":"Saho","location":"Eritrea","id":4096,"tag":"ssy-ER","version":"Release 10"},"sah":{"language":"Sakha","location":null,"id":133,"tag":"sah","version":"Release 7"},"sah-ru":{"language":"Sakha","location":"Russia","id":1157,"tag":"sah-RU","version":"Release V"},"saq":{"language":"Samburu","location":null,"id":4096,"tag":"saq","version":"Release 10"},"saq-ke":{"language":"Samburu","location":"Kenya","id":4096,"tag":"saq-KE","version":"Release 10"},"smn":{"language":"Sami (Inari)","location":null,"id":28731,"tag":"smn","version":"Windows 7"},"smn-fi":{"language":"Sami (Inari)","location":"Finland","id":9275,"tag":"smn-FI","version":"Release E1"},"smj":{"language":"Sami (Lule)","location":null,"id":31803,"tag":"smj","version":"Windows 7"},"smj-no":{"language":"Sami (Lule)","location":"Norway","id":4155,"tag":"smj-NO","version":"Release E1"},"smj-se":{"language":"Sami (Lule)","location":"Sweden","id":5179,"tag":"smj-SE","version":"Release E1"},"se":{"language":"Sami (Northern)","location":null,"id":59,"tag":"se","version":"Release 7"},"se-fi":{"language":"Sami (Northern)","location":"Finland","id":3131,"tag":"se-FI","version":"Release E1"},"se-no":{"language":"Sami (Northern)","location":"Norway","id":1083,"tag":"se-NO","version":"Release E1"},"se-se":{"language":"Sami (Northern)","location":"Sweden","id":2107,"tag":"se-SE","version":"Release E1"},"sms":{"language":"Sami (Skolt)","location":null,"id":29755,"tag":"sms","version":"Windows 7"},"sms-fi":{"language":"Sami (Skolt)","location":"Finland","id":8251,"tag":"sms-FI","version":"Release E1"},"sma":{"language":"Sami (Southern)","location":null,"id":30779,"tag":"sma","version":"Windows 7"},"sma-no":{"language":"Sami (Southern)","location":"Norway","id":6203,"tag":"sma-NO","version":"Release E1"},"sma-se":{"language":"Sami (Southern)","location":"Sweden","id":7227,"tag":"sma-SE","version":"Release E1"},"sg":{"language":"Sango","location":null,"id":4096,"tag":"sg","version":"Release 10"},"sg-cf":{"language":"Sango","location":"Central African Republic","id":4096,"tag":"sg-CF","version":"Release 10"},"sbp":{"language":"Sangu","location":null,"id":4096,"tag":"sbp","version":"Release 10"},"sbp-tz":{"language":"Sangu","location":"Tanzania","id":4096,"tag":"sbp-TZ","version":"Release 10"},"sa":{"language":"Sanskrit","location":null,"id":79,"tag":"sa","version":"Release 7"},"sa-in":{"language":"Sanskrit","location":"India","id":1103,"tag":"sa-IN","version":"Release C"},"gd":{"language":"Scottish Gaelic","location":null,"id":145,"tag":"gd","version":"Windows 7"},"gd-gb":{"language":"Scottish Gaelic","location":"United Kingdom","id":1169,"tag":"gd-GB","version":"Release 7"},"seh":{"language":"Sena","location":null,"id":4096,"tag":"seh","version":"Release 10"},"seh-mz":{"language":"Sena","location":"Mozambique","id":4096,"tag":"seh-MZ","version":"Release 10"},"sr-cyrl":{"language":"Serbian (Cyrillic)","location":null,"id":27674,"tag":"sr-Cyrl","version":"Windows 7"},"sr-cyrl-ba":{"language":"Serbian (Cyrillic)","location":"Bosnia and Herzegovina","id":7194,"tag":"sr-Cyrl-BA","version":"Release E1"},"sr-cyrl-me":{"language":"Serbian (Cyrillic)","location":"Montenegro","id":12314,"tag":"sr-Cyrl-ME","version":"Release 7"},"sr-cyrl-rs":{"language":"Serbian (Cyrillic)","location":"Serbia","id":10266,"tag":"sr-Cyrl-RS","version":"Release 7"},"sr-cyrl-cs":{"language":"Serbian (Cyrillic)","location":"Serbia and Montenegro (Former)","id":3098,"tag":"sr-Cyrl-CS","version":"Release B"},"sr-latn":{"language":"Serbian (Latin)","location":null,"id":28698,"tag":"sr-Latn","version":"Windows 7"},"sr":{"language":"Serbian (Latin)","location":null,"id":31770,"tag":"sr","version":"Release 7"},"sr-latn-ba":{"language":"Serbian (Latin)","location":"Bosnia and Herzegovina","id":6170,"tag":"sr-Latn-BA","version":"Release E1"},"sr-latn-me":{"language":"Serbian (Latin)","location":"Montenegro","id":11290,"tag":"sr-Latn-ME","version":"Release 7"},"sr-latn-rs":{"language":"Serbian (Latin)","location":"Serbia","id":9242,"tag":"sr-Latn-RS","version":"Release 7"},"sr-latn-cs":{"language":"Serbian (Latin)","location":"Serbia and Montenegro (Former)","id":2074,"tag":"sr-Latn-CS","version":"Release B"},"nso":{"language":"Sesotho sa Leboa","location":null,"id":108,"tag":"nso","version":"Release 7"},"nso-za":{"language":"Sesotho sa Leboa","location":"South Africa","id":1132,"tag":"nso-ZA","version":"Release E1"},"tn":{"language":"Setswana","location":null,"id":50,"tag":"tn","version":"Release 7"},"tn-bw":{"language":"Setswana","location":"Botswana","id":2098,"tag":"tn-BW","version":"Release 8"},"tn-za":{"language":"Setswana","location":"South Africa","id":1074,"tag":"tn-ZA","version":"Release E1"},"ksb":{"language":"Shambala","location":null,"id":4096,"tag":"ksb","version":"Release 10"},"ksb-tz":{"language":"Shambala","location":"Tanzania","id":4096,"tag":"ksb-TZ","version":"Release 10"},"sn":{"language":"Shona","location":null,"id":4096,"tag":"sn","version":"Release 8.1"},"sn-latn":{"language":"Shona","location":"Latin","id":4096,"tag":"sn-Latn","version":"Release 8.1"},"sn-latn-zw":{"language":"Shona","location":"Zimbabwe","id":4096,"tag":"sn-Latn-ZW","version":"Release 8.1"},"sd":{"language":"Sindhi","location":null,"id":89,"tag":"sd","version":"Release 8"},"sd-arab":{"language":"Sindhi","location":null,"id":31833,"tag":"sd-Arab","version":"Release 8"},"sd-arab-pk":{"language":"Sindhi","location":"Islamic Republic of Pakistan","id":2137,"tag":"sd-Arab-PK","version":"Release 8"},"si":{"language":"Sinhala","location":null,"id":91,"tag":"si","version":"Release 7"},"si-lk":{"language":"Sinhala","location":"Sri Lanka","id":1115,"tag":"si-LK","version":"Release V"},"sk":{"language":"Slovak","location":null,"id":27,"tag":"sk","version":"Release 7"},"sk-sk":{"language":"Slovak","location":"Slovakia","id":1051,"tag":"sk-SK","version":"Release A"},"sl":{"language":"Slovenian","location":null,"id":36,"tag":"sl","version":"Release 7"},"sl-si":{"language":"Slovenian","location":"Slovenia","id":1060,"tag":"sl-SI","version":"Release A"},"xog":{"language":"Soga","location":null,"id":4096,"tag":"xog","version":"Release 10"},"xog-ug":{"language":"Soga","location":"Uganda","id":4096,"tag":"xog-UG","version":"Release 10"},"so":{"language":"Somali","location":null,"id":119,"tag":"so","version":"Release 8.1"},"so-dj":{"language":"Somali","location":"Djibouti","id":4096,"tag":"so-DJ","version":"Release 10"},"so-et":{"language":"Somali","location":"Ethiopia","id":4096,"tag":"so-ET","version":"Release 10"},"so-ke":{"language":"Somali","location":"Kenya","id":4096,"tag":"so-KE","version":"Release 10"},"so-so":{"language":"Somali","location":"Somalia","id":1143,"tag":"so-SO","version":"Release 8.1"},"st":{"language":"Sotho","location":null,"id":48,"tag":"st","version":"Release 8.1"},"st-za":{"language":"Sotho","location":"South Africa","id":1072,"tag":"st-ZA","version":"Release 8.1"},"nr":{"language":"South Ndebele","location":null,"id":4096,"tag":"nr","version":"Release 10"},"nr-za":{"language":"South Ndebele","location":"South Africa","id":4096,"tag":"nr-ZA","version":"Release 10"},"st-ls":{"language":"Southern Sotho","location":"Lesotho","id":4096,"tag":"st-LS","version":"Release 10"},"es":{"language":"Spanish","location":null,"id":10,"tag":"es","version":"Release 7"},"es-ar":{"language":"Spanish","location":"Argentina","id":11274,"tag":"es-AR","version":"Release B"},"es-bz":{"language":"Spanish","location":"Belize","id":4096,"tag":"es-BZ","version":"Release 10.3"},"es-ve":{"language":"Spanish","location":"Bolivarian Republic of Venezuela","id":8202,"tag":"es-VE","version":"Release B"},"es-bo":{"language":"Spanish","location":"Bolivia","id":16394,"tag":"es-BO","version":"Release B"},"es-br":{"language":"Spanish","location":"Brazil","id":4096,"tag":"es-BR","version":"Release 10.2"},"es-cl":{"language":"Spanish","location":"Chile","id":13322,"tag":"es-CL","version":"Release B"},"es-co":{"language":"Spanish","location":"Colombia","id":9226,"tag":"es-CO","version":"Release B"},"es-cr":{"language":"Spanish","location":"Costa Rica","id":5130,"tag":"es-CR","version":"Release B"},"es-cu":{"language":"Spanish","location":"Cuba","id":23562,"tag":"es-CU","version":"Release 10"},"es-do":{"language":"Spanish","location":"Dominican Republic","id":7178,"tag":"es-DO","version":"Release B"},"es-ec":{"language":"Spanish","location":"Ecuador","id":12298,"tag":"es-EC","version":"Release B"},"es-sv":{"language":"Spanish","location":"El Salvador","id":17418,"tag":"es-SV","version":"Release B"},"es-gq":{"language":"Spanish","location":"Equatorial Guinea","id":4096,"tag":"es-GQ","version":"Release 10"},"es-gt":{"language":"Spanish","location":"Guatemala","id":4106,"tag":"es-GT","version":"Release B"},"es-hn":{"language":"Spanish","location":"Honduras","id":18442,"tag":"es-HN","version":"Release B"},"es-419":{"language":"Spanish","location":"Latin America","id":22538,"tag":"es-419","version":"Release 8.1"},"es-mx":{"language":"Spanish","location":"Mexico","id":2058,"tag":"es-MX","version":"Release A"},"es-ni":{"language":"Spanish","location":"Nicaragua","id":19466,"tag":"es-NI","version":"Release B"},"es-pa":{"language":"Spanish","location":"Panama","id":6154,"tag":"es-PA","version":"Release B"},"es-py":{"language":"Spanish","location":"Paraguay","id":15370,"tag":"es-PY","version":"Release B"},"es-pe":{"language":"Spanish","location":"Peru","id":10250,"tag":"es-PE","version":"Release B"},"es-ph":{"language":"Spanish","location":"Philippines","id":4096,"tag":"es-PH","version":"Release 10"},"es-pr":{"language":"Spanish","location":"Puerto Rico","id":20490,"tag":"es-PR","version":"Release B"},"es-es_tradnl":{"language":"Spanish","location":"Spain","id":1034,"tag":"es-ES_tradnl","version":"Release A"},"es-es":{"language":"Spanish","location":"Spain","id":3082,"tag":"es-ES","version":"Release A"},"es-us":{"language":"Spanish","location":"United States","id":21514,"tag":"es-US","version":"Release V"},"es-uy":{"language":"Spanish","location":"Uruguay","id":14346,"tag":"es-UY","version":"Release B"},"zgh":{"language":"Standard Moroccan ","location":null,"id":4096,"tag":"zgh","version":"Release 8.1"},"zgh-tfng-ma":{"language":"Standard Moroccan ","location":"Morocco","id":4096,"tag":"zgh-Tfng-MA","version":"Release 8.1"},"zgh-tfng":{"language":"Standard Moroccan ","location":"Tifinagh","id":4096,"tag":"zgh-Tfng","version":"Release 8.1"},"ss":{"language":"Swati","location":null,"id":4096,"tag":"ss","version":"Release 10"},"ss-za":{"language":"Swati","location":"South Africa","id":4096,"tag":"ss-ZA","version":"Release 10"},"ss-sz":{"language":"Swati","location":"Swaziland","id":4096,"tag":"ss-SZ","version":"Release 10"},"sv":{"language":"Swedish","location":null,"id":29,"tag":"sv","version":"Release 7"},"sv-ax":{"language":"Swedish","location":"Åland Islands","id":4096,"tag":"sv-AX","version":"Release 10"},"sv-fi":{"language":"Swedish","location":"Finland","id":2077,"tag":"sv-FI","version":"ReleaseB"},"sv-se":{"language":"Swedish","location":"Sweden","id":1053,"tag":"sv-SE","version":"Release A"},"syr":{"language":"Syriac","location":null,"id":90,"tag":"syr","version":"Release 7"},"syr-sy":{"language":"Syriac","location":"Syria","id":1114,"tag":"syr-SY","version":"Release D"},"shi":{"language":"Tachelhit","location":null,"id":4096,"tag":"shi","version":"Release 10"},"shi-tfng":{"language":"Tachelhit","location":"Tifinagh","id":4096,"tag":"shi-Tfng","version":"Release 10"},"shi-tfng-ma":{"language":"Tachelhit","location":"Tifinagh, Morocco","id":4096,"tag":"shi-Tfng-MA","version":"Release 10"},"shi-latn":{"language":"Tachelhit (Latin)","location":null,"id":4096,"tag":"shi-Latn","version":"Release 10"},"shi-latn-ma":{"language":"Tachelhit (Latin)","location":"Morocco","id":4096,"tag":"shi-Latn-MA","version":"Release 10"},"dav":{"language":"Taita","location":null,"id":4096,"tag":"dav","version":"Release 10"},"dav-ke":{"language":"Taita","location":"Kenya","id":4096,"tag":"dav-KE","version":"Release 10"},"tg":{"language":"Tajik (Cyrillic)","location":null,"id":40,"tag":"tg","version":"Release 7"},"tg-cyrl":{"language":"Tajik (Cyrillic)","location":null,"id":31784,"tag":"tg-Cyrl","version":"Windows 7"},"tg-cyrl-tj":{"language":"Tajik (Cyrillic)","location":"Tajikistan","id":1064,"tag":"tg-Cyrl-TJ","version":"Release V"},"tzm":{"language":"Tamazight (Latin)","location":null,"id":95,"tag":"tzm","version":"Release 7"},"tzm-latn":{"language":"Tamazight (Latin)","location":null,"id":31839,"tag":"tzm-Latn","version":"Windows 7"},"tzm-latn-dz":{"language":"Tamazight (Latin)","location":"Algeria","id":2143,"tag":"tzm-Latn-DZ","version":"Release V"},"ta":{"language":"Tamil","location":null,"id":73,"tag":"ta","version":"Release 7"},"ta-in":{"language":"Tamil","location":"India","id":1097,"tag":"ta-IN","version":"Release C"},"ta-my":{"language":"Tamil","location":"Malaysia","id":4096,"tag":"ta-MY","version":"Release 10"},"ta-sg":{"language":"Tamil","location":"Singapore","id":4096,"tag":"ta-SG","version":"Release 10"},"ta-lk":{"language":"Tamil","location":"Sri Lanka","id":2121,"tag":"ta-LK","version":"Release 8"},"twq":{"language":"Tasawaq","location":null,"id":4096,"tag":"twq","version":"Release 10"},"twq-ne":{"language":"Tasawaq","location":"Niger","id":4096,"tag":"twq-NE","version":"Release 10"},"tt":{"language":"Tatar","location":null,"id":68,"tag":"tt","version":"Release 7"},"tt-ru":{"language":"Tatar","location":"Russia","id":1092,"tag":"tt-RU","version":"Release D"},"te":{"language":"Telugu","location":null,"id":74,"tag":"te","version":"Release 7"},"te-in":{"language":"Telugu","location":"India","id":1098,"tag":"te-IN","version":"Release D"},"teo":{"language":"Teso","location":null,"id":4096,"tag":"teo","version":"Release 10"},"teo-ke":{"language":"Teso","location":"Kenya","id":4096,"tag":"teo-KE","version":"Release 10"},"teo-ug":{"language":"Teso","location":"Uganda","id":4096,"tag":"teo-UG","version":"Release 10"},"th":{"language":"Thai","location":null,"id":30,"tag":"th","version":"Release 7"},"th-th":{"language":"Thai","location":"Thailand","id":1054,"tag":"th-TH","version":"Release B"},"bo":{"language":"Tibetan","location":null,"id":81,"tag":"bo","version":"Release 7"},"bo-in":{"language":"Tibetan","location":"India","id":4096,"tag":"bo-IN","version":"Release 10"},"bo-cn":{"language":"Tibetan","location":"People\'s Republic of China","id":1105,"tag":"bo-CN","version":"Release V"},"tig":{"language":"Tigre","location":null,"id":4096,"tag":"tig","version":"Release 10"},"tig-er":{"language":"Tigre","location":"Eritrea","id":4096,"tag":"tig-ER","version":"Release 10"},"ti":{"language":"Tigrinya","location":null,"id":115,"tag":"ti","version":"Release 8"},"ti-er":{"language":"Tigrinya","location":"Eritrea","id":2163,"tag":"ti-ER","version":"Release 8"},"ti-et":{"language":"Tigrinya","location":"Ethiopia","id":1139,"tag":"ti-ET","version":"Release 8"},"to":{"language":"Tongan","location":null,"id":4096,"tag":"to","version":"Release 10"},"to-to":{"language":"Tongan","location":"Tonga","id":4096,"tag":"to-TO","version":"Release 10"},"ts":{"language":"Tsonga","location":null,"id":49,"tag":"ts","version":"Release 8.1"},"ts-za":{"language":"Tsonga","location":"South Africa","id":1073,"tag":"ts-ZA","version":"Release 8.1"},"tr":{"language":"Turkish","location":null,"id":31,"tag":"tr","version":"Release 7"},"tr-cy":{"language":"Turkish","location":"Cyprus","id":4096,"tag":"tr-CY","version":"Release 10"},"tr-tr":{"language":"Turkish","location":"Turkey","id":1055,"tag":"tr-TR","version":"Release A"},"tk":{"language":"Turkmen","location":null,"id":66,"tag":"tk","version":"Release 7"},"tk-tm":{"language":"Turkmen","location":"Turkmenistan","id":1090,"tag":"tk-TM","version":"Release V"},"uk":{"language":"Ukrainian","location":null,"id":34,"tag":"uk","version":"Release 7"},"uk-ua":{"language":"Ukrainian","location":"Ukraine","id":1058,"tag":"uk-UA","version":"Release B"},"hsb":{"language":"Upper Sorbian","location":null,"id":46,"tag":"hsb","version":"Release 7"},"hsb-de":{"language":"Upper Sorbian","location":"Germany","id":1070,"tag":"hsb-DE","version":"Release V"},"ur":{"language":"Urdu","location":null,"id":32,"tag":"ur","version":"Release 7"},"ur-in":{"language":"Urdu","location":"India","id":2080,"tag":"ur-IN","version":"Release 8.1"},"ur-pk":{"language":"Urdu","location":"Islamic Republic of Pakistan","id":1056,"tag":"ur-PK","version":"Release C"},"ug":{"language":"Uyghur","location":null,"id":128,"tag":"ug","version":"Release 7"},"ug-cn":{"language":"Uyghur","location":"People\'s Republic of China","id":1152,"tag":"ug-CN","version":"Release V"},"uz-arab":{"language":"Uzbek","location":"Perso-Arabic","id":4096,"tag":"uz-Arab","version":"Release 10"},"uz-arab-af":{"language":"Uzbek","location":"Perso-Arabic, Afghanistan","id":4096,"tag":"uz-Arab-AF","version":"Release 10"},"uz-cyrl":{"language":"Uzbek (Cyrillic)","location":null,"id":30787,"tag":"uz-Cyrl","version":"Windows 7"},"uz-cyrl-uz":{"language":"Uzbek (Cyrillic)","location":"Uzbekistan","id":2115,"tag":"uz-Cyrl-UZ","version":"Release C"},"uz":{"language":"Uzbek (Latin)","location":null,"id":67,"tag":"uz","version":"Release 7"},"uz-latn":{"language":"Uzbek (Latin)","location":null,"id":31811,"tag":"uz-Latn","version":"Windows 7"},"uz-latn-uz":{"language":"Uzbek (Latin)","location":"Uzbekistan","id":1091,"tag":"uz-Latn-UZ","version":"Release C"},"vai":{"language":"Vai","location":null,"id":4096,"tag":"vai","version":"Release 10"},"vai-vaii":{"language":"Vai","location":null,"id":4096,"tag":"vai-Vaii","version":"Release 10"},"vai-vaii-lr":{"language":"Vai","location":"Liberia","id":4096,"tag":"vai-Vaii-LR","version":"Release 10"},"vai-latn-lr":{"language":"Vai (Latin)","location":"Liberia","id":4096,"tag":"vai-Latn-LR","version":"Release 10"},"vai-latn":{"language":"Vai (Latin)","location":null,"id":4096,"tag":"vai-Latn","version":"Release 10"},"ca-es-":{"language":"Valencian","location":"Spain","id":2051,"tag":"ca-ES-","version":"Release 8"},"ve":{"language":"Venda","location":null,"id":51,"tag":"ve","version":"Release 10"},"ve-za":{"language":"Venda","location":"South Africa","id":1075,"tag":"ve-ZA","version":"Release 10"},"vi":{"language":"Vietnamese","location":null,"id":42,"tag":"vi","version":"Release 7"},"vi-vn":{"language":"Vietnamese","location":"Vietnam","id":1066,"tag":"vi-VN","version":"Release B"},"vo":{"language":"Volapük","location":null,"id":4096,"tag":"vo","version":"Release 10"},"vo-001":{"language":"Volapük","location":"World","id":4096,"tag":"vo-001","version":"Release 10"},"vun":{"language":"Vunjo","location":null,"id":4096,"tag":"vun","version":"Release 10"},"vun-tz":{"language":"Vunjo","location":"Tanzania","id":4096,"tag":"vun-TZ","version":"Release 10"},"wae":{"language":"Walser","location":null,"id":4096,"tag":"wae","version":"Release 10"},"wae-ch":{"language":"Walser","location":"Switzerland","id":4096,"tag":"wae-CH","version":"Release 10"},"cy":{"language":"Welsh","location":null,"id":82,"tag":"cy","version":"Release 7"},"cy-gb":{"language":"Welsh","location":"United Kingdom","id":1106,"tag":"cy-GB","version":"ReleaseE1"},"wal":{"language":"Wolaytta","location":null,"id":4096,"tag":"wal","version":"Release 10"},"wal-et":{"language":"Wolaytta","location":"Ethiopia","id":4096,"tag":"wal-ET","version":"Release 10"},"wo":{"language":"Wolof","location":null,"id":136,"tag":"wo","version":"Release 7"},"wo-sn":{"language":"Wolof","location":"Senegal","id":1160,"tag":"wo-SN","version":"Release V"},"xh":{"language":"Xhosa","location":null,"id":52,"tag":"xh","version":"Release 7"},"xh-za":{"language":"Xhosa","location":"South Africa","id":1076,"tag":"xh-ZA","version":"Release E1"},"yav":{"language":"Yangben","location":null,"id":4096,"tag":"yav","version":"Release 10"},"yav-cm":{"language":"Yangben","location":"Cameroon","id":4096,"tag":"yav-CM","version":"Release 10"},"ii":{"language":"Yi","location":null,"id":120,"tag":"ii","version":"Release 7"},"ii-cn":{"language":"Yi","location":"People\'s Republic of China","id":1144,"tag":"ii-CN","version":"Release V"},"yo":{"language":"Yoruba","location":null,"id":106,"tag":"yo","version":"Release 7"},"yo-bj":{"language":"Yoruba","location":"Benin","id":4096,"tag":"yo-BJ","version":"Release 10"},"yo-ng":{"language":"Yoruba","location":"Nigeria","id":1130,"tag":"yo-NG","version":"Release V"},"dje":{"language":"Zarma","location":null,"id":4096,"tag":"dje","version":"Release 10"},"dje-ne":{"language":"Zarma","location":"Niger","id":4096,"tag":"dje-NE","version":"Release 10"},"zu":{"language":"Zulu","location":null,"id":53,"tag":"zu","version":"Release 7"},"zu-za":{"language":"Zulu","location":"South Africa","id":1077,"tag":"zu-ZA","version":"Release E1"}}')
            },
        "./src/index.js":
            /*!**********************!*\
              !*** ./src/index.js ***!
              \**********************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./js/BoxedTree */ "./src/js/BoxedTree.js"),
                    i = t( /*! ./js/CircleTree */ "./src/js/CircleTree.js");
                n.default = {
                    boxedTree: a.default,
                    circleTree: i.default
                }
            },
        "./src/js/BaseTree.js":
            /*!****************************!*\
              !*** ./src/js/BaseTree.js ***!
              \****************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./CustomD3 */ "./src/js/CustomD3.js"),
                    i = t( /*! ./NodeSettings */ "./src/js/NodeSettings.js"),
                    o = t( /*! ./LoadOnDemandSettings */ "./src/js/LoadOnDemandSettings.js"),
                    s = t( /*! events */ "events");

                function r(e) {
                    return (r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function u(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function l(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function c(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function d(e, n) {
                    return !n || "object" !== r(n) && "function" != typeof n ? g(e) : n
                }

                function g(e) {
                    if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return e
                }

                function h() {
                    if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                    if (Reflect.construct.sham) return !1;
                    if ("function" == typeof Proxy) return !0;
                    try {
                        return Date.prototype.toString.call(Reflect.construct(Date, [], (function() {}))), !0
                    } catch (e) {
                        return !1
                    }
                }

                function f(e) {
                    return (f = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function m(e, n) {
                    return (m = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }

                function p(e) {
                    return function(e) {
                        if (Array.isArray(e)) return _(e)
                    }(e) || function(e) {
                        if ("undefined" != typeof Symbol && Symbol.iterator in Object(e)) return Array.from(e)
                    }(e) || y(e) || function() {
                        throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.")
                    }()
                }

                function v(e) {
                    if ("undefined" == typeof Symbol || null == e[Symbol.iterator]) {
                        if (Array.isArray(e) || (e = y(e))) {
                            var n = 0,
                                t = function() {};
                            return {
                                s: t,
                                n: function() {
                                    return n >= e.length ? {
                                        done: !0
                                    } : {
                                        done: !1,
                                        value: e[n++]
                                    }
                                },
                                e: function(e) {
                                    throw e
                                },
                                f: t
                            }
                        }
                        throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.")
                    }
                    var a, i, o = !0,
                        s = !1;
                    return {
                        s: function() {
                            a = e[Symbol.iterator]()
                        },
                        n: function() {
                            var e = a.next();
                            return o = e.done, e
                        },
                        e: function(e) {
                            s = !0, i = e
                        },
                        f: function() {
                            try {
                                o || null == a.return || a.return()
                            } finally {
                                if (s) throw i
                            }
                        }
                    }
                }

                function y(e, n) {
                    if (e) {
                        if ("string" == typeof e) return _(e, n);
                        var t = Object.prototype.toString.call(e).slice(8, -1);
                        return "Object" === t && e.constructor && (t = e.constructor.name), "Map" === t || "Set" === t ? Array.from(t) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _(e, n) : void 0
                    }
                }

                function _(e, n) {
                    (null == n || n > e.length) && (n = e.length);
                    for (var t = 0, a = new Array(n); t < n; t++) a[t] = e[t];
                    return a
                }
                var b = function(e) {
                    ! function(e, n) {
                        if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                        e.prototype = Object.create(n && n.prototype, {
                            constructor: {
                                value: e,
                                writable: !0,
                                configurable: !0
                            }
                        }), n && m(e, n)
                    }(b, e);
                    var n, t, s, y, _ = (n = b, function() {
                        var e, t = f(n);
                        if (h()) {
                            var a = f(this).constructor;
                            e = Reflect.construct(t, arguments, a)
                        } else e = t.apply(this, arguments);
                        return d(this, e)
                    });

                    function b(e) {
                        var n;
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, b), n = _.call(this), e = e || {};
                        var t = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? u(Object(t), !0).forEach((function(n) {
                                    l(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : u(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, b.defaults, {}, e);
                        return n._root = null, n._svg = null, n._panningContainer = null, n._view = null, n._treeGenerator = null, n._linkPathGenerator = null, n._visibleNodes = null, n._links = null, n._zoomListener = null, n.setTheme(t.theme), n.setOrientation(t.orientation), n.setData(t.data), n.setElement(t.element), n.setWidthWithoutMargins(t.widthWithoutMargins), n.setHeightWithoutMargins(t.heightWithoutMargins), n.setMargins(t.margins), n.setDuration(t.duration), n.setAllowPan(t.allowPan), n.setAllowZoom(t.allowZoom), n.setAllowFocus(t.allowFocus), n.setAllowNodeCentering(t.allowNodeCentering), n.setMinScale(t.minScale), n.setMaxScale(t.maxScale), n.setIsFlatData(t.isFlatData), n.setNodeDepthMultiplier(t.nodeDepthMultiplier), n.loadOnDemandSettings = new o.default(g(n), t.loadOnDemandSettings), n.nodeSettings = new i.default(g(n), t.nodeSettings), n._getId = t.getId, n._getChildren = t.getChildren, n._getParentId = t.getParentId, n
                    }
                    return t = b, (s = [{
                        key: "_nodeEnter",
                        value: function(e, n) {
                            throw "The function _nodeEnter must be implemented"
                        }
                    }, {
                        key: "_nodeUpdate",
                        value: function(e, n, t) {
                            throw "The function _nodeUpdate must be implemented"
                        }
                    }, {
                        key: "_nodeExit",
                        value: function(e, n, t) {
                            throw "The function _nodeExit must be implemented"
                        }
                    }, {
                        key: "_getLinkPathGenerator",
                        value: function() {
                            throw "The function _getLinkPathGenerator must be implemented"
                        }
                    }, {
                        key: "_linkEnter",
                        value: function(e, n, t, a) {
                            throw "The function _linkEnter must be implemented"
                        }
                    }, {
                        key: "_linkUpdate",
                        value: function(e, n, t, a, i) {
                            throw "The function _linkUpdate must be implemented"
                        }
                    }, {
                        key: "_linkExit",
                        value: function(e, n, t, a, i) {
                            throw "The function _linkExit must be implemented"
                        }
                    }, {
                        key: "_getNodeSize",
                        value: function() {
                            throw "The function _getNodeSize must be implemented"
                        }
                    }, {
                        key: "focusToNode",
                        value: function(e) {
                            this.removeSelection(this.getRoot());
                            var n = e;
                            "object" !== r(n) && null !== n && (n = this.getNode(n));
                            var t = null;
                            for (t = n.parent; t;) t._children && this.expand(t), t = t.parent;
                            if (this.getAllowFocus()) {
                                for (t = n.parent; t;) this.hideSiblings(t), t = t.parent;
                                this.updateTreeWithFocusOnNode(n), n.selected = !0
                            }
                            return this.update(this.getRoot()), this.centerNode(n), this
                        }
                    }, {
                        key: "getIsFlatData",
                        value: function() {
                            return this._isFlatData
                        }
                    }, {
                        key: "setIsFlatData",
                        value: function(e) {
                            return this._isFlatData = e, this
                        }
                    }, {
                        key: "regenerateNodeData",
                        value: function() {
                            var e = this;
                            if (this.getIsFlatData()) {
                                if (!this._getParentId) throw "If you are providing flat structured data, then you must set the getParentId accessor property.";
                                var n = a.default.stratify().id((function(n, t, a) {
                                    return e.getId.call(e, n)
                                })).parentId((function(n, t, a) {
                                    return e.getParentId.call(e, n)
                                }));
                                this._root = n(this.getData())
                            } else {
                                if (!this._getChildren) throw "If you are providing hierarchical structured data, then you must set the getChildren accessor property.";
                                this._root = a.default.hierarchy(this.getData(), (function(n) {
                                    return e.getChildren.call(e, n)
                                }))
                            }
                            return this
                        }
                    }, {
                        key: "getTheme",
                        value: function() {
                            return this._theme
                        }
                    }, {
                        key: "setTheme",
                        value: function(e) {
                            return this._theme = e, this
                        }
                    }, {
                        key: "getOrientation",
                        value: function() {
                            return this._orientation
                        }
                    }, {
                        key: "setOrientation",
                        value: function(e) {
                            return this._orientation = e, this
                        }
                    }, {
                        key: "getData",
                        value: function() {
                            return this._data
                        }
                    }, {
                        key: "setData",
                        value: function(e) {
                            return this._data = e, this
                        }
                    }, {
                        key: "getNodeDepthMultiplier",
                        value: function() {
                            return this._nodeDepthMultiplier
                        }
                    }, {
                        key: "setNodeDepthMultiplier",
                        value: function(e) {
                            return this._nodeDepthMultiplier = e, this
                        }
                    }, {
                        key: "getDuration",
                        value: function() {
                            return this._duration
                        }
                    }, {
                        key: "setDuration",
                        value: function(e) {
                            return this._duration = e, this
                        }
                    }, {
                        key: "getAllowPan",
                        value: function() {
                            return this._allowPan
                        }
                    }, {
                        key: "setAllowPan",
                        value: function(e) {
                            return this._allowPan = e, this
                        }
                    }, {
                        key: "getAllowZoom",
                        value: function() {
                            return this._allowZoom
                        }
                    }, {
                        key: "setAllowZoom",
                        value: function(e) {
                            return this._allowZoom = e, this
                        }
                    }, {
                        key: "getAllowFocus",
                        value: function() {
                            return this._allowFocus
                        }
                    }, {
                        key: "setAllowFocus",
                        value: function(e) {
                            return this._allowFocus = e, this
                        }
                    }, {
                        key: "getAllowNodeCentering",
                        value: function() {
                            return this._allowNodeCentering
                        }
                    }, {
                        key: "setAllowNodeCentering",
                        value: function(e) {
                            return this._allowNodeCentering = e, this
                        }
                    }, {
                        key: "getMinScale",
                        value: function() {
                            return this._minScale
                        }
                    }, {
                        key: "setMinScale",
                        value: function(e) {
                            return this._minScale = e, this
                        }
                    }, {
                        key: "getMaxScale",
                        value: function() {
                            return this._maxScale
                        }
                    }, {
                        key: "setMaxScale",
                        value: function(e) {
                            return this._maxScale = e, this
                        }
                    }, {
                        key: "getLoadOnDemandSettings",
                        value: function() {
                            return this.loadOnDemandSettings
                        }
                    }, {
                        key: "getNodeSettings",
                        value: function() {
                            return this.nodeSettings
                        }
                    }, {
                        key: "getElement",
                        value: function() {
                            return this._element
                        }
                    }, {
                        key: "setElement",
                        value: function(e) {
                            return this._element = e, this
                        }
                    }, {
                        key: "getRoot",
                        value: function() {
                            return this._root
                        }
                    }, {
                        key: "getSvg",
                        value: function() {
                            return this._svg
                        }
                    }, {
                        key: "getView",
                        value: function() {
                            return this._view
                        }
                    }, {
                        key: "getPanningContainer",
                        value: function() {
                            return this._panningContainer
                        }
                    }, {
                        key: "getTreeGenerator",
                        value: function() {
                            return this._treeGenerator
                        }
                    }, {
                        key: "getNode",
                        value: function(e) {
                            var n = this,
                                t = e;
                            return "object" === r(t) && null !== t && (t = this.getId(t)),
                                function e(n, t, a) {
                                    if (a(n)) return n;
                                    var i = t(n),
                                        o = i.find(a);
                                    if (!o) {
                                        var s, r = v(i);
                                        try {
                                            for (r.s(); !(s = r.n()).done && !(o = e(s.value, t, a)););
                                        } catch (e) {
                                            r.e(e)
                                        } finally {
                                            r.f()
                                        }
                                    }
                                    return o
                                }(this.getRoot(), (function(e) {
                                    return e._children ? e._children : []
                                }), (function(e) {
                                    return n.getId(e.data) == t
                                }))
                        }
                    }, {
                        key: "getDataItem",
                        value: function(e) {
                            return this.getNode(e).data
                        }
                    }, {
                        key: "getNodes",
                        value: function() {
                            return this._nodes
                        }
                    }, {
                        key: "getVisibleNodes",
                        value: function() {
                            return this._visibleNodes
                        }
                    }, {
                        key: "getLinks",
                        value: function() {
                            return this._links
                        }
                    }, {
                        key: "getZoomListener",
                        value: function() {
                            return this._zoomListener
                        }
                    }, {
                        key: "getId",
                        value: function(e) {
                            return this._getId(e)
                        }
                    }, {
                        key: "getChildren",
                        value: function(e) {
                            return this._getChildren(e)
                        }
                    }, {
                        key: "getParentId",
                        value: function(e) {
                            return this._getParentId(e)
                        }
                    }, {
                        key: "setIdAccessor",
                        value: function(e) {
                            return this._getId = e, this
                        }
                    }, {
                        key: "setChildrenAccessor",
                        value: function(e) {
                            return this._getChildren = e, this
                        }
                    }, {
                        key: "setParentIdAccessor",
                        value: function(e) {
                            return this._getParentId = e, this
                        }
                    }, {
                        key: "getWidth",
                        value: function() {
                            return this.getWidthWithoutMargins() - this.getMargins().left - this.getMargins().right
                        }
                    }, {
                        key: "getHeight",
                        value: function() {
                            return this.getHeightWithoutMargins() - this.getMargins().top - this.getMargins().bottom
                        }
                    }, {
                        key: "setMargins",
                        value: function(e) {
                            return this._margins = e, this
                        }
                    }, {
                        key: "getMargins",
                        value: function() {
                            return this._margins
                        }
                    }, {
                        key: "setWidthWithoutMargins",
                        value: function(e) {
                            return this._widthWithoutMargin = e, this
                        }
                    }, {
                        key: "getWidthWithoutMargins",
                        value: function() {
                            return this._widthWithoutMargin
                        }
                    }, {
                        key: "setHeightWithoutMargins",
                        value: function(e) {
                            return this._heightWithoutMargin = e, this
                        }
                    }, {
                        key: "getHeightWithoutMargins",
                        value: function() {
                            return this._heightWithoutMargin
                        }
                    }, {
                        key: "updateDimensions",
                        value: function() {
                            this.getSvg().attr("viewBox", "0 0 " + this.getWidthWithoutMargins() + " " + this.getHeightWithoutMargins());
                            var e, n, t = this.getMargins(),
                                a = !1,
                                i = this.nodeSettings.getSizingMode();
                            return "string" == typeof i && (i = i.trim().toLowerCase()), "nodesize" === i ? (this.getTreeGenerator().nodeSize(this._getNodeSize()), !1 === this.getAllowFocus() && (a = !0)) : this.getTreeGenerator().size([this.getHeight(), this.getWidth()]), !1 === a ? this.getView().attr("transform", "translate(" + t.left + "," + t.top + ")") : this.getView().attr("transform", "translate(" + t.left + ", " + (this.getHeight() / 2 + t.top) + ")"), "topToBottom" === this.getOrientation() ? (e = !1 === a ? this.getWidth() / 2 : 0, n = this.getHeight() / 4) : (e = !1 === a ? this.getHeight() / 2 : 0, n = 0), this.getRoot().x0 = e, this.getRoot().y0 = n, this.getZoomListener() && this.getZoomListener().extent([
                                [0, 0],
                                [this.getWidthWithoutMargins(), this.getHeightWithoutMargins()]
                            ]), this
                        }
                    }, {
                        key: "validateSettings",
                        value: function() {
                            if (!this.getElement()) throw "Need to pass in an element as part of the options";
                            if (!this.getData()) throw "Need to pass in data as part of the options";
                            if (!this._getId) throw "Need to define the getId function as part of the options";
                            return this.loadOnDemandSettings.validateSettings(), this
                        }
                    }, {
                        key: "initialize",
                        value: function() {
                            var e = this;
                            for (this.validateSettings(), this.regenerateNodeData(); this.getElement().firstChild;) this.getElement().removeChild(this.getElement().firstChild);
                            return this._svg = a.default.select(this.getElement()).append("svg").classed("mitch-d3-tree", !0).classed(this.getTheme(), !0).attr("preserveAspectRatio", "xMidYMid meet").style("width", "100%").style("height", "100%"), this._view = this.getSvg().append("g").classed("view", !0), this._treeGenerator = a.default.tree(), this._panningContainer = this.getView().append("g").classed("panningContainer", !0), this._zoomListener = a.default.zoom().scaleExtent([this.getMinScale(), this.getMaxScale()]).on("zoom", (function() {
                                var n = a.default.event.transform;
                                e.getPanningContainer().attr("transform", n)
                            })), this.getSvg().call(this.getZoomListener()), !1 === this.getAllowPan() && this.getSvg().on("mousedown.zoom", null).on("touchstart.zoom", null).on("touchmove.zoom", null).on("touchend.zoom", null), !1 === this.getAllowZoom() && this.getSvg().on("dblclick.zoom", null).on("wheel.zoom", null), this.updateDimensions(), this._populateUnderlyingChildren(this.getRoot()), this.getRoot().children && this.getRoot().children.forEach(this.collapseRecursively), this.removeSelection(this.getRoot()), this.update(this.getRoot()), this.centerNode(this.getRoot()), this
                        }
                    }, {
                        key: "expand",
                        value: function(e) {
                            return e.children = e._children, this
                        }
                    }, {
                        key: "expandRecursively",
                        value: function(e) {
                            return function e(n) {
                                n.children && (n.children.forEach(e), n.children = n._children)
                            }(e), this
                        }
                    }, {
                        key: "collapse",
                        value: function(e) {
                            return e.children = null, this
                        }
                    }, {
                        key: "collapseRecursively",
                        value: function(e) {
                            return function e(n) {
                                n.children && (n.children.forEach(e), n.children = null)
                            }(e), this
                        }
                    }, {
                        key: "_populateUnderlyingChildren",
                        value: function(e) {
                            return function e(n) {
                                n.children && (n._children = n.children, n._children.forEach(e))
                            }(e), this
                        }
                    }, {
                        key: "removeSelection",
                        value: function(e) {
                            return function e(n) {
                                n.selected = !1, n.children && n.children.forEach(e)
                            }(e), this
                        }
                    }, {
                        key: "centerNode",
                        value: function(e) {
                            var n, t, i, o, s = a.default.zoomTransform(this.getSvg().node()).k;
                            return "toptobottom" === this.getOrientation().toLowerCase() ? (n = -e.x0, t = -e.y0, i = n * s + this.getWidth() / 2, o = t * s + this.getHeight() / 2) : (n = -e.y0, t = -e.x0, i = n * s + this.getWidth() / 4, o = t * s + this.getHeight() / 2), this.getSvg().transition().duration(this.getDuration()).call(this.getZoomListener().transform, a.default.zoomIdentity.translate(i, o).scale(s)), this
                        }
                    }, {
                        key: "_onNodeClick",
                        value: function(e, n, t) {
                            var a = {
                                type: this.getAllowFocus() ? "focus" : e.children ? "collapse" : "expand",
                                continue: !0,
                                nodeDataItem: e,
                                nodeDataItemIndex: n,
                                nodeDataItems: t,
                                preventDefault: function() {
                                    a.continue = !1
                                }
                            };
                            return this.emit("nodeClick", a), !1 !== a.continue && (this.getAllowFocus() ? this.nodeFocus.call(this, e) : this.nodeToggle.call(this, e), !0)
                        }
                    }, {
                        key: "_createNode",
                        value: function(e, n) {
                            var t = a.default.hierarchy(n);
                            return t.depth = e.depth + 1, t.height = e.height - 1, t.parent = e, t.id = this.getId.call(this, n), t
                        }
                    }, {
                        key: "_addUnderlyingChildNode",
                        value: function(e, n) {
                            var t = this._createNode(e, n);
                            return e._children.push(t), t
                        }
                    }, {
                        key: "_processLoadedDataForNodeFocus",
                        value: function(e, n) {
                            var t = this;
                            e._children = [], n.forEach((function(n) {
                                return t._addUnderlyingChildNode(e, n)
                            })), this._populateUnderlyingChildren(e), this.updateTreeWithFocusOnNode(e);
                            var a = e.selected;
                            return this.removeSelection(this.getRoot()), e.selected = !0, this.update(e), !0 !== this.getAllowNodeCentering() || !1 !== a && void 0 !== a || this.centerNode(e), this
                        }
                    }, {
                        key: "nodeFocus",
                        value: function(e) {
                            var n = this;
                            if (!e.children && !e._children && this.loadOnDemandSettings.isEnabled() && this.loadOnDemandSettings.hasChildren(e.data)) this.loadOnDemandSettings.loadChildren(e.data, (function(t) {
                                return n._processLoadedDataForNodeFocus(e, t)
                            }));
                            else {
                                this.updateTreeWithFocusOnNode(e);
                                var t = e.selected;
                                this.removeSelection(this.getRoot()), e.selected = !0, this.update(e), !0 !== this.getAllowNodeCentering() || !1 !== t && void 0 !== t || this.centerNode(e)
                            }
                            return this
                        }
                    }, {
                        key: "_processLoadedDataForNodeToggle",
                        value: function(e, n) {
                            var t = this;
                            return e._children = [], n.forEach((function(n) {
                                return t._addUnderlyingChildNode(e, n)
                            })), this.expand(e), this.update(e), this
                        }
                    }, {
                        key: "nodeToggle",
                        value: function(e) {
                            var n = this;
                            return this.removeSelection(this.getRoot()), e.selected = !0, !e.children && !e._children && this.loadOnDemandSettings.isEnabled() && this.loadOnDemandSettings.hasChildren(e.data) ? this.loadOnDemandSettings.loadChildren(e.data, (function(t) {
                                n._processLoadedDataForNodeToggle(e, t), !0 === n.getAllowNodeCentering() && n.centerNode(e)
                            })) : (e.children ? this.collapse(e) : this.expand(e), this.update(e), !0 === this.getAllowNodeCentering() && this.centerNode(e)), this
                        }
                    }, {
                        key: "hideSiblings",
                        value: function(e) {
                            var n = this,
                                t = e.parent;
                            if (t) {
                                var a = this.getId(e.data);
                                t.children.filter((function(e) {
                                    return n.getId(e.data) != a
                                })).forEach(this.collapseRecursively), t.children = [], t.children.push(e)
                            }
                            return this
                        }
                    }, {
                        key: "updateTreeWithFocusOnNode",
                        value: function(e) {
                            return !e.children && e._children ? (this.hideSiblings(e), this.expand(e), e.children.forEach(this.collapseRecursively)) : e.children && !1 == !e.children.some((function(e, n, t) {
                                return e.children
                            })) && (this.collapseRecursively(e), this.expand(e)), this
                        }
                    }, {
                        key: "_updateNodes",
                        value: function(e, n) {
                            var t = this;
                            n.forEach((function(e) {
                                return e.y = e.depth * t.getNodeDepthMultiplier()
                            }));
                            var a = (n = this.getPanningContainer().selectAll("g.node").data(n, (function(e) {
                                return t.getId.call(t, e.data)
                            }))).enter().append("g").classed("node", !0).attr("transform", (function(n, a, i) {
                                return "toptobottom" === t.getOrientation().toLowerCase() ? "translate(" + e.x0 + "," + e.y0 + ")" : "translate(" + e.y0 + "," + e.x0 + ")"
                            })).on("click", (function(e, n, a) {
                                return t._onNodeClick.call(t, e, n, a)
                            }));
                            this._nodeEnter(a, n);
                            var i = a.merge(n),
                                o = i.transition().duration(this.getDuration());
                            i.classed("collapsed", (function(e, n, a) {
                                return !(e.children || !e._children) || !(!t.loadOnDemandSettings.isEnabled() || !t.loadOnDemandSettings.hasChildren(e.data) || e.children || e._children)
                            })).classed("expanded", (function(e, n, t) {
                                return e.children
                            })).classed("childless", (function(e, n, t) {
                                return !e.children && !e._children
                            })).classed("selected", (function(e, n, t) {
                                return e.selected
                            })), this._nodeUpdate(i, o, n);
                            var s = n.exit(),
                                r = s.transition().duration(this.getDuration());
                            return this._nodeExit(s, r, n), this
                        }
                    }, {
                        key: "_updateLinks",
                        value: function(e, n) {
                            var t = this,
                                a = this._getLinkPathGenerator(),
                                i = this.getPanningContainer().selectAll("path.link").data(n, (function(e) {
                                    return t.getId.call(t, e.data)
                                })),
                                o = i.enter().insert("path", "g").classed("link", !0);
                            this._linkEnter(e, o, i, a);
                            var s = o.merge(i),
                                r = s.transition().duration(this.getDuration());
                            this._linkUpdate(e, s, r, i, a);
                            var u = i.exit(),
                                l = u.transition().duration(this.getDuration());
                            return this._linkExit(e, u, l, i, a), this.getVisibleNodes().forEach((function(e) {
                                e.x0 = e.x, e.y0 = e.y
                            })), this
                        }
                    }, {
                        key: "update",
                        value: function(e) {
                            var n = this.getTreeGenerator()(this.getRoot());
                            return this._visibleNodes = n.descendants(), this._nodes = [this.getRoot()].concat(p(function e(n, t) {
                                var a = [],
                                    i = t(n);
                                if (i) {
                                    var o, s = v(i);
                                    try {
                                        for (s.s(); !(o = s.n()).done;) {
                                            var r = o.value;
                                            a.push(r);
                                            var u = e(r, t);
                                            u && (a = [].concat(p(a), p(u)))
                                        }
                                    } catch (e) {
                                        s.e(e)
                                    } finally {
                                        s.f()
                                    }
                                }
                                return a
                            }(this.getRoot(), (function(e) {
                                return e._children
                            })))), this._links = n.descendants().slice(1), this._updateNodes(e, this.getVisibleNodes())._updateLinks(e, this.getLinks()), this
                        }
                    }]) && c(t.prototype, s), y && c(t, y), b
                }(t.n(s).a);
                b.defaults = {
                    theme: "default",
                    orientation: "leftToRight",
                    allowPan: !0,
                    allowZoom: !0,
                    allowFocus: !0,
                    allowNodeCentering: !0,
                    minScale: 1,
                    maxScale: 2,
                    nodeDepthMultiplier: 300,
                    isFlatData: !1,
                    getId: null,
                    getParentId: null,
                    getChildren: null,
                    widthWithoutMargins: 960,
                    heightWithoutMargins: 800,
                    margins: {
                        top: 40,
                        right: 20,
                        bottom: 40,
                        left: 100
                    },
                    duration: 750,
                    loadOnDemandSettings: {},
                    nodeSettings: {}
                }, n.default = b
            },
        "./src/js/BoxedNodeSettings.js":
            /*!*************************************!*\
              !*** ./src/js/BoxedNodeSettings.js ***!
              \*************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return (a = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function i(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function o(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function s(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function r(e, n) {
                    return !n || "object" !== a(n) && "function" != typeof n ? function(e) {
                        if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                        return e
                    }(e) : n
                }

                function u() {
                    if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                    if (Reflect.construct.sham) return !1;
                    if ("function" == typeof Proxy) return !0;
                    try {
                        return Date.prototype.toString.call(Reflect.construct(Date, [], (function() {}))), !0
                    } catch (e) {
                        return !1
                    }
                }

                function l(e) {
                    return (l = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function c(e, n) {
                    return (c = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }
                t.r(n);
                var d = function(e) {
                    ! function(e, n) {
                        if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                        e.prototype = Object.create(n && n.prototype, {
                            constructor: {
                                value: e,
                                writable: !0,
                                configurable: !0
                            }
                        }), n && c(e, n)
                    }(h, e);
                    var n, t, a, d, g = (n = h, function() {
                        var e, t = l(n);
                        if (u()) {
                            var a = l(this).constructor;
                            e = Reflect.construct(t, arguments, a)
                        } else e = t.apply(this, arguments);
                        return r(this, e)
                    });

                    function h(e, n) {
                        var t;
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, h), t = g.call(this, e, n);
                        var a = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? i(Object(t), !0).forEach((function(n) {
                                    o(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : i(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, h.defaults, {}, n);
                        return t._bodyBoxWidth = a.bodyBoxWidth, t._bodyBoxHeight = a.bodyBoxHeight, t._bodyBoxPadding = a.bodyBoxPadding, t._titleBoxWidth = a.titleBoxWidth, t._titleBoxHeight = a.titleBoxHeight, t._titleBoxPadding = a.titleBoxPadding, t
                    }
                    return t = h, (a = [{
                        key: "getBodyBoxWidth",
                        value: function() {
                            return this._bodyBoxWidth
                        }
                    }, {
                        key: "setBodyBoxWidth",
                        value: function(e) {
                            return this._bodyBoxWidth = e, this
                        }
                    }, {
                        key: "getBodyBoxHeight",
                        value: function() {
                            return this._bodyBoxHeight
                        }
                    }, {
                        key: "setBodyBoxHeight",
                        value: function(e) {
                            return this._bodyBoxHeight = e, this
                        }
                    }, {
                        key: "setBodyBoxPadding",
                        value: function(e) {
                            return this._bodyBoxPadding = e, this
                        }
                    }, {
                        key: "getBodyBoxPadding",
                        value: function() {
                            return this._bodyBoxPadding
                        }
                    }, {
                        key: "getTitleBoxWidth",
                        value: function() {
                            return this._titleBoxWidth ? this._titleBoxWidth : this.getBodyBoxWidth() / 2
                        }
                    }, {
                        key: "setTitleBoxWidth",
                        value: function(e) {
                            return this._titleBoxWidth = e, this
                        }
                    }, {
                        key: "getTitleBoxHeight",
                        value: function() {
                            return this._titleBoxHeight
                        }
                    }, {
                        key: "setTitleBoxHeight",
                        value: function(e) {
                            return this._titleBoxHeight = e, this
                        }
                    }, {
                        key: "getTitleBoxPadding",
                        value: function() {
                            return this._titleBoxPadding
                        }
                    }, {
                        key: "setTitleBoxPadding",
                        value: function(e) {
                            return this._titleBoxPadding = e, this
                        }
                    }]) && s(t.prototype, a), d && s(t, d), h
                }(t( /*! ./NodeSettings */ "./src/js/NodeSettings.js").default);
                d.defaults = {
                    bodyBoxWidth: 200,
                    bodyBoxHeight: 75,
                    bodyBoxPadding: {
                        top: 5,
                        right: 10,
                        bottom: 5,
                        left: 10
                    },
                    titleBoxWidth: null,
                    titleBoxHeight: 40,
                    titleBoxPadding: {
                        top: 2,
                        right: 5,
                        bottom: 2,
                        left: 5
                    }
                }, n.default = d
            },
        "./src/js/BoxedTree.js":
            /*!*****************************!*\
              !*** ./src/js/BoxedTree.js ***!
              \*****************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./CustomD3 */ "./src/js/CustomD3.js"),
                    i = t( /*! d3plus-text */ "./node_modules/d3plus-text/es/index.js"),
                    o = t( /*! ./BaseTree */ "./src/js/BaseTree.js"),
                    s = t( /*! ./BoxedNodeSettings */ "./src/js/BoxedNodeSettings.js");

                function r(e) {
                    return (r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function u(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function l(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function c(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function d(e, n, t) {
                    return (d = "undefined" != typeof Reflect && Reflect.get ? Reflect.get : function(e, n, t) {
                        var a = function(e, n) {
                            for (; !Object.prototype.hasOwnProperty.call(e, n) && null !== (e = m(e)););
                            return e
                        }(e, n);
                        if (a) {
                            var i = Object.getOwnPropertyDescriptor(a, n);
                            return i.get ? i.get.call(t) : i.value
                        }
                    })(e, n, t || e)
                }

                function g(e, n) {
                    return !n || "object" !== r(n) && "function" != typeof n ? h(e) : n
                }

                function h(e) {
                    if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return e
                }

                function f() {
                    if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                    if (Reflect.construct.sham) return !1;
                    if ("function" == typeof Proxy) return !0;
                    try {
                        return Date.prototype.toString.call(Reflect.construct(Date, [], (function() {}))), !0
                    } catch (e) {
                        return !1
                    }
                }

                function m(e) {
                    return (m = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function p(e, n) {
                    return (p = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }
                var v = function(e) {
                    ! function(e, n) {
                        if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                        e.prototype = Object.create(n && n.prototype, {
                            constructor: {
                                value: e,
                                writable: !0,
                                configurable: !0
                            }
                        }), n && p(e, n)
                    }(_, e);
                    var n, t, r, v, y = (n = _, function() {
                        var e, t = m(n);
                        if (f()) {
                            var a = m(this).constructor;
                            e = Reflect.construct(t, arguments, a)
                        } else e = t.apply(this, arguments);
                        return g(this, e)
                    });

                    function _(e) {
                        var n;
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, _), n = y.call(this, e);
                        var t = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? u(Object(t), !0).forEach((function(n) {
                                    l(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : u(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, o.default.defaults, {}, _.defaults, {}, e);
                        return n._getBodyDisplayText = t.getBodyDisplayText, n._getTitleDisplayText = t.getTitleDisplayText, n.nodeSettings = new s.default(h(n), t.nodeSettings), n
                    }
                    return t = _, (r = [{
                        key: "initialize",
                        value: function() {
                            return d(m(_.prototype), "initialize", this).call(this), this.getSvg().classed("boxed-tree", !0), this
                        }
                    }, {
                        key: "_nodeEnter",
                        value: function(e, n) {
                            var t = this,
                                o = t.nodeSettings.getBodyBoxWidth(),
                                s = t.nodeSettings.getBodyBoxHeight(),
                                r = t.nodeSettings.getBodyBoxPadding(),
                                u = t.nodeSettings.getTitleBoxWidth(),
                                l = t.nodeSettings.getTitleBoxHeight(),
                                c = t.nodeSettings.getTitleBoxPadding(),
                                d = e.append("g").classed("body-group", !0);
                            return d.append("rect").classed("body-box", !0).attr("width", 1e-6).attr("height", 1e-6), d.each((function(e, n, u) {
                                var c = a.default.select(this),
                                    d = [];
                                d.push(e);
                                var g = r.top;
                                t.getTitleDisplayText.call(t, e) && (g += l / 2), (new i.TextBox).select(this).data(d).text((function(e, n, a) {
                                    return t.getBodyDisplayText.call(t, e)
                                })).textAnchor("middle").verticalAlign("middle").fontSize(13).x(r.left).y(g - s / 2).width(o - r.left - r.right).height(s - g - r.bottom).ellipsis((function(n, a) {
                                    return c.append("title").text(t.getBodyDisplayText(e)), n.replace(/\.|,$/g, "") + "..."
                                })).render()
                            })), e.append("g").classed("title-group", !0).attr("transform", "translate(" + -u / 3 + ", " + (-l / 2 - s / 2) + ")").each((function(e, n, o) {
                                if (t.getTitleDisplayText.call(t, e)) {
                                    var s = a.default.select(this),
                                        r = [];
                                    r.push(e), s.append("rect").classed("title-box", !0).attr("width", u).attr("height", l), (new i.TextBox).select(this).data(r).text((function(e, n, a) {
                                        return t.getTitleDisplayText.call(t, e)
                                    })).textAnchor("middle").verticalAlign("middle").x(c.left).y(c.top).fontWeight(700).fontMin(6).fontMax(16).fontResize(!0).width(u - c.left - c.right).height(l - c.top - c.bottom).render()
                                }
                            })), t
                        }
                    }, {
                        key: "_nodeUpdate",
                        value: function(e, n, t) {
                            "toptobottom" === this.getOrientation().toLowerCase() ? n.attr("transform", (function(e, n, t) {
                                return "translate(" + e.x + "," + e.y + ")"
                            })) : n.attr("transform", (function(e, n, t) {
                                return "translate(" + e.y + "," + e.x + ")"
                            }));
                            var a = this.nodeSettings.getBodyBoxWidth(),
                                i = this.nodeSettings.getBodyBoxHeight();
                            return e.select(".node .body-group .body-box").attr("y", -i / 2).attr("width", a).attr("height", i), e.select(".d3plus-textBox").style("fill-opacity", 1), this
                        }
                    }, {
                        key: "_nodeExit",
                        value: function(e, n, t) {
                            var a = this,
                                i = this.nodeSettings.getBodyBoxWidth(),
                                o = this.nodeSettings.getBodyBoxHeight();
                            return n.attr("transform", (function(e, n, t) {
                                for (var s = e.parent; s.parent && !s.parent.children;) s = s.parent;
                                return "toptobottom" === a.getOrientation().toLowerCase() ? "translate(" + (s.x + i / 2) + "," + (s.y + o) + ")" : "translate(" + (s.y + i) + "," + (s.x + o / 2) + ")"
                            })).remove(), n.select(".node .body-group rect").attr("width", 1e-6).attr("height", 1e-6), n.select(".node .body-group .d3plus-textBox").style("fill-opacity", 1e-6).attr("transform", (function(e, n, t) {
                                return "translate(0," + -o / 2 + ")"
                            })).selectAll("text").style("font-size", 0).attr("y", "0px").attr("x", "0px"), n.select(".node .title-group").attr("transform", "translate(0, " + -o / 2 + ")"), n.select(".node .title-group rect").attr("width", 1e-6).attr("height", 1e-6), n.select(".node .title-group .d3plus-textBox").style("fill-opacity", 1e-6).attr("transform", "translate(0,0)").selectAll("text").style("font-size", 0).attr("y", "0px").attr("x", "0px"), n.select(".d3plus-textBox").style("fill-opacity", 1e-6), this
                        }
                    }, {
                        key: "_getNodeSize",
                        value: function() {
                            return "toptobottom" === this.getOrientation().toLowerCase() ? [this.nodeSettings.getBodyBoxWidth() + this.nodeSettings.getHorizontalSpacing(), this.nodeSettings.getBodyBoxHeight() + this.nodeSettings.getVerticalSpacing()] : [this.nodeSettings.getBodyBoxHeight() + this.nodeSettings.getVerticalSpacing(), this.nodeSettings.getBodyBoxWidth() + this.nodeSettings.getHorizontalSpacing()]
                        }
                    }, {
                        key: "_linkEnter",
                        value: function(e, n, t, a) {
                            return n.attr("d", (function(n, t, i) {
                                var o = {
                                    x: e.x0,
                                    y: e.y0
                                };
                                return a({
                                    source: o,
                                    target: o
                                })
                            })), this
                        }
                    }, {
                        key: "_linkUpdate",
                        value: function(e, n, t, a, i) {
                            return t.attr("d", (function(e, n, t) {
                                var a = e,
                                    o = e.parent;
                                return i({
                                    source: a,
                                    target: o
                                })
                            })), this
                        }
                    }, {
                        key: "_linkExit",
                        value: function(e, n, t, a, i) {
                            var o = this;
                            return t.attr("d", (function(e, n, t) {
                                for (var a = e.parent; a.parent && !a.parent.children;) a = a.parent;
                                var s = null;
                                if ("toptobottom" === o.getOrientation().toLowerCase()) {
                                    var r = o.nodeSettings.getBodyBoxHeight();
                                    s = {
                                        x: a.x,
                                        y: a.y + r
                                    }
                                } else {
                                    var u = o.nodeSettings.getBodyBoxWidth();
                                    s = {
                                        x: a.x,
                                        y: a.y + u
                                    }
                                }
                                var l = {
                                    x: a.x,
                                    y: a.y
                                };
                                return i({
                                    source: s,
                                    target: l
                                })
                            })), this
                        }
                    }, {
                        key: "_getLinkPathGenerator",
                        value: function() {
                            var e = this.nodeSettings.getBodyBoxWidth(),
                                n = this.nodeSettings.getBodyBoxHeight();
                            return "toptobottom" === this.getOrientation().toLowerCase() ? a.default.linkVertical().source((function(t) {
                                return [t.source.x + e / 2, t.source.y - n / 2]
                            })).target((function(t) {
                                return [t.target.x + e / 2, t.target.y + n / 2]
                            })) : a.default.linkHorizontal().source((function(e) {
                                return [e.source.y, e.source.x]
                            })).target((function(n) {
                                return [n.target.y + e, n.target.x]
                            }))
                        }
                    }, {
                        key: "validateSettings",
                        value: function() {
                            if (d(m(_.prototype), "validateSettings", this).call(this), !this._getBodyDisplayText) throw "Need to define the getBodyDisplayText function as part of the options";
                            return this
                        }
                    }, {
                        key: "setBodyDisplayTextAccessor",
                        value: function(e) {
                            return this._getBodyDisplayText = e, this
                        }
                    }, {
                        key: "getBodyDisplayText",
                        value: function(e) {
                            return this._getBodyDisplayText(e.data)
                        }
                    }, {
                        key: "setTitleDisplayTextAccessor",
                        value: function(e) {
                            return this._getTitleDisplayText = e, this
                        }
                    }, {
                        key: "getTitleDisplayText",
                        value: function(e) {
                            return this._getTitleDisplayText(e.data)
                        }
                    }, {
                        key: "centerNode",
                        value: function(e) {
                            var n = this.nodeSettings.getBodyBoxWidth(),
                                t = this.nodeSettings.getBodyBoxHeight();
                            return "toptobottom" === this.getOrientation().toLowerCase() ? (e.x0 = e.x0, e.y0 = e.y0 + t / 2) : (e.y0 = e.y0 + n / 2, e.x0 = e.x0), d(m(_.prototype), "centerNode", this).call(this, e)
                        }
                    }]) && c(t.prototype, r), v && c(t, v), _
                }(o.default);
                v.defaults = {
                    getBodyDisplayText: null,
                    getTitleDisplayText: function(e) {
                        return null
                    }
                }, n.default = v
            },
        "./src/js/CircleNodeSettings.js":
            /*!**************************************!*\
              !*** ./src/js/CircleNodeSettings.js ***!
              \**************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e) {
                    return (a = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function i(e, n) {
                    if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                }

                function o(e, n) {
                    return !n || "object" !== a(n) && "function" != typeof n ? function(e) {
                        if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                        return e
                    }(e) : n
                }

                function s() {
                    if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                    if (Reflect.construct.sham) return !1;
                    if ("function" == typeof Proxy) return !0;
                    try {
                        return Date.prototype.toString.call(Reflect.construct(Date, [], (function() {}))), !0
                    } catch (e) {
                        return !1
                    }
                }

                function r(e) {
                    return (r = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function u(e, n) {
                    return (u = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }
                t.r(n);
                var l = function(e) {
                    ! function(e, n) {
                        if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                        e.prototype = Object.create(n && n.prototype, {
                            constructor: {
                                value: e,
                                writable: !0,
                                configurable: !0
                            }
                        }), n && u(e, n)
                    }(a, e);
                    var n, t = (n = a, function() {
                        var e, t = r(n);
                        if (s()) {
                            var a = r(this).constructor;
                            e = Reflect.construct(t, arguments, a)
                        } else e = t.apply(this, arguments);
                        return o(this, e)
                    });

                    function a() {
                        return i(this, a), t.apply(this, arguments)
                    }
                    return a
                }(t( /*! ./NodeSettings */ "./src/js/NodeSettings.js").default);
                n.default = l
            },
        "./src/js/CircleTree.js":
            /*!******************************!*\
              !*** ./src/js/CircleTree.js ***!
              \******************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! ./CustomD3 */ "./src/js/CustomD3.js"),
                    i = t( /*! ./BaseTree */ "./src/js/BaseTree.js"),
                    o = t( /*! ./CircleNodeSettings */ "./src/js/CircleNodeSettings.js");

                function s(e) {
                    return (s = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(e) {
                        return typeof e
                    } : function(e) {
                        return e && "function" == typeof Symbol && e.constructor === Symbol && e !== Symbol.prototype ? "symbol" : typeof e
                    })(e)
                }

                function r(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function u(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function l(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }

                function c(e, n, t) {
                    return (c = "undefined" != typeof Reflect && Reflect.get ? Reflect.get : function(e, n, t) {
                        var a = function(e, n) {
                            for (; !Object.prototype.hasOwnProperty.call(e, n) && null !== (e = f(e)););
                            return e
                        }(e, n);
                        if (a) {
                            var i = Object.getOwnPropertyDescriptor(a, n);
                            return i.get ? i.get.call(t) : i.value
                        }
                    })(e, n, t || e)
                }

                function d(e, n) {
                    return !n || "object" !== s(n) && "function" != typeof n ? g(e) : n
                }

                function g(e) {
                    if (void 0 === e) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return e
                }

                function h() {
                    if ("undefined" == typeof Reflect || !Reflect.construct) return !1;
                    if (Reflect.construct.sham) return !1;
                    if ("function" == typeof Proxy) return !0;
                    try {
                        return Date.prototype.toString.call(Reflect.construct(Date, [], (function() {}))), !0
                    } catch (e) {
                        return !1
                    }
                }

                function f(e) {
                    return (f = Object.setPrototypeOf ? Object.getPrototypeOf : function(e) {
                        return e.__proto__ || Object.getPrototypeOf(e)
                    })(e)
                }

                function m(e, n) {
                    return (m = Object.setPrototypeOf || function(e, n) {
                        return e.__proto__ = n, e
                    })(e, n)
                }
                var p = function(e) {
                    ! function(e, n) {
                        if ("function" != typeof n && null !== n) throw new TypeError("Super expression must either be null or a function");
                        e.prototype = Object.create(n && n.prototype, {
                            constructor: {
                                value: e,
                                writable: !0,
                                configurable: !0
                            }
                        }), n && m(e, n)
                    }(y, e);
                    var n, t, s, p, v = (n = y, function() {
                        var e, t = f(n);
                        if (h()) {
                            var a = f(this).constructor;
                            e = Reflect.construct(t, arguments, a)
                        } else e = t.apply(this, arguments);
                        return d(this, e)
                    });

                    function y(e) {
                        var n;
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, y), n = v.call(this, e);
                        var t = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? r(Object(t), !0).forEach((function(n) {
                                    u(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : r(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, i.default.defaults, {}, y.defaults, {}, e);
                        return n._getDisplayText = t.getDisplayText, n.nodeSettings = new o.default(g(n), t.nodeSettings), n
                    }
                    return t = y, (s = [{
                        key: "initialize",
                        value: function() {
                            return c(f(y.prototype), "initialize", this).call(this), this.getSvg().classed("circle-tree", !0), this
                        }
                    }, {
                        key: "_nodeEnter",
                        value: function(e, n) {
                            var t = this;
                            return e.append("circle").attr("r", "0.5em"), e.append("text").text((function(e, n, a) {
                                return t.getDisplayText.call(t, e)
                            })), this
                        }
                    }, {
                        key: "_nodeUpdate",
                        value: function(e, n, t) {
                            return e.classed("middle", (function(e, n, t) {
                                var a = !1;
                                if (e.parent && e.parent.children.length % 2 != 0) {
                                    var i = e.parent.children;
                                    i.indexOf(e) === Math.floor(i.length / 2) && (a = !0)
                                }
                                return a
                            })), "toptobottom" === this.getOrientation().toLowerCase() ? n.attr("transform", (function(e, n, t) {
                                return "translate(" + e.x + "," + e.y + ")"
                            })) : n.attr("transform", (function(e, n, t) {
                                return "translate(" + e.y + "," + e.x + ")"
                            })), e.select("text").style("fill-opacity", 1), this
                        }
                    }, {
                        key: "_nodeExit",
                        value: function(e, n, t) {
                            var a = this;
                            return n.attr("transform", (function(e, n, t) {
                                for (var i = e.parent; i.parent && !i.parent.children;) i = i.parent;
                                return "toptobottom" === a.getOrientation().toLowerCase() ? "translate(" + i.x + "," + i.y + ")" : "translate(" + i.y + "," + i.x + ")"
                            })).remove(), n.select("circle").attr("r", "0.000001em"), n.select("text").style("fill-opacity", 1e-6), this
                        }
                    }, {
                        key: "_getNodeSize",
                        value: function() {
                            return [this.nodeSettings.getVerticalSpacing(), this.nodeSettings.getHorizontalSpacing()]
                        }
                    }, {
                        key: "_linkEnter",
                        value: function(e, n, t, a) {
                            return n.attr("d", (function(n, t, i) {
                                var o = {
                                    x: e.x0,
                                    y: e.y0
                                };
                                return a({
                                    source: o,
                                    target: o
                                })
                            })), this
                        }
                    }, {
                        key: "_linkUpdate",
                        value: function(e, n, t, a, i) {
                            return t.attr("d", (function(e, n, t) {
                                var a = e,
                                    o = e.parent;
                                return i({
                                    source: a,
                                    target: o
                                })
                            })), this
                        }
                    }, {
                        key: "_linkExit",
                        value: function(e, n, t, a, i) {
                            return t.attr("d", (function(e, n, t) {
                                for (var a = e.parent; a.parent && !a.parent.children;) a = a.parent;
                                var o = {
                                        x: a.x,
                                        y: a.y
                                    },
                                    s = {
                                        x: a.x,
                                        y: a.y
                                    };
                                return i({
                                    source: o,
                                    target: s
                                })
                            })), this
                        }
                    }, {
                        key: "_getLinkPathGenerator",
                        value: function() {
                            return "toptobottom" === this.getOrientation().toLowerCase() ? a.default.linkVertical().source((function(e) {
                                return [e.source.x, e.source.y]
                            })).target((function(e) {
                                return [e.target.x, e.target.y]
                            })) : a.default.linkHorizontal().source((function(e) {
                                return [e.source.y, e.source.x]
                            })).target((function(e) {
                                return [e.target.y, e.target.x]
                            }))
                        }
                    }, {
                        key: "validateSettings",
                        value: function() {
                            if (c(f(y.prototype), "validateSettings", this).call(this), !this.getDisplayText) throw "Need to define the getDisplayText function as part of the options";
                            return this
                        }
                    }, {
                        key: "setDisplayTextAccessor",
                        value: function(e) {
                            return this._getDisplayText = e, this
                        }
                    }, {
                        key: "getDisplayText",
                        value: function(e) {
                            return this._getDisplayText(e.data)
                        }
                    }]) && l(t.prototype, s), p && l(t, p), y
                }(i.default);
                p.defaults = {
                    getDisplayText: function(e) {
                        return null
                    }
                }, n.default = p
            },
        "./src/js/CustomD3.js":
            /*!****************************!*\
              !*** ./src/js/CustomD3.js ***!
              \****************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";
                t.r(n);
                var a = t( /*! d3-selection */ "./node_modules/d3-selection/src/index.js"),
                    i = t( /*! d3-hierarchy */ "./node_modules/d3-hierarchy/src/index.js"),
                    o = t( /*! d3-zoom */ "./node_modules/d3-zoom/src/index.js"),
                    s = t( /*! d3-shape */ "./node_modules/d3-shape/src/index.js");

                function r(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function u(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }
                var l = function(e) {
                    for (var n = 1; n < arguments.length; n++) {
                        var t = null != arguments[n] ? arguments[n] : {};
                        n % 2 ? r(Object(t), !0).forEach((function(n) {
                            u(e, n, t[n])
                        })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : r(Object(t)).forEach((function(n) {
                            Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                        }))
                    }
                    return e
                }({
                    select: a.select,
                    selectAll: a.selectAll,
                    get event() {
                        return a.event
                    },
                    linkHorizontal: s.linkHorizontal,
                    linkVertical: s.linkVertical
                }, i, {}, o);
                n.default = l
            },
        "./src/js/LoadOnDemandSettings.js":
            /*!****************************************!*\
              !*** ./src/js/LoadOnDemandSettings.js ***!
              \****************************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function i(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function o(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }
                t.r(n);
                var s = function() {
                    function e(n, t) {
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, e);
                        var o = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? a(Object(t), !0).forEach((function(n) {
                                    i(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : a(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, e.defaults, {}, t);
                        this._ownerObject = n, this._hasChildren = o.hasChildren, this._loadChildren = o.loadChildren
                    }
                    var n, t, s;
                    return n = e, (t = [{
                        key: "back",
                        value: function() {
                            return this._ownerObject
                        }
                    }, {
                        key: "validateSettings",
                        value: function() {
                            if (!this.hasChildren && this.loadChildren) throw "With load on demand enabled, you need to define hasChildren as part of the options";
                            if (!this.loadChildren && this.hasChildren) throw "With load on demand enabled, you need to define loadChildren as part of the options";
                            return this
                        }
                    }, {
                        key: "loadChildren",
                        value: function(e, n) {
                            return this._loadChildren.call(this._ownerObject, e, n)
                        }
                    }, {
                        key: "hasChildren",
                        value: function(e) {
                            return this._hasChildren.call(this._ownerObject, e)
                        }
                    }, {
                        key: "setLoadChildrenMethod",
                        value: function(e) {
                            return this._loadChildren = e, this
                        }
                    }, {
                        key: "setHasChildrenMethod",
                        value: function(e) {
                            return this._hasChildren = e, this
                        }
                    }, {
                        key: "isEnabled",
                        value: function() {
                            return this._hasChildren || this._loadChildren
                        }
                    }]) && o(n.prototype, t), s && o(n, s), e
                }();
                s.defaults = {
                    hasChildren: null,
                    loadChildren: null
                }, n.default = s
            },
        "./src/js/NodeSettings.js":
            /*!********************************!*\
              !*** ./src/js/NodeSettings.js ***!
              \********************************/
            /*! exports provided: default */
            function(e, n, t) {
                "use strict";

                function a(e, n) {
                    var t = Object.keys(e);
                    if (Object.getOwnPropertySymbols) {
                        var a = Object.getOwnPropertySymbols(e);
                        n && (a = a.filter((function(n) {
                            return Object.getOwnPropertyDescriptor(e, n).enumerable
                        }))), t.push.apply(t, a)
                    }
                    return t
                }

                function i(e, n, t) {
                    return n in e ? Object.defineProperty(e, n, {
                        value: t,
                        enumerable: !0,
                        configurable: !0,
                        writable: !0
                    }) : e[n] = t, e
                }

                function o(e, n) {
                    for (var t = 0; t < n.length; t++) {
                        var a = n[t];
                        a.enumerable = a.enumerable || !1, a.configurable = !0, "value" in a && (a.writable = !0), Object.defineProperty(e, a.key, a)
                    }
                }
                t.r(n);
                var s = function() {
                    function e(n, t) {
                        ! function(e, n) {
                            if (!(e instanceof n)) throw new TypeError("Cannot call a class as a function")
                        }(this, e);
                        var o = function(e) {
                            for (var n = 1; n < arguments.length; n++) {
                                var t = null != arguments[n] ? arguments[n] : {};
                                n % 2 ? a(Object(t), !0).forEach((function(n) {
                                    i(e, n, t[n])
                                })) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : a(Object(t)).forEach((function(n) {
                                    Object.defineProperty(e, n, Object.getOwnPropertyDescriptor(t, n))
                                }))
                            }
                            return e
                        }({}, e.defaults, {}, t);
                        this._ownerObject = n, this._sizingMode = o.sizingMode, this._horizontalSpacing = o.horizontalSpacing, this._verticalSpacing = o.verticalSpacing
                    }
                    var n, t, s;
                    return n = e, (t = [{
                        key: "back",
                        value: function() {
                            return this._ownerObject
                        }
                    }, {
                        key: "getHorizontalSpacing",
                        value: function() {
                            return this._horizontalSpacing
                        }
                    }, {
                        key: "setHorizontalSpacing",
                        value: function(e) {
                            return this._horizontalSpacing = e, this
                        }
                    }, {
                        key: "getVerticalSpacing",
                        value: function() {
                            return this._verticalSpacing
                        }
                    }, {
                        key: "setVerticalSpacing",
                        value: function(e) {
                            return this._verticalSpacing = e, this
                        }
                    }, {
                        key: "getSizingMode",
                        value: function() {
                            return this._sizingMode
                        }
                    }, {
                        key: "setSizingMode",
                        value: function(e) {
                            return this._sizingMode = e, this
                        }
                    }]) && o(n.prototype, t), s && o(n, s), e
                }();
                s.defaults = {
                    sizingMode: "size",
                    horizontalSpacing: 25,
                    verticalSpacing: 25
                }, n.default = s
            },
        events:
            /*!*************************!*\
              !*** external "events" ***!
              \*************************/
            /*! no static exports found */
            function(e, n) {
                e.exports = require("events")
            }
    }).default
}));
