function showPopup(ten, hinhAnh, danhMuc, gia, moTa) {
    document.getElementById("popup-name").textContent = ten;
    document.getElementById("popup-img").src = hinhAnh;
    document.getElementById("popup-category").textContent = danhMuc;
    document.getElementById("popup-price").textContent = gia;
    document.getElementById("popup-desc").textContent = moTa;

    document.getElementById("popup").style.display = "block";
    document.getElementById("popup-bg").style.display = "block";
}

function closePopup() {
    document.getElementById("popup").style.display = "none";
    document.getElementById("popup-bg").style.display = "none";
}

function themVaoGioHang() {
    var ten = document.getElementById("popup-name").textContent;
    var sl = document.getElementById("popup-qty").value;
    alert(`✅ Đã thêm ${sl} x ${ten} vào giỏ hàng`);
    closePopup();
}