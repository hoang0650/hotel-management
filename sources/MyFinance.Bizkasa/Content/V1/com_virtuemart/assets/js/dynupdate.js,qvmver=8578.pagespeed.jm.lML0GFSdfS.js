if(typeof Virtuemart==="undefined")
var Virtuemart={};jQuery(function($){Virtuemart.isUpdatingContent=false;Virtuemart.updateContent=function(url){if(Virtuemart.isUpdatingContent)return false;Virtuemart.isUpdatingContent=true;url+=url.indexOf('&')==-1?'?tmpl=component':'&tmpl=component';console.log("UpdateContent URI "+url);$.ajax({url:url,dataType:'html',success:function(data){var el=$(data).find(Virtuemart.containerSelector);if(!el.length)el=$(data).filter(Virtuemart.containerSelector);if(el.length){Virtuemart.container.html(el.html());Virtuemart.updateCartListener();Virtuemart.updateDynamicUpdateListeners();if(Virtuemart.updateImageEventListeners)Virtuemart.updateImageEventListeners();if(Virtuemart.updateChosenDropdownLayout)Virtuemart.updateChosenDropdownLayout();}
Virtuemart.isUpdatingContent=false;}});Virtuemart.isUpdatingContent=false;}
Virtuemart.updateCartListener=function(){Virtuemart.product(jQuery(".product"));jQuery('body').trigger('updateVirtueMartProductDetail');}
Virtuemart.updateDynamicUpdateListeners=function(){var elements=jQuery('*[data-dynamic-update=1]');elements.each(function(i,el){var nodeName=el.nodeName;el=$(el);switch(nodeName){case'A':el[0].onclick=null;el.click(function(event){event.preventDefault();var url=el.attr('href');setBrowserNewState(url);Virtuemart.updateContent(url);});break;default:el[0].onchange=null;el.change(function(event){event.preventDefault();var url=jQuery(el).attr('url');console.log('updateDynamicUpdateListeners found URL attri ',url,el);if(typeof url===typeof undefined||url===false){url=el.val();console.log('updateDynamicUpdateListeners URL attrib empty '+url);}
if(url!=null){console.log('updateDynamicUpdateListeners onchange set URL '+url);setBrowserNewState(url);Virtuemart.updateContent(url);}});}});}
var everPushedHistory=false;var everFiredPopstate=false;function setBrowserNewState(url){if(typeof window.onpopstate=="undefined")
return;var stateObj={url:url}
everPushedHistory=true;console.log('setBrowserNewState '+url);history.pushState(stateObj,"",url);}
var browserStateChangeEvent=function(event){if(!everPushedHistory&&event.state==null&&!everFiredPopstate)
return;everFiredPopstate=true;var url;if(event.state==null){url=window.location.href;}else{url=event.state.url;}
console.log('browserStateChangeEvent '+url);Virtuemart.updateContent(url);}
window.onpopstate=browserStateChangeEvent;});