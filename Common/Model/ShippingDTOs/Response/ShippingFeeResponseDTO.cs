﻿namespace Common.Model.ShippingDTOs.Response
{
    public class ShippingFeeResponseDTO
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public int total { get; set; }
            public int service_fee { get; set; }
            public int insurance_fee { get; set; }
            public int pick_station_fee { get; set; }
            public int coupon_value { get; set; }
            public int r2s_fee { get; set; }
            public int return_again { get; set; }
            public int document_return { get; set; }
            public int double_check { get; set; }
            public int cod_fee { get; set; }
            public int pick_remote_areas_fee { get; set; }
            public int deliver_remote_areas_fee { get; set; }
            public int cod_failed_fee { get; set; }
        }
    }
}
