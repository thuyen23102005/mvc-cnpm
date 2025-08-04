function formatCurrency(num) {
    return parseFloat(num).toLocaleString('vi-VN') + " ₫";
}

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".btn-chi-tiet").forEach(btn => {
        btn.addEventListener("click", function () {
            const id = this.dataset.id;

            fetch(`/Account/ChiTietDonHang/${id}`)
                .then(res => res.json())
                .then(data => {
                    document.getElementById("ct-ngay").textContent = data.NgayDat;
                    document.getElementById("ct-diachi").textContent = data.DiaChi;
                    document.getElementById("ct-tong").textContent = formatCurrency(data.TongTien);

                    const tbody = document.getElementById("ct-danhsach");
                    tbody.innerHTML = "";
                    data.SanPham.forEach(sp => {
                        tbody.innerHTML += `
                            <tr>
                                <td>${sp.Ten}</td>
                                <td>${sp.SoLuong}</td>
                                <td>${formatCurrency(sp.DonGia)}</td>
                                <td>${formatCurrency(sp.ThanhTien)}</td>
                            </tr>
                        `;
                    });

                    new bootstrap.Modal(document.getElementById('modalChiTiet')).show();
                });
        });
    });
});