import DeliveryAddress from "@/components/profile/account/DeliveryAddress";
import UserInfo from "@/components/profile/account/UserInfo";
import React from "react";

function page() {
  return (
    <div>
      <div className="mt-4 flex justify-center gap-24">
        <UserInfo />
        <DeliveryAddress />
      </div>
    </div>
  );
}

export default page;
