function locDanhMuc(danhMuc) {
    const cards = document.querySelectorAll('.product-card');
    cards.forEach(card => {
        const dm = card.getAttribute('data-danhmuc');
        if (danhMuc === 'TatCa' || dm === danhMuc) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });

    // Xử lý active class
    const buttons = document.querySelectorAll('.tab-bar button');
    buttons.forEach(btn => btn.classList.remove('active'));

    const activeBtn = document.querySelector(`.tab-bar button[data-danhmuc="${danhMuc}"]`);
    if (activeBtn) activeBtn.classList.add('active');
}

document.addEventListener("DOMContentLoaded", function () {
    locDanhMuc('TatCa'); // mặc định
});