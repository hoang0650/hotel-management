/**
 * @version: 1.3.17
 * @author: Dan Grossman http://www.dangrossman.info/
 * @date: 2014-11-25
 * @copyright: Copyright (c) 2012-2014 Dan Grossman. All rights reserved.
 * @license: Licensed under the MIT license. See http://www.opensource.org/licenses/mit-license.php
 * @website: http://www.improvely.com/
 */
! function (a, b) {
    if ("function" == typeof define && define.amd) define(["moment", "jquery", "exports"], function (c, d, e) {
        a.daterangepicker = b(a, e, c, d)
    });
    else if ("undefined" != typeof exports) {
        var c, d = require("moment");
        try {
            c = require("jquery")
        } catch (e) {
            if (c = window.jQuery, !c) throw new Error("jQuery dependency not found")
        }
        b(a, exports, d, c)
    } else a.daterangepicker = b(a, {}, a.moment, a.jQuery || a.Zepto || a.ender || a.$)
}(this, function (a, b, c, d) {
    var e = function (a, b, c) {
        this.parentEl = "body", this.element = d(a), this.isShowing = !1;
        var e = '<div class="daterangepicker dropdown-menu"><div class="calendar first left"></div><div class="calendar second right"></div><div class="ranges"><div class="range_inputs"><div class="daterangepicker_start_input"><label for="daterangepicker_start"></label><input class="input-mini" type="text" name="daterangepicker_start" value="" /></div><div class="daterangepicker_end_input"><label for="daterangepicker_end"></label><input class="input-mini" type="text" name="daterangepicker_end" value="" /></div><button class="applyBtn" disabled="disabled"></button>&nbsp;<button class="cancelBtn"></button></div></div></div>';
        ("object" != typeof b || null === b) && (b = {}), this.parentEl = d("object" == typeof b && b.parentEl && d(b.parentEl).length ? b.parentEl : this.parentEl), this.container = d(e).appendTo(this.parentEl), this.setOptions(b, c);
        var f = this.container;
        d.each(this.buttonClasses, function (a, b) {
            f.find("button").addClass(b)
        }), this.container.find(".daterangepicker_start_input label").html(this.locale.fromLabel), this.container.find(".daterangepicker_end_input label").html(this.locale.toLabel), this.applyClass.length && this.container.find(".applyBtn").addClass(this.applyClass), this.cancelClass.length && this.container.find(".cancelBtn").addClass(this.cancelClass), this.container.find(".applyBtn").html(this.locale.applyLabel), this.container.find(".cancelBtn").html(this.locale.cancelLabel), this.container.find(".calendar").on("click.daterangepicker", ".prev", d.proxy(this.clickPrev, this)).on("click.daterangepicker", ".next", d.proxy(this.clickNext, this)).on("click.daterangepicker", "td.available", d.proxy(this.clickDate, this)).on("mouseenter.daterangepicker", "td.available", d.proxy(this.hoverDate, this)).on("mouseleave.daterangepicker", "td.available", d.proxy(this.updateFormInputs, this)).on("change.daterangepicker", "select.yearselect", d.proxy(this.updateMonthYear, this)).on("change.daterangepicker", "select.monthselect", d.proxy(this.updateMonthYear, this)).on("change.daterangepicker", "select.hourselect,select.minuteselect,select.secondselect,select.ampmselect", d.proxy(this.updateTime, this)), this.container.find(".ranges").on("click.daterangepicker", "button.applyBtn", d.proxy(this.clickApply, this)).on("click.daterangepicker", "button.cancelBtn", d.proxy(this.clickCancel, this)).on("click.daterangepicker", ".daterangepicker_start_input,.daterangepicker_end_input", d.proxy(this.showCalendars, this)).on("change.daterangepicker", ".daterangepicker_start_input,.daterangepicker_end_input", d.proxy(this.inputsChanged, this)).on("keydown.daterangepicker", ".daterangepicker_start_input,.daterangepicker_end_input", d.proxy(this.inputsKeydown, this)).on("click.daterangepicker", "li", d.proxy(this.clickRange, this)).on("mouseenter.daterangepicker", "li", d.proxy(this.enterRange, this)).on("mouseleave.daterangepicker", "li", d.proxy(this.updateFormInputs, this)), this.element.is("input") ? this.element.on({
            "click.daterangepicker": d.proxy(this.show, this),
            "focus.daterangepicker": d.proxy(this.show, this),
            "keyup.daterangepicker": d.proxy(this.updateFromControl, this)
        }) : this.element.on("click.daterangepicker", d.proxy(this.toggle, this))
    };
    e.prototype = {
        constructor: e,
        setOptions: function (a, b) {
            if (this.startDate = c().startOf("day"), this.endDate = c().endOf("day"), this.timeZone = c().zone(), this.minDate = !1, this.maxDate = !1, this.dateLimit = !1, this.showDropdowns = !1, this.showWeekNumbers = !1, this.timePicker = !1, this.timePickerSeconds = !1, this.timePickerIncrement = 30, this.timePicker12Hour = !0, this.singleDatePicker = !1, this.ranges = {}, this.opens = "right", this.element.hasClass("pull-right") && (this.opens = "left"), this.buttonClasses = ["btn", "btn-small btn-sm"], this.applyClass = "btn-success", this.cancelClass = "btn-default", this.format = "MM/DD/YYYY", this.separator = " - ", this.locale = {
                applyLabel: "Apply",
                cancelLabel: "Cancel",
                fromLabel: "From",
                toLabel: "To",
                weekLabel: "W",
                customRangeLabel: "Custom Range",
                daysOfWeek: c.weekdaysMin(),
                monthNames: c.monthsShort(),
                firstDay: c.localeData()._week.dow
            }, this.cb = function () { }, "string" == typeof a.format && (this.format = a.format), "string" == typeof a.separator && (this.separator = a.separator), "string" == typeof a.startDate && (this.startDate = c(a.startDate, this.format)), "string" == typeof a.endDate && (this.endDate = c(a.endDate, this.format)), "string" == typeof a.minDate && (this.minDate = c(a.minDate, this.format)), "string" == typeof a.maxDate && (this.maxDate = c(a.maxDate, this.format)), "object" == typeof a.startDate && (this.startDate = c(a.startDate)), "object" == typeof a.endDate && (this.endDate = c(a.endDate)), "object" == typeof a.minDate && (this.minDate = c(a.minDate)), "object" == typeof a.maxDate && (this.maxDate = c(a.maxDate)), "string" == typeof a.applyClass && (this.applyClass = a.applyClass), "string" == typeof a.cancelClass && (this.cancelClass = a.cancelClass), "object" == typeof a.dateLimit && (this.dateLimit = a.dateLimit), "object" == typeof a.locale && ("object" == typeof a.locale.daysOfWeek && (this.locale.daysOfWeek = a.locale.daysOfWeek.slice()), "object" == typeof a.locale.monthNames && (this.locale.monthNames = a.locale.monthNames.slice()), "number" == typeof a.locale.firstDay && (this.locale.firstDay = a.locale.firstDay), "string" == typeof a.locale.applyLabel && (this.locale.applyLabel = a.locale.applyLabel), "string" == typeof a.locale.cancelLabel && (this.locale.cancelLabel = a.locale.cancelLabel), "string" == typeof a.locale.fromLabel && (this.locale.fromLabel = a.locale.fromLabel), "string" == typeof a.locale.toLabel && (this.locale.toLabel = a.locale.toLabel), "string" == typeof a.locale.weekLabel && (this.locale.weekLabel = a.locale.weekLabel), "string" == typeof a.locale.customRangeLabel && (this.locale.customRangeLabel = a.locale.customRangeLabel)), "string" == typeof a.opens && (this.opens = a.opens), "boolean" == typeof a.showWeekNumbers && (this.showWeekNumbers = a.showWeekNumbers), "string" == typeof a.buttonClasses && (this.buttonClasses = [a.buttonClasses]), "object" == typeof a.buttonClasses && (this.buttonClasses = a.buttonClasses), "boolean" == typeof a.showDropdowns && (this.showDropdowns = a.showDropdowns), "boolean" == typeof a.singleDatePicker && (this.singleDatePicker = a.singleDatePicker, this.singleDatePicker && (this.endDate = this.startDate.clone())), "boolean" == typeof a.timePicker && (this.timePicker = a.timePicker), "boolean" == typeof a.timePickerSeconds && (this.timePickerSeconds = a.timePickerSeconds), "number" == typeof a.timePickerIncrement && (this.timePickerIncrement = a.timePickerIncrement), "boolean" == typeof a.timePicker12Hour && (this.timePicker12Hour = a.timePicker12Hour), 0 != this.locale.firstDay)
                for (var e = this.locale.firstDay; e > 0;) this.locale.daysOfWeek.push(this.locale.daysOfWeek.shift()), e--;
            var f, g, h;
            if ("undefined" == typeof a.startDate && "undefined" == typeof a.endDate && d(this.element).is("input[type=text]")) {
                var i = d(this.element).val(),
                    j = i.split(this.separator);
                f = g = null, 2 == j.length ? (f = c(j[0], this.format), g = c(j[1], this.format)) : this.singleDatePicker && "" !== i && (f = c(i, this.format), g = c(i, this.format)), null !== f && null !== g && (this.startDate = f, this.endDate = g)
            }
            if ("string" == typeof a.timeZone || "number" == typeof a.timeZone ? (this.timeZone = a.timeZone, this.startDate.zone(this.timeZone), this.endDate.zone(this.timeZone)) : this.timeZone = c(this.startDate).zone(), "object" == typeof a.ranges) {
                for (h in a.ranges) f = "string" == typeof a.ranges[h][0] ? c(a.ranges[h][0], this.format) : c(a.ranges[h][0]), g = "string" == typeof a.ranges[h][1] ? c(a.ranges[h][1], this.format) : c(a.ranges[h][1]), this.minDate && f.isBefore(this.minDate) && (f = c(this.minDate)), this.maxDate && g.isAfter(this.maxDate) && (g = c(this.maxDate)), this.minDate && g.isBefore(this.minDate) || this.maxDate && f.isAfter(this.maxDate) || (this.ranges[h] = [f, g]);
                var k = "<ul>";
                for (h in this.ranges) k += "<li>" + h + "</li>";
                k += "<li>" + this.locale.customRangeLabel + "</li>", k += "</ul>", this.container.find(".ranges ul").remove(), this.container.find(".ranges").prepend(k)
            }
            if ("function" == typeof b && (this.cb = b), this.timePicker || (this.startDate = this.startDate.startOf("day"), this.endDate = this.endDate.endOf("day")), this.singleDatePicker ? (this.opens = "right", this.container.addClass("single"), this.container.find(".calendar.right").show(), this.container.find(".calendar.left").hide(), this.timePicker ? this.container.find(".ranges .daterangepicker_start_input, .ranges .daterangepicker_end_input").hide() : this.container.find(".ranges").hide(), this.container.find(".calendar.right").hasClass("single") || this.container.find(".calendar.right").addClass("single")) : (this.container.removeClass("single"), this.container.find(".calendar.right").removeClass("single"), this.container.find(".ranges").show()), this.oldStartDate = this.startDate.clone(), this.oldEndDate = this.endDate.clone(), this.oldChosenLabel = this.chosenLabel, this.leftCalendar = {
                month: c([this.startDate.year(), this.startDate.month(), 1, this.startDate.hour(), this.startDate.minute(), this.startDate.second()]),
                calendar: []
            }, this.rightCalendar = {
                month: c([this.endDate.year(), this.endDate.month(), 1, this.endDate.hour(), this.endDate.minute(), this.endDate.second()]),
                calendar: []
            }, "right" == this.opens || "center" == this.opens) {
                var l = this.container.find(".calendar.first"),
                    m = this.container.find(".calendar.second");
                m.hasClass("single") && (m.removeClass("single"), l.addClass("single")), l.removeClass("left").addClass("right"), m.removeClass("right").addClass("left"), this.singleDatePicker && (l.show(), m.hide())
            }
            "undefined" != typeof a.ranges || this.singleDatePicker || this.container.addClass("show-calendar"), this.container.addClass("opens" + this.opens), this.updateView(), this.updateCalendars()
        },
        setStartDate: function (a) {
            "string" == typeof a && (this.startDate = c(a, this.format).zone(this.timeZone)), "object" == typeof a && (this.startDate = c(a)), this.timePicker || (this.startDate = this.startDate.startOf("day")), this.oldStartDate = this.startDate.clone(), this.updateView(), this.updateCalendars(), this.updateInputText()
        },
        setEndDate: function (a) {
            "string" == typeof a && (this.endDate = c(a, this.format).zone(this.timeZone)), "object" == typeof a && (this.endDate = c(a)), this.timePicker || (this.endDate = this.endDate.endOf("day")), this.oldEndDate = this.endDate.clone(), this.updateView(), this.updateCalendars(), this.updateInputText()
        },
        updateView: function () {
            this.leftCalendar.month.month(this.startDate.month()).year(this.startDate.year()).hour(this.startDate.hour()).minute(this.startDate.minute()), this.rightCalendar.month.month(this.endDate.month()).year(this.endDate.year()).hour(this.endDate.hour()).minute(this.endDate.minute()), this.updateFormInputs()
        },
        updateFormInputs: function () {
            this.container.find("input[name=daterangepicker_start]").val(this.startDate.format(this.format)), this.container.find("input[name=daterangepicker_end]").val(this.endDate.format(this.format)), this.startDate.isSame(this.endDate) || this.startDate.isBefore(this.endDate) ? this.container.find("button.applyBtn").removeAttr("disabled") : this.container.find("button.applyBtn").attr("disabled", "disabled")
        },
        updateFromControl: function () {
            if (this.element.is("input") && this.element.val().length) {
                var a = this.element.val().split(this.separator),
                    b = null,
                    d = null;
                2 === a.length && (b = c(a[0], this.format).zone(this.timeZone), d = c(a[1], this.format).zone(this.timeZone)), (this.singleDatePicker || null === b || null === d) && (b = c(this.element.val(), this.format).zone(this.timeZone), d = b), d.isBefore(b) || (this.oldStartDate = this.startDate.clone(), this.oldEndDate = this.endDate.clone(), this.startDate = b, this.endDate = d, this.startDate.isSame(this.oldStartDate) && this.endDate.isSame(this.oldEndDate) || this.notify(), this.updateCalendars())
            }
        },
        notify: function () {
            this.updateView(), this.cb(this.startDate, this.endDate, this.chosenLabel)
        },
        move: function () {
            var a = {
                top: 0,
                left: 0
            },
                b = d(window).width();
            this.parentEl.is("body") || (a = {
                top: this.parentEl.offset().top - this.parentEl.scrollTop(),
                left: this.parentEl.offset().left - this.parentEl.scrollLeft()
            }, b = this.parentEl[0].clientWidth + this.parentEl.offset().left), "left" == this.opens ? (this.container.css({
                top: this.element.offset().top + this.element.outerHeight() - a.top,
                right: b - this.element.offset().left - this.element.outerWidth(),
                left: "auto"
            }), this.container.offset().left < 0 && this.container.css({
                right: "auto",
                left: 9
            })) : "center" == this.opens ? (this.container.css({
                top: this.element.offset().top + this.element.outerHeight() - a.top,
                left: this.element.offset().left - a.left + this.element.outerWidth() / 2 - this.container.outerWidth() / 2,
                right: "auto"
            }), this.container.offset().left < 0 && this.container.css({
                right: "auto",
                left: 9
            })) : (this.container.css({
                top: this.element.offset().top + this.element.outerHeight() - a.top,
                left: this.element.offset().left - a.left,
                right: "auto"
            }), this.container.offset().left + this.container.outerWidth() > d(window).width() && this.container.css({
                left: "auto",
                right: 0
            }))
        },
        toggle: function () {
            this.element.hasClass("active") ? this.hide() : this.show()
        },
        show: function () {
            this.isShowing || (this.element.addClass("active"), this.container.show(), this.move(), this._outsideClickProxy = d.proxy(function (a) {
                this.outsideClick(a)
            }, this), d(document).on("mousedown.daterangepicker", this._outsideClickProxy).on("touchend.daterangepicker", this._outsideClickProxy).on("click.daterangepicker", "[data-toggle=dropdown]", this._outsideClickProxy).on("focusin.daterangepicker", this._outsideClickProxy), this.isShowing = !0, this.element.trigger("show.daterangepicker", this))
        },
        outsideClick: function (a) {
            debugger
            var b = d(a.target);
            "focusin" == a.type || b.closest(this.element).length || b.closest(this.container).length || b.closest(".calendar-date").length 
            //|| this.hide()
        },
        hide: function () {
            this.isShowing && (d(document).off(".daterangepicker"), this.element.removeClass("active"), this.container.hide(), this.startDate.isSame(this.oldStartDate) && this.endDate.isSame(this.oldEndDate) || this.notify(), this.oldStartDate = this.startDate.clone(), this.oldEndDate = this.endDate.clone(), this.isShowing = !1, this.element.trigger("hide.daterangepicker", this))
        },
        enterRange: function (a) {
            var b = a.target.innerHTML;
            if (b == this.locale.customRangeLabel) this.updateView();
            else {
                var c = this.ranges[b];
                this.container.find("input[name=daterangepicker_start]").val(c[0].format(this.format)), this.container.find("input[name=daterangepicker_end]").val(c[1].format(this.format))
            }
        },
        showCalendars: function () {
            this.container.addClass("show-calendar"), this.move(), this.element.trigger("showCalendar.daterangepicker", this)
        },
        hideCalendars: function () {
            this.container.removeClass("show-calendar"), this.element.trigger("hideCalendar.daterangepicker", this)
        },
        inputsChanged: function (a) {
            var b = d(a.target),
                e = c(b.val(), this.format);
            if (e.isValid()) {
                var f, g;
                "daterangepicker_start" === b.attr("name") ? (f = e, g = this.endDate) : (f = this.startDate, g = e), this.setCustomDates(f, g)
            }
        },
        inputsKeydown: function (a) {
            13 === a.keyCode && (this.inputsChanged(a), this.notify())
        },
        updateInputText: function () {
            this.element.is("input") && !this.singleDatePicker ? this.element.val(this.startDate.format(this.format) + this.separator + this.endDate.format(this.format)) : this.element.is("input") && this.element.val(this.endDate.format(this.format))
        },
        clickRange: function (a) {
            var b = a.target.innerHTML;
            if (this.chosenLabel = b, b == this.locale.customRangeLabel) this.showCalendars();
            else {
                var c = this.ranges[b];
                this.startDate = c[0], this.endDate = c[1], this.timePicker || (this.startDate.startOf("day"), this.endDate.endOf("day")), this.leftCalendar.month.month(this.startDate.month()).year(this.startDate.year()).hour(this.startDate.hour()).minute(this.startDate.minute()), this.rightCalendar.month.month(this.endDate.month()).year(this.endDate.year()).hour(this.endDate.hour()).minute(this.endDate.minute()), this.updateCalendars(), this.updateInputText(), this.hideCalendars(), this.hide(), this.element.trigger("apply.daterangepicker", this)
            }
        },
        clickPrev: function (a) {
            var b = d(a.target).parents(".calendar");
            b.hasClass("left") ? this.leftCalendar.month.subtract(1, "month") : this.rightCalendar.month.subtract(1, "month"), this.updateCalendars()
        },
        clickNext: function (a) {
            var b = d(a.target).parents(".calendar");
            b.hasClass("left") ? this.leftCalendar.month.add(1, "month") : this.rightCalendar.month.add(1, "month"), this.updateCalendars()
        },
        hoverDate: function (a) {
            var b = d(a.target).attr("data-title"),
                c = b.substr(1, 1),
                e = b.substr(3, 1),
                f = d(a.target).parents(".calendar");
            f.hasClass("left") ? this.container.find("input[name=daterangepicker_start]").val(this.leftCalendar.calendar[c][e].format(this.format)) : this.container.find("input[name=daterangepicker_end]").val(this.rightCalendar.calendar[c][e].format(this.format))
        },
        setCustomDates: function (a, b) {
            if (this.chosenLabel = this.locale.customRangeLabel, a.isAfter(b)) {
                var d = this.endDate.diff(this.startDate);
                b = c(a).add(d, "ms")
            }
            this.startDate = a, this.endDate = b, this.updateView(), this.updateCalendars()
        },
        clickDate: function (a) {
            var b, e, f = d(a.target).attr("data-title"),
                g = f.substr(1, 1),
                h = f.substr(3, 1),
                i = d(a.target).parents(".calendar");
            if (i.hasClass("left")) {
                if (b = this.leftCalendar.calendar[g][h], e = this.endDate, "object" == typeof this.dateLimit) {
                    var j = c(b).add(this.dateLimit).startOf("day");
                    e.isAfter(j) && (e = j)
                }
            } else if (b = this.startDate, e = this.rightCalendar.calendar[g][h], "object" == typeof this.dateLimit) {
                var k = c(e).subtract(this.dateLimit).startOf("day");
                b.isBefore(k) && (b = k)
            }
            this.singleDatePicker && i.hasClass("left") ? e = b.clone() : this.singleDatePicker && i.hasClass("right") && (b = e.clone()), i.find("td").removeClass("active"), d(a.target).addClass("active"), this.setCustomDates(b, e), this.timePicker || e.endOf("day"), this.singleDatePicker && !this.timePicker && this.clickApply()
        },
        clickApply: function () {
            this.updateInputText(), this.hide(), this.element.trigger("apply.daterangepicker", this)
        },
        clickCancel: function () {
            this.startDate = this.oldStartDate, this.endDate = this.oldEndDate, this.chosenLabel = this.oldChosenLabel, this.updateView(), this.updateCalendars(), this.hide(), this.element.trigger("cancel.daterangepicker", this)
        },
        updateMonthYear: function (a) {
            var b = d(a.target).closest(".calendar").hasClass("left"),
                c = b ? "left" : "right",
                e = this.container.find(".calendar." + c),
                f = parseInt(e.find(".monthselect").val(), 10),
                g = e.find(".yearselect").val();
            this[c + "Calendar"].month.month(f).year(g), this.updateCalendars()
        },
        updateTime: function (a) {
            var b = d(a.target).closest(".calendar"),
                c = b.hasClass("left"),
                e = parseInt(b.find(".hourselect").val(), 10),
                f = parseInt(b.find(".minuteselect").val(), 10),
                g = 0;
            if (this.timePickerSeconds && (g = parseInt(b.find(".secondselect").val(), 10)), this.timePicker12Hour) {
                var h = b.find(".ampmselect").val();
                "PM" === h && 12 > e && (e += 12), "AM" === h && 12 === e && (e = 0)
            }
            if (c) {
                var i = this.startDate.clone();
                i.hour(e), i.minute(f), i.second(g), this.startDate = i, this.leftCalendar.month.hour(e).minute(f).second(g), this.singleDatePicker && (this.endDate = i.clone())
            } else {
                var j = this.endDate.clone();
                j.hour(e), j.minute(f), j.second(g), this.endDate = j, this.singleDatePicker && (this.startDate = j.clone()), this.rightCalendar.month.hour(e).minute(f).second(g)
            }
            this.updateView(), this.updateCalendars()
        },
        updateCalendars: function () {
            this.leftCalendar.calendar = this.buildCalendar(this.leftCalendar.month.month(), this.leftCalendar.month.year(), this.leftCalendar.month.hour(), this.leftCalendar.month.minute(), this.leftCalendar.month.second(), "left"), this.rightCalendar.calendar = this.buildCalendar(this.rightCalendar.month.month(), this.rightCalendar.month.year(), this.rightCalendar.month.hour(), this.rightCalendar.month.minute(), this.rightCalendar.month.second(), "right"), this.container.find(".calendar.left").empty().html(this.renderCalendar(this.leftCalendar.calendar, this.startDate, this.minDate, this.maxDate, "left")), this.container.find(".calendar.right").empty().html(this.renderCalendar(this.rightCalendar.calendar, this.endDate, this.singleDatePicker ? this.minDate : this.startDate, this.maxDate, "right")), this.container.find(".ranges li").removeClass("active");
            var a = !0,
                b = 0;
            for (var c in this.ranges) this.timePicker ? this.startDate.isSame(this.ranges[c][0]) && this.endDate.isSame(this.ranges[c][1]) && (a = !1, this.chosenLabel = this.container.find(".ranges li:eq(" + b + ")").addClass("active").html()) : this.startDate.format("YYYY-MM-DD") == this.ranges[c][0].format("YYYY-MM-DD") && this.endDate.format("YYYY-MM-DD") == this.ranges[c][1].format("YYYY-MM-DD") && (a = !1, this.chosenLabel = this.container.find(".ranges li:eq(" + b + ")").addClass("active").html()), b++;
            a && (this.chosenLabel = this.container.find(".ranges li:last").addClass("active").html(), this.showCalendars())
        },
        buildCalendar: function (a, b, d, e, f, g) {
            var h, i = c([b, a]).daysInMonth(),
                j = c([b, a, 1]),
                k = c([b, a, i]),
                l = c(j).subtract(1, "month").month(),
                m = c(j).subtract(1, "month").year(),
                n = c([m, l]).daysInMonth(),
                o = j.day(),
                p = [];
            for (p.firstDay = j, p.lastDay = k, h = 0; 6 > h; h++) p[h] = [];
            var q = n - o + this.locale.firstDay + 1;
            q > n && (q -= 7), o == this.locale.firstDay && (q = n - 6);
            var r, s, t = c([m, l, q, 12, e, f]).zone(this.timeZone);
            for (h = 0, r = 0, s = 0; 42 > h; h++, r++, t = c(t).add(24, "hour")) h > 0 && r % 7 === 0 && (r = 0, s++), p[s][r] = t.clone().hour(d), t.hour(12), this.minDate && p[s][r].format("YYYY-MM-DD") == this.minDate.format("YYYY-MM-DD") && p[s][r].isBefore(this.minDate) && "left" == g && (p[s][r] = this.minDate.clone()), this.maxDate && p[s][r].format("YYYY-MM-DD") == this.maxDate.format("YYYY-MM-DD") && p[s][r].isAfter(this.maxDate) && "right" == g && (p[s][r] = this.maxDate.clone());
            return p
        },
        renderDropdowns: function (a, b, c) {
            for (var d = a.month(), e = a.year(), f = c && c.year() || e + 5, g = b && b.year() || e - 50, h = '<select class="monthselect">', i = e == g, j = e == f, k = 0; 12 > k; k++) (!i || k >= b.month()) && (!j || k <= c.month()) && (h += "<option value='" + k + "'" + (k === d ? " selected='selected'" : "") + ">" + this.locale.monthNames[k] + "</option>");
            h += "</select>";
            for (var l = '<select class="yearselect">', m = g; f >= m; m++) l += '<option value="' + m + '"' + (m === e ? ' selected="selected"' : "") + ">" + m + "</option>";
            return l += "</select>", h + l
        },
        renderCalendar: function (a, b, c, e, f) {
            var g = '<div class="calendar-date">';
            g += '<table class="table-condensed">', g += "<thead>", g += "<tr>", this.showWeekNumbers && (g += "<th></th>"), g += !c || c.isBefore(a.firstDay) ? '<th class="prev available"><i class="fa fa-arrow-left icon-arrow-left glyphicon glyphicon-arrow-left"></i></th>' : "<th></th>";
            var h = this.locale.monthNames[a[1][1].month()] + a[1][1].format(" YYYY");
            this.showDropdowns && (h = this.renderDropdowns(a[1][1], c, e)), g += '<th colspan="5" class="month">' + h + "</th>", g += !e || e.isAfter(a.lastDay) ? '<th class="next available"><i class="fa fa-arrow-right icon-arrow-right glyphicon glyphicon-arrow-right"></i></th>' : "<th></th>", g += "</tr>", g += "<tr>", this.showWeekNumbers && (g += '<th class="week">' + this.locale.weekLabel + "</th>"), d.each(this.locale.daysOfWeek, function (a, b) {
                g += "<th>" + b + "</th>"
            }), g += "</tr>", g += "</thead>", g += "<tbody>";
            for (var i = 0; 6 > i; i++) {
                g += "<tr>", this.showWeekNumbers && (g += '<td class="week">' + a[i][0].week() + "</td>");
                for (var j = 0; 7 > j; j++) {
                    var k = "available ";
                    k += a[i][j].month() == a[1][1].month() ? "" : "off", c && a[i][j].isBefore(c, "day") || e && a[i][j].isAfter(e, "day") ? k = " off disabled " : a[i][j].format("YYYY-MM-DD") == b.format("YYYY-MM-DD") ? (k += " active ", a[i][j].format("YYYY-MM-DD") == this.startDate.format("YYYY-MM-DD") && (k += " start-date "), a[i][j].format("YYYY-MM-DD") == this.endDate.format("YYYY-MM-DD") && (k += " end-date ")) : a[i][j] >= this.startDate && a[i][j] <= this.endDate && (k += " in-range ", a[i][j].isSame(this.startDate) && (k += " start-date "), a[i][j].isSame(this.endDate) && (k += " end-date "));
                    var l = "r" + i + "c" + j;
                    g += '<td class="' + k.replace(/\s+/g, " ").replace(/^\s?(.*?)\s?$/, "$1") + '" data-title="' + l + '">' + a[i][j].date() + "</td>"
                }
                g += "</tr>"
            }
            g += "</tbody>", g += "</table>", g += "</div>";
            var m;
            if (this.timePicker) {
                g += '<div class="calendar-time">', g += '<select class="hourselect">';
                var n = 0,
                    o = 23;
                c && ("left" == f || this.singleDatePicker) && b.format("YYYY-MM-DD") == c.format("YYYY-MM-DD") && (n = c.hour(), b.hour() < n && b.hour(n), this.timePicker12Hour && n >= 12 && b.hour() >= 12 && (n -= 12), this.timePicker12Hour && 12 == n && (n = 1)), e && ("right" == f || this.singleDatePicker) && b.format("YYYY-MM-DD") == e.format("YYYY-MM-DD") && (o = e.hour(), b.hour() > o && b.hour(o), this.timePicker12Hour && o >= 12 && b.hour() >= 12 && (o -= 12));
                var p = 0,
                    q = 23,
                    r = b.hour();
                for (this.timePicker12Hour && (p = 1, q = 12, r >= 12 && (r -= 12), 0 === r && (r = 12)), m = p; q >= m; m++) g += m == r ? '<option value="' + m + '" selected="selected">' + m + "</option>" : n > m || m > o ? '<option value="' + m + '" disabled="disabled" class="disabled">' + m + "</option>" : '<option value="' + m + '">' + m + "</option>";
                g += "</select> : ", g += '<select class="minuteselect">';
                var s = 0,
                    t = 59;
                for (c && ("left" == f || this.singleDatePicker) && b.format("YYYY-MM-DD h A") == c.format("YYYY-MM-DD h A") && (s = c.minute(), b.minute() < s && b.minute(s)), e && ("right" == f || this.singleDatePicker) && b.format("YYYY-MM-DD h A") == e.format("YYYY-MM-DD h A") && (t = e.minute(), b.minute() > t && b.minute(t)), m = 0; 60 > m; m += this.timePickerIncrement) {
                    var u = m;
                    10 > u && (u = "0" + u), g += m == b.minute() ? '<option value="' + m + '" selected="selected">' + u + "</option>" : s > m || m > t ? '<option value="' + m + '" disabled="disabled" class="disabled">' + u + "</option>" : '<option value="' + m + '">' + u + "</option>"
                }
                if (g += "</select> ", this.timePickerSeconds) {
                    for (g += ': <select class="secondselect">', m = 0; 60 > m; m += this.timePickerIncrement) {
                        var u = m;
                        10 > u && (u = "0" + u), g += m == b.second() ? '<option value="' + m + '" selected="selected">' + u + "</option>" : '<option value="' + m + '">' + u + "</option>"
                    }
                    g += "</select>"
                }
                if (this.timePicker12Hour) {
                    g += '<select class="ampmselect">';
                    var v = "",
                        w = "";
                    c && ("left" == f || this.singleDatePicker) && b.format("YYYY-MM-DD") == c.format("YYYY-MM-DD") && c.hour() >= 12 && (v = ' disabled="disabled" class="disabled"'), e && ("right" == f || this.singleDatePicker) && b.format("YYYY-MM-DD") == e.format("YYYY-MM-DD") && e.hour() < 12 && (w = ' disabled="disabled" class="disabled"'), g += b.hour() >= 12 ? '<option value="AM"' + v + '>AM</option><option value="PM" selected="selected"' + w + ">PM</option>" : '<option value="AM" selected="selected"' + v + '>AM</option><option value="PM"' + w + ">PM</option>", g += "</select>"
                }
                g += "</div>"
            }
            return g
        },
        remove: function () {
            this.container.remove(), this.element.off(".daterangepicker"), this.element.removeData("daterangepicker")
        }
    }, d.fn.daterangepicker = function (a, b) {
        return this.each(function () {
            var c = d(this);
            c.data("daterangepicker") && c.data("daterangepicker").remove(), c.data("daterangepicker", new e(c, a, b))
        }), this
    }
});