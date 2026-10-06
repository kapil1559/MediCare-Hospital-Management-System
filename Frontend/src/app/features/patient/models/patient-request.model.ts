export interface PatientRequest {
  mode: 'Add' | 'Edit';
  patientID: number;
  patientCode: string;
  firstName: string;
  lastName: string | null;
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
}