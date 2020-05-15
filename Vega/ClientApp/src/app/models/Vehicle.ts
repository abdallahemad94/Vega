export class Vehicle {
    id: number;
    model: {
      id: number,
      name: string
    };
    make: {
        id: number,
        name: string
  };
  isRegistered: boolean;
    features: [{
        id: number,
        name: string
    }];
    lastUpdated: Date;
    contactInfo: {
        name: string,
        phone: string,
        email: string
  };
}
