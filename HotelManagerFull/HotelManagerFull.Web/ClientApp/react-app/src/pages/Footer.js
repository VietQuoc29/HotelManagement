import React from "react";
import { Link } from "react-router-dom";

function Footer() {
  return (
    <div>
      <footer className="footer-area section_gap">
        <div className="container">
          <div className="row">
            <div className="col-lg-5 col-md-6 col-sm-6">
              <div className="single-footer-widget">
                <h6 className="footer_title">Thông tin cho nhà cung cấp</h6>
                <p>
                  Thế giới đã trở nên có nhịp độ nhanh đến mức mọi người không
                  muốn đứng đọc một trang thông tin, họ muốn xem bản trình bày
                  và hiểu thông điệp. Nó đã đến một điểm{" "}
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6 col-sm-6">
              <div className="single-footer-widget">
                <h6 className="footer_title">LIÊN KẾT DI CHUYỂN</h6>
                <div className="row">
                  <div className="col-4">
                    <ul className="list_style">
                      <li>
                        <Link to={"/"}>Trang chủ</Link>
                      </li>
                      <li>
                        <Link to={"/about"}>Về chúng tôi</Link>
                      </li>
                    </ul>
                  </div>
                  <div className="col-4">
                    <ul className="list_style">
                      <li>
                        <Link to={"/gallery"}>Bộ sưu tập</Link>
                      </li>
                      <li>
                        <Link to={"/contact"}>Liên hệ</Link>
                      </li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
            <div className="col-lg-3 col-md-6 col-sm-6">
              <div className="single-footer-widget">
                <h6 className="footer_title">THÔNG TIN</h6>
                <p>
                  Đối với các chuyên gia kinh doanh bị mắc kẹt giữa giá OEM cao
                  và sản lượng đồ họa và bản in tầm thường{" "}
                </p>
              </div>
            </div>
          </div>
          <div className="border_line"></div>
          <div className="row footer-bottom d-flex justify-content-between align-items-center">
            <p className="col-lg-12 col-sm-12 footer-text m-0">
              *Một số khách sạn yêu cầu hủy hơn 24 giờ trước ngày nhận phòng.
              Xem thêm chi tiết trên trang.
              <br />
              ©2022 Hotels.com là công ty thuộc Expedia Group. Mọi quyền được
              bảo lưu.
              <br />
              Hotels.com và logo Hotels.com là thương hiệu hoặc thương hiệu đã
              đăng ký bảo hộ của Hotels.com, LP tại Mỹ và/hoặc các quốc gia
              khác. Mọi thương hiệu khác thuộc quyền sở hữu của các chủ sở hữu
              tương ứng.
            </p>
          </div>
        </div>
      </footer>
    </div>
  );
}
export default Footer;
