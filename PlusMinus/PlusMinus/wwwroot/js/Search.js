let searchInput = document.getElementById('search-item');

function search() {
	let resultValue = document.getElementById('search-item').value.toUpperCase();
	let list = document.getElementById('prods');
	let products = list.querySelectorAll('.shop_product');

	for (let i = 0; i < products.length; i++) {
		let a = products[i].getElementsByTagName('p')[0];
		if (a.innerHTML.toUpperCase().indexOf(resultValue) > -1) {
			products[i].style.display = '';
		} else {
			products[i].style.display = 'none';
		}
	}
}