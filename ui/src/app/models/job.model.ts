export interface JobModel {
  jobId: number;
  engineer: string;
  when: Date;
  customerId: number;
  customer?: string;
  type?: string;
}