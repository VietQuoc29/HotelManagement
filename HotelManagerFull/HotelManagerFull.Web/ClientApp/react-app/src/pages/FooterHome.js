import React from "react";

function FooterHome() {
  return (
    <div>
      <section className="facilities_area section_gap">
        <div
          className="overlay bg-parallax"
          data-stellar-ratio="0.8"
          data-stellar-vertical-offset="0"
          data-background=""
        ></div>
        <div className="container">
          <div className="section_title text-center">
            <h2 className="title_w">Cơ Sở Vật Chất Hoàng Gia</h2>
            <p>
              Những người cực kỳ yêu thích hệ thống thân thiện với môi trường.
            </p>
          </div>
          <div className="row mb_30">
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-dinner"></i>Nhà Hàng
                </h4>
                <p>
                  Nhà hàng trong khách sạn phục vụ khách từ 6h đến 24h hàng
                  ngày, có nơi phục vụ 24/24h.
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-bicycle"></i>Câu Lạc Bộ Thể Thao
                </h4>
                <p>
                  Đường tập golf trong nhà, sân bóng rổ, đường dạo bộ, và đặc
                  biệt là bể bơi ngoài trời.
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-shirt"></i>Hồ Bơi
                </h4>
                <p>
                  Hồ bơi được thiết kế đẹp mắt, xung quanh là những chiếc ghế
                  đệm để du khách nghỉ ngơi.
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-car"></i>Dịch Vụ Thuê Xe
                </h4>
                <p>
                  Đảm bảo chất lượng dịch vụ uy tín với giá tốt nhất, tài xế lái
                  xe nhiều năm kinh nghiệm, lái xe an toàn.
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-construction"></i>Thể Dục
                </h4>
                <p>
                  Với các thiết bị thể dục của hãng Techno Gym, bạn có thể thoả
                  sức tập luyện theo cách mà mình muốn.
                </p>
              </div>
            </div>
            <div className="col-lg-4 col-md-6">
              <div className="facilities_item">
                <h4 className="sec_h4">
                  <i className="lnr lnr-coffee-cup"></i>Bar
                </h4>
                <p>
                  Khách có thể thoải mái trò chuyện, giao lưu hoặc cùng nhau
                  thưởng thức rượu, chơi vài ván bida, phi tiêu.
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="about_history_area section_gap">
        <div className="container">
          <div className="row">
            <div className="col-md-6 d_flex align-items-center">
              <div className="about_content ">
                <h2 className="title title_color">
                  Chào Mừng Bạn Đến Với Hotels.com
                </h2>
                <p>
                  Hotels.com™ là nhà cung cấp dịch vụ đặt phòng hàng đầu thế
                  giới, với hệ thống dịch vụ đặt phòng qua các trang web được
                  bản địa hóa. Tại Hotels.com, khách có nhiều lựa chọn, từ khách
                  sạn độc lập đến chuỗi khách sạn lớn hay nơi lưu trú tự phục vụ
                  tại hàng trăm nghìn nơi lưu trú khác nhau trên toàn cầu. Chúng
                  tôi mang đến cho khách trang web đặt phòng với đầy đủ tính
                  năng so sánh giá cả, tiện nghi, dịch vụ và lượng phòng.
                </p>
              </div>
            </div>
            <div className="col-md-6">
              <img
                className="img-fluid"
                src="/assets/image/about_bg.jpg"
                alt="img"
              />
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
export default FooterHome;
