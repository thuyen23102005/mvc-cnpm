function showPopup(id, ten, hinhAnh, danhMuc, gia, moTa) {
    document.getElementById("popup-id").value = id;
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
    var sl = parseInt(document.getElementById("popup-qty").value);
    var id = parseInt(document.getElementById("popup-id").value);

    fetch("/GioHang/ThemVaoGio", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ id: id, soLuong: sl })
    })
        .then(res => {
            if (res.ok) {
                alert(`✅ Đã thêm ${sl} x ${ten} vào giỏ hàng`);
                closePopup();
            } else {
                throw new Error("Lỗi khi thêm");
            }
        })
        .catch(() => {
            alert("❌ Lỗi khi thêm vào giỏ hàng.");
        });
}

document.addEventListener("DOMContentLoaded", function () {
    locDanhMuc('TatCa');
});