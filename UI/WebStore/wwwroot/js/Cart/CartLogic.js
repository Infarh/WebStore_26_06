Cart = {
	_properties: {
		addToCartLink: "",
		getCartViewLink: ""
	},

	init: function(properties) {
		$.extend(Cart._properties, properties);

		Cart.initAddToCart();
	},

	initAddToCart: function() {
		$("a.CallAddToCart").click(Cart.addToCart);
	},

	addToCart: function(event) {
		event.preventDefault(); // Отключение стандартного поведения ссылки

		var button = $(this);

		var id = button.data("id");

		$.get(Cart._properties.addToCartLink + "/" + id)
			.done(function() {
				Cart.showTooltip(button);
				Cart.refreshCartView();
			})
			.fail(function () { console.log("addToCart error"); });
	},

	refreshCartView: function() {
		var container = $("#cartContainer");
		$.get(Cart._properties.getCartViewLink)
			.done(function(result) {
				container.html(result);
			})
			.fail(function () { console.log("refreshCartView error"); });
	},

	showTooltip: function(button) {
		button.tooltip({
			title : "Добавлено в корзину"
		}).tooltip("show");

		setTimeout(function() {
			button.tooltip("destroy");
		}, 1000);
	}
}