/*
 Name: 			Core Initializer
 Written by: 	Okler Themes - (http://www.okler.net)
 Version: 		3.1.1
 */

(function() {

    "use strict";
    var Core = {
        initialized: false,
        initialize: function() {

            if (this.initialized)
                return;
            this.initialized = true;

            this.build();
            this.events();

        },
        build: function() {

            // Adds browser version on html class.
            jQuery.browserSelector();

            // Adds window smooth scroll on chrome.
            if (jQuery("html").hasClass("chrome")) {
                jQuery.smoothScroll();
            }

            // Scroll to Top Button.
            jQuery.scrollToTop();

            // Nav Menu
            this.navMenu();

            // Header Search
            this.headerSearch();

            // Animations
            this.animations();

            // Word Rotate
            this.wordRotate();

            // Newsletter
            this.newsletter();

            // Featured Boxes
            this.featuredBoxes();

            // Tooltips
            jQuery("a[rel=tooltip]").tooltip();

            // Owl Carousel
            this.owlCarousel();

            // Sort
            this.sort();

            // Toggle
            this.toggle();

            // Twitter
            this.latestTweets();

            // Flickr Feed
            this.flickrFeed();

            // Lightbox
            this.lightbox();

            // Media Element
            this.mediaElement();

            // Parallax
            this.parallax();

            // Account
            this.account();

        },
        events: function() {

            // Anchors Position
            jQuery("a[data-hash]").on("click", function(e) {

                e.preventDefault();
                var header = jQuery("#header"),
                        headerHeight = header.height(),
                        target = jQuery(this).attr("href"),
                        jQuerythis = jQuery(this);

                if (jQuery(window).width() > 991) {
                    jQuery("html,body").animate({scrollTop: jQuery(target).offset().top - (headerHeight + 50)}, 600, "easeOutQuad");
                } else {
                    jQuery("html,body").animate({scrollTop: jQuery(target).offset().top - 30}, 600, "easeOutQuad");
                }

                return false;

            });

            jQuery("body").waitForImages(function() {
                Core.productInfoBox();
            });

            jQuery(window).afterResize(function() {
                if (typeof (Core.productInfoBox) != "undefined") {
                    Core.productInfoBox();
                }
            });

        },
        navMenu: function() {

            // Responsive Menu Events
            var addActiveClass = false;

            jQuery("#mainMenu li.dropdown > a, #mainMenu li.dropdown-submenu > a").on("click", function(e) {

                if (jQuery(window).width() > 979)
                    return;

                e.preventDefault();

                addActiveClass = jQuery(this).parent().hasClass("resp-active");

                jQuery("#mainMenu").find(".resp-active").removeClass("resp-active");

                if (!addActiveClass) {
                    jQuery(this).parents("li").addClass("resp-active");
                }

                return;

            });

            // Submenu Check Visible Space
            jQuery("#mainMenu li.dropdown-submenu").hover(function() {

                if (jQuery(window).width() < 767)
                    return;

                var subMenu = jQuery(this).find("ul.dropdown-menu");

                if (!subMenu.get(0))
                    return;

                var screenWidth = jQuery(window).width(),
                        subMenuOffset = subMenu.offset(),
                        subMenuWidth = subMenu.width(),
                        subMenuParentWidth = subMenu.parents("ul.dropdown-menu").width(),
                        subMenuPosRight = subMenu.offset().left + subMenu.width();

                if (subMenuPosRight > screenWidth) {
                    subMenu.css("margin-left", "-" + (subMenuParentWidth + subMenuWidth + 10) + "px");
                } else {
                    subMenu.css("margin-left", 0);
                }

            });

            // Mega Menu
            jQuery(document).on("click", ".mega-menu .dropdown-menu", function(e) {
                e.stopPropagation()
            });

            // Mobile Redirect
            jQuery(".mobile-redirect").on("click", function() {
                if (jQuery(window).width() < 991) {
                    self.location = jQuery(this).attr("href");
                }
            });

        },
        stickyMenu: function() {

            if (jQuery("body").hasClass("boxed"))
                return false;

            var jQuerythis = this,
                    jQuerybody = jQuery("body"),
                    header = jQuery("#header"),
                    headerContainer = header.parent(),
                    menuAfterHeader = (typeof header.data('after-header') !== 'undefined'),
                    headerHeight = header.height(),
                    flatParentItems = jQuery("#header.flat-menu ul.nav-main > li > a"),
                    logoWrapper = header.find(".logo"),
                    logo = header.find(".logo img"),
                    logoWidth = logo.attr("width"),
                    logoHeight = logo.attr("height"),
                    logoPaddingTop = parseInt(logo.attr("data-sticky-padding") ? logo.attr("data-sticky-padding") : "28"),
                    logoSmallWidth = parseInt(logo.attr("data-sticky-width") ? logo.attr("data-sticky-width") : "82"),
                    logoSmallHeight = parseInt(logo.attr("data-sticky-height") ? logo.attr("data-sticky-height") : "40");

            if (menuAfterHeader) {
                headerContainer.css("min-height", header.height());
            }

            jQuery(window).afterResize(function() {
                headerContainer.css("min-height", header.height());
            });

            jQuerythis.checkStickyMenu = function() {

                if (jQuerybody.hasClass("boxed") || jQuery(window).width() < 991) {
                    jQuerythis.stickyMenuDeactivate();
                    header.removeClass("fixed")
                    return false;
                }

                if (!menuAfterHeader) {

                    if (jQuery(window).scrollTop() > ((headerHeight - 15) - logoSmallHeight)) {

                        jQuerythis.stickyMenuActivate();

                    } else {

                        jQuerythis.stickyMenuDeactivate();

                    }

                } else {

                    if (jQuery(window).scrollTop() > header.parent().offset().top) {

                        header.addClass("fixed");

                    } else {

                        header.removeClass("fixed");

                    }

                }

            }

            jQuerythis.stickyMenuActivate = function() {

                if (jQuerybody.hasClass("sticky-menu-active"))
                    return false;

                logo.stop(true, true);

                jQuerybody.addClass("sticky-menu-active").css("padding-top", headerHeight);
                flatParentItems.addClass("sticky-menu-active");

                logoWrapper.addClass("logo-sticky-active");

                logo.animate({
                    width: logoSmallWidth,
                    height: logoSmallHeight,
                    top: logoPaddingTop + "px"
                }, 200, function() {
                });

            }

            jQuerythis.stickyMenuDeactivate = function() {

                if (jQuerybody.hasClass("sticky-menu-active")) {

                    jQuerybody.removeClass("sticky-menu-active").css("padding-top", 0);
                    flatParentItems.removeClass("sticky-menu-active");

                    logoWrapper.removeClass("logo-sticky-active");

                    logo.animate({
                        width: logoWidth,
                        height: logoHeight,
                        top: "0px"
                    }, 200);

                }

            }


            jQuery(window).on("scroll", function() {

                jQuerythis.checkStickyMenu();

            });

            jQuerythis.checkStickyMenu();

        },
        headerSearch: function() {

            jQuery("#searchForm").validate({
                rules: {
                    q: {
                        required: true
                    }
                },
                errorPlacement: function(error, element) {

                },
                highlight: function(element) {
                    jQuery(element)
                            .closest(".input-group")
                            .removeClass("has-success")
                            .addClass("has-error");
                },
                success: function(element) {
                    jQuery(element)
                            .closest(".input-group")
                            .removeClass("has-error")
                            .addClass("has-success");
                }
            });

        },
        animations: function() {

            // Animation Appear
            jQuery("[data-appear-animation]").each(function() {

                var jQuerythis = jQuery(this);

                jQuerythis.addClass("appear-animation");

                if (!jQuery("html").hasClass("no-csstransitions") && jQuery(window).width() > 767) {

                    jQuerythis.appear(function() {

                        var delay = (jQuerythis.attr("data-appear-animation-delay") ? jQuerythis.attr("data-appear-animation-delay") : 1);

                        if (delay > 1)
                            jQuerythis.css("animation-delay", delay + "ms");
                        jQuerythis.addClass(jQuerythis.attr("data-appear-animation"));

                        setTimeout(function() {
                            jQuerythis.addClass("appear-animation-visible");
                        }, delay);

                    }, {accX: 0, accY: -150});

                } else {

                    jQuerythis.addClass("appear-animation-visible");

                }

            });

            // Animation Progress Bars
            jQuery("[data-appear-progress-animation]").each(function() {

                var jQuerythis = jQuery(this);

                jQuerythis.appear(function() {

                    var delay = (jQuerythis.attr("data-appear-animation-delay") ? jQuerythis.attr("data-appear-animation-delay") : 1);

                    if (delay > 1)
                        jQuerythis.css("animation-delay", delay + "ms");
                    jQuerythis.addClass(jQuerythis.attr("data-appear-animation"));

                    setTimeout(function() {

                        jQuerythis.animate({
                            width: jQuerythis.attr("data-appear-progress-animation")
                        }, 1500, "easeOutQuad", function() {
                            jQuerythis.find(".progress-bar-tooltip").animate({
                                opacity: 1
                            }, 500, "easeOutQuad");
                        });

                    }, delay);

                }, {accX: 0, accY: -50});

            });

            // Count To
            jQuery(".counters [data-to]").each(function() {

                var jQuerythis = jQuery(this);

                jQuerythis.appear(function() {

                    jQuerythis.countTo({
                        onComplete: function() {
                            if (jQuerythis.data("append")) {
                                jQuerythis.html(jQuerythis.html() + jQuerythis.data("append"));
                            }
                        }
                    });

                }, {accX: 0, accY: -150});

            });

            /* Circular Bars - Knob */
            if (typeof (jQuery.fn.knob) != "undefined") {
                jQuery(".knob").knob({});
            }

        },
        wordRotate: function() {

            jQuery(".word-rotate").each(function() {

                var jQuerythis = jQuery(this),
                        itemsWrapper = jQuery(this).find(".word-rotate-items"),
                        items = itemsWrapper.find("> span"),
                        firstItem = items.eq(0),
                        firstItemClone = firstItem.clone(),
                        itemHeight = 0,
                        currentItem = 1,
                        currentTop = 0;

                itemHeight = firstItem.height();

                itemsWrapper.append(firstItemClone);

                jQuerythis
                        .height(itemHeight)
                        .addClass("active");

                setInterval(function() {

                    currentTop = (currentItem * itemHeight);

                    itemsWrapper.animate({
                        top: -(currentTop) + "px"
                    }, 300, "easeOutQuad", function() {

                        currentItem++;

                        if (currentItem > items.length) {

                            itemsWrapper.css("top", 0);
                            currentItem = 1;

                        }

                    });

                }, 2000);

            });

        },
        newsletter: function() {

            jQuery("#newsletterForm").validate({
                submitHandler: function(form) {

                    jQuery.ajax({
                        type: "POST",
                        url: jQuery("#newsletterForm").attr("action"),
                        data: {
                            "email": jQuery("#newsletterForm #newsletterEmail").val()
                        },
                        dataType: "json",
                        success: function(data) {
                            if (data.response == "success") {

                                jQuery("#newsletterSuccess").removeClass("hidden");
                                jQuery("#newsletterError").addClass("hidden");

                                jQuery("#newsletterForm #newsletterEmail")
                                        .val("")
                                        .blur()
                                        .closest(".control-group")
                                        .removeClass("success")
                                        .removeClass("error");

                            } else {

                                jQuery("#newsletterError").html(data.message);
                                jQuery("#newsletterError").removeClass("hidden");
                                jQuery("#newsletterSuccess").addClass("hidden");

                                jQuery("#newsletterForm #newsletterEmail")
                                        .blur()
                                        .closest(".control-group")
                                        .removeClass("success")
                                        .addClass("error");

                            }
                        }
                    });

                },
                rules: {
                    email: {
                        required: true,
                        email: true
                    }
                },
                errorPlacement: function(error, element) {

                },
                highlight: function(element) {
                    jQuery(element)
                            .closest(".control-group")
                            .removeClass("success")
                            .addClass("error");
                },
                success: function(element) {
                    jQuery(element)
                            .closest(".control-group")
                            .removeClass("error")
                            .addClass("success");
                }
            });

        },
        featuredBoxes: function() {

            jQuery("div.featured-box").css("height", "auto");

            jQuery("div.featured-boxes:not(.manual)").each(function() {

                var wrapper = jQuery(this);
                var minBoxHeight = 0;

                jQuery("div.featured-box", wrapper).each(function() {
                    if (jQuery(this).height() > minBoxHeight)
                        minBoxHeight = jQuery(this).height();
                });

                jQuery("div.featured-box", wrapper).height(minBoxHeight);

            });

        },
        owlCarousel: function(options) {

            var total = jQuery("div.owl-carousel:not(.manual)").length,
                    count = 0;

            jQuery("div.owl-carousel:not(.manual)").each(function() {

                var slider = jQuery(this);

                var defaults = {
                    // Most important owl features
                    items: 5,
                    itemsCustom: false,
                    itemsDesktop: [1199, 4],
                    itemsDesktopSmall: [980, 3],
                    itemsTablet: [768, 2],
                    itemsTabletSmall: false,
                    itemsMobile: [479, 1],
                    singleItem: true,
                    itemsScaleUp: false,
                    //Basic Speeds
                    slideSpeed: 200,
                    paginationSpeed: 800,
                    rewindSpeed: 1000,
                    //Autoplay
                    autoPlay: false,
                    stopOnHover: false,
                    // Navigation
                    navigation: false,
                    navigationText: ["<i class=\"icon icon-chevron-left\"></i>", "<i class=\"icon icon-chevron-right\"></i>"],
                    rewindNav: true,
                    scrollPerPage: false,
                    //Pagination
                    pagination: true,
                    paginationNumbers: false,
                    // Responsive
                    responsive: true,
                    responsiveRefreshRate: 200,
                    responsiveBaseWidth: window,
                    // CSS Styles
                    baseClass: "owl-carousel",
                    theme: "owl-theme",
                    //Lazy load
                    lazyLoad: false,
                    lazyFollow: true,
                    lazyEffect: "fade",
                    //Auto height
                    autoHeight: false,
                    //JSON
                    jsonPath: false,
                    jsonSuccess: false,
                    //Mouse Events
                    dragBeforeAnimFinish: true,
                    mouseDrag: true,
                    touchDrag: true,
                    //Transitions
                    transitionStyle: false,
                    // Other
                    addClassActive: false,
                    //Callbacks
                    beforeUpdate: false,
                    afterUpdate: false,
                    beforeInit: false,
                    afterInit: false,
                    beforeMove: false,
                    afterMove: false,
                    afterAction: false,
                    startDragging: false,
                    afterLazyLoad: false
                }

                var config = jQuery.extend({}, defaults, options, slider.data("plugin-options"));

                // Initialize Slider
                slider.owlCarousel(config).addClass("owl-carousel-init");

            });

        },
        sort: function() {

            jQuery("ul.sort-source:not(.manual)").each(function() {

                var source = jQuery(this);
                var destination = jQuery("ul.sort-destination[data-sort-id=" + jQuery(this).attr("data-sort-id") + "]");

                if (destination.get(0)) {

                    var minParagraphHeight = 0;
                    var paragraphs = jQuery("span.thumb-info-caption p", destination);

                    paragraphs.each(function() {
                        if (jQuery(this).height() > minParagraphHeight)
                            minParagraphHeight = (jQuery(this).height() + 10);
                    });

                    paragraphs.height(minParagraphHeight);

                    jQuery(window).load(function() {

                        destination.isotope({
                            itemSelector: "li",
                            layoutMode: 'sloppyMasonry'
                        });

                        source.find("a").click(function(e) {

                            e.preventDefault();

                            var jQuerythis = jQuery(this),
                                    filter = jQuerythis.parent().attr("data-option-value");

                            source.find("li.active").removeClass("active");
                            jQuerythis.parent().addClass("active");

                            destination.isotope({
                                filter: filter
                            });

                            if (window.location.hash != "" || filter.replace(".", "") != "*") {
                                window.location.hash = filter.replace(".", "");
                            }

                            return false;

                        });

                        jQuery(window).bind("hashchange", function(e) {

                            var hashFilter = "." + location.hash.replace("#", ""),
                                    hash = (hashFilter == "." || hashFilter == ".*" ? "*" : hashFilter);

                            source.find("li.active").removeClass("active");
                            source.find("li[data-option-value='" + hash + "']").addClass("active");

                            destination.isotope({
                                filter: hash
                            });

                        });

                        var hashFilter = "." + (location.hash.replace("#", "") || "*");

                        var initFilterEl = source.find("li[data-option-value='" + hashFilter + "'] a");

                        if (initFilterEl.get(0)) {
                            source.find("li[data-option-value='" + hashFilter + "'] a").click();
                        } else {
                            source.find("li:first-child a").click();
                        }

                    });

                }

            });

        },
        toggle: function() {

            var jQuerythis = this,
                    previewParClosedHeight = 25;

            jQuery("section.toggle > label").prepend(jQuery("<i />").addClass("icon icon-plus"));
            jQuery("section.toggle > label").prepend(jQuery("<i />").addClass("icon icon-minus"));
            jQuery("section.toggle.active > p").addClass("preview-active");
            jQuery("section.toggle.active > div.toggle-content").slideDown(350, function() {
            });

            jQuery("section.toggle > label").click(function(e) {

                var parentSection = jQuery(this).parent(),
                        parentWrapper = jQuery(this).parents("div.toogle"),
                        previewPar = false,
                        isAccordion = parentWrapper.hasClass("toogle-accordion");

                if (isAccordion && typeof (e.originalEvent) != "undefined") {
                    parentWrapper.find("section.toggle.active > label").trigger("click");
                }

                parentSection.toggleClass("active");

                // Preview Paragraph
                if (parentSection.find("> p").get(0)) {

                    previewPar = parentSection.find("> p");
                    var previewParCurrentHeight = previewPar.css("height");
                    previewPar.css("height", "auto");
                    var previewParAnimateHeight = previewPar.css("height");
                    previewPar.css("height", previewParCurrentHeight);

                }

                // Content
                var toggleContent = parentSection.find("> div.toggle-content");

                if (parentSection.hasClass("active")) {

                    jQuery(previewPar).animate({
                        height: previewParAnimateHeight
                    }, 350, function() {
                        jQuery(this).addClass("preview-active");
                    });

                    toggleContent.slideDown(350, function() {
                    });

                } else {

                    jQuery(previewPar).animate({
                        height: previewParClosedHeight
                    }, 350, function() {
                        jQuery(this).removeClass("preview-active");
                    });

                    toggleContent.slideUp(350, function() {
                    });

                }

            });

        },
        lightbox: function(options) {

            if (typeof (jQuery.magnificPopup) == "undefined") {
                return false;
            }

            // Internationalization of Lightbox
            jQuery.extend(true, jQuery.magnificPopup.defaults, {
                tClose: 'Close (Esc)', // Alt text on close button
                tLoading: 'Loading...', // Text that is displayed during loading. Can contain %curr% and %total% keys
                gallery: {
                    tPrev: 'Previous (Left arrow key)', // Alt text on left arrow
                    tNext: 'Next (Right arrow key)', // Alt text on right arrow
                    tCounter: '%curr% of %total%' // Markup for "1 of 7" counter
                },
                image: {
                    tError: '<a href="%url%">The image</a> could not be loaded.' // Error message when image could not be loaded
                },
                ajax: {
                    tError: '<a href="%url%">The content</a> could not be loaded.' // Error message when ajax request failed
                }
            });

            jQuery(".lightbox:not(.manual)").each(function() {

                var el = jQuery(this);

                var config, defaults = {}
                if (el.data("plugin-options"))
                    config = jQuery.extend({}, defaults, options, el.data("plugin-options"));

                jQuery(this).magnificPopup(config);

            });

        },
        flickrFeed: function(options) {

            jQuery("ul.flickr-feed:not(.manual)").each(function() {

                var el = jQuery(this);

                var defaults = {
                    limit: 6,
                    qstrings: {
                        id: ''
                    }
                }

                var config = jQuery.extend({}, defaults, options, el.data("plugin-options"));

                el.jflickrfeed(config, function(data) {

                    el.magnificPopup({
                        delegate: "a",
                        type: "image",
                        gallery: {
                            enabled: true,
                            navigateByImgClick: true,
                            preload: [0, 1]
                        },
                        zoom: {
                            enabled: true,
                            duration: 300,
                            opener: function(element) {
                                return element.find('img');
                            }
                        }
                    });

                });


            });

        },
        mediaElement: function(options) {

            if (typeof (mejs) == "undefined") {
                return false;
            }

            jQuery("video:not(.manual)").each(function() {

                var el = jQuery(this);

                var defaults = {
                    defaultVideoWidth: 480,
                    defaultVideoHeight: 270,
                    videoWidth: -1,
                    videoHeight: -1,
                    audioWidth: 400,
                    audioHeight: 30,
                    startVolume: 0.8,
                    loop: false,
                    enableAutosize: true,
                    features: ['playpause', 'progress', 'current', 'duration', 'tracks', 'volume', 'fullscreen'],
                    alwaysShowControls: false,
                    iPadUseNativeControls: false,
                    iPhoneUseNativeControls: false,
                    AndroidUseNativeControls: false,
                    alwaysShowHours: false,
                    showTimecodeFrameCount: false,
                    framesPerSecond: 25,
                    enableKeyboard: true,
                    pauseOtherPlayers: true,
                    keyActions: []
                }

                var config = jQuery.extend({}, defaults, options, el.data("plugin-options"));

                el.mediaelementplayer(config);

            });

        },
        parallax: function() {

            if (typeof (jQuery.stellar) == "undefined") {
                return false;
            }

            jQuery(window).load(function() {

                if (jQuery(".parallax").get(0)) {
                    if (!Modernizr.touch) {
                        jQuery(window).stellar({
                            responsive: true,
                            scrollProperty: 'scroll',
                            parallaxElements: false,
                            horizontalScrolling: false,
                            horizontalOffset: 0,
                            verticalOffset: 0
                        });
                    } else {
                        jQuery(".parallax").addClass("disabled");
                    }
                }
            });

        },
        latestTweets: function() {

            var wrapper = jQuery("#tweet"),
                    accountId = wrapper.data("account-id");

            if (wrapper.get(0) && accountId != "") {
                getTwitters("tweet", {
                    id: accountId,
                    count: 2
                });

                wrapper.before(jQuery("<a />").addClass("twitter-account").html("@" + accountId).attr("href", "http://www.twitter.com/" + accountId).attr("target", "_blank"));

            } else {
                wrapper.empty();
            }

        },
        fixRevolutionSlider: function() {

            jQuery(".revslider-initialised").each(function() {
                try {
                    jQuery(this).revredraw();
                } catch (e) {
                }
            });

        },
        productInfoBox: function() {

            if (jQuery(window).width() > 991) {
                jQuery(".product-thumb-info").css("height", "auto");

                jQuery(".product-thumb-info-list:not(.manual)").each(function() {

                    var wrapper = jQuery(this);
                    var minBoxHeight = 0;

                    jQuery(".product-thumb-info", wrapper).each(function() {
                        if (jQuery(this).height() > minBoxHeight)
                            minBoxHeight = jQuery(this).height();
                    });

                    jQuery(".product-thumb-info", wrapper).height(minBoxHeight);

                });
            } else {
                jQuery(".product-thumb-info").css("height", "auto");
            }

        },
        account: function() {

            var headerAccountWrapper = jQuery("#headerAccount"),
                    closeEventAdded = false;

            // Events
            headerAccountWrapper.find("input").on("focus", function() {
                headerAccountWrapper.addClass("open");

                if (closeEventAdded)
                    return;

                jQuery(document).mouseup(function(e) {
                    if (!headerAccountWrapper.is(e.target) && headerAccountWrapper.has(e.target).length === 0) {
                        headerAccountWrapper.removeClass("open");
                    }
                });

                closeEventAdded = true;
            });

            jQuery("#headerSignUp").on("click", function(e) {
                e.preventDefault();
                headerAccountWrapper.addClass("signup").removeClass("signin").removeClass("recover");
                headerAccountWrapper.find(".signup-form input:first").focus();
            });

            jQuery("#headerSignIn").on("click", function(e) {
                e.preventDefault();
                headerAccountWrapper.addClass("signin").removeClass("signup").removeClass("recover");
                headerAccountWrapper.find(".signin-form input:first").focus();
            });

            jQuery("#headerRecover").on("click", function(e) {
                e.preventDefault();
                headerAccountWrapper.addClass("recover").removeClass("signup").removeClass("signin");
                headerAccountWrapper.find(".recover-form input:first").focus();
            });

            jQuery("#headerRecoverCancel").on("click", function(e) {
                e.preventDefault();
                headerAccountWrapper.addClass("signin").removeClass("signup").removeClass("recover");
                headerAccountWrapper.find(".signin-form input:first").focus();
            });

        }

    };

    Core.initialize();

    jQuery(window).load(function() {

        // Sticky Meny
        Core.stickyMenu();

        // Window Resize
        jQuery(window).afterResize(function() {

            // Featured Boxes
            if (typeof (Core.featuredBoxes) != "undefined") {
                Core.featuredBoxes();
            }

            // Sticky Menu
            if (typeof (Core.checkStickyMenu) != "undefined") {
                Core.checkStickyMenu();
            }

            // Revolution Slider Fix
            if (typeof (Core.fixRevolutionSlider) != "undefined") {
                Core.fixRevolutionSlider();
            }

            // Product Info Box
            if (typeof (Core.productInfoBox) != "undefined") {
                Core.productInfoBox();
            }

            // Isotope
            if (jQuery(".isotope").get(0)) {
                jQuery(".isotope").isotope('reLayout');
            }

        }, true, 100);

    });

})(jQuery);