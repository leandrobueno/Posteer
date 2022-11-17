import axios from "axios";
import React, { useContext, useEffect, useState } from "react";
import qs from "query-string";

export default function ConfirmEmail() {
  const [confirmed, setConfirmed] = useState<boolean>(false);
  const parsed = qs.parse(location.search);
  console.log(parsed);

  const confirmEmail = () => {
    axios.post(`/api/account/verifyEmail/?email=${parsed.email}&token=${parsed.token}`).then(function (r) {
      if (r.data === "Confirmed") {
        setConfirmed(true);
      }
    });
  };

  useEffect(() => {}, [confirmed]);

  return (
    <>
      <div>{confirmed ? <p>Email confirmed, please login to continue</p> : <button onClick={confirmEmail}>Confirm Email</button>}</div>
    </>
  );
}
