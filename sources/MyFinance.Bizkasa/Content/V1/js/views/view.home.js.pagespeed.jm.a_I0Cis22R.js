(function(){"use strict";var Home={initialized:false,initialize:function(){if(this.initialized)
return;this.initialized=true;this.build();this.events();},build:function(options){if(typeof(jQuery.fn.flipshow())!="undefined"){var circleContainer=jQuery("#fcSlideshow");if(circleContainer.get(0)){circleContainer.flipshow();setInterval(function(){circleContainer.data().flipshow._navigate(circleContainer.find("div.fc-right span:first"),"right");},3000);}}
jQuery("#revolutionSlider").each(function(){var slider=jQuery(this);var defaults={delay:9000,startheight:495,startwidth:960,hideThumbs:10,thumbWidth:100,thumbHeight:50,thumbAmount:5,navigationType:"both",navigationArrows:"verticalcentered",navigationStyle:"round",touchenabled:"on",onHoverStop:"on",navOffsetHorizontal:0,navOffsetVertical:20,stopAtSlide:0,stopAfterLoops:-1,shadow:0,fullWidth:"on",videoJsPath:"vendor/rs-plugin/videojs/"}
var config=jQuery.extend({},defaults,options,slider.data("plugin-options"));var sliderApi=slider.revolution(config).addClass("slider-init");sliderApi.bind("revolution.slide.onloaded ",function(e,data){jQuery(".home-player").addClass("visible");});});if(jQuery("#revolutionSliderFullScreen").get(0)){var rev=jQuery("#revolutionSliderFullScreen").revolution({delay:9000,startwidth:1170,startheight:600,hideThumbs:200,thumbWidth:100,thumbHeight:50,thumbAmount:5,navigationType:"both",navigationArrows:"verticalcentered",navigationStyle:"round",touchenabled:"on",onHoverStop:"on",navOffsetHorizontal:0,navOffsetVertical:20,stopAtSlide:-1,stopAfterLoops:-1,shadow:0,fullWidth:"on",fullScreen:"on",fullScreenOffsetContainer:".header",videoJsPath:"vendor/rs-plugin/videojs/"});}
if(jQuery("#nivoSlider").get(0)){jQuery("#nivoSlider").nivoSlider({effect:'random',slices:15,boxCols:8,boxRows:4,animSpeed:500,pauseTime:3000,startSlide:0,directionNav:true,controlNav:true,controlNavThumbs:false,pauseOnHover:true,manualAdvance:false,prevText:'Prev',nextText:'Next',randomStart:false,beforeChange:function(){},afterChange:function(){},slideshowEnd:function(){},lastSlide:function(){},afterLoad:function(){}});}},events:function(){this.moveCloud();},moveCloud:function(){var jQuerythis=this;jQuery(".cloud").animate({"top":"+=20px"},3000,"linear",function(){jQuery(".cloud").animate({"top":"-=20px"},3000,"linear",function(){jQuerythis.moveCloud();});});}};Home.initialize();})(jQuery);