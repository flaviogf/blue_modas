$(document).ready(function () {
  $(".shopping-cart__list__item__quantity").on("input", debounce(function () {
    $(this).parent().submit();
  }, 500));
});
