import React, { useState } from "react";
import CurrencyFormat from "react-currency-format";
import { Link } from "react-router-dom";
import { loadJs, STRING_EMPTY } from "../common/constants";

function BannerHome(props) {
  // define
  loadJs("/assets/js/custom.js");
  const [formValue, setFormValue] = useState({
    provincesValue: STRING_EMPTY,
    priceFormValue: STRING_EMPTY,
    priceToValue: STRING_EMPTY,
    starValue: "5",
  });

  const handleChangeValue = (event) => {
    const value = event.target.value;
    setFormValue({
      ...formValue,
      [event.target.id]: value,
    });
  };

  return (
    <div>
      <section className="banner_area">
        <div className="booking_table d_flex align-items-center">
          <div
            className="overlay bg-parallax"
            data-stellar-ratio="0.9"
            data-stellar-vertical-offset="0"
            data-background=""
          ></div>
          <div className="container">
            <div className="banner_content text-center">
              <h6>TRÁNH XA CUỘC SỐNG ĐỘC ĐÁO</h6>
              <h2>Thư Giãn Tâm Trí Của Bạn</h2>
              <p>
                Nếu bạn đang xem các phòng trống trên web, bạn có thể rất bối
                rối về sự khác biệt
                <br /> trong giá. Bạn có thể thấy một số với giá thấp tới
                500.000 vnđ với mỗi phòng.
              </p>
            </div>
          </div>
        </div>
        <div className="hotel_booking_area position">
          <div className="container">
            <div className="hotel_booking_table">
              <div className="col-md-3">
                <div className="book_tabel_item">
                  <div className="input-group">
                    <select
                      className="wide select-provinces"
                      id="provincesValue"
                      onChange={handleChangeValue}
                      value={formValue.provincesValue}
                    >
                      <option value="">Tất cả tỉnh thành</option>
                      {props.listProvinces.map((item) => (
                        <option key={item.id} value={item.id}>
                          {item.name}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>
              </div>
              <div className="col-md-2">
                <div className="book_tabel_item">
                  <div className="input-group">
                    <CurrencyFormat
                      thousandSeparator={true}
                      prefix={""}
                      className="input-price"
                      placeholder="Từ"
                      maxLength={11}
                      id="priceFormValue"
                      value={formValue.priceFormValue}
                      onChange={handleChangeValue}
                    />
                  </div>
                </div>
              </div>
              <div className="col-md-2">
                <div className="book_tabel_item">
                  <div className="input-group">
                    <CurrencyFormat
                      thousandSeparator={true}
                      prefix={""}
                      className="input-price"
                      placeholder="Đến"
                      maxLength={11}
                      id="priceToValue"
                      value={formValue.priceToValue}
                      onChange={handleChangeValue}
                    />
                  </div>
                </div>
              </div>
              <div className="col-md-3">
                <div className="book_tabel_item">
                  <div className="input-group">
                    <select
                      className="wide select-star"
                      id="starValue"
                      value={formValue.starValue}
                      onChange={handleChangeValue}
                    >
                      <option value="1">&#xf005;</option>
                      <option value="2">&#xf005; &#xf005;</option>
                      <option value="3">&#xf005; &#xf005; &#xf005;</option>
                      <option value="4">
                        &#xf005; &#xf005; &#xf005; &#xf005;
                      </option>
                      <option value="5">
                        &#xf005; &#xf005; &#xf005; &#xf005; &#xf005;
                      </option>
                    </select>
                  </div>
                </div>
              </div>
              <div className="col-md-2">
                <Link to={`/${formValue.starValue}`}
                  className="book_now_btn button_hover"
                //onClick={handleSearchSubmit}
                >
                  Tìm kiếm
                </Link>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
export default BannerHome;
