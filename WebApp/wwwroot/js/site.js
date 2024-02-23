const btn = document.querySelector('#mobile-menu-btn');
btn.addEventListener('click', () => {
    const menu = document.querySelector('#mobile-menu');
    menu.classList.toggle('open');

    const isOpen = menu.getAttribute('aria-expanded') == 'true';

    menu.setAttribute('aria-expanded', !isOpen);
})