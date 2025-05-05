export interface Ship {
  id: number;
  name: string;
  imoNumber?: string;
  maxSpeed?: number;
}

export interface FullDetailShip extends Ship {
  imoNumber?: string;
  shipType?: string;
  flagCountry?: string;
  owner?: string;
  operator?: string;
  yearBuilt?: number;
  grossTonnage?: number;
  deadweight?: number;
  lengthMeters?: number;
  beamMeters?: number;
  draftMeters?: number;
  lastKnownLat?: number;
  lastKnownLon?: number;
  lastSeenAt?: string; 
}
