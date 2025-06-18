'use client'

import { useEffect, useState } from "react";

const CONNECTION_STRING = process.env.NEXT_PUBLIC_CONNECTION_STRING || "http://localhost:5264";
const API_URL = `${CONNECTION_STRING}/api`;

export default function VehicleList() {
  const [vehicles, setVehicles] = useState([]);
  const [brands, setBrands] = useState([]);
  const [equipment, setEquipment] = useState([]);
  const [search, setSearch] = useState("");
  const [editingVehicle, setEditingVehicle] = useState(null);
  const [showForm, setShowForm] = useState(false);

  useEffect(() => {
    fetchVehicles();
    fetchBrands();
    fetchEquipment();
  }, []);

  const fetchVehicles = () => {
    fetch(`${API_URL}/vehicles`)
      .then((res) => res.json())
      .then((data) => {
        const values = data?.$values || [];
        console.log("Get Vehicles: ", values);
        setVehicles(values);
      });
  };
  const fetchBrands = () => {
    fetch(`${API_URL}/brands`)
      .then((res) => res.json())
      .then((data) => {
        const values = data?.$values || [];
        setBrands(values);
      });
  };
  const fetchEquipment = () => {
    fetch(`${API_URL}/vehicleequipments`)
      .then((res) => res.json())
      .then((data) => {
        const values = data?.$values || [];
        setEquipment(values);
      });
  };

  const handleDelete = (id) => {
    fetch(`${API_URL}/vehicles/${id}`, { method: "DELETE" })
      .then(() => fetchVehicles());
  };

  const handleSave = (vehicle) => {
    const method = vehicle.id ? "PUT" : "POST";
    const url = vehicle.id ? `${API_URL}/vehicles/${vehicle.id}` : `${API_URL}/vehicles`;
    fetch(url, {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(vehicle),
    }).then(() => {
      fetchVehicles();
      setEditingVehicle(null);
      setShowForm(false);
    });
  };

  const filteredVehicles = vehicles.filter(v =>
    v.model.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="p-6 max-w-5xl mx-auto">
      <div className="flex items-center gap-4 mb-6">
        <input
          className="border rounded px-4 py-2 flex-1"
          placeholder="Search vehicle..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <button
          className="bg-blue-600 text-white px-4 py-2 rounded"
          onClick={() => {
            setEditingVehicle(null);
            setShowForm(true);
          }}
        >
          Add Vehicle
        </button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {filteredVehicles.map((v) => (
          <div key={v.id} className="border rounded-lg p-4 shadow">
            <h2 className="text-xl font-semibold">{v.model} ({v.year})</h2>
            <p className="text-sm text-gray-600">Vehicle Identification Number: {v.vehicleIdentificationNumber}</p>
            <p className="text-sm text-gray-600">License Plate: {v.licensePlate}</p>
            <p className="text-sm text-gray-600">Brand: {v.brand?.name}</p>
            <p className="text-sm text-gray-600 mb-4">
              Equipment: {v.equipment?.$values.map(e => e.name).join(", ") || "None"}
            </p>
            <div className="flex gap-2">
              <button
                className="text-white bg-yellow-500 px-3 py-1 rounded"
                onClick={() => {
                  setEditingVehicle(v);
                  setShowForm(true);
                }}
              >
                Edit
              </button>
              <button
                className="text-white bg-red-600 px-3 py-1 rounded"
                onClick={() => handleDelete(v.id)}
              >
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>

      {showForm && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white p-6 rounded shadow max-w-md w-full">
            <VehicleForm
              vehicle={editingVehicle}
              brands={brands}
              equipment={equipment}
              onSave={handleSave}
            />
            <button
              className="mt-4 text-gray-600 hover:text-black"
              onClick={() => setShowForm(false)}
            >
              Cancel
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

function VehicleForm({ vehicle, brands, equipment, onSave }) {
  const safeVehicle = vehicle || {};
  const [model, setModel] = useState(safeVehicle.model || "");
  const [vehicleIdentificationNumber, setVehicleIdentificationNumber] = useState(safeVehicle.vehicleIdentificationNumber || "");
  const [licensePlate, setLicensePlate] = useState(safeVehicle.licensePlate || "");
  const [year, setYear] = useState(safeVehicle.year || "");
  const [brandId, setBrandId] = useState(safeVehicle.brandId || "");
  const [selectedEquipment, setSelectedEquipment] = useState(safeVehicle.equipment?.$values.map(e => e.id) || []);
  

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave({
      ...safeVehicle,
      model,
      vehicleIdentificationNumber: parseInt(vehicleIdentificationNumber),
      licensePlate: licensePlate,
      year: parseInt(year),
      brandId: parseInt(brandId),
      equipmentIds: selectedEquipment
    });
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <label className="block text-sm font-medium text-gray-700 mb-1">
        Vehicle Identification Number (VIN)
      </label>
      <input
        className="w-full border px-3 py-2 rounded"
        placeholder="VIN"
        value={vehicleIdentificationNumber}
        onChange={(e) => setVehicleIdentificationNumber(e.target.value)}
        required
      />
      <label className="block text-sm font-medium text-gray-700 mb-1">
        License Plate
      </label>
      <input
        className="w-full border px-3 py-2 rounded"
        placeholder="License Plate"
        value={licensePlate}
        onChange={(e) => setLicensePlate(e.target.value)}
        required
      />
      <label className="block text-sm font-medium text-gray-700 mb-1">
        Model
      </label>
      <input
        className="w-full border px-3 py-2 rounded"
        placeholder="Model"
        value={model}
        onChange={(e) => setModel(e.target.value)}
        required
      />
      <label className="block text-sm font-medium text-gray-700 mb-1">
        Production Year
      </label>
      <input
        type="number"
        className="w-full border px-3 py-2 rounded"
        placeholder="Year"
        value={year}
        onChange={(e) => setYear(e.target.value)}
        required
      />
      <label className="block text-sm font-medium text-gray-700 mb-1">
        Brand
      </label>
      <select
        className="w-full border px-3 py-2 rounded"
        value={brandId}
        onChange={(e) => setBrandId(e.target.value)}
        required
      >
        <option value="">Select Brand</option>
        {brands.map(b => (
          <option key={b.id} value={b.id}>{b.name}</option>
        ))}
      </select>
      <label className="block text-sm font-medium text-gray-700 mb-1">
        Equipment
      </label>
      <div className="space-y-1">
        {equipment.map(e => (
          <label key={e.id} className="flex items-center gap-2">
            <input
              type="checkbox"
              checked={selectedEquipment.includes(e.id)}
              onChange={() => {
                setSelectedEquipment(prev =>
                  prev.includes(e.id) ? prev.filter(id => id !== e.id) : [...prev, e.id]
                );
              }}
            />
            {e.name}
          </label>
        ))}
      </div>
      <button type="submit" className="bg-green-600 text-white px-4 py-2 rounded">
        Save
      </button>
    </form>
  );
}
