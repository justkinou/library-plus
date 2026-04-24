import ChangePassword from "@/components/profile/account/ChangePassword";
import DangerZone from "@/components/profile/account/DangerZone";
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
      <div className="mt-4 flex justify-center gap-24">
        <ChangePassword />
        <DangerZone />
      </div>
    </div>
  );
}

export default page;
