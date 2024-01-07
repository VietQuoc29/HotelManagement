import React, { useEffect, useState } from "react";
import LoadingOverlay from "react-loading-overlay";
import { Carousel } from "react-responsive-carousel";
import { useParams } from "react-router-dom";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import {
  FaShuttleVan,
  FaParking,
  FaWifi,
  FaSmoking,
  FaServer,
  FaChalkboard,
  FaBreadSlice,
  FaTicketAlt,
  FaTaxi,
  FaHospitalUser,
  FaHelicopter,
  FaHotel,
  FaMapMarkerAlt,
  FaStar,
} from "react-icons/fa";
import clientApi from "../api/clientApi";
import {
  DATE_FORMAT_YYYYMMDDHHmmss,
  loadJs,
  STRING_EMPTY,
  URL_API,
} from "../common/constants";
import OrderRoom from "./OrderRoom";
import {
  NotificationContainer,
  NotificationManager,
} from "react-notifications";
import axios from "axios";
import moment from "moment";

function RoomDetail() {
  // define
  loadJs("/assets/js/custom.js");
  LoadingOverlay.propTypes = undefined;
  const [isLoading, setIsLoading] = useState(true);
  const [name, setName] = useState(STRING_EMPTY);
  const [star, setStar] = useState(STRING_EMPTY);
  const [introduce, setIntroduce] = useState(STRING_EMPTY);
  const [hotelName, setHotelName] = useState(STRING_EMPTY);
  const [floorName, setFloorName] = useState(STRING_EMPTY);
  const [listHotelImage, setListHotelImage] = useState([]);
  const { id } = useParams();
  const [visible, setVisible] = useState(false);

  // init
  useEffect(() => {
    fetchData();
    window.scrollTo(0, 0);
  }, []); // eslint-disable-line react-hooks/exhaustive-deps

  // function
  const fetchData = async () => {
    const params = { roomId: id };
    const response = await clientApi.getAllRoomDetail(params);
    if (response.responseCode === 200) {
      setName(response.data.name);
      setStar(response.data.star);
      setIntroduce(response.data.introduce);
      setHotelName(response.data.hotelName);
      setFloorName(response.data.floorName);
      setListHotelImage(response.data.listHotelImage);
      setIsLoading(false);
    }
  };

  const onCreate = async (values) => {
    const bodyFormData = new FormData();
    bodyFormData.append("fullName", values.fullName ?? STRING_EMPTY);
    bodyFormData.append("email", values.email ?? STRING_EMPTY);
    bodyFormData.append("phoneNumber", values.phoneNumber ?? STRING_EMPTY);
    bodyFormData.append("roomId", id);
    bodyFormData.append(
      "timeFrom",
      values.timeFrom
        ? moment(values.timeFrom).format(DATE_FORMAT_YYYYMMDDHHmmss)
        : STRING_EMPTY
    );
    bodyFormData.append(
      "timeTo",
      values.timeTo
        ? moment(values.timeTo).format(DATE_FORMAT_YYYYMMDDHHmmss)
        : STRING_EMPTY
    );
    bodyFormData.append("note", values.note ?? STRING_EMPTY);
    axios({
      method: "post",
      url: URL_API + "client/OrderRoom",
      data: bodyFormData,
      headers: { "Content-Type": "multipart/form-data" },
    })
      .then(function (response) {
        //handle success
        if (response.data.responseCode === 200) {
          NotificationManager.success(response.data.responseMessage);
          setVisible(false);
        } else {
          NotificationManager.error(response.data.responseMessage);
        }
      })
      .catch(function (response) {
        //handle error
        NotificationManager.error(response.message);
      });
  };

  return (
    <div>
      <LoadingOverlay active={isLoading} spinner text="Loading your content...">
        <section className="breadcrumb_area">
          <div
            className="overlay bg-parallax"
            data-stellar-ratio="0.8"
            data-stellar-vertical-offset="0"
            data-background=""
          ></div>
          <div className="container">
            <div className="page-cover text-center">
              <h2 className="page-cover-tittle">Thông Tin Phòng</h2>
            </div>
          </div>
        </section>

        <section className="blog_area single-post-area">
          <div className="container">
            <div className="row">
              <div className="col-lg-8 posts-list">
                <div className="single-post row">
                  <div className="col-lg-12">
                    <div className="feature-img">
                      <Carousel
                        autoPlay={true}
                        infiniteLoop={true}
                        showStatus={false}
                      >
                        {listHotelImage.map((item) => (
                          <div key={item.id}>
                            <img
                              className="img-fluid"
                              src={item.imageLink}
                              alt=""
                            />
                          </div>
                        ))}
                      </Carousel>
                    </div>
                  </div>
                  <div className="col-lg-12 col-md-12 blog_details">
                    <h2>
                      {name}
                      &nbsp;
                      {(() => {
                        switch (star) {
                          case 1:
                            return (
                              <span>
                                <FaStar />
                              </span>
                            );
                          case 2:
                            return (
                              <span>
                                <FaStar />
                                <FaStar />
                              </span>
                            );
                          case 3:
                            return (
                              <span>
                                <FaStar />
                                <FaStar />
                                <FaStar />
                              </span>
                            );
                          case 4:
                            return (
                              <span>
                                <FaStar />
                                <FaStar />
                                <FaStar />
                                <FaStar />
                              </span>
                            );
                          case 5:
                            return (
                              <span>
                                <FaStar />
                                <FaStar />
                                <FaStar />
                                <FaStar />
                                <FaStar />
                              </span>
                            );
                          default:
                            return <span></span>;
                        }
                      })()}
                    </h2>
                    <span className="uitk-text-default-theme">
                      <FaHotel /> {hotelName} &nbsp; <FaMapMarkerAlt />{" "}
                      {floorName}
                    </span>
                    <p
                      className="excert"
                      dangerouslySetInnerHTML={{ __html: introduce }}
                    ></p>
                  </div>
                </div>
              </div>
              <div className="col-lg-4">
                <div className="blog_right_sidebar">
                  <aside className="single_sidebar_widget search_widget">
                    <div className="input-group">
                      <button
                        className="book_now_btn button_hover"
                        onClick={() => {
                          setVisible(true);
                        }}
                      >
                        Đặt Phòng Ngày Giữ Giá Tốt
                      </button>
                    </div>
                    <div className="br"></div>
                  </aside>
                  <aside className="single_sidebar_widget author_widget">
                    <img
                      className="author_img rounded-circle"
                      src="/assets/image/ico_dam_bao_hoan_tien.png"
                      alt=""
                    />
                    <div className="br"></div>
                  </aside>
                  <aside className="single_sidebar_widget post_category_widget">
                    <h4 className="widget_title">Tiện Nghi Khách Sạn</h4>
                    <ul className="list_style cat-list">
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Đưa đón khách sạn sân bay</p>
                          <p>
                            <FaShuttleVan />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Bãi đỗ xe</p>
                          <p>
                            <FaParking />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Wifi miễn phí</p>
                          <p>
                            <FaWifi />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Phòng hút thuốc</p>
                          <p>
                            <FaSmoking />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Thang máy</p>
                          <p>
                            <FaServer />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Truyền hình cáp/vệ tinh</p>
                          <p>
                            <FaChalkboard />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Giặt là</p>
                          <p>
                            <FaBreadSlice />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Hỗ trợ đặt tour</p>
                          <p>
                            <FaTicketAlt />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Thuê xe, taxi</p>
                          <p>
                            <FaTaxi />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Tiếp tân 24/24</p>
                          <p>
                            <FaHospitalUser />
                          </p>
                        </span>
                      </li>
                      <li>
                        <span className="d-flex justify-content-between">
                          <p>Vé tàu xe, máy bay</p>
                          <p>
                            <FaHelicopter />
                          </p>
                        </span>
                      </li>
                    </ul>
                  </aside>
                </div>
              </div>
            </div>
          </div>
        </section>
      </LoadingOverlay>
      <OrderRoom
        visible={visible}
        onCreate={onCreate}
        onCancel={() => {
          setVisible(false);
        }}
        name={name}
        star={star}
        floorName={floorName}
      />
      <NotificationContainer />
    </div>
  );
}
export default RoomDetail;
