if(typeof Virtuemart==="undefined")
var Virtuemart={};Virtuemart.setproducttype=function(form,id){form.view=null;var datas=form.serialize();var prices=form.parent(".productdetails").find(".product-price");if(0==prices.length){prices=jQuery("#productPrice"+id);}
datas=datas.replace("&view=cart","");prices.fadeTo("fast",0.75);jQuery.ajax({type:"POST",cache:false,dataType:"json",url:window.vmSiteurl+"index.php?&option=com_virtuemart&view=productdetails&task=recalculate&format=json&nosef=1"+window.vmLang,data:datas}).done(function(data,textStatus){prices.fadeTo("fast",1);jQuery("#system-message-container #system-message div.vmprices-message").remove();for(var key in data){var value=data[key];if(key=='messages'){var newmessages=jQuery(data[key]).find("div.alert").addClass("vmprices-message");if(!jQuery("#system-message-container #system-message").length&&newmessages.length){jQuery("#system-message-container").append("<div id='system-message'></div>");}
newmessages.appendTo("#system-message-container #system-message");}else{if(value!=0)prices.find("span.Price"+key).show().html(value);else prices.find(".Price"+key).html(0).hide();}}});return false;}
Virtuemart.productUpdate=function(){jQuery('body').trigger('updateVirtueMartCartModule');}
Virtuemart.sendtocart=function(form){if(Virtuemart.addtocart_popup==1){Virtuemart.cartEffect(form);}else{form.append('<input type="hidden" name="task" value="add" />');form.submit();}}
Virtuemart.cartEffect=function(form){var $=jQuery;var dat=form.serialize();if(usefancy){jQuery.fancybox.showActivity();}
jQuery.ajax({type:"POST",cache:false,dataType:"json",url:window.vmSiteurl+"index.php?option=com_virtuemart&nosef=1&view=cart&task=addJS&format=json"+vmLang,data:dat}).done(function(datas,textStatus){if(datas.stat==1){var txt=datas.msg;}else if(datas.stat==2){var txt=datas.msg+"<H4>"+form.find(".pname").val()+"</H4>";}else{var txt="<H4>"+vmCartError+"</H4>"+datas.msg;}
if(usefancy){jQuery.fancybox({"titlePosition":"inside","transitionIn":"fade","transitionOut":"fade","changeFade":"fast","type":"html","autoCenter":true,"closeBtn":false,"closeClick":false,"content":txt});}else{jQuery.facebox.settings.closeImage=closeImage;jQuery.facebox.settings.loadingImage=loadingImage;jQuery.facebox({text:txt},'my-groovy-style');}
Virtuemart.productUpdate();});}
Virtuemart.product=function(carts){carts.each(function(){var cart=jQuery(this),step=cart.find('input[name="quantity"]'),addtocart=cart.find('input.addtocart-button'),plus=cart.find('.quantity-plus'),minus=cart.find('.quantity-minus'),select=cart.find('select:not(.no-vm-bind)'),radio=cart.find('input:radio:not(.no-vm-bind)'),virtuemart_product_id=cart.find('input[name="virtuemart_product_id[]"]').val(),quantity=cart.find('.quantity-input');var Ste=parseInt(step.val());if(isNaN(Ste)){Ste=1;}
addtocart.click(function(e){Virtuemart.sendtocart(cart);return false;});plus.click(function(){var Qtt=parseInt(quantity.val());if(!isNaN(Qtt)){quantity.val(Qtt+Ste);Virtuemart.setproducttype(cart,virtuemart_product_id);}});minus.click(function(){var Qtt=parseInt(quantity.val());if(!isNaN(Qtt)&&Qtt>Ste){quantity.val(Qtt-Ste);}else quantity.val(Ste);Virtuemart.setproducttype(cart,virtuemart_product_id);});select.change(function(){Virtuemart.setproducttype(cart,virtuemart_product_id);});radio.change(function(){Virtuemart.setproducttype(cart,virtuemart_product_id);});quantity.keyup(function(){Virtuemart.setproducttype(cart,virtuemart_product_id);});});}
Virtuemart.checkQuantity=function(obj,step,myStr){reminder=obj.value%step;quantity=obj.value;if(reminder!=0){alert(myStr.replace("%s",step));if(quantity!=reminder&&quantity>reminder){obj.value=quantity-reminder;}else{obj.value=step;}
return false;}
return true;}
jQuery.noConflict();jQuery(document).ready(function($){Virtuemart.product(jQuery("form.product"));});