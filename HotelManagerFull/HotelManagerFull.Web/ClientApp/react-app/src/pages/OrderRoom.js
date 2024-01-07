import { Input, DatePicker, Form, Modal, InputNumber } from "antd";
import React, { useEffect, useRef } from "react";
import { UserOutlined, MailOutlined, PhoneOutlined } from "@ant-design/icons";
import { DATE_FORMAT_DDMMYYYYHHmmss } from "../common/constants";
import moment from "moment";
import { FaStar } from "react-icons/fa";

const { TextArea } = Input;
const dateFormat = DATE_FORMAT_DDMMYYYYHHmmss;

function disabledDate(current) {
  // Can not select days before today and today
  return current && current < moment().endOf("day");
}

const OrderRoom = ({ visible, onCreate, onCancel, name, star, floorName }) => {
  const [form] = Form.useForm();
  const prevVisibleRef = useRef();

  useEffect(() => {
    prevVisibleRef.current = visible;
  }, [visible]); // eslint-disable-line react-hooks/exhaustive-deps

  const prevVisible = prevVisibleRef.current;
  useEffect(() => {
    if (!visible && prevVisible) {
      form.resetFields();
    }
  }, [visible]); // eslint-disable-line react-hooks/exhaustive-deps

  return (
    <Modal
      width={800}
      visible={visible}
      title="Thông tin đặt phòng trước"
      okText="Đặc phòng trước"
      cancelText="Thoát"
      onCancel={onCancel}
      onOk={() => {
        form
          .validateFields()
          .then((values) => {
            form.resetFields();
            onCreate(values);
          })
          .catch((info) => {});
      }}
    >
      <Form
        form={form}
        layout="vertical"
        name="form_in_modal"
        initialValues={{
          modifier: "public",
        }}
      >
        <div className="alert alert-info lable-modal">
          {name}
          &nbsp; - {floorName}
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
        </div>
        <Form.Item
          label="Họ và tên"
          name="fullName"
          rules={[
            {
              required: true,
              message: "Yêu cầu nhập Họ và tên!",
            },
          ]}
        >
          <Input
            placeholder="Họ và tên"
            maxLength={50}
            prefix={<UserOutlined />}
          />
        </Form.Item>
        <Form.Item
          label="Email"
          name="email"
          rules={[
            {
              required: true,
              message: "Yêu cầu nhập Email!",
            },
            {
              type: "email",
              message: "Email chưa đúng định dạng!",
            },
          ]}
        >
          <Input placeholder="Email" prefix={<MailOutlined />} maxLength={50} />
        </Form.Item>
        <Form.Item
          label="Thời gian thuê phòng dự kiến"
          name="timeFrom"
          rules={[
            {
              required: true,
              message: "Yêu cầu chọn Thời gian thuê phòng dự kiến!",
            },
          ]}
        >
          <DatePicker
            className="date-picker"
            placeholder="Thời gian thuê phòng dự kiến"
            format={dateFormat}
            allowClear={false}
            disabledDate={disabledDate}
            showTime={{
              defaultValue: moment("00:00:00", "HH:mm:ss"),
            }}
          />
        </Form.Item>
        <Form.Item
          label="Thời gian trả phòng dự kiến"
          name="timeTo"
          rules={[
            {
              required: true,
              message: "Yêu cầu chọn Thời gian trả phòng dự kiến!",
            },
          ]}
        >
          <DatePicker
            className="date-picker"
            placeholder="Thời gian trả phòng dự kiến"
            format={dateFormat}
            allowClear={false}
            disabledDate={disabledDate}
            showTime={{
              defaultValue: moment("00:00:00", "HH:mm:ss"),
            }}
          />
        </Form.Item>
        <Form.Item
          label="Số điện thoại"
          name="phoneNumber"
          rules={[
            {
              required: true,
              message: "Yêu cầu nhập Số điện thoại!",
            },
          ]}
        >
          <InputNumber
            className="input-number"
            placeholder="Số điện thoại"
            maxLength={11}
            prefix={<PhoneOutlined />}
          />
        </Form.Item>
        <Form.Item label="Ghi chú" name="note">
          <TextArea
            style={{ height: 120 }}
            placeholder="Ghi chú"
            maxLength={500}
          />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default OrderRoom;
