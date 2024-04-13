
document.getElementById('save-course').addEventListener('click', function (event) {

    event.preventDefault();
    alert('The course has been saved to your courses');
    this.form.submit();

});