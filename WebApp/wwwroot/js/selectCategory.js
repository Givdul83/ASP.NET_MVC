document.addEventListener('DOMContentLoaded', function () {
    const categorySelect = document.getElementById('categorySelect');
    categorySelect.addEventListener('change', function () {
        this.form.submit();
    });

    window.DOMContentLoaded = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const category = urlParams.get('category');
        if (category !== null) {
            categorySelect.value = category;
        }
    };

    categorySelect.addEventListener('change', function () {
        localStorage.setItem('selectedCategory', this.value);
    });


    const savedCategory = localStorage.getItem('selectedCategory');
    if (savedCategory !== null) {
        categorySelect.value = savedCategory;
    }
});