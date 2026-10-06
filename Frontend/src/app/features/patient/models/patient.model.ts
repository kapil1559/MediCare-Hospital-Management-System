export interface Patient {
  patientID: number;
  hospitalID: number;
  locationID: number;
  patientCode: string;
  firstName: string;
  lastName: string | null;
  fullName: string;
  gender: string | null;
  dob: string | null;
  age: number | null;
  bloodGroup: string | null;
  mobileNo: string | null;
  email: string | null;
  address: string | null;
  city: string | null;
  state: string | null;
  pinCode: string | null;
  rowStatus: boolean;
}